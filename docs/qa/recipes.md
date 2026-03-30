# Recipes

Welcome to the Recipes section! Here you will find a collection of instructions and answers to common questions related to various technical problems. Each recipe provides a solution to a specific problem, helping you overcome challenges in your development journey.

[[toc]]

## How to debug an application?

Adjust logging level:

```json
{
    "Logging": {
        "Keycloak.AuthServices": "Debug",
        "Keycloak.AuthServices.Authorization": "Trace"
    }
}
```

> [!NOTE]
> ☝️`Keycloak.AuthServices` supports OpenTelemetry. See [Keycloak.AuthServices.OpenTelemetry](/opentelemetry).

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
using Keycloak.AuthServices.Common;

var keycloakOptions = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;
```

Or:

```csharp
using Keycloak.AuthServices.Common;

KeycloakAuthorizationOptions options = new();
configuration.BindKeycloakOptions(options);
```

## How to get an access token via Swagger UI?

Here is an example of how to use [NSwag](https://github.com/RicoSuter/NSwag/wiki/AspNetCore-Middleware#add-oauth2-authentication-openapi-3):

::: details Code

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

:::

## How to connect a containerized API to Keycloak?

When running your .NET API inside a Docker container alongside a Keycloak container, you may see:

```
WWW-Authenticate: Bearer error="invalid_token", error_description="The issuer 'http://localhost:8080/realms/my-realm' is invalid"
```

### Root Cause

Keycloak embeds its own URL in every token's `iss` (issuer) claim. In dev mode, the issuer is derived from the URL used when the token was obtained. Your containerized API fetches OIDC metadata from a different base URL (e.g., `host.docker.internal`) than the one used to issue the token (e.g., `localhost`), causing an issuer mismatch.

### Solution 1 — Pin Keycloak's hostname (recommended)

Set `KC_HOSTNAME` so Keycloak always issues tokens with a fixed base URL regardless of how it is accessed:

```bash
docker run -p 8080:8080 \
  -e KEYCLOAK_ADMIN=admin \
  -e KEYCLOAK_ADMIN_PASSWORD=admin \
  -e KC_HOSTNAME=http://localhost:8080/ \
  quay.io/keycloak/keycloak:26.4.2 start-dev
```

Then configure your API to fetch OIDC metadata from `host.docker.internal` (reachable from inside the container) while validating tokens against the fixed issuer:

```json
{
  "Keycloak": {
    "realm": "my-realm",
    "auth-server-url": "http://host.docker.internal:8080/",
    "ssl-required": "none",
    "resource": "my-client"
  }
}
```

```csharp
builder.Services.AddKeycloakWebApiAuthentication(configuration, options =>
{
    // Tokens are issued with localhost:8080 as issuer (from KC_HOSTNAME)
    options.TokenValidationParameters.ValidIssuer = "http://localhost:8080/realms/my-realm";
    options.RequireHttpsMetadata = false;
});
```

### Solution 2 — Use `host.docker.internal` everywhere

Obtain tokens using `http://host.docker.internal:8080` (configure your HTTP client or Postman to use this URL) and set `auth-server-url` to the same value. Both the token issuer and the API's authority will match automatically.

### Solution 3 — Accept multiple valid issuers

For maximum flexibility during local development, accept tokens from multiple issuers:

```csharp
builder.Services.AddKeycloakWebApiAuthentication(configuration, options =>
{
    options.TokenValidationParameters.ValidIssuers = new[]
    {
        "http://localhost:8080/realms/my-realm",
        "http://host.docker.internal:8080/realms/my-realm"
    };
    options.RequireHttpsMetadata = false;
});
```

> [!TIP]
> For production, always set `KC_HOSTNAME` to a stable public URL and ensure `auth-server-url` in your API configuration points to a URL that is reachable from inside the container.

## How to setup resiliency to HTTP Clients?

Every HTTP Client provided by `Keycloak.AuthServices` expose `IHttpClientBuilder`. It a standard way to extend behavior of `HttpClient`. We can use it to our advantage!

Install [Microsoft.Extensions.Http.Resilience](https://www.nuget.org/packages/Microsoft.Extensions.Http.Resilience)

```bash
dotnet add package Microsoft.Extensions.Http.Resilience
```

Add resilience handler globally (for all `HttpClient`s including ones provided by `Keycloak.AuthServices`)

Add globally:

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services

builder.Services.ConfigureHttpClientDefaults(http => http.AddStandardResilienceHandler());
```

Add per-client:

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services

services
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(builder.Configuration)
    .AddStandardResilienceHandler();
```
