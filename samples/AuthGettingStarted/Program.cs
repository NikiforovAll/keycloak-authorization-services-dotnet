using System.Security.Claims;
using Api;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.AuthZ;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

host.ConfigureLogger();

services
    .AddEndpointsApiExplorer()
    .AddSwagger();

var authenticationOptions = configuration
    .GetSection(KeycloakAuthenticationOptions.Section)
    .Get<KeycloakAuthenticationOptions>();

services.AddKeycloakAuthentication(authenticationOptions);

var authorizationOptions = configuration
    .GetSection(KeycloakProtectionClientOptions.Section)
    .Get<KeycloakProtectionClientOptions>();

services
    .AddAuthorization(o => o.AddPolicy("IsAdmin", b =>
    {
        b.RequireRealmRoles("admin");
        b.RequireResourceRoles("r-admin");
        // TokenValidationParameters.RoleClaimType is overriden
        // by KeycloakRolesClaimsTransformation
        b.RequireRole("r-admin");
    }))
    .AddKeycloakAuthorization(authorizationOptions);

var adminClientOptions = configuration
    .GetSection(KeycloakAdminClientOptions.Section)
    .Get<KeycloakAdminClientOptions>();

services.AddKeycloakAdminHttpClient(adminClientOptions);

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (ClaimsPrincipal user) =>
{
    // TokenValidationParameters.NameClaimType is overriden based on keycloak specific claim
    app.Logger.LogInformation("{@User}", user.Identity.Name);
}).RequireAuthorization("IsAdmin");

app.Run();
