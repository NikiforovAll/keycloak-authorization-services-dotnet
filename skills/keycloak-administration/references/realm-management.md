# Realm Management

## Creating Realms

- **Master realm**: Administrative only — never use for applications
- **Application realms**: One per app or environment (e.g., `production`, `staging`)

Create via Admin Console: Realm dropdown → Create Realm

## Realm Settings

### Login Settings

| Setting | Purpose |
|---------|---------|
| User registration | Allow public self-registration |
| Forgot password | Enable password reset flow |
| Remember me | Session persistence across browser restarts |
| Verify email | Require email verification for new users |
| Login with email | Allow email as username alternative |
| Edit username | Allow/disallow username changes |

### Token Settings

| Setting | Recommended | Default |
|---------|-------------|---------|
| Access token lifespan | 5–15 min | 5 min |
| SSO session idle | 30 min | 30 min |
| SSO session max | 10 hours | 10 hours |
| Client session idle | 30 min | — |
| Offline session idle | 30 days | 30 days |

### Themes

- Login theme: customize login/registration pages
- Account theme: user self-service UI
- Email theme: email template styling

## User Management

### Create Users

1. Users → Add User
2. Set username (required), email, first/last name
3. Enable account, set email verified status
4. Add required actions: set password, verify email, update profile

### User Attributes

Custom key-value pairs for application metadata. Access via token mappers or Admin API.

### Credentials

- Set temporary password (user must change on first login)
- Set permanent password
- Configure OTP devices
- Manage WebAuthn credentials

### Required Actions

Actions users must complete on next login:
- Update Password
- Verify Email
- Update Profile
- Configure OTP

## Group Management

### Hierarchy

Groups support nesting — child groups inherit parent role mappings.

```
/organization
  /engineering
    /backend
    /frontend
  /marketing
```

### Role Mapping via Groups

1. Groups → Select group → Role Mappings
2. Assign realm roles and/or client roles
3. All members (including subgroups) inherit these roles

**Best practice**: Assign roles to groups, not individual users.

## Session Management

### Active Sessions

- View per-user sessions: Users → Sessions tab
- View all realm sessions: Sessions menu
- Terminate individual or all sessions

### Session Types

| Type | Description |
|------|-------------|
| SSO Session | Browser session shared across clients |
| Client Session | Per-client session within SSO session |
| Offline Session | Long-lived session for offline access tokens |

## Realm Export/Import

```bash
# Export single realm
bin/kc.sh export --dir /backup --realm my-realm

# Export all realms
bin/kc.sh export --dir /backup

# Import
bin/kc.sh import --dir /backup

# Partial import via Admin Console
Realm Settings → Action → Partial Import (JSON)
```

Exports include: realm config, clients, roles, groups, identity providers.
Exports do NOT include: user credentials (passwords), user sessions.
