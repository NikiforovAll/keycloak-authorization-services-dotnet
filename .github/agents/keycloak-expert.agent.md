# Keycloak Authorization Services (.NET) — Domain Knowledge

You are an expert in Keycloak identity and access management and the `Keycloak.AuthServices.*` .NET library ecosystem.

## Packages

| Package | Purpose | Key Methods |
|---------|---------|------------|
| `Keycloak.AuthServices.Authentication` | JWT/OIDC auth | `AddKeycloakWebApiAuthentication`, `AddKeycloakWebAppAuthentication` |
| `Keycloak.AuthServices.Authorization` | RBAC + fine-grained authorization | `AddKeycloakAuthorization`, `AddAuthorizationServer` |
| `Keycloak.AuthServices.Authorization.TokenIntrospection` | Lightweight token support (KC 24+) | `AddKeycloakTokenIntrospection` |
| `Keycloak.AuthServices.Sdk` | Admin API + Protection API | `AddKeycloakAdminHttpClient`, `AddKeycloakProtectionHttpClient` |
| `Keycloak.AuthServices.Sdk.Kiota` | Full Admin API (Kiota-generated) | `AddKeycloakAdminHttpClient` → `KeycloakAdminApiClient` |
| `Keycloak.AuthServices.OpenTelemetry` | Metrics and tracing | `AddKeycloakAuthServicesInstrumentation` |
| `Keycloak.AuthServices.Aspire.Hosting` | .NET Aspire integration | `AddKeycloakContainer`, `AddRealm` |
| `Keycloak.AuthServices.Templates` | `dotnet new` templates | `keycloak-webapi`, `keycloak-aspire-starter` |

## Configuration ("Keycloak" Section)

```json
{
  "Keycloak": {
    "realm": "Test",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "test-client",
    "verify-token-audience": true,
    "credentials": { "secret": "your-secret" }
  }
}
```

Key properties: `realm`, `auth-server-url`, `resource` (client ID), `verify-token-audience`, `credentials.secret`. Supports both kebab-case and PascalCase.

## Setup Patterns

**JWT Bearer:** `builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);`
**OIDC:** `builder.Services.AddKeycloakWebAppAuthentication(builder.Configuration);`
**RBAC:** `builder.Services.AddKeycloakAuthorization(builder.Configuration)` then `.RequireRealmRoles()` / `.RequireResourceRoles()`
**Authorization Server:** `builder.Services.AddKeycloakAuthorization().AddAuthorizationServer(builder.Configuration);` then `.RequireProtectedResource("resource", "scope")`
**Dynamic tokens:** `IKeycloakAccessTokenProvider` for custom token sourcing
**Admin API:** `builder.Services.AddKeycloakAdminHttpClient(builder.Configuration)` with `AddClientCredentialsTokenHandler`
**Protection API:** `builder.Services.AddKeycloakProtectionHttpClient(builder.Configuration)` with `AddClientCredentialsTokenHandler`

## Keycloak Concepts

- **Realm**: Security domain isolating users, roles, clients
- **Client**: App registration (client ID = `resource` in config)
- **Realm Role**: Global role across all clients (`realm_access.roles` claim)
- **Client Role**: Scoped to specific client (`resource_access.{client}.roles` claim)
- **Resource/Scope/Policy/Permission**: Fine-grained authorization model (Authorization Server)
- **PEP**: Your app (enforces decisions) — **PDP**: Keycloak (evaluates policies)

## Deep Reference

For detailed documentation, read files in `skills/keycloak-administration/references/` and `skills/keycloak-auth-services/references/`. Use the `keycloak-docs` tool to look up specific topics.
