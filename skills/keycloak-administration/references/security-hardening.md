# Security Hardening

## Password Policies

Realm Settings → Authentication → Password Policy:

| Policy | Recommended |
|--------|-------------|
| Minimum length | 12 characters |
| Uppercase | At least 1 |
| Digits | At least 1 |
| Special characters | At least 1 |
| Not username | Enabled |
| Password history | 5 (prevent reuse) |
| Expiry | 90 days (if required by policy) |

## Brute Force Detection

Realm Settings → Security Defenses → Brute Force Detection:

| Setting | Recommended |
|---------|-------------|
| Enabled | Yes |
| Permanent lockout | No (use temporary) |
| Max login failures | 5 |
| Wait increment | 60 seconds |
| Max wait | 900 seconds (15 min) |
| Failure reset time | 12 hours |

## SSL/TLS

- **Require SSL**: all requests in production
- TLS 1.2+ only, strong cipher suites
- Valid certificates (Let's Encrypt or commercial CA)
- HSTS headers via reverse proxy

## Token Security

- Access token lifespan: 5–15 minutes
- Refresh token: 30 min – 1 hour idle
- Enable refresh token rotation
- Revoke refresh tokens on logout
- Validate: signature, issuer (`iss`), audience (`aud`), expiration (`exp`), not-before (`nbf`)

## Admin Security

- Strong passwords (16+ chars) with MFA for all admin accounts
- Minimize number of admin accounts
- Use master realm only for administration
- Restrict admin console access by IP / VPN
- Enable admin event logging
- Audit admin actions regularly

## Event Logging & Auditing

### Setup

1. Realm Settings → Events → Enable Save Events
2. Login events retention: 30–90 days
3. Admin events retention: 90–180 days

### Events to Monitor

- Failed login attempts (brute force indicator)
- Password and credential changes
- Role/permission modifications
- Client configuration changes
- Token issuance and revocation

### SIEM Integration

Export events to centralized logging (Splunk, ELK, etc.) for alerting and analysis.

## Production Checklist

- [ ] SSL/TLS enabled for all traffic
- [ ] Password policy configured
- [ ] Brute force detection enabled
- [ ] Admin accounts secured with MFA
- [ ] Event logging enabled
- [ ] Token lifespans tuned
- [ ] No wildcard redirect URIs
- [ ] Unused clients/realms disabled
- [ ] Regular security updates applied
