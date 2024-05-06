# Migration Guide

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
