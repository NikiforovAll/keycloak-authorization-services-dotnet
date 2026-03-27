# OAuth 2.0 Server Metadata Discovery (RFC 8414) Sample

Demonstrates using RFC 8414 `/.well-known/oauth-authorization-server` metadata discovery instead of the default OIDC discovery, using `Keycloak.AuthServices.Authentication`.

## Prerequisites

- Docker / Podman
- .NET 10 SDK
- `jq` and `curl` (for scripts)

## Quick Start

```bash
# 1. Start Keycloak
docker compose up -d

# 2. Wait for Keycloak to be ready, then verify both discovery endpoints
bash ./assets/verify-metadata.sh

# 3. Get a bearer token and save to .env
bash ./assets/get-token.sh > .env

# 4. Run the sample
dotnet run
```

## What This Demonstrates

The sample configures JWT Bearer authentication to use **RFC 8414** metadata discovery:

```csharp
services.AddKeycloakWebApiAuthentication(options =>
{
    configuration.BindKeycloakOptions(options);
    options.MetadataAddress = KeycloakConstants.OAuthAuthorizationServerMetadataPath;
});
```

Instead of fetching server metadata from:
- `{realm}/.well-known/openid-configuration` (OIDC, default)

It fetches from:
- `{realm}/.well-known/oauth-authorization-server` (RFC 8414)

Both endpoints return the same signing keys and token endpoints, but RFC 8414 is the correct endpoint for pure OAuth 2.0 scenarios (M2M, MCP, non-OIDC clients).

## Endpoints

| Endpoint | Auth | Description |
|----------|------|-------------|
| `GET /` | Yes | Protected endpoint |
| `GET /metadata-info` | No | Shows configured metadata URLs |

## Cleanup

```bash
docker compose down -v
```
