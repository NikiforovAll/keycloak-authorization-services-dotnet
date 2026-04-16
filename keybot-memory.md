# KeyBot Memory

## Last Run
- Date: 2026-04-16
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24537350549
- Tasks: Task 2 (Issue Comment), Task 5 (Coding Improvements), Task 11 (Monthly Summary)
- Status: SUCCESS

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN, updated this run

## PRs Open (KeyBot)
- #250: fix: propagate RequestAborted in UmaAuthorizationMiddlewareResultHandler - OPEN draft
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113 - OPEN draft
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115 - OPEN draft
- NEW (this run): improve: refactor KeycloakRolesClaimsTransformation to reduce duplication and use O(1) dedup
  - Branch: keybot/improve-roles-transformation-dedup-20260416
  - Changes: extract AddRolesFromJsonElement helper, replace FirstOrDefault dedup with HashSet

## Recently Merged
- #249: fix: propagate HttpContext.RequestAborted in authorization handlers (2026-04-15)
- #248: UMA Support (merged)
- #247: 3.0.0 release preparation (merged) - major breaking changes
- #245: feat: add client_secret_jwt sample (merged, closes #174)
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync (merged)
- Current main: 1 commit past tag 3.0.0-rc.1

## Issues Commented On
- #96: UMA Support (multiple times — now CLOSED by #248)
- #104: Deep-dive + fix PR #223 (MERGED)
- #113: WithDatabase for Keycloak Aspire (2026-04-16 - status update pointing to PR #230)
- #115: Docker issuer mismatch (2026-03-30)
- #135: Multi-client guidance (2026-03-29)
- #174: Signed JWT client auth (2026-04-05, 2026-04-12, 2026-04-15 - now CLOSED, PR #245 merged)
- #196: Organization-scoped token exchange (2026-04-08)
- #198: DPoP support (2026-04-01)
- #242: KeyBot infrastructure failure (2026-04-14, 2026-04-15 — noted as resolved)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: direct HTTP POST to http://host.docker.internal:80/mcp/safeoutputs
- safeoutputs MCP: must POST initialize first to get Mcp-Session-Id
- safeoutputs MCP: auth token in /home/runner/.copilot/mcp-config.json
- dotnet csharpier: use `dotnet csharpier format <file>` (not positional args)
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)

## Backlog
- private_key_jwt native support (issue #174 closed with sample - native impl is future)
- DPoP support (issue #198)
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
- Organization-scoped token exchange (issue #196) - tracking Keycloak upstream
