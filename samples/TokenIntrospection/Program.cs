using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.TokenIntrospection;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;
var useCustomTransform = configuration.GetValue<bool>("CustomTransform");

services.AddKeycloakWebApiAuthentication(configuration);

if (useCustomTransform)
{
    services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
}

services
    .AddKeycloakAuthorization(options =>
    {
        configuration.GetSection("Keycloak").Bind(options);
        options.EnableRolesMapping = RolesClaimTransformationSource.All;
    })
    .AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRealmRoles("Admin"));

services.AddKeycloakTokenIntrospection(configuration);

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

internal sealed class CustomClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;
        if (identity is not null && !identity.HasClaim("custom_marker", "true"))
        {
            identity.AddClaim(new Claim("custom_marker", "true"));
        }

        return Task.FromResult(principal);
    }
}
