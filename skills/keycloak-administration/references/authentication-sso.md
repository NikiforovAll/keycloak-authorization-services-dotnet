# Authentication & Single Sign-On (SSO)

## Authentication Flows

### Browser Flow (Default Login)

1. Cookie check (existing session)
2. Kerberos (if configured)
3. Identity Provider redirector
4. Username/password form
5. OTP (if MFA enabled)
6. WebAuthn (if configured)

### Direct Grant Flow (API Login)

1. Username/password validation
2. OTP validation (if required)

### Customizing Flows

1. Authentication → Flows
2. **Duplicate** existing flow (never modify built-in flows)
3. Add/remove/reorder authenticators
4. Set requirements: Required, Alternative, Disabled, Conditional
5. Bind custom flow to realm

## Multi-Factor Authentication (MFA)

### OTP (Time-Based)

1. Authentication → Required Actions → Enable "Configure OTP"
2. Users enroll via authenticator app (Google Authenticator, Authy, etc.)
3. Backup: generate recovery codes

### WebAuthn (FIDO2)

1. Add WebAuthn authenticator to auth flow
2. Users register hardware keys or biometrics
3. Supports passwordless authentication

### Conditional MFA

Apply MFA selectively based on:
- IP range / network location
- User attributes or group membership
- Authentication age (re-auth after timeout)

Configure via "Conditional OTP" sub-flow in authentication flow.

## Single Sign-On (SSO)

### How It Works

1. User authenticates with Keycloak once
2. Keycloak creates SSO session (browser cookie)
3. Subsequent apps skip login — session is shared
4. Each app gets its own client session within the SSO session

### Session Configuration

| Setting | Purpose | Recommended |
|---------|---------|-------------|
| SSO Session Idle | Timeout on inactivity | 30 min |
| SSO Session Max | Absolute session limit | 10 hours |
| Remember Me | Survive browser close | Use case dependent |
| Client Session Idle | Per-app inactivity | Match SSO or shorter |

### Single Logout (SLO)

Logout from one app terminates all sessions:
- OIDC: backchannel logout or frontchannel logout
- SAML: SAML logout request propagation

## Identity Brokering

Login with external identity providers.

### OIDC Identity Providers

1. Identity Providers → Add Provider → OpenID Connect
2. Configure: Authorization URL, Token URL, Client ID/Secret
3. Set up attribute/role mappers
4. Configure first login flow (auto-create or link existing users)

### SAML Identity Providers

1. Identity Providers → Add Provider → SAML
2. Import or enter IdP metadata
3. Configure name ID mapping
4. Set up assertion attribute mappers

### Social Login

Built-in providers: Google, GitHub, Facebook, Microsoft, Twitter, LinkedIn, etc.

1. Identity Providers → Add Provider → select social provider
2. Enter OAuth app credentials (Client ID/Secret from provider)
3. Configure scope and attribute mappers

### First Login Flow

Controls what happens when a user logs in via external IdP for the first time:
- **Review Profile**: Let user review/edit imported profile
- **Create User**: Auto-create Keycloak user
- **Link Existing**: Match and link to existing account (by email)

## Custom Authenticators (SPI)

For advanced scenarios, implement custom authenticators via Keycloak SPI:
- Custom authentication logic
- External system integration
- Hardware token validation
- Risk-based authentication
