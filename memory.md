# KeyBot Memory

## Last Run
- Date: 2026-04-12
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24317938303
- Tasks: Task 3 (Issue Fix), Task 2 (Issue Comment), Task 11 (Monthly Summary)
- Status: All GitHub writes completed successfully via MCP safeoutputs (direct HTTP with session init)

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN

## PRs Created (All Open/Draft)
- #221: perf: avoid HashSet allocation in role requirement handlers
- #223: fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval - fixes #104
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113
- #233: test: add WebApiAuthenticationRegistrationTests
- #235: feat: add AdditionalAudiences to KeycloakAuthenticationOptions - partially closes #135
- #237: docs: improve XML documentation in PoliciesBuilderExtensions
- #238: perf: single-pass org claims scan and avoid ToArray in VerificationPlan
- #239: eng: add startup validators for Keycloak SDK client options
- #240: test: add WebAppAuthenticationRegistrationTests for OIDC/Cookie options
- (new): fix: handle non-JSON error bodies in EnsureResponseAsync (branch keybot/fix-ensure-response-non-json-error-20260412-7306648)

## Merged PRs
- #225: fix: robustify RptRequirementHandler JSON parsing - MERGED 2026-04-12
- #226: refactor: improve VerificationPlan enumerator and clear logic - MERGED
- #228: fix: correct nameof() in requirement handler log messages - MERGED
- #241: fix: correct nameof() references in DecisionRequirementHandler - MERGED 2026-04-12

## Pending Local Work (Not Pushed)
- Branch keybot/fix-rpt-requirement-json-parsing-20260411 ready to push (may be superseded by merged #225)

## Issues Commented On
- #96: UMA Support (2026-04-03, 2026-04-10)
- #104: Deep-dive + fix PR #223
- #113: Aspire DB integration (2026-04-02)
- #115: Docker issuer mismatch (2026-03-30)
- #135: Multi-client guidance (2026-03-29)
- #174: Signed JWT client auth (2026-04-05, 2026-04-12 - IClientAssertionService workaround documented)
- #196: Organization-scoped token exchange (2026-04-08)
- #198: DPoP support (2026-04-01)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- RptRequirementHandler: GetProperty() throws on missing fields; fixed with TryGetProperty
- safeoutputs MCP: must POST init first to get Mcp-Session-Id, then POST with session header; simple POST without init returns 400
- dotnet csharpier: target specific files (e.g., `dotnet csharpier format file.cs`) to avoid reformatting all 777 files
- dotnet cake Test: only runs Authorization.Tests; SDK tests need `dotnet test tests/Keycloak.AuthServices.Sdk.Tests` separately

## Backlog
- Check if keybot/fix-rpt-requirement-json-parsing-20260411 is now superseded by merged #225; drop if so
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync
- client_secret_jwt native support (issue #174 - priority 1)
- private_key_jwt support (issue #174 - priority 2)
