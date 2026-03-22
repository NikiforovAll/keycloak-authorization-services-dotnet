# Troubleshooting & Diagnostics

## Login Failures

### Users Can't Login

| Check | How |
|-------|-----|
| User enabled? | Users → user → Details → Enabled |
| Email verified? | Required if "Verify email" is on |
| Required actions? | Pending: update password, configure OTP |
| Redirect URI? | Must match exactly — no trailing slashes, correct scheme |
| Client enabled? | Clients → client → Enabled |
| Auth flow? | Correct flow bound to realm |

**Debug**: Events → Login Events — shows exact error reason per attempt.

### "Invalid redirect_uri" Error

- Redirect URI in request must exactly match one in client's Valid Redirect URIs
- Check scheme (`http` vs `https`), trailing slashes, port numbers
- Wildcards (`*`) should only be used in development

### "Invalid client_secret" or "Unauthorized client"

- Verify client authentication method (secret vs JWT vs certificate)
- Regenerate secret if compromised
- Check client is Confidential (not Public) if using secrets

## Token Issues

### Token Validation Fails

| Issue | Solution |
|-------|----------|
| Signature invalid | Use correct realm public key / JWKS endpoint |
| Token expired | Check `exp` claim, adjust token lifespan |
| Wrong issuer | Issuer must match `{AuthServerUrl}/realms/{realm}` |
| Audience mismatch | Add audience mapper to client scope |
| Clock skew | Sync server clocks (NTP), use `TokenClockSkew` config |

### Token Missing Expected Claims

- Check client scopes assigned to the client
- Verify protocol mappers exist and are configured correctly
- Check "Add to access token" / "Add to ID token" flags on mapper
- Verify "Full scope allowed" setting on client

### Refresh Token Fails

- Token may be expired (check refresh token lifespan)
- SSO session may have ended
- Offline tokens: check offline session settings
- Client may have been reconfigured (invalidates existing tokens)

## LDAP/Federation Issues

### Users Not Syncing

1. Test connection: User Federation → Test Connection
2. Verify bind DN has read access
3. Check Users DN matches LDAP structure
4. Review LDAP attribute mappers
5. Run manual sync: Sync All Users / Sync Changed Users

### LDAP Users Can't Authenticate

- Verify LDAP password policy compatibility
- Check if account is locked in LDAP
- Ensure Edit Mode allows password validation against LDAP
- Review Kerberos integration if applicable

## Session Problems

### Unexpected Logouts

| Cause | Fix |
|-------|-----|
| SSO Session Idle too short | Increase idle timeout |
| SSO Session Max reached | Increase max lifespan |
| Client Session Idle | Align with SSO settings |
| Load balancer not sticky | Enable sticky sessions or distributed cache |
| Cookie domain mismatch | Verify hostname configuration |

### Sessions Not Sharing (SSO Broken)

- All apps must use same realm
- Check cookie domain — must be accessible by all apps
- Verify HTTPS and Secure cookie flag settings
- Check SameSite cookie attribute

## Performance Issues

### Slow Login

- Enable database connection pooling
- Check database query performance
- Review custom authenticators for slow external calls
- Enable caching (realm, user, authorization)

### High Memory Usage

- Tune Infinispan cache sizes
- Reduce session timeouts to limit active sessions
- Monitor with `/metrics` endpoint (Prometheus)

## Logging & Diagnostics

### Enable Debug Logging

```bash
# Startup flag
bin/kc.sh start-dev --log-level=DEBUG

# Specific categories
bin/kc.sh start-dev --log-level=org.keycloak:DEBUG

# Useful categories
# org.keycloak.authentication — auth flow debugging
# org.keycloak.authorization — policy evaluation
# org.keycloak.broker — identity provider issues
# org.keycloak.storage — user federation/LDAP
```

### Event Logging

1. Realm Settings → Events
2. Enable Login Events and Admin Events
3. Set event expiration (e.g., 30 days)
4. Save events to database for audit trail

**Login Events**: login, logout, login_error, register, code_to_token
**Admin Events**: realm/client/user/role CRUD operations

### Health Endpoints

```
GET /health/ready    — readiness probe
GET /health/live     — liveness probe
GET /metrics         — Prometheus metrics
```
