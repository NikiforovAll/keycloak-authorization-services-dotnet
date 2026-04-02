# KeyBot Memory

## Last Run
- Date: 2026-04-02
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/23924973388
- Tasks: Task 5 (Coding Improvements), Task 3 (Issue Investigation and Fix), Task 11 (Monthly Summary)

## Monthly Summary Issue
- 2026-03/04: Issue #220 "[KeyBot] Monthly Activity 2026-03" (updated for April 2nd run)

## PRs Created
- #221 (open, draft): `perf: avoid HashSet allocation in role requirement handlers`
- #223 (open, draft): `fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval` — fixes #104
- #224 (open, draft): `docs: add recipe for connecting containerized API to Keycloak` — closes #115
- #225 (open, draft): `fix: robustify RptRequirementHandler JSON parsing` — disposes JsonDocument, TryGetProperty, 9 tests
- #226 (open, draft): `refactor: improve VerificationPlan enumerator and clear logic` — yield return, Count check, Clear()
- #227 (open, draft): `test: expand KeycloakConfigurationProvider tests to cover key normalization` — 5 tests
- New (draft): `fix: correct nameof() in requirement handler log messages and add missing XML docs`
  - Branch: keybot/improve-requirement-handler-log-names-20260402
  - Fixed: RealmAccessRequirementHandler used ParameterizedProtectedResourceRequirementHandler in logs
  - Fixed: ResourceAccessRequirementHandler used RealmAccessRequirement in metrics
  - Added XML docs to 3 requirement handler classes
- New (draft): `feat(aspire): add IKeycloakDbAdapter and WithDatabase extension` — closes #113
  - Branch: keybot/fix-issue-113-keycloak-db-adapter-20260402
  - IKeycloakDbAdapter interface with DbVendor, GetDbUrlAsync, GetDbUsernameAsync, GetDbPasswordAsync
  - WithDatabase(IKeycloakDbAdapter) extension on KeycloakBuilderExtensions

## Issues Commented On
- #104: Deep-dive analysis (2026-03-29) + fix in PR #223
- #113: Aspire DB integration (2026-04-02) — IKeycloakDbAdapter adapter pattern, linked PR
- #115: Docker issuer mismatch (2026-03-30) — KC_HOSTNAME, host.docker.internal, ValidIssuers
- #135: Multi-client guidance (2026-03-29) — Keycloak audience mappers, ValidAudiences, multiple schemes
- #198: DPoP support (2026-04-01) — Phase 1 actionable via Duende, Phase 2 blocked on dotnet/aspnetcore#58016

## Technical Notes
- Clone() in .NET creates shallow copy: original and clone SHARE identity objects — not a deep copy
  (verified empirically: ReferenceEquals(principal.Identity, clone.Identity) == true)
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used for GetTokenAsync
- Docker container issue (#115): Keycloak iss claim derives from request URL in dev mode
- RptRequirementHandler fix: JsonDocument disposal + TryGetProperty for permissions/rsname/scopes
- VerificationPlan: yield return in GetEnumerator(), Count==0 for emptiness, Clear() instead of RemoveAll
- KeycloakConfigurationProvider: NormalizeKey transforms kebab-case JSON to PascalCase config keys
- IKeycloakDbAdapter: adapter for KC_DB, KC_DB_URL, KC_DB_USERNAME, KC_DB_PASSWORD env vars

## Backlog / Ideas
- #96: UMA Support — complex, needs investigation
- #198: DPoP support — Phase 1 (client-side) actionable, Phase 2 blocked on ecosystem
- #174: Signed JWT client auth — awaiting user feedback on use case
- #104: Consider additive AddAuthorizationServerForWebApp() convenience extension
- Aspire: add concrete PostgreSQL adapter package (once #113 PR merged and approach approved)
- Test coverage for ServiceCollectionExtensions (WebApi/WebApp authentication registration)
