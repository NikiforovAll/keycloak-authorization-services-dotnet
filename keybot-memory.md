# KeyBot Memory

## Last Run
- Date: 2026-04-18
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24615338196
- Tasks: Task 6 (Maintain PRs — rebased #230 and #224), Task 2 (skipped, no new human activity), Task 11 (Monthly Summary)
- Status: SUCCESS

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN, updated this run

## PRs Open (KeyBot)
- #252: fix: propagate CancellationToken in UmaTokenHandler.CloneRequestAsync - OPEN draft
- #251: improve: refactor KeycloakRolesClaimsTransformation to reduce duplication - OPEN draft
- #250: fix: propagate RequestAborted in UmaAuthorizationMiddlewareResultHandler - OPEN draft
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113 - OPEN draft (rebased this run)
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115 - OPEN draft (rebased this run)

## Recently Merged
- #249: fix: propagate HttpContext.RequestAborted in authorization handlers (2026-04-15)
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync (2026-04-12)
- #241: fix: correct nameof() in DecisionRequirementHandler (2026-04-10)
- #228: fix: correct nameof() in requirement handler log messages (2026-04-02)
- #248: UMA Support (merged)
- #247: 3.0.0 release preparation (merged)
- #245: feat: add client_secret_jwt sample (merged, closes #174)

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
- dotnet csharpier: use `dotnet tool restore` first, then `dotnet csharpier format <file>`
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)
- DefaultHttpContext needs RequestServices with FakeAuthenticationService for GetTokenAsync in tests
- UmaTokenHandler tests: use FakeAuthenticationService (implements IAuthenticationService) to mock GetTokenAsync
- push_to_pull_request_branch: requires message, branch, pull_request_number, force params

## Backlog
- private_key_jwt native support (issue #174 closed with sample - native impl is future)
- DPoP support (issue #198) - Phase 1 (client-side) actionable
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
- Organization-scoped token exchange (issue #196) - tracking Keycloak upstream
