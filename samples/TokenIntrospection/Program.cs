using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddKeycloakWebApiAuthentication(configuration);

// Token introspection must be registered BEFORE authorization
// so that lightweight tokens are enriched before role mapping runs.
services.AddKeycloakTokenIntrospection(configuration);

services
    .AddKeycloakAuthorization(options =>
    {
        configuration.GetSection("Keycloak").Bind(options);
        options.EnableRolesMapping = RolesClaimTransformationSource.All;
    })
    .AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRealmRoles("Admin"));

var app = builder.Build();

app.UseAuthentication().UseAuthorization();

app.MapGet(
        "/me",
        (ClaimsPrincipal user) =>
            Results.Ok(
                new
                {
                    Name = user.Identity?.Name,
                    Claims = user.Claims.Select(c => new { c.Type, c.Value }),
                }
            )
    )
    .RequireAuthorization();

app.MapGet(
        "/roles",
        (ClaimsPrincipal user) =>
        {
            var roles = user.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
            return Results.Ok(new { Roles = roles });
        }
    )
    .RequireAuthorization();

app.MapGet("/admin", () => Results.Ok(new { Message = "Admin area" }))
    .RequireAuthorization("AdminOnly");

await app.RunAsync();
