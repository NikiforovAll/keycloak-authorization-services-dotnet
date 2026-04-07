# KeyBot Memory

## Last Run
- Date: 2026-04-07
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24107564096
- Tasks: Task 8 (Performance), Task 3 (Issue Investigation & Fix - no new fix found), Task 11 (Monthly Summary)

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
- (new, draft): `perf: single-pass org claims scan and avoid ToArray in VerificationPlan` — branch: keybot/perf-single-pass-org-claims-verificationplan-20260407

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
- #198: DPoP support (2026-04-01)

## Technical Notes
- KeycloakUrlRealm includes trailing slash: "https://keycloak.example.com/realms/test/"
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- MetadataAddress in config should NOT have leading slash
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used
- ClaimsPrincipal.Clone() does a SHALLOW copy of ClaimsIdentity in practice (same identity reference)
- Project uses Central Package Management (no packages.lock.json); use Directory.Packages.props for NuGet cache keys
- PR #226 (MERGED): improved VerificationPlan enumerator with yield return; my new PR further removes ToArray() via string[] storage

## Backlog / Ideas
- #96: UMA Permission Ticket API — Phase 1 roadmap identified; good candidate for next Task 3
- #198: DPoP support — Phase 1 (client-side) actionable, Phase 2 blocked on ecosystem
- #174: Signed JWT client auth — commented (2026-04-05); private_key_jwt via Duende recommended
- Add WebAppAuthenticationRegistrationTests (complement to PR #233)
- Consider additive AddAuthorizationServerForWebApp() convenience extension
- RequireResourceRoles missing RequireAuthenticatedUser() unlike sibling methods (intentionally not changed)
