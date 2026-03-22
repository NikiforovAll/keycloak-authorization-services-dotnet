# Authorization & Role-Based Access Control (RBAC)

## Roles

### Realm Roles

Global roles across all clients in a realm. Example: `admin`, `user`, `manager`.

### Client Roles

Scoped to a specific client. Example: `my-app:editor`, `my-app:viewer`.

### Composite Roles

A role that includes other roles. Example: `admin` = `user` + `manager`.
Simplifies assignment — assign one composite role instead of many individual roles.

### Default Roles

Automatically assigned to new users. Configure in Realm Settings → Roles → Default Roles.

## Role Mapping

### To Users

Users → Select user → Role Mappings → Assign realm/client roles.

### To Groups (Preferred)

Groups → Select group → Role Mappings. All members inherit roles.

**Best practice**: Map roles to groups, assign users to groups. Avoids per-user role sprawl.

### Effective Roles

The union of: directly assigned roles + group-inherited roles + composite role expansions.

## Token Role Claims

Roles appear in tokens under:

```json
{
  "realm_access": {
    "roles": ["admin", "user"]
  },
  "resource_access": {
    "my-client": {
      "roles": ["editor"]
    }
  }
}
```

Control via client scope mappers — can customize claim name, nesting, and which token types include roles.

## Fine-Grained Authorization (UMA)

Resource-level authorization with policies. Enable on a client's Authorization tab.

### Components

| Component | Description | Example |
|-----------|-------------|---------|
| **Resource** | Protected object | `/api/documents`, `document-123` |
| **Scope** | Action on a resource | `read`, `write`, `delete` |
| **Policy** | Access rule | "Only admins can delete" |
| **Permission** | Binds resource/scope to policy | `delete` on `/api/documents` requires `admin-policy` |

### Policy Types

| Type | Evaluates |
|------|-----------|
| Role | User has specific realm/client role |
| User | Specific user identity |
| Group | Group membership |
| Time | Date/time window |
| JavaScript | Custom logic (scripted) |
| Aggregated | Combines multiple policies (AND/OR) |
| Client | Specific requesting client |
| Regex | Claim matches regex pattern |

### Decision Strategies

- **Unanimous**: ALL policies must grant (default, most secure)
- **Affirmative**: ANY policy grants access
- **Consensus**: Majority of policies grant

### Authorization Flow (RPT)

1. Client requests a Resource Permission Ticket (RPT) from Keycloak
2. Keycloak evaluates applicable policies
3. Returns RPT with granted permissions
4. Client presents RPT to resource server
5. Resource server validates RPT claims

### Evaluating Permissions

Test authorization decisions without a real request:

1. Client → Authorization → Evaluate
2. Select user, client, and resource
3. View policy evaluation results and decision

### Protection API

Programmatic resource management:
- Register/update/delete resources
- Query permissions
- Used by resource servers to manage their protected resources dynamically

## Integration with Keycloak.AuthServices

This library provides .NET integration for Keycloak authorization:

- **Realm roles**: `RequireRealmRoles("admin")` in policy builder
- **Client roles**: `RequireResourceRoles("editor")` in policy builder
- **Resource protection**: `[ProtectedResource("resource", "scope")]` attribute
- **Authorization Server (RPT)**: `IAuthorizationServerClient` for token-based authorization
- **Protection API**: `IKeycloakProtectionClient` for resource management
