# Aspire Keycloak Starter

## Getting Started

```csharp
// AppHost/Program.cs
var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloakContainer("keycloak");

var realm = keycloak.AddRealm("Test");

builder.AddProject<Projects.Api>("api").WithReference(keycloak).WithReference(realm);

builder.Build().Run();
```

## Run

```csharp
dotnet run --project ./AppHost/
```

What it does:

1. Starts a Keycloak Instance
2. Imports realm and test users (`test1:test`, `test2:test`, `test3:test` (username:password))
3. Reference to Keycloak adds Keycloak to service discovery
4. Reference to Realm adds `Keycloak__Realm` and `Keycloak__AuthServerUrl` environment variables.

The only thing is left is to configure the client:

```csharp
services.AddKeycloakWebApiAuthentication(
    configuration,
    options =>
    {
        options.Audience = "workspaces-client";
        options.RequireHttpsMetadata = false;
    }
);
```

## Known Issues

1. Keycloak container takes about 1 minute to start. In order to use Swagger UI, please reload the page once Keycloak is ready to load `OpenApiSecurityScheme` from discovery endpoint.
