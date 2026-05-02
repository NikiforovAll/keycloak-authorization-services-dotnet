using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Microsoft.OpenApi;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.AddServiceDefaults();

const string clientName = "workspaces-client";
const string schemeName = "oauth2";

var keycloakOptions = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;
var realmUrl = keycloakOptions.KeycloakUrlRealm.TrimEnd('/') + "/";

services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        (document, _, _) =>
        {
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes ??=
                new Dictionary<string, IOpenApiSecurityScheme>();
            document.Components.SecuritySchemes[schemeName] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{realmUrl}protocol/openid-connect/auth"),
                        TokenUrl = new Uri($"{realmUrl}protocol/openid-connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            ["openid"] = "OpenID",
                            ["profile"] = "Profile",
                        },
                    },
                },
            };

            document.Security =
            [
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(schemeName, document)] = [],
                },
            ];

            return Task.CompletedTask;
        }
    );
});

services.AddKeycloakWebApiAuthentication(
    configuration,
    options =>
    {
        options.Audience = clientName;
        options.RequireHttpsMetadata = false;
    }
);
services.AddAuthorization();

var app = builder.Build();

app.MapOpenApi();
app.UseSwaggerUi(options =>
{
    options.DocumentPath = "/openapi/v1.json";
    options.Path = string.Empty;
    options.OAuth2Client = new OAuth2ClientSettings
    {
        ClientId = clientName,
        AppName = clientName,
        UsePkceWithAuthorizationCodeGrant = true,
        Scopes = { "openid", "profile" },
    };
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet(
        "/hello",
        (ClaimsPrincipal user) =>
            new
            {
                message = "Hello World!",
                name = user.Identity?.Name,
                authenticationType = user.Identity?.AuthenticationType,
                isAuthenticated = user.Identity?.IsAuthenticated ?? false,
                claims = user.Claims.Select(c => new
                {
                    c.Type,
                    c.Value,
                    c.Issuer,
                }),
            }
    )
    .RequireAuthorization();

app.Run();
