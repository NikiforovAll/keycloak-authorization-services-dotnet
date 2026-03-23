using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.Requirements;
using Keycloak.AuthServices.Common.Claims;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddKeycloakWebApiAuthentication(configuration);

services
    .AddKeycloakAuthorization()
    .AddAuthorizationBuilder()
    .AddPolicy(
        "AcmeOnly",
        policy => policy.RequireOrganizationMembership("acme-corp").RequireRealmRoles("user")
    );

var app = builder.Build();

app.UseAuthentication().UseAuthorization();

// Requires membership in any organization
app.MapGet(
        "/orgs",
        (ClaimsPrincipal user) =>
        {
            var orgs = user.GetOrganizations();
            return Results.Ok(
                orgs.Select(o => new
                {
                    o.Alias,
                    o.Id,
                    o.Attributes,
                })
            );
        }
    )
    .RequireOrganizationMembership();

// Requires membership in the specific organization (resolved from route)
app.MapGet(
        "/orgs/{orgId}/projects",
        (string orgId, ClaimsPrincipal user) =>
            Results.Ok(new { Organization = orgId, Projects = new[] { "project-alpha" } })
    )
    .RequireOrganizationMembership("{orgId}");

// Requires membership in a specific organization (static)
app.MapGet("/acme/settings", () => Results.Ok(new { Theme = "dark", Tier = "enterprise" }))
    .RequireOrganizationMembership("acme-corp");

// Policy-based: combines org membership with realm roles
app.MapGet("/acme/admin", () => Results.Ok(new { Message = "Acme admin area" }))
    .RequireAuthorization("AcmeOnly");

// Header-based: org resolved from X-Organization header (custom resolver)
app.MapGet(
        "/tenant/projects",
        (ClaimsPrincipal user) =>
            Results.Ok(new { Projects = new[] { "project-from-header-tenant" } })
    )
    .RequireOrganizationMembership<RouteHandlerBuilder, HeaderParameterResolver>(
        "{X-Organization}"
    );

// Imperative: check access to a specific org programmatically
app.MapGet(
    "/check/{orgId}",
    async (string orgId, ClaimsPrincipal user, IAuthorizationService authorizationService) =>
    {
        var result = await authorizationService.AuthorizeAsync(
            user,
            null,
            new OrganizationRequirement(orgId)
        );

        return result.Succeeded
            ? Results.Ok(new { OrgId = orgId, Access = true })
            : Results.Forbid();
    }
);

// Imperative: filter a list of resources by org membership
app.MapGet(
    "/workspaces",
    async (ClaimsPrincipal user, IAuthorizationService authorizationService) =>
    {
        var allWorkspaces = new[]
        {
            new
            {
                Id = 1,
                Name = "Acme Platform",
                Org = "acme-corp",
            },
            new
            {
                Id = 2,
                Name = "Partner Portal",
                Org = "partner-inc",
            },
            new
            {
                Id = 3,
                Name = "Startup MVP",
                Org = "startup-co",
            },
        };

        var accessible = new List<object>();
        foreach (var workspace in allWorkspaces)
        {
            var result = await authorizationService.AuthorizeAsync(
                user,
                null,
                new OrganizationRequirement(workspace.Org)
            );

            if (result.Succeeded)
            {
                accessible.Add(workspace);
            }
        }

        return Results.Ok(accessible);
    }
);

await app.RunAsync();
