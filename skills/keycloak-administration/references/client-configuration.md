# Client Configuration

## Client Types

### OIDC Clients (Recommended)

| Flow | Use Case |
|------|----------|
| Authorization Code | Web apps, server-side (standard flow) |
| Authorization Code + PKCE | SPAs, mobile, public clients |
| Client Credentials | Machine-to-machine, service accounts |
| Device Authorization | Limited-input devices (TVs, CLIs) |

**Avoid**: Implicit flow (deprecated), Direct Access Grants (resource owner password — use sparingly).

### SAML Clients

For legacy enterprise apps requiring SAML 2.0. Supports SSO and Single Logout (SLO).

## Creating an OIDC Client

1. Clients → Create Client
2. Client type: OpenID Connect
3. Client ID: unique identifier (e.g., `my-web-app`)

### Key Settings

| Setting | Confidential (Server) | Public (SPA/Mobile) |
|---------|----------------------|---------------------|
| Client Authentication | On | Off |
| Standard flow | Enabled | Enabled |
| Service accounts | Optional | N/A |
| PKCE | Optional | **Required** |

### URLs

- **Root URL**: Base app URL
- **Valid Redirect URIs**: Exact callback URLs (e.g., `https://app.example.com/callback`)
- **Valid Post Logout Redirect URIs**: Where to go after logout
- **Web Origins**: CORS allowed origins

**Important**: Never use wildcards (`*`) in redirect URIs in production.

### Advanced Settings

- Access token lifespan override
- Proof Key for Code Exchange (PKCE): S256 method
- Consent required: prompt user to approve scopes
- Full scope allowed: inherit all realm role mappings (disable for least privilege)

## Service Account Clients

For machine-to-machine (no user interaction):

1. Create client with Client Authentication: On
2. Enable Service Accounts
3. Disable Standard flow and Implicit flow
4. Assign roles in Service Account Roles tab
5. Authenticate with `client_credentials` grant

## Client Scopes

Control what claims appear in tokens.

### Built-in Scopes

| Scope | Claims |
|-------|--------|
| `openid` | Required for OIDC (`sub`) |
| `profile` | `name`, `family_name`, `given_name`, etc. |
| `email` | `email`, `email_verified` |
| `roles` | `realm_access`, `resource_access` |
| `web-origins` | Allowed CORS origins |

### Custom Scopes

1. Client Scopes → Create
2. Add protocol mappers for custom claims
3. Assign to clients as Default (always included) or Optional (requested via `scope` param)

## Protocol Mappers

Transform user/client data into token claims.

| Mapper Type | Purpose |
|-------------|---------|
| User Attribute | Map user attribute to claim |
| User Realm Role | Include realm roles |
| User Client Role | Include client-specific roles |
| Audience | Add `aud` claim for a client |
| Group Membership | Include user's groups |
| Hardcoded Claim | Static value in token |
| Script | Custom JavaScript logic |

### Common Mapper Config

- **Token Claim Name**: JSON path in token (e.g., `realm_access.roles`)
- **Add to ID token**: Include in identity token
- **Add to access token**: Include in access token
- **Add to userinfo**: Include in userinfo endpoint response

## Client Authentication Methods

| Method | Description |
|--------|-------------|
| Client ID and Secret | Standard shared secret |
| Signed JWT | Client authenticates with signed JWT (`private_key_jwt`) |
| X509 Certificate | Mutual TLS client authentication |

## Discovery Endpoint

All client configuration details available at:

```
{AuthServerUrl}/realms/{realm}/.well-known/openid-configuration
```

Returns: authorization endpoint, token endpoint, JWKS URI, supported scopes, grant types, etc.
