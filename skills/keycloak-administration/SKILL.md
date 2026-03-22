---
name: keycloak-administration
description: >-
  Keycloak identity and access management (IAM) administration guidance — realm management,
  client configuration, authentication flows, authorization policies, security hardening,
  and troubleshooting. Use when configuring Keycloak, setting up SSO, managing realms/clients,
  troubleshooting authentication issues, or implementing RBAC. Trigger phrases include
  "Keycloak", "SSO", "OIDC", "SAML", "identity provider", "IAM", "realm", "access management".
user-invocable: true
---

# Keycloak Administration

## Quick Start

Choose your task and load the appropriate reference:

1. **New Installation** → Continue below
2. **Realm & User Management** → Load [realm-management.md](references/realm-management.md)
3. **Client Configuration** → Load [client-configuration.md](references/client-configuration.md)
4. **Authentication & SSO** → Load [authentication-sso.md](references/authentication-sso.md)
5. **Authorization & RBAC** → Load [authorization-rbac.md](references/authorization-rbac.md)
6. **User Federation (LDAP/AD)** → Load [user-federation.md](references/user-federation.md)
7. **Security Hardening** → Load [security-hardening.md](references/security-hardening.md)
8. **High Availability & Scaling** → Load [ha-scalability.md](references/ha-scalability.md)
9. **Troubleshooting** → Load [troubleshooting.md](references/troubleshooting.md)
10. **Integration Examples** → Load [integration-examples.md](references/integration-examples.md)

## Installation & Setup

### Docker (Recommended for Development)

```bash
docker run -d \
  --name keycloak \
  -p 8080:8080 \
  -e KEYCLOAK_ADMIN=admin \
  -e KEYCLOAK_ADMIN_PASSWORD=admin \
  quay.io/keycloak/keycloak:latest \
  start-dev
```

### Production Mode

```bash
bin/kc.sh build --db=postgres

export KC_DB=postgres
export KC_DB_URL=jdbc:postgresql://localhost/keycloak
export KC_DB_USERNAME=keycloak
export KC_DB_PASSWORD=password
export KC_HOSTNAME=keycloak.example.com

bin/kc.sh start --optimized
```

### Initial Configuration Checklist

1. **Admin account** — strong password (12+ chars)
2. **Hostname** — configure `KC_HOSTNAME` for production
3. **SSL/TLS** — required for production
4. **Database** — PostgreSQL recommended
5. **SMTP** — for email verification and password reset

## Core Concepts

| Concept | Description |
|---------|-------------|
| **Realm** | Tenant boundary. Master realm for admin only; create app realms per environment |
| **Client** | Application registration. OIDC (modern) or SAML (legacy). Confidential (server) or Public (SPA/mobile) |
| **User/Group** | Identity with credentials. Groups for hierarchical organization |
| **Realm Role** | Global permission across all clients in a realm |
| **Client Role** | Permission scoped to a single client |
| **Composite Role** | Role that inherits other roles |

## Common Tasks

### Configure SSO for an Application

1. Create OIDC client with your app's `client-id`
2. Set **Valid Redirect URIs** (exact URLs, avoid wildcards)
3. Set **Client Authentication**: On (confidential) or Off (public with PKCE)
4. Get discovery endpoint: `{AuthServerUrl}/realms/{realm}/.well-known/openid-configuration`
5. Integrate with your app — see [client-configuration.md](references/client-configuration.md)

### Enable MFA

1. Authentication → Flows → Duplicate Browser flow
2. Add OTP or WebAuthn authenticator
3. Set as Required or Conditional
4. Bind custom flow to realm

### Connect LDAP/Active Directory

1. User Federation → Add LDAP Provider
2. Configure: URL, Bind DN, Search Base (`ou=users,dc=example,dc=com`)
3. Set up attribute mappers
4. Test connection, then sync

## Essential CLI Commands

```bash
# Admin CLI setup
bin/kcadm.sh config credentials --server http://localhost:8080 --realm master --user admin

# Realm operations
bin/kcadm.sh create realms -s realm=my-realm -s enabled=true
bin/kcadm.sh get realms/my-realm

# User operations
bin/kcadm.sh create users -r my-realm -s username=john -s enabled=true
bin/kcadm.sh set-password -r my-realm --username john --new-password secret

# Export/Import
bin/kc.sh export --dir /backup --realm my-realm
bin/kc.sh import --dir /backup
```

## Best Practices

- **Realm separation**: one realm per app/environment, never use Master for apps
- **Token lifespans**: access tokens 5–15 min, refresh tokens based on use case
- **Public clients**: always require PKCE
- **Roles**: use groups for assignment, roles for permissions, composite roles for aggregation
- **Production security**: SSL/TLS, brute force protection, MFA for admins, event logging

## Reference Documentation

- [realm-management.md](references/realm-management.md) — Realms, users, groups, attributes, sessions
- [client-configuration.md](references/client-configuration.md) — OIDC/SAML clients, scopes, mappers, service accounts
- [authentication-sso.md](references/authentication-sso.md) — Auth flows, MFA, identity brokering, social login
- [authorization-rbac.md](references/authorization-rbac.md) — Roles, fine-grained authorization (UMA), policies, permissions
- [user-federation.md](references/user-federation.md) — LDAP/AD integration, sync, mappers, custom providers
- [security-hardening.md](references/security-hardening.md) — Password policies, brute force, TLS, audit, production checklist
- [ha-scalability.md](references/ha-scalability.md) — Clustering, database tuning, caching, monitoring, backup/DR
- [troubleshooting.md](references/troubleshooting.md) — Login failures, token issues, LDAP sync, session problems, logging
- [integration-examples.md](references/integration-examples.md) — .NET, Spring Boot, Node.js, token validation
