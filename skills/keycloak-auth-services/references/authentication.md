# Authentication

## JWT Bearer (Web API)

`AddKeycloakWebApiAuthentication` configures JWT Bearer authentication from the `"Keycloak"` config section.

What it does:
- Adds and configures `AddJwtBearer` based on Keycloak config
- Registers `IOptions<KeycloakAuthenticationOptions>` and `IOptions<JwtBearerOptions>`

### ServiceCollection Extensions

From configuration (default `"Keycloak"` section):

```csharp
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
```

With explicit section name:

```csharp
builder.Services.AddKeycloakWebApiAuthentication(
    builder.Configuration,
    configSectionName: "MyKeycloak"
);
```

With `IConfigurationSection`:

```csharp
builder.Services.AddKeycloakWebApiAuthentication(
    builder.Configuration.GetSection("MyKeycloak")
);
```

With inline `JwtBearerOptions` overrides:

```csharp
builder.Services.AddKeycloakWebApiAuthentication(
    builder.Configuration,
    jwtBearerOptions =>
    {
        jwtBearerOptions.RequireHttpsMetadata = false;
        jwtBearerOptions.Audience = "my-custom-audience";
    }
);
```

### AuthenticationBuilder Extensions

For custom authentication scheme or more verbose setup:

```csharp
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(builder.Configuration);
```

Inline configuration:

```csharp
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(
        options =>
        {
            options.Resource = "test-client";
            options.Realm = "Test";
            options.AuthServerUrl = "http://localhost:8080/";
            options.VerifyTokenAudience = false;
        },
        jwtBearerOptions =>
        {
            jwtBearerOptions.RequireHttpsMetadata = false;
        }
    );
```

### Configuration Precedence

`KeycloakAuthenticationOptions` (`"Keycloak"` section) takes precedence over `Authentication:Schemes:{SchemeName}` (`"Bearer"` — `JwtBearerOptions`) in default config.

You can also use ASP.NET Core's standard config:

```json
{
  "Keycloak": {
    "ssl-required": "internal",
    "resource": "test-client",
    "verify-token-audience": true,
    "credentials": { "secret": "secret" }
  },
  "Authentication": {
    "DefaultScheme": "Bearer",
    "Schemes": {
      "Bearer": {
        "ValidAudiences": ["my-audience"],
        "RequireHttpsMetadata": true,
        "Authority": "http://localhost:8080/realms/Test",
        "TokenValidationParameters": {
          "ValidateAudience": false
        }
      }
    }
  }
}
```

## OpenID Connect (Web App)

`AddKeycloakWebAppAuthentication` configures OIDC + Cookie authentication for server-rendered web apps (MVC, Razor Pages).

What it does:
- Adds and configures `OpenIdConnect` based on Keycloak config
- Registers `IOptions<KeycloakAuthenticationOptions>`, `IOptions<OpenIdConnectOptions>`, `IOptions<CookieAuthenticationOptions>`

### ServiceCollection Extensions

```csharp
builder.Services.AddKeycloakWebAppAuthentication(builder.Configuration);
```

### AuthenticationBuilder Extensions

```csharp
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddKeycloakWebApp(builder.Configuration);
```

Inline:

```csharp
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddKeycloakWebApp(
        configureKeycloakOptions: options =>
        {
            options.Resource = "webapp-client";
            options.Realm = "Test";
            options.AuthServerUrl = "http://localhost:8080/";
        },
        configureOpenIdConnectOptions: oidcOptions =>
        {
            oidcOptions.SaveTokens = true;
        }
    );
```

## Adapter File Configuration Provider

Use a standalone `keycloak.json` file instead of `appsettings.json`:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureKeycloakConfigurationSource("keycloak.json");

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
```

```json
// keycloak.json
{
  "realm": "Test",
  "auth-server-url": "http://localhost:8088/",
  "ssl-required": "external",
  "resource": "test-client",
  "verify-token-audience": true
}
```

## Server Metadata Discovery (RFC 8414)

By default, the library uses OIDC discovery (`.well-known/openid-configuration`). For pure OAuth 2.0 scenarios, switch to RFC 8414 metadata:

```csharp
using Keycloak.AuthServices.Common;

builder.Services.AddKeycloakWebApiAuthentication(options =>
{
    options.MetadataAddress = KeycloakConstants.OAuthAuthorizationServerMetadataPath;
});
```

The constant resolves to `.well-known/oauth-authorization-server`.

**When to use RFC 8414:**
- Machine-to-machine flows (client credentials) with no OIDC/ID token involved
- MCP (Model Context Protocol) authorization server support
- OAuth 2.0-only clients that look for RFC 8414 metadata, not OIDC
- Protocol correctness when the consumer is a pure OAuth 2.0 resource server

For most use cases, the default OIDC discovery works fine — only switch to RFC 8414 when you have a specific reason.

## Audience Mapper

By default, `Keycloak.AuthServices.Authentication` validates that the token audience matches the `resource` (client ID). If you get **401 Unauthorized**, either:

1. Configure an Audience Mapper in Keycloak (Client → Client Scopes → `{client_id}-dedicated` → Mappers → Add "Audience" mapper)
2. Or disable audience validation: `verify-token-audience: false`
