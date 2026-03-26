# Token Introspection Sample

Demonstrates Keycloak token introspection for lightweight access tokens using `Keycloak.AuthServices.Authorization`.

## Prerequisites

- Docker / Podman
- .NET 10 SDK
- `jq` and `curl` (for seed/token scripts)

## Quick Start

```bash
# 1. Start Keycloak
docker compose up -d

# 2. Seed: configure client to issue lightweight tokens (removes role mappers)
bash ./assets/seed.sh

# 3. Get a bearer token and save to .env
bash ./assets/get-token.sh > .env

# 4. Run the sample
dotnet run
```

## What Happens

The seed script removes `realm roles` and `client roles` protocol mappers from the test-client, simulating lightweight access tokens. The resulting JWT will **not** contain `realm_access` or `resource_access` claims.

Without introspection, role-based authorization would silently fail (403 Forbidden). With `AddKeycloakTokenIntrospection()`, the library calls the Keycloak introspection endpoint to resolve the full claim set, and role mapping works transparently.

## What Gets Created

| Resource | Details |
|----------|---------|
| Realm | `Test` |
| Client | `test-client` (confidential, direct access grants) |
| Users | `test` / `test` (Reader role), `testadminuser` / `test` (Admin role + TestClientRole) |
| Mappers | `realm roles` and `client roles` mappers removed → lightweight tokens |

## Endpoints

Test with `assets/run.http` (VS Code REST Client) or curl.

| Endpoint | Description |
|----------|-------------|
| `GET /me` | Returns user identity and all claims (enriched via introspection) |
| `GET /roles` | Returns resolved role claims |
| `GET /admin` | Requires `Admin` realm role (resolved via introspection) |

## How It Works

```csharp
// Program.cs — key registration order:
services.AddKeycloakTokenIntrospection(configuration);  // 1. Introspect lightweight tokens
services.AddKeycloakAuthorization(options =>     // 2. Map roles from enriched claims
{
    options.EnableRolesMapping = RolesClaimTransformationSource.All;
});
```

The introspection transformation runs **before** the roles transformation because `IClaimsTransformation` instances execute in registration order. This ensures `realm_access` and `resource_access` claims are available when role mapping runs.

## Token Comparison

**Without introspection** (lightweight token):
```json
{
  "sub": "...",
  "typ": "Bearer",
  "azp": "test-client",
  "scope": "openid profile email"
  // No realm_access, no resource_access, no preferred_username
}
```

**After introspection** (enriched claims):
```json
{
  "sub": "...",
  "realm_access": { "roles": ["Admin", "default-roles-test", "offline_access", "uma_authorization"] },
  "resource_access": { "test-client": { "roles": ["TestClientRole"] } },
  "preferred_username": "testadminuser"
}
```

## Cleanup

```bash
docker compose down -v
```
