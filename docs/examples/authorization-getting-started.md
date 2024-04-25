# AuthorizationGettingStarted

```csharp
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakWebApiAuthentication(configuration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                Policies.RequireAspNetCoreRole,
                builder => builder.RequireRole(Roles.AspNetCoreRole));

            options.AddPolicy(
                Policies.RequireRealmRole,
                builder => builder.RequireRealmRoles(Roles.RealmRole));

            options.AddPolicy(
                Policies.RequireClientRole,
                builder => builder.RequireResourceRoles(Roles.ClientRole));

            options.AddPolicy(
                Policies.RequireToBeInKeycloakGroupAsReader,
                builder => builder
                    .RequireAuthenticatedUser()
                    .RequireProtectedResource("workspace", "workspaces:read"));

        }).AddKeycloakAuthorization(configuration);

        return services;
    }
}

public static class AuthorizationConstants
{
    public static class Roles
    {
        public const string AspNetCoreRole = "realm-role";

        public const string RealmRole = "realm-role";

        public const string ClientRole = "client-role";
    }

    public static class Policies
    {
        public const string RequireAspNetCoreRole = nameof(RequireAspNetCoreRole);

        public const string RequireRealmRole = nameof(RequireRealmRole);

        public const string RequireClientRole = nameof(RequireClientRole);

        public const string RequireToBeInKeycloakGroupAsReader = nameof(RequireToBeInKeycloakGroupAsReader);
    }
}
```

```csharp
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using static Microsoft.Extensions.DependencyInjection.AuthorizationConstants.Policies;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

builder.AddSerilog();

services
    .AddApplicationSwagger(configuration)
    .AddAuth(configuration);

services.Configure<JsonOptions>(opts =>
{
    opts.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    opts.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app
    .UseHttpsRedirection()
    .UseApplicationSwagger(configuration)
    .UseAuthentication()
    .UseAuthorization();

// login with required aspnet core identity role
app.MapGet("/endpoint1", (ClaimsPrincipal user) => user)
    .RequireAuthorization(RequireAspNetCoreRole);

// login with requireed realm role evaluated from corresponding claim
app.MapGet("/endpoint2", (ClaimsPrincipal user) => user)
    .RequireAuthorization(RequireRealmRole);

// login with requireed client role evaluated from corresponding claim
app.MapGet("/endpoint3", (ClaimsPrincipal user) => user)
    .RequireAuthorization(RequireClientRole);

// login based on remotely executed policy
// authorization is performed by Keycloak (Authorization Server)
app.MapGet("/endpoint4", (ClaimsPrincipal user) => user)
    .RequireAuthorization(RequireToBeInKeycloakGroupAsReader);

await app.RunAsync();
```

See sample source code: [keycloak-authorization-services-dotnet/tree/main/samples/AuthGettingStarted](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/AuthGettingStarted)
