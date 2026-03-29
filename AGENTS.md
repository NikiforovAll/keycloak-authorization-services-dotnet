# AGENTS.md

This file provides guidance to AI coding agents when working with code in this repository.

## Project Overview

.NET library for Keycloak integration — authentication (JWT Bearer, OIDC), authorization (RBAC, resource protection, Authorization Server/RPT), Admin REST API SDK, and Protection API SDK. Published as NuGet packages under `Keycloak.AuthServices.*`.

## Build & Test Commands

```bash
# Build the solution
dotnet build -p:WarningLevel=0 /clp:ErrorsOnly

# Run unit tests (excludes integration tests)
dotnet cake --target=Test

# Run a single test project
dotnet test tests/Keycloak.AuthServices.Authorization.Tests

# Run a single test by name
dotnet test tests/Keycloak.AuthServices.Authorization.Tests --filter "FullyQualifiedName~TestMethodName"

# Run integration tests (requires Docker for Testcontainers)
dotnet cake --target=IntegrationTest

# Create NuGet packages
dotnet cake --target=Pack

# Full CI pipeline (clean, build, test)
dotnet cake
```

## Architecture

### Source Projects (src/)

| Project | Purpose |
|---------|---------|
| **Common** | Shared config (`KeycloakInstallationOptions`), constants, claims utilities |
| **Authentication** | `AddKeycloakWebApiAuthentication()` (JWT Bearer) and `AddKeycloakWebAppAuthentication()` (OIDC+Cookies) |
| **Authorization** | Authorization handlers, policy providers, `[ProtectedResource]` attribute, Authorization Server (RPT) client |
| **Sdk** | Hand-written HTTP clients for Admin API (`IKeycloakRealmClient`, `IKeycloakUserClient`, etc.) and Protection API (`IKeycloakProtectionClient`) |
| **Sdk.Kiota** | Auto-generated (Kiota) typed client for Keycloak Admin REST API |
| **OpenTelemetry** | Metrics and tracing extensions |
| **Aspire.Hosting** | .NET Aspire `KeycloakResource` integration |
| **Templates** | `dotnet new` project templates |

### Test Projects (tests/)

- `*.Tests` — Unit tests (xUnit, FluentAssertions, MockHttp)
- `IntegrationTests` — Full Keycloak integration via Testcontainers (requires Docker)
- `TestWebApi`, `TestWebApiWithControllers` — Test host apps used by integration tests

### Key Patterns

- **Configuration section**: All projects bind to a `"Keycloak"` config section by default. Key properties: `AuthServerUrl`, `Realm`, `Resource` (client ID), `Credentials:Secret`.
- **Central package management**: Versions in `src/Directory.Packages.props` and `tests/Directory.Packages.props`.
- **MinVer versioning**: Version derived from git tags.
- **Target framework**: net10.0
- **File-scoped namespaces** preferred (`.editorconfig`).
- **Nullable reference types** enabled globally.

## Code Style

- 4 spaces indentation, file-scoped namespaces
- Expression-bodied members preferred
- XML documentation on public APIs

## KeyBot (Agentic Workflow)

- KeyBot is an automated repository assistant running as a GitHub Agentic Workflow
- PRs from KeyBot have `[KeyBot]` title prefix and `automation`/`keybot` labels
- KeyBot uses persistent repo memory on `memory/keybot` branch
- To trigger on-demand: comment `/keybot <instructions>` on any issue or PR
- Workflow spec: `.github/workflows/keybot.md`
- Domain knowledge: `.github/agents/keycloak-expert.agent.md` + `skills/` directory
