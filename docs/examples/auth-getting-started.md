# AuthGettingStarted

```csharp
using System.Security.Claims;
using Api;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

host.ConfigureLogger();

services.AddEndpointsApiExplorer().AddSwagger();

services.AddKeycloakWebApiAuthentication(configuration);

services
    .AddAuthorization(o =>
        o.AddPolicy(
            "IsAdmin",
            b =>
            {
                b.RequireRealmRoles("admin");
                b.RequireResourceRoles("r-admin");
                // TokenValidationParameters.RoleClaimType is overridden
                // by KeycloakRolesClaimsTransformation
                b.RequireRole("r-admin");
            }
        )
    )
    .AddKeycloakAuthorization(configuration);

services.AddKeycloakAdminHttpClient(configuration);

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (ClaimsPrincipal user) => app.Logger.LogInformation("{@User}", user.Identity.Name))
    .RequireAuthorization("IsAdmin");

app.Run();

```

See sample source code: [keycloak-authorization-services-dotnet/tree/main/samples/AuthGettingStarted](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/AuthGettingStarted)
