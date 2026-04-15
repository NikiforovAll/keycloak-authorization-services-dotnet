# KeyBot Memory

## Last Run
- Date: 2026-04-15
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24481756062
- Tasks: Task 2 (Issue Comment), Task 3 (Issue Investigation and Fix), Task 11 (Monthly Summary)
- Status: SUCCESS

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN, updated this run

## PRs Open (KeyBot)
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115 - OPEN draft
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113 - OPEN draft
- NEW (this run): fix: propagate RequestAborted in UmaAuthorizationMiddlewareResultHandler (draft)
  - Branch: keybot/fix-uma-cancellation-token-20260415
  - Changes: UmaAuthorizationMiddlewareResultHandler - pass context.RequestAborted to protection client calls; re-throw OperationCanceledException
  - Tests: 147 Authorization tests pass, 75 SDK tests pass

## Recently Merged
- #249: fix: propagate HttpContext.RequestAborted in authorization handlers (2026-04-15)
- #248: UMA Support (merged) - closes #96
- #247: 3.0.0 release preparation (merged) - major breaking changes
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync (merged)
- Current main: 1 commit past tag 3.0.0-rc.1

## Issues Commented On
- #96: UMA Support (multiple times — now CLOSED by #248)
- #104: Deep-dive + fix PR #223 (MERGED)
- #113: Aspire DB integration (2026-04-02)
- #115: Docker issuer mismatch (2026-03-30)
- #135: Multi-client guidance (2026-03-29)
- #174: Signed JWT client auth (2026-04-05, 2026-04-12 - IClientAssertionService workaround documented)
- #196: Organization-scoped token exchange (2026-04-08)
- #198: DPoP support (2026-04-01)
- #242: KeyBot infrastructure failure (2026-04-14, 2026-04-15 — noted as resolved)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: direct HTTP POST to http://host.docker.internal:80/mcp/safeoutputs
- safeoutputs MCP: must POST initialize first to get Mcp-Session-Id
- dotnet csharpier: use `dotnet csharpier format <file>` (not positional args)
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces

## Backlog
- private_key_jwt support (issue #174)
- Testing improvements for UMA authorization handlers (add cancellation token tests — partially done this run)
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
