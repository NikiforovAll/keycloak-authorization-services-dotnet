# Configuration Reference

## Configuration Section

All Keycloak.AuthServices packages bind to a `"Keycloak"` config section by default. Override with the `configSectionName` parameter.

## KeycloakAuthenticationOptions

Used by `Keycloak.AuthServices.Authentication`:

```json
{
  "Keycloak": {
    "realm": "Test",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "test-client",
    "verify-token-audience": true,
    "confidential-port": 0,
    "credentials": {
      "secret": "your-secret"
    }
  }
}
```

| Property | Type | Description |
|----------|------|-------------|
| `realm` | string | Realm name |
| `auth-server-url` | string | Keycloak base URL |
| `ssl-required` | string | `none` / `external` / `all` |
| `resource` | string | Client ID (also used as audience) |
| `verify-token-audience` | bool | Validate `aud` claim matches `resource` |
| `credentials.secret` | string | Client secret for confidential clients |
| `confidential-port` | int | HTTPS port (rarely needed) |

## KeycloakAuthorizationOptions

Used by `Keycloak.AuthServices.Authorization`:

```json
{
  "Keycloak": {
    "resource": "test-client",
    "EnableRolesMapping": "None",
    "RolesResource": "test-client",
    "RoleClaimType": "role"
  }
}
```

| Property | Type | Description |
|----------|------|-------------|
| `resource` | string | Default client for role lookups |
| `EnableRolesMapping` | enum | `None` / `Realm` / `ResourceAccess` / `All` |
| `RolesResource` | string | Override client for resource role mapping |
| `RoleClaimType` | string | Target claim type for mapped roles (default: `"role"`) |

## KeycloakAdminClientOptions

Used by `Keycloak.AuthServices.Sdk` (Admin API):

```json
{
  "Keycloak": {
    "realm": "master",
    "auth-server-url": "http://localhost:8080/",
    "resource": "admin-api",
    "credentials": {
      "secret": "your-secret"
    }
  }
}
```

## KeycloakProtectionClientOptions

Used by `Keycloak.AuthServices.Sdk` (Protection API):

```json
{
  "Keycloak": {
    "realm": "my-realm",
    "auth-server-url": "http://localhost:8080/",
    "resource": "my-client",
    "credentials": {
      "secret": "your-secret"
    }
  }
}
```

## Naming Conventions

Both kebab-case (Keycloak adapter format) and PascalCase are supported:

```json
{
  "Keycloak_kebab": {
    "realm": "Test",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "test-client",
    "verify-token-audience": true,
    "credentials": { "secret": "secret" }
  },
  "Keycloak_pascal": {
    "Realm": "Test",
    "AuthServerUrl": "http://localhost:8080/",
    "SslRequired": "none",
    "Resource": "test-client",
    "VerifyTokenAudience": true,
    "Credentials": { "Secret": "secret" }
  }
}
```

Default format is kebab-case (matching Keycloak's adapter config download).

## Adapter File

Instead of `appsettings.json`, use a standalone `keycloak.json`:

```csharp
builder.Host.ConfigureKeycloakConfigurationSource("keycloak.json");
```

## Resolving Options

From DI:

```csharp
// Authentication options (named, per scheme)
var authOptions = serviceProvider
    .GetRequiredService<IOptionsMonitor<KeycloakAuthenticationOptions>>()
    .Get(JwtBearerDefaults.AuthenticationScheme);

// Authorization options
var authzOptions = serviceProvider
    .GetRequiredService<IOptionsMonitor<KeycloakAuthorizationOptions>>()
    .CurrentValue;
```

Before DI is built (at startup):

```csharp
using Keycloak.AuthServices.Common;

var options = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;

// Or bind manually:
KeycloakAuthorizationOptions options = new();
configuration.BindKeycloakOptions(options);
```
