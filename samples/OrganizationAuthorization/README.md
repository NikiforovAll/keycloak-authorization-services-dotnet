# Organization Authorization Sample

Demonstrates Keycloak organization-aware authorization using `Keycloak.AuthServices.Authorization`.

## Prerequisites

- Docker / Podman
- .NET 10 SDK
- `jq` and `curl` (for seed/token scripts)

## Quick Start

```bash
# 1. Start Keycloak
docker compose up -d

# 2. Seed organizations and assign members (waits for KC to be ready)
bash ./assets/seed.sh

# 3. Get a bearer token and save to .env
bash ./assets/get-token.sh > .env

# 4. Run the sample
dotnet run
```

## What Gets Created

| Resource | Details |
|----------|---------|
| Realm | `Test` with organizations enabled |
| Client | `test-client` (public, direct access grants) |
| User | `test` / `test` with `user` realm role |
| Organizations | `acme-corp`, `partner-inc`, `startup-co` |
| Membership | `test` belongs to `acme-corp` + `startup-co` (NOT `partner-inc`) |

## Endpoints

Test with `assets/run.http` (VS Code REST Client) or curl.

### Declarative Authorization

| Endpoint | Pattern | Description |
|----------|---------|-------------|
| `GET /orgs` | Any org | Requires membership in any organization |
| `GET /orgs/{orgId}/projects` | Route param | Resolves org from `{orgId}` route parameter |
| `GET /acme/settings` | Static | Requires membership in `acme-corp` specifically |
| `GET /acme/admin` | Policy | Combines org membership + `user` realm role |
| `GET /tenant/projects` | Header | Resolves org from `X-Organization` header |

### Imperative Authorization

| Endpoint | Description |
|----------|-------------|
| `GET /check/{orgId}` | Programmatic check against a specific org |
| `GET /workspaces` | Filters a list of resources by org membership |

## Token

The `get-token.sh` script requests the `organization:*` scope, which includes all org memberships as an `organization` claim in the JWT.

```bash
# Get token and decode payload
bash ./assets/get-token.sh
```

## Cleanup

```bash
docker compose down -v
```
