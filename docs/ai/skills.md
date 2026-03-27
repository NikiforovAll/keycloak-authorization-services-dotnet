# Claude Code Skills

This project ships [Claude Code skills](https://docs.anthropic.com/en/docs/claude-code/skills) — specialized knowledge modules that help AI coding assistants work effectively with Keycloak.AuthServices.

## Available Skills

### keycloak-auth-services

Implementation guide for the Keycloak.AuthServices .NET library covering:

- **Authentication** — JWT Bearer (Web API), OpenID Connect (Web App), RFC 8414 server metadata discovery
- **Authorization** — RBAC (realm/client roles), role claims transformation, token introspection for lightweight access tokens
- **Resource Protection** — Authorization Server integration, Protected Resource Builder, dynamic resources, `IProtectedResourcePolicyBuilder`, pluggable parameter resolvers, `IKeycloakAccessTokenProvider`
- **Organization Authorization** — Multi-tenancy with `RequireOrganizationMembership()`, organization claim parsing, declarative and imperative authorization patterns
- **Admin REST API** — Hand-written SDK and Kiota-generated client, access token management
- **Protection API** — UMA-compliant resource, permission, and policy management
- **Developer Experience** — .NET Aspire integration, project templates, OpenTelemetry instrumentation
- **Configuration** — All options classes, naming conventions, adapter file support
- **Troubleshooting** — Common issues, recipes, Swagger UI integration

**Invoke:** `/keycloak-authservices:keycloak-auth-services`

### keycloak-administration

Keycloak IAM administration guidance covering:

- **Realm Management** — Realm creation, configuration, import/export
- **Client Configuration** — Client types, authentication modes, service accounts, audience mappers
- **Authentication & SSO** — Authentication flows, OIDC, SAML, identity providers
- **Authorization & RBAC** — Roles, policies, permissions, authorization services
- **User Federation** — LDAP, Active Directory, custom providers
- **Security Hardening** — TLS, brute force protection, session management, token policies
- **High Availability** — Clustering, database configuration, caching, load balancing
- **Troubleshooting** — Common errors, debugging, logging

**Invoke:** `/keycloak-authservices:keycloak-administration`

## Installation

Skills are included in the repository under the `skills/` directory. When using [Claude Code](https://docs.anthropic.com/en/docs/claude-code) within this project, skills are automatically available.

To use in another project, install via the plugin system:

```bash
# Add the marketplace
/plugin marketplace add NikiforovAll/keycloak-authorization-services-dotnet

# Install the plugin
/plugin install keycloak-authservices@NikiforovAll-keycloak-authorization-services-dotnet
```

Once installed, skills are available as namespaced slash commands:

```bash
/keycloak-authservices:keycloak-auth-services
/keycloak-authservices:keycloak-administration
```

## How Skills Work

Skills provide context-aware assistance by loading relevant reference documentation on demand. When you ask about a specific topic (e.g., "how do I set up organization authorization?"), the skill loads only the relevant reference file — keeping the context focused and responses accurate.

Each skill consists of:

- **SKILL.md** — Entry point with quick start guide and topic index
- **references/** — Detailed reference files for each feature area
