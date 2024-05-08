# Recipes

Welcome to the Recipes section! Here you will find a collection of instructions and answers to common questions related to various technical problems. Each recipe provides a solution to a specific problem, helping you overcome challenges in your development journey.

[[toc]]

## how to get an access token from Swagger UI?

Here is an example of how to use [NSwag](https://github.com/RicoSuter/NSwag/wiki/AspNetCore-Middleware#add-oauth2-authentication-openapi-3):

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddOpenApiDocument(
    (document, sp) =>
    {
        var keycloakOptions = sp.GetRequiredService<IOptionsMonitor<KeycloakAuthenticationOptions>>()
            ?.Get(JwtBearerDefaults.AuthenticationScheme)!;

        document.AddSecurity(
            OpenIdConnectDefaults.AuthenticationScheme,
            [],
            new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = keycloakOptions.OpenIdConnectUrl,
            }
        );

        document.OperationProcessors.Add(
            new OperationSecurityScopeProcessor(OpenIdConnectDefaults.AuthenticationScheme)
        );
    });

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi(ui =>
{
    var keycloakOptions = builder
        .Configuration
        .GetKeycloakOptions<KeycloakAuthenticationOptions>()!;
    ui.DocumentTitle = "Workspaces";

    ui.OAuth2Client = new OAuth2ClientSettings
    {
        ClientId = keycloakOptions.Resource,
        ClientSecret = keycloakOptions?.Credentials?.Secret,
    };
});

app.Run();
```

## How to get Options from DI?

```csharp
var keycloakAuthenticationOptions = serviceProvider
    .GetRequiredService<IOptionsMonitor<KeycloakAuthenticationOptions>>()
    .Get(JwtBearerDefaults.AuthenticationScheme);

var keycloakAuthenticationOptions = serviceProvider
    .GetRequiredService<IOptionsMonitor<KeycloakAuthorizationOptions>>()
    .CurrentValue;
```

> [!NOTE]
> To retrieve `KeycloakAuthenticationOptions` you need to use `IOptionsMonitor.Get(string name)` because this options are registered per Scheme.

## How to get Options outside of `IServiceProvider`?

Sometimes you need to resolve options before the DI container is built. E.g: application startup.

```csharp
var keycloakOptions = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;
// OR
KeycloakAuthorizationOptions options = new();
configuration.BindKeycloakOptions(options);
```

## How to debug an application?

Adjust logging level:

```json
{
    "Logging": {
            "Keycloak.AuthServices": "Debug",
            "Keycloak.AuthServices.Authorization": "Trace"
        }
    }
}
```
