using System.Security.Claims;
using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Keycloak.AuthServices.Authorization.Uma;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.AddServiceDefaults();

var resourceServerClientId = "uma-resource-server";

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(
        configuration,
        configureJwtBearerOptions: options =>
        {
            options.Audience = resourceServerClientId;
            options.RequireHttpsMetadata = false;
            options.MapInboundClaims = false;
        }
    );

services.AddAuthorization().AddKeycloakAuthorization().AddUmaPermissionTicketChallenge();

services.AddAuthorizationServer(configuration).AddStandardResilienceHandler();

var tokenClientName = ClientCredentialsClientName.Parse("uma_protection");

services.AddDistributedMemoryCache();
services
    .AddClientCredentialsTokenManagement()
    .AddClient(
        tokenClientName,
        client =>
        {
            var options = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;
            client.ClientId = ClientId.Parse(options.Resource);
            client.ClientSecret = ClientSecret.Parse(options.Credentials.Secret);
            client.TokenEndpoint = new Uri(options.KeycloakTokenEndpoint);
        }
    );

services
    .AddKeycloakProtectionHttpClient(configuration)
    .AddClientCredentialsTokenHandler(tokenClientName);

services.AddProblemDetails();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/documents/{name}", (string name) => new DocumentResponse(name, $"Content of {name}"))
    .RequireProtectedResource("shared-document", "read");

app.MapGet(
        "/documents/{name}/details",
        (string name) => new DocumentResponse(name, $"Detailed content of {name}")
    )
    .RequireProtectedResource("shared-document", "write");

app.MapGet(
        "/documents",
        () => new[] { new DocumentResponse("shared-document", "A document shared via UMA") }
    )
    .RequireAuthorization();

app.MapPost(
        "/permissions/request",
        async (
            PermissionRequestBody request,
            ClaimsPrincipal user,
            IKeycloakProtectionClient protectionClient,
            IOptions<KeycloakAuthorizationServerOptions> authzOptions
        ) =>
        {
            var realm = authzOptions.Value.Realm;
            var userId = user.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userId))
            {
                return Results.Unauthorized();
            }

            var resourceIds = await protectionClient.GetResourcesIdsAsync(
                realm,
                new GetResourcesRequestParameters { Name = request.Resource, ExactName = true }
            );

            if (resourceIds.Count == 0)
            {
                return Results.NotFound(new { Error = $"Resource '{request.Resource}' not found" });
            }

            var resourceId = resourceIds[0];

            // Check if a pending ticket already exists for this user/resource/scope
            var existing = await protectionClient.GetPermissionTicketsAsync(
                realm,
                new GetPermissionTicketsRequestParameters
                {
                    ResourceId = resourceId,
                    Requester = userId,
                }
            );

            var requestedScopes = request.Scopes ?? ["read"];
            var existingScopes = existing.Select(t => t.ScopeName ?? t.Scope).ToHashSet();

            var created = new List<string>();
            foreach (var scope in requestedScopes)
            {
                if (existingScopes.Contains(scope))
                {
                    continue;
                }

                var ticket = new PermissionTicket
                {
                    Resource = resourceId,
                    Requester = userId,
                    ScopeName = scope,
                    Granted = false,
                };

                // Create via the Permission Ticket API (POST /permission/ticket)
                var response = await protectionClient.CreatePermissionTicketWithResponseAsync(
                    realm,
                    ticket
                );

                if (response.IsSuccessStatusCode)
                {
                    created.Add(scope);
                }
            }

            return Results.Ok(
                new
                {
                    Message = $"Access requested for [{string.Join(", ", created)}] on '{request.Resource}'",
                    Scopes = created,
                }
            );
        }
    )
    .RequireAuthorization();

app.MapGet(
        "/permissions/pending",
        async (
            IKeycloakProtectionClient protectionClient,
            IOptions<KeycloakAuthorizationServerOptions> authzOptions
        ) =>
        {
            var realm = authzOptions.Value.Realm;

            var tickets = await protectionClient.GetPermissionTicketsAsync(
                realm,
                new GetPermissionTicketsRequestParameters { Granted = false, ReturnNames = true }
            );

            return Results.Ok(tickets);
        }
    )
    .RequireAuthorization();

app.MapPut(
        "/permissions/{id}/approve",
        async (
            string id,
            IKeycloakProtectionClient protectionClient,
            IOptions<KeycloakAuthorizationServerOptions> authzOptions
        ) =>
        {
            var realm = authzOptions.Value.Realm;

            var ticket = new PermissionTicket { Id = id, Granted = true };
            await protectionClient.UpdatePermissionTicketAsync(realm, ticket);

            return Results.NoContent();
        }
    )
    .RequireAuthorization();

app.MapDelete(
        "/permissions/{id}",
        async (
            string id,
            IKeycloakProtectionClient protectionClient,
            IOptions<KeycloakAuthorizationServerOptions> authzOptions
        ) =>
        {
            var realm = authzOptions.Value.Realm;
            await protectionClient.DeletePermissionTicketAsync(realm, id);

            return Results.NoContent();
        }
    )
    .RequireAuthorization();

app.MapGet(
        "/me",
        (ClaimsPrincipal user) =>
            new
            {
                Name = user.Identity?.Name,
                Claims = user.Claims.Select(c => new { c.Type, c.Value }),
            }
    )
    .RequireAuthorization();

app.MapDefaultEndpoints();

app.Run();

internal record DocumentResponse(string Name, string Content);

internal record PermissionRequestBody(string Resource, string[]? Scopes);
