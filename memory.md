# KeyBot Memory

## Last Run
- Date: 2026-04-03
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/23964588919
- Tasks: Task 2 (Issue Investigation and Comment), Task 4 (Engineering Investments), Task 11 (Monthly Summary)

## Monthly Summary Issue
- Issue #220: "[KeyBot] Monthly Activity 2026-03" (updated for April 3rd run)

## PRs Created
- #221 (open, draft): `perf: avoid HashSet allocation in role requirement handlers`
- #223 (open, draft): `fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval` — fixes #104
- #224 (open, draft): `docs: add recipe for connecting containerized API to Keycloak` — closes #115
- #225 (open, draft): `fix: robustify RptRequirementHandler JSON parsing`
- #226 (open, draft): `refactor: improve VerificationPlan enumerator and clear logic`
- #227 (open, draft): `test: expand KeycloakConfigurationProvider tests to cover key normalization`
- #230 (open, draft): `feat(aspire): add IKeycloakDbAdapter and WithDatabase extension` — closes #113
- (new, draft): `fix: correct nameof() in requirement handler log messages and add missing XML docs`
  - Branch: keybot/improve-requirement-handler-log-names-20260402
- (new, draft): `chore: bump outdated NuGet packages`
  - Branch: keybot/eng-dependency-updates-20260403
  - Aspire.Hosting 13.1.3→13.2.1, IdentityModel 8.16.0→8.17.0, OpenTelemetry 1.15.0→1.15.1
  - Swashbuckle 10.1.5→10.1.7 (samples), OTel Exporter/Extensions 1.15.0→1.15.1 (samples)

## Issues Commented On
- #96: UMA Support (2026-04-03) — analysis of current state (Protection API exists), missing pieces (Permission Ticket, Entitlement), phased roadmap
- #104: Deep-dive analysis (2026-03-29) + fix in PR #223
- #113: Aspire DB integration (2026-04-02) — IKeycloakDbAdapter adapter pattern, linked PR #230
- #115: Docker issuer mismatch (2026-03-30) — KC_HOSTNAME, host.docker.internal, ValidIssuers
- #135: Multi-client guidance (2026-03-29) — Keycloak audience mappers, ValidAudiences, multiple schemes
- #198: DPoP support (2026-04-01) — Phase 1 actionable via Duende, Phase 2 blocked on dotnet/aspnetcore#58016

## UMA Support (#96) Analysis
- Protection API (IKeycloakProtectionClient): resources + policies CRUD — already implemented
- RPT validation (RptRequirementHandler): already implemented
- MISSING: Permission Ticket API (/authz/protection/permission), Entitlement API, standalone RPT acquisition
- Phase 1: Add CreatePermissionTicketAsync, GetPermissionTicketsAsync, UpdatePermissionTicketAsync, DeletePermissionTicketAsync
- Phase 2: GetRptAsync with caching
- Phase 3: Entitlement API

## Technical Notes
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used for GetTokenAsync
- Docker container issue (#115): Keycloak iss claim derives from request URL in dev mode
- RptRequirementHandler fix: JsonDocument disposal + TryGetProperty for permissions/rsname/scopes
- IKeycloakDbAdapter: adapter for KC_DB, KC_DB_URL, KC_DB_USERNAME, KC_DB_PASSWORD env vars

## Backlog / Ideas
- #96: UMA Permission Ticket API — Phase 1 roadmap identified, good candidate for next Task 3
- #198: DPoP support — Phase 1 (client-side) actionable, Phase 2 blocked on ecosystem
- #174: Signed JWT client auth — awaiting user feedback on use case
- #104: Consider additive AddAuthorizationServerForWebApp() convenience extension
- Aspire: add concrete PostgreSQL adapter package (once #113 PR merged and approach approved)
- Test coverage for ServiceCollectionExtensions (WebApi/WebApp authentication registration)
