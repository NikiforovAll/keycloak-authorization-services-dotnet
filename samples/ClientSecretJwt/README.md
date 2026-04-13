# `client_secret_jwt` Authentication Sample

Demonstrates how to use Keycloak's **"Signed JWT with Client Secret"** (`client_secret_jwt`) authentication method with `Keycloak.AuthServices.Sdk` and `Duende.AccessTokenManagement`.

> **Why?** Some compliance policies prohibit sending the shared secret over the wire, even when protected by TLS. With `client_secret_jwt`, the secret is used locally as an HMAC signing key — only a short-lived, signed JWT travels to Keycloak.

## How `client_secret_jwt` Works

```
Your App                   Keycloak Token Endpoint
   │                               │
   │  1. Build JWT assertion        │
   │     iss = api-client           │
   │     sub = api-client           │
   │     aud = <token-endpoint>     │
   │     exp = now + 60s            │
   │     Sign with HS256(secret)    │
   │                               │
   │  2. POST /token               │
   │     client_assertion=<jwt>    │
   │     client_assertion_type=    │
   │       urn:...:jwt-bearer      │
   │──────────────────────────────►│
   │                               │  3. Verify JWT signature
   │                               │     using the stored shared secret
   │◄──────────────────────────────│
   │  4. { access_token: "..." }   │
```

The shared secret **never leaves** the application.

## Prerequisites

- Docker / Podman
- .NET 10 SDK

## Quick Start

```bash
# 1. Start Keycloak with the pre-configured Test realm
docker compose up -d

# 2. Run the sample (waits ~10s for Keycloak to be ready)
dotnet run
```

Then use `assets/run.http` (VS Code REST Client) to test the endpoints:

```bash
# Or with curl:
curl http://localhost:5099/realm
curl http://localhost:5099/users
```

## What Gets Created

Keycloak is seeded automatically via `--import-realm`:

| Resource | Details |
|----------|---------|
| Realm | `Test` |
| Client | `api-client` — confidential, `clientAuthenticatorType: client-secret-jwt`, service accounts enabled |
| Secret | `s3cr3t-f0r-signing-jwt-ass3rtion` (shared HMAC key, not sent over wire) |
| Service Account Roles | `view-realm`, `view-users`, `query-users`, `query-realms` from `realm-management` |

## Keycloak UI Setup (manual reference)

If configuring a real Keycloak instance manually:

1. Open **Clients** → your client → **Credentials** tab
2. Set **Client Authenticator** → **"Signed JWT with Client Secret"**
3. Note the **Secret** value — this becomes your HMAC signing key

![Keycloak client_secret_jwt UI](https://github.com/user-attachments/assets/1b0d3953-3b62-4604-b6d5-92afd9358c06)

## How It Works

### 1. Register the assertion service

```csharp
// ClientSecretJwtAssertionService.cs
// Implements Duende's IClientAssertionService to produce the JWT assertion.
// The raw secret (credentials.secret) is used only as the HS256 signing key.
services.AddSingleton<IClientAssertionService, ClientSecretJwtAssertionService>();
```

### 2. Configure token management — no ClientSecret

```csharp
services
    .AddClientCredentialsTokenManagement()
    .AddClient("keycloak", client =>
    {
        client.ClientId = keycloakOptions.Resource;
        client.TokenEndpoint = new Uri(keycloakOptions.KeycloakTokenEndpoint);
        // No ClientSecret — IClientAssertionService handles auth
    });
```

### 3. Attach the token handler to the HttpClient

```csharp
services
    .AddKeycloakAdminHttpClient(configuration)
    .AddClientCredentialsTokenHandler("keycloak");
```

### 4. The assertion builder (`ClientSecretJwtAssertionService`)

```csharp
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
var token = new JwtSecurityToken(
    issuer: clientId,
    audience: tokenEndpoint,
    claims: [new Claim("sub", clientId), new Claim("jti", Guid.NewGuid().ToString())],
    expires: DateTime.UtcNow.AddMinutes(1),
    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
);
// Sent as: client_assertion_type=urn:ietf:params:oauth:client-assertion-type:jwt-bearer
//          client_assertion=<signed JWT>
```

## Endpoints

| Endpoint | Description | Required role |
|----------|-------------|---------------|
| `GET /realm` | Returns `Test` realm info | `view-realm` |
| `GET /users` | Lists users in `Test` realm | `view-users` |

## Comparison with Other Auth Methods

| Method | Secret on wire? | Library support |
|--------|----------------|-----------------|
| `client_secret_post` | ✅ Yes (POST body, TLS-protected) | ✅ Default |
| `client_secret_basic` | ✅ Yes (Authorization header, TLS-protected) | ✅ Via Duende |
| `client_secret_jwt` | ❌ No — only HMAC-signed JWT sent | ✅ **This sample** |
| `private_key_jwt` | ❌ No — only asymmetric-signed JWT sent | 🔲 Future |

## Cleanup

```bash
docker compose down -v
```
