# KeyBot Memory

## Last Run
- Date: 2026-04-13
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24370283905
- Tasks: Task 3 (Issue Fix), Task 2 (Issue Comment), Task 11 (Monthly Summary)
- Status: INFRASTRUCTURE FAILURE — safeoutputs MCP blocked by 401 on MCP registry policy API; _temp path read-only; NO GitHub writes succeeded. See issue #242.

## Previous Run
- Date: 2026-04-12
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24317938303
- Status: All GitHub writes completed successfully

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN

## PRs Open (KeyBot)
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113

## Recently Merged (since last memory update)
- #221, #223, #233, #235, #237, #238, #239, #240: all merged to main
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync - MERGED
- #225, #226, #228, #241: previously noted as merged

## Pending Fix (NEEDS NEXT RUN - NOT PUSHED)
Branch `keybot/fix-cancellation-token-propagation-20260413` was created locally but NOT pushed (no git credentials).
**MUST re-implement this fix on next working run:**
- File 1: `src/Keycloak.AuthServices.Authorization/Requirements/DecisionRequirement.cs`
  - In `HandleRequirementAsync` (around line 132): change the cancellation token arg passed to `ProtectedResourceVerifier.Verify()` from `CancellationToken.None` to `httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None`
- File 2: `src/Keycloak.AuthServices.Authorization/Requirements/ParameterizedProtectedResourceRequirement.cs`
  - In foreach loop (around line 95): same change
- PR Title: `fix: propagate HttpContext.RequestAborted in authorization handlers`
- Root cause: Both handlers had `CancellationToken.None` hardcoded, preventing request cancellation from stopping in-flight RPT/permission calls

## Issues Commented On
- #96: UMA Support (2026-04-03, 2026-04-10)
- #104: Deep-dive + fix PR #223 (MERGED)
- #113: Aspire DB integration (2026-04-02)
- #115: Docker issuer mismatch (2026-03-30)
- #135: Multi-client guidance (2026-03-29)
- #174: Signed JWT client auth (2026-04-05, 2026-04-12 - IClientAssertionService workaround documented)
- #196: Organization-scoped token exchange (2026-04-08)
- #198: DPoP support (2026-04-01)
- #242: KeyBot infrastructure failure - NOT YET COMMENTED (add acknowledgement comment on next run)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- RptRequirementHandler: GetProperty() throws on missing fields; fixed with TryGetProperty
- safeoutputs MCP: must POST init first to get Mcp-Session-Id, then POST with session header; simple POST without init returns 400
- safeoutputs MCP: BLOCKED by MCP registry policy 401 in runs 24317938303-adjacent and 24370283905; _temp path is ro bind-mount
- dotnet csharpier: target specific files (e.g., `dotnet csharpier format file.cs`) to avoid reformatting all 777 files
- dotnet cake Test: only runs Authorization.Tests; SDK tests need `dotnet test tests/Keycloak.AuthServices.Sdk.Tests` separately

## Backlog
- Re-implement & push CancellationToken fix (see Pending Fix above) — HIGH PRIORITY
- Comment on issue #242 acknowledging infrastructure failure
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync
- private_key_jwt support (issue #174)
