# Keycloak.AuthServices — Claude Code Plugin

AI coding agent skills for Keycloak administration and Keycloak.AuthServices .NET library development.

## Skills

| Skill | Description |
|-------|-------------|
| `/keycloak-authservices:keycloak-administration` | Keycloak IAM administration — realms, clients, SSO, RBAC, troubleshooting |

## Installation

### From GitHub

```bash
# 1. Add the marketplace
/plugin marketplace add NikiforovAll/keycloak-authorization-services-dotnet

# 2. Install the plugin
/plugin install keycloak-authservices@keycloak-authservices
```

### From Local Path (Development)

```bash
# Add local directory as marketplace
/plugin marketplace add ./path/to/keycloak-authorization-services-dotnet

# Install
/plugin install keycloak-authservices@keycloak-authservices
```

### Quick Test (Development)

```bash
# Load plugin directly without installation
claude --plugin-dir ./path/to/keycloak-authorization-services-dotnet
```

### Verify Installation

After installation, run `/reload-plugins` and the skill becomes available:

```
/keycloak-authservices:keycloak-administration
```

## Plugin Structure

```
.claude-plugin/
  plugin.json                          # Plugin manifest
  marketplace.json                     # Marketplace catalog
skills/
  keycloak-administration/
    SKILL.md                           # Main skill instructions
    references/
      realm-management.md              # Realms, users, groups, sessions
      client-configuration.md          # OIDC/SAML clients, scopes, mappers
      authentication-sso.md            # Auth flows, MFA, identity brokering
      authorization-rbac.md            # Roles, UMA, policies, permissions
      user-federation.md               # LDAP/AD integration, sync, mappers
      security-hardening.md            # Password policies, TLS, audit
      ha-scalability.md                # Clustering, monitoring, backup/DR
      troubleshooting.md               # Diagnostics and common issues
      integration-examples.md          # .NET, Spring Boot, Node.js examples
```

## License

MIT
