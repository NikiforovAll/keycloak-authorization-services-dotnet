# KeyBot Memory

## Last Run
- Date: 2026-04-10
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24267118558
- Tasks: Task 3 (Issue Fix), Task 2 (Issue Comment), Task 11 (Monthly Summary)

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" — OPEN

## PRs Created (All Open/Draft)
- #221: `perf: avoid HashSet allocation in role requirement handlers`
- #223: `fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval` — fixes #104
- #224: `docs: add recipe for connecting containerized API to Keycloak` — closes #115
- #225: `fix: robustify RptRequirementHandler JSON parsing`
- #230: `feat(aspire): add IKeycloakDbAdapter and WithDatabase extension` — closes #113
- #233: `test: add WebApiAuthenticationRegistrationTests`
- #235: `feat: add AdditionalAudiences to KeycloakAuthenticationOptions` — partially closes #135
- #237: `docs: improve XML documentation in PoliciesBuilderExtensions and KeycloakAuthorizationServerOptions`
- #238: `perf: single-pass org claims scan and avoid ToArray in VerificationPlan`
- #239: `eng: add startup validators for Keycloak SDK client options`
- #240: `test: add WebAppAuthenticationRegistrationTests for OIDC/Cookie options`
- (new): `fix: correct nameof() references in DecisionRequirementHandler` — branch: keybot/fix-decision-requirement-handler-nameof-20260410

## Merged PRs
- #226: `refactor: improve VerificationPlan enumerator and clear logic` — MERGED
- #228: `fix: correct nameof() in requirement handler log messages` — MERGED (missed DecisionRequirementHandler — fixed in new branch above)

## Blocked Issues
- #232: Dependency update issue — Directory.Packages.props is protected; branch keybot/eng-dependency-updates-20260403-c4136f4a18e1123f has the changes

## Issues Commented On
- #96: UMA Support (2026-04-03, 2026-04-10 — added Phase 1-3 roadmap with code)
- #104: Deep-dive + fix PR #223
- #113: Aspire DB integration (2026-04-02)
- #115: Docker issuer mismatch (2026-03-30)
- #135: Multi-client guidance (2026-03-29)
- #174: Signed JWT client auth (2026-04-05)
- #196: Organization-scoped token exchange (2026-04-08)
- #198: DPoP support (2026-04-01)

## Technical Notes
- KeycloakUrlRealm includes trailing slash: "https://keycloak.example.com/realms/test/"
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used
- Project uses Central Package Management (no packages.lock.json); use Directory.Packages.props for NuGet cache keys
- AuthServerUrl setter in KeycloakInstallationOptions normalizes "" to "/" — use Uri.TryCreate(UriKind.Absolute) in validators
- DecisionRequirementHandler had wrong nameof() references (ParameterizedProtectedResourceRequirement*) — fixed in new branch
- safeoutputs MCP: use HTTP with Mcp-Session-Id header, Accept: application/json, text/event-stream

## Backlog / Ideas
- #96: UMA Phase 1 (Permission Ticket endpoint) is next actionable PR — IKeycloakProtectionClient.CreatePermissionTicketAsync
- #198: DPoP support — Phase 1 (client-side) actionable, Phase 2 blocked on ecosystem
- #174: Signed JWT client auth — private_key_jwt via Duende recommended
- Consider additive AddAuthorizationServerForWebApp() convenience extension
- RequireResourceRoles missing RequireAuthenticatedUser() unlike sibling methods (intentionally not changed — RequireClaim blocks unauthenticated anyway)

