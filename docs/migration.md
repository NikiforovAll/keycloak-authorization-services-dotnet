# Migration Guide

## Key Changes in 3.0.0

### Removed deprecated `AddKeycloakAuthentication` methods

The `AddKeycloakAuthentication` extension methods (deprecated in 2.0.0) have been removed. Use `AddKeycloakWebApiAuthentication` instead.

```csharp
// Before (removed)
services.AddKeycloakAuthentication(configuration);

// After
services.AddKeycloakWebApiAuthentication(configuration);
```

### Namespace changes for better discoverability

Several extension method classes have been moved to standard namespaces following ASP.NET Core conventions. This means you may no longer need explicit `using` directives for these methods — they are automatically available via implicit usings in Web projects.

| Extension Methods | Old Namespace | New Namespace |
|---|---|---|
| `AddKeycloakWebApiAuthentication` | `Keycloak.AuthServices.Authentication` | `Microsoft.Extensions.DependencyInjection` |
| `AddKeycloakWebAppAuthentication` | `Keycloak.AuthServices.Authentication` | `Microsoft.Extensions.DependencyInjection` |
| `AddKeycloakTokenIntrospection` | `Keycloak.AuthServices.Authorization.TokenIntrospection` | `Microsoft.Extensions.DependencyInjection` |

**Action required**: Remove explicit `using` directives that are no longer needed:

```csharp
// Before
using Keycloak.AuthServices.Authentication; // [!code --]
services.AddKeycloakWebApiAuthentication(configuration);

// After — just works, no using needed
services.AddKeycloakWebApiAuthentication(configuration);
```

::: tip
If you reference types directly (e.g., `KeycloakAuthenticationOptions`, `KeycloakRolesClaimsTransformation`), you still need the original `using` directive.
:::

## Key Changes in 2.0.0

* Breaking change 💥: Lot's of changes `Keycloak.AuthServices.Sdk` - API has changed, no backward compatibility.
* Breaking change 💥: Removed dependencies on `Refit` and `IdentityModel.AspNetCore`. Tokens are no longer managed by this library and **you need to configure it separately**.
* `RolesClaimTransformationSource` changed to `None` from `ResourceAccess` meaning we no longer map to `AspNetCore` roles by default. Renamed to `EnableRolesMapping`. Moved to `Keycloak.AuthServices.Authorization`.
* Moved `IKeycloakProtectionClient` to `Keycloak.AuthServices.Authorization` and renamed it to `IAuthorizationServerClient`. Removed `AddKeycloakProtectionHttpClient`, added `AddAuthorizationServer` instead. Note, `IKeycloakProtectionClient` is used as umbrella interface for Protection API now. (can be confusing if you used previous versions)

```csharp
// Before
.AddKeycloakAuthorization(configuration)
// After
.AddKeycloakAuthorization().AddAuthorizationServer(configuration)
```

* Dropped namespace `Keycloak.AuthServices.Sdk.AuthZ`
* `AddKeycloakAuthentication` has been deprecated in favor of `AddKeycloakWebApiAuthentication`.
* Breaking change 💥: Changed default Configuration format from kebab-case to PascalCase. See the [KeycloakInstallationOptionsTests.cs](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/blob/main/tests/Keycloak.AuthServices.Common.Tests/KeycloakInstallationOptionsTests.cs) for more details.

```json
{
  // new default
  "Keycloak1": {
    "realm": "Test",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "test-client",
    "verify-token-audience": true,
    "credentials": {
      "secret": "secret"
    }
  },
  // old default
  "Keycloak2": {
    "Realm": "Test",
    "AuthServerUrl": "http://localhost:8080/",
    "SslRequired": "none",
    "Resource": "test-client",
    "VerifyTokenAudience": true,
    "Credentials": {
      "Secret": "secret"
    }
  }
}

```
