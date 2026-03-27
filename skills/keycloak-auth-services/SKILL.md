---
name: keycloak-auth-services
description: "Implementation guide for Keycloak.AuthServices .NET library — authentication (JWT Bearer, OIDC, RFC 8414), authorization (RBAC, resource protection, Authorization Server, organizations, multi-tenancy), Admin REST API SDK, Protection API SDK, and developer experience tooling (.NET Aspire, templates, OpenTelemetry). Trigger phrases include Keycloak.AuthServices, ProtectedResource, Admin SDK, Protection API, organization, RFC 8414, token introspection."
user-invocable: true
---

# Keycloak.AuthServices Implementation Guide

## Quick Start

Choose your task and load the appropriate reference:

1. **JWT Bearer Authentication (Web API)** → Continue below
2. **OIDC Authentication (Web App)** → Load [authentication.md](references/authentication.md)
3. **Authorization & RBAC** → Load [authorization.md](references/authorization.md)
4. **Resource Protection & Authorization Server** → Load [resource-protection.md](references/resource-protection.md)
5. **Admin REST API SDK** → Load [admin-sdk.md](references/admin-sdk.md)
6. **Protection API SDK** → Load [protection-api.md](references/protection-api.md)
7. **Developer Experience (Aspire, Templates)** → Load [devex.md](references/devex.md)
8. **Configuration Reference** → Load [configuration.md](references/configuration.md)
9. **Recipes & Troubleshooting** → Load [troubleshooting.md](references/troubleshooting.md)
10. **Token Introspection (Lightweight Tokens)** → Load [authorization.md](references/authorization.md) (see "Token Introspection" section)
11. **Organization Authorization (Multi-Tenancy)** → Load [organization-authorization.md](references/organization-authorization.md)
12. **RFC 8414 Server Metadata Discovery** → Load [authentication.md](references/authentication.md) (see "Server Metadata Discovery" section)
13. **Custom Token Provider (IKeycloakAccessTokenProvider)** → Load [resource-protection.md](references/resource-protection.md) (see "IKeycloakAccessTokenProvider" section)
14. **Extensible Policy Builder (IProtectedResourcePolicyBuilder)** → Load [resource-protection.md](references/resource-protection.md) (see "IProtectedResourcePolicyBuilder" section)
15. **Pluggable Parameter Resolvers** → Load [resource-protection.md](references/resource-protection.md) (see "Pluggable Parameter Resolvers" section)

## Packages Overview

| Package | Purpose |
|---------|---------|
| `Keycloak.AuthServices.Authentication` | JWT Bearer (Web API) and OpenID Connect (Web App) authentication |
| `Keycloak.AuthServices.Authorization` | RBAC (realm/client roles), Authorization Server client, `[ProtectedResource]` attribute, organization authorization |
| `Keycloak.AuthServices.Sdk` | Hand-written Admin REST API + Protection API HTTP clients |
| `Keycloak.AuthServices.Sdk.Kiota` | Auto-generated (Kiota) Admin REST API client — full API coverage |
| `Keycloak.AuthServices.Common` | Shared configuration (`KeycloakInstallationOptions`), claims utilities |
| `Keycloak.AuthServices.OpenTelemetry` | Metrics and tracing instrumentation |
| `Keycloak.AuthServices.Aspire.Hosting` | .NET Aspire `KeycloakResource` integration |
| `Keycloak.AuthServices.Templates` | `dotnet new` project templates |

## Minimal Web API Setup

```bash
dotnet add package Keycloak.AuthServices.Authentication
dotnet add package Keycloak.AuthServices.Common
```

```csharp
using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!").RequireAuthorization();
app.Run();
```

```json
// appsettings.json — "Keycloak" section (kebab-case from adapter config)
{
  "Keycloak": {
    "realm": "Test",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "test-client",
    "verify-token-audience": true,
    "credentials": {
      "secret": "your-client-secret"
    }
  }
}
```

## Configuration Section

All packages bind to `"Keycloak"` config section by default. Key properties:

| Property | Description |
|----------|-------------|
| `realm` | Keycloak realm name |
| `auth-server-url` | Keycloak server URL (e.g., `http://localhost:8080/`) |
| `resource` | Client ID |
| `ssl-required` | `none`, `external`, or `all` |
| `verify-token-audience` | Validate audience claim against `resource` |
| `credentials.secret` | Client secret (confidential clients) |

Both kebab-case (Keycloak adapter format) and PascalCase are supported.

## Adding Authorization (RBAC)

```bash
dotnet add package Keycloak.AuthServices.Authorization
```

```csharp
builder.Services.AddKeycloakAuthorization(builder.Configuration)
    .AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRealmRoles("admin"))
    .AddPolicy("EditorOnly", policy => policy.RequireResourceRoles("editor"));
```

## Adding Authorization Server (Resource Protection)

```csharp
builder.Services
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(builder.Configuration);

app.MapGet("/workspaces", () => "Hello World!")
    .RequireProtectedResource("workspaces", "workspace:read");
```

## Adding Admin SDK

```bash
dotnet add package Keycloak.AuthServices.Sdk
```

```csharp
builder.Services.AddKeycloakAdminHttpClient(builder.Configuration);

app.MapGet("/users", async (IKeycloakUserClient client) =>
    await client.GetUsers("my-realm"));
```

## Essential Patterns

- **Configuration section**: defaults to `"Keycloak"`, override via `configSectionName` parameter
- **IHttpClientBuilder**: all HTTP clients return `IHttpClientBuilder` for resilience, handlers, etc.
- **Token management**: use `Duende.AccessTokenManagement` for service account tokens
- **OpenTelemetry**: `AddKeycloakAuthServicesInstrumentation()` for metrics and tracing
- **Aspire**: `AddKeycloakContainer("keycloak")` + `AddRealm("Test")` for local dev

## Reference Documentation

- [authentication.md](references/authentication.md) — JWT Bearer and OIDC setup, all overloads, adapter file config, RFC 8414 server metadata discovery
- [authorization.md](references/authorization.md) — RBAC, realm/client roles, role claims transformation, token introspection
- [organization-authorization.md](references/organization-authorization.md) — Organization-based multi-tenancy, membership requirements, parameter resolvers
- [resource-protection.md](references/resource-protection.md) — Authorization Server, Protected Resource Builder, dynamic resources, policy provider, IKeycloakAccessTokenProvider, IProtectedResourcePolicyBuilder, pluggable parameter resolvers
- [admin-sdk.md](references/admin-sdk.md) — Admin REST API (hand-written + Kiota), access token management
- [protection-api.md](references/protection-api.md) — UMA Protection API, resource/permission/policy management
- [devex.md](references/devex.md) — .NET Aspire, templates, OpenTelemetry
- [configuration.md](references/configuration.md) — All configuration options, naming conventions, adapter file
- [troubleshooting.md](references/troubleshooting.md) — Common issues, recipes, debugging
