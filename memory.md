# KeyBot Memory

## Last Run
- Date: 2026-04-04
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/23988925727
- Tasks: Task 9 (Testing Improvements), Task 4 (Engineering Investments), Task 11 (Monthly Summary)

## Monthly Summary Issue
- Issue #220: "[KeyBot] Monthly Activity 2026-03" — CLOSED (superseded)
- New issue created: "[KeyBot] Monthly Activity 2026-04" (aw_apr26)

## PRs Created
- #221 (open, draft): `perf: avoid HashSet allocation in role requirement handlers`
- #223 (open, draft): `fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval` — fixes #104
- #224 (open, draft): `docs: add recipe for connecting containerized API to Keycloak` — closes #115
- #225 (open, draft): `fix: robustify RptRequirementHandler JSON parsing`
- #230 (open, draft): `feat(aspire): add IKeycloakDbAdapter and WithDatabase extension` — closes #113
- (new, draft): `test: add WebApiAuthenticationRegistrationTests` — branch: keybot/test-authentication-registration-20260404

## Merged PRs (previously open)
- #226: `refactor: improve VerificationPlan enumerator and clear logic` — MERGED
- #227: `test: expand KeycloakConfigurationProvider tests` — status unclear (branch exists)
- #228: `fix: correct nameof() in requirement handler log messages` — MERGED (was issue #229)

## Blocked PR / Issues
- #232: `chore: bump outdated NuGet packages` — issue created (not PR) because Directory.Packages.props is protected
  - Branch: keybot/eng-dependency-updates-20260403-c4136f4a18e1123f
  - Bumps: Aspire.Hosting 13.2.1, IdentityModel 8.17.0, OTel 1.15.1, Swashbuckle 10.1.7

## Issues Commented On
- #96: UMA Support (2026-04-03) — phased roadmap, Protection API exists, missing Permission Ticket API
- #104: Deep-dive analysis (2026-03-29) + fix in PR #223
- #113: Aspire DB integration (2026-04-02) — IKeycloakDbAdapter adapter pattern, linked PR #230
- #115: Docker issuer mismatch (2026-03-30) — KC_HOSTNAME, host.docker.internal, ValidIssuers
- #135: Multi-client guidance (2026-03-29) — Keycloak audience mappers, ValidAudiences, multiple schemes
- #198: DPoP support (2026-04-01) — Phase 1 actionable via Duende, Phase 2 blocked on dotnet/aspnetcore#58016

## Technical Notes
- KeycloakUrlRealm includes trailing slash: "https://keycloak.example.com/realms/test/"
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- MetadataAddress in config should NOT have leading slash (use ".well-known/..." not "/.well-known/...")
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used

## Backlog / Ideas
- #96: UMA Permission Ticket API — Phase 1 roadmap identified, good candidate for next Task 3
- #198: DPoP support — Phase 1 (client-side) actionable, Phase 2 blocked on ecosystem
- #174: Signed JWT client auth — awaiting user feedback on use case
- Add WebAppAuthenticationRegistrationTests (similar to new WebApi tests)
- Consider additive AddAuthorizationServerForWebApp() convenience extension
