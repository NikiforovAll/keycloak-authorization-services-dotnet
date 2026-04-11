# KeyBot Memory

## Last Run
- Date: 2026-04-11
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24293035338
- Tasks: Task 2 (Issue Comment), Task 3 (Issue Fix), Task 11 (Monthly Summary)
- Status: MCP servers blocked by policy (bad credentials for MCP registry check); GitHub writes not possible

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN

## PRs Created (All Open/Draft)
- #221: perf: avoid HashSet allocation in role requirement handlers
- #223: fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval - fixes #104
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115
- #225: fix: robustify RptRequirementHandler JSON parsing - superseded by new branch
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113
- #233: test: add WebApiAuthenticationRegistrationTests
- #235: feat: add AdditionalAudiences to KeycloakAuthenticationOptions - partially closes #135
- #237: docs: improve XML documentation in PoliciesBuilderExtensions
- #238: perf: single-pass org claims scan and avoid ToArray in VerificationPlan
- #239: eng: add startup validators for Keycloak SDK client options
- #240: test: add WebAppAuthenticationRegistrationTests for OIDC/Cookie options
- #241: fix: correct nameof() references in DecisionRequirementHandler

## Merged PRs
- #226: refactor: improve VerificationPlan enumerator and clear logic - MERGED
- #228: fix: correct nameof() in requirement handler log messages - MERGED

## Pending Local Work (Not Pushed)
- Branch keybot/fix-rpt-requirement-json-parsing-20260411 ready to push
  - Uses TryGetProperty instead of GetProperty in RptRequirementHandler
  - Adds using for JsonDocument disposal
  - 9 new unit tests, all 120 tests pass

## Drafted Issue Comment (Not Posted)
- Issue #174: Signed JWT client auth workaround using Duende IClientAssertionService

## Issues Commented On
- #96: UMA Support (2026-04-03, 2026-04-10)
- #104: Deep-dive + fix PR #223
- #113: Aspire DB integration (2026-04-02)
- #115: Docker issuer mismatch (2026-03-30)
- #135: Multi-client guidance (2026-03-29)
- #174: Signed JWT client auth (2026-04-05) - update drafted for next run
- #196: Organization-scoped token exchange (2026-04-08)
- #198: DPoP support (2026-04-01)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- RptRequirementHandler: GetProperty() throws on missing fields; fixed with TryGetProperty
- safeoutputs MCP blocked in run 24293035338 due to MCP registry policy check failure (401)

## Backlog
- Push local fix branch keybot/fix-rpt-requirement-json-parsing-20260411 next run
- Post drafted comment for issue #174 next run
- Update monthly summary issue #234 next run
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync
