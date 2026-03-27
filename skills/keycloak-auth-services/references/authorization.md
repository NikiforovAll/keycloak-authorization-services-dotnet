# Authorization (RBAC)

Role-Based Access Control using Keycloak realm roles and client (resource) roles.

## Setup

```bash
dotnet add package Keycloak.AuthServices.Authorization
```

```csharp
builder.Services.AddKeycloakAuthorization(builder.Configuration);
```

## Require Realm Roles

Realm roles are global roles that apply to the entire Keycloak realm.

```csharp
builder.Services.AddKeycloakAuthorization(builder.Configuration)
    .AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRealmRoles("admin"));
```

## Require Resource (Client) Roles

Resource roles are scoped to a specific client.

With explicit client name:

```csharp
builder.Services.AddKeycloakAuthorization(builder.Configuration)
    .AddAuthorizationBuilder()
    .AddPolicy("EditorOnly", policy =>
        policy.RequireResourceRoles("editor")); // uses default client from config
```

The client name is taken from `KeycloakAuthorizationOptions.Resource` in config:

```json
{
  "Keycloak": {
    "resource": "test-client"
  }
}
```

Override default source with `RolesResource`:

```csharp
builder.Services.AddKeycloakAuthorization(options =>
{
    options.RolesResource = "other-client";
});
```

## Keycloak Role Claims Transformation

Map Keycloak roles to standard ASP.NET Core role claims. **Disabled by default.**

Enable via configuration:

```json
{
  "Keycloak": {
    "EnableRolesMapping": "Realm"
  }
}
```

Or inline:

```csharp
builder.Services.AddKeycloakAuthorization(options =>
{
    options.EnableRolesMapping = RolesClaimTransformationSource.All;
    options.RolesResource = "test-client";
});
```

### Mapping Sources

| `EnableRolesMapping` | `RolesResource` | Source |
|---------------------|-----------------|--------|
| `Realm` | N/A | `token.realm_access.roles` |
| `ResourceAccess` | `test-client` | `token.resource_access.test-client.roles` |
| `All` | `test-client` | Both realm + resource roles combined |

Once mapped, use standard ASP.NET Core role-based authorization:

```csharp
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
```

### Target Claim Type

The target claim type defaults to `"role"`. Override with:

```csharp
options.RoleClaimType = ClaimTypes.Role;
```

## JWT Token Structure

Keycloak JWT tokens contain roles in two locations:

```json
{
  "realm_access": {
    "roles": ["default-roles-test", "offline_access", "uma_authorization"]
  },
  "resource_access": {
    "test-client": {
      "roles": ["manage-account", "view-profile"]
    }
  }
}
```

`RequireRealmRoles` checks `realm_access.roles`, `RequireResourceRoles` checks `resource_access.{client}.roles`.

## Token Introspection (Lightweight Access Tokens)

Keycloak 24+ lightweight access tokens lack `realm_access`/`resource_access` claims. Use `AddKeycloakTokenIntrospection()` to resolve them via the introspection endpoint.

```csharp
builder.Services.AddKeycloakTokenIntrospection(builder.Configuration);

builder.Services.AddKeycloakAuthorization(options =>
{
    builder.Configuration.GetSection("Keycloak").Bind(options);
    options.EnableRolesMapping = RolesClaimTransformationSource.All;
});
```

Requirements: confidential client with `credentials.secret` in config.

Features:
- `HybridCache`-based caching per token (`jti` or SHA256 hash), configurable `CacheDuration` (default 60s)
- Returns `IHttpClientBuilder` for resilience policy chaining (e.g. `.AddStandardResilienceHandler()`)
- Startup validation — fails fast if client credentials missing
- `OnTokenIntrospected` delegate for custom claim processing after default enrichment
- Skips introspection if `realm_access`/`resource_access` already present (not a lightweight token)

### Default Claim Mapping

Skipped claims (infrastructure): `active`, `iat`, `exp`, `nbf`, `iss`, `sub`, `aud`, `jti`, `typ`, `azp`, `auth_time`, `session_state`, `acr`, `sid`

JSON claims (stored as `ValueType="JSON"`): `resource_access`, `realm_access`, `organization`

### Extending Introspection

```csharp
builder.Services.AddKeycloakTokenIntrospection(options =>
{
    builder.Configuration.GetSection("Keycloak").Bind(options);
    options.OnTokenIntrospected = (identity, claims) =>
    {
        if (claims.TryGetValue("department", out var dept))
            identity.AddClaim(new Claim("department", dept.GetString()!));
    };
});
```
