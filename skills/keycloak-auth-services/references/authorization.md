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
