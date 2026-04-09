# KeyBot Memory

## Last Run
- Date: 2026-04-09
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24216511733
- Tasks: Task 9 (Testing Improvements), Task 6 (Maintain KeyBot PRs), Task 11 (Monthly Summary)

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
- (new): `test: add WebAppAuthenticationRegistrationTests for OIDC/Cookie options` — branch: keybot/test-webapp-authentication-registration-20260409 — 14 tests

## Merged PRs
- #226: `refactor: improve VerificationPlan enumerator and clear logic` — MERGED
- #228: `fix: correct nameof() in requirement handler log messages` — MERGED

## Blocked Issues
- #232: Dependency update issue — Directory.Packages.props is protected; branch keybot/eng-dependency-updates-20260403-c4136f4a18e1123f has the changes

## Issues Commented On
- #96: UMA Support (2026-04-03)
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
- MetadataAddress in config should NOT have leading slash
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used
- ClaimsPrincipal.Clone() does a SHALLOW copy of ClaimsIdentity in practice (same identity reference)
- Project uses Central Package Management (no packages.lock.json); use Directory.Packages.props for NuGet cache keys
- AuthServerUrl setter in KeycloakInstallationOptions normalizes "" → "/" and "  " → "  /", so use Uri.TryCreate(UriKind.Absolute) not IsNullOrWhiteSpace in validators
- safeoutputs MCP tools not available as function calls in Copilot CLI context; use HTTP calls to host.docker.internal:80/mcp/safeoutputs directly

## Backlog / Ideas
- #96: UMA Permission Ticket API — Phase 1 roadmap identified; good candidate for next Task 3
- #198: DPoP support — Phase 1 (client-side) actionable, Phase 2 blocked on ecosystem
- #174: Signed JWT client auth — commented (2026-04-05); private_key_jwt via Duende recommended
- Consider additive AddAuthorizationServerForWebApp() convenience extension
- RequireResourceRoles missing RequireAuthenticatedUser() unlike sibling methods (intentionally not changed)
- Task 6: All 10 open KeyBot branches have no merge conflicts (verified 2026-04-09)
