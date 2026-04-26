# KeyBot Memory

## Last Run
- Date: 2026-04-26
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24968677786
- Tasks: Task 6 (rebased and pushed PRs #224 and #230 onto main — SUCCESS), Task 2 (no new issues), Task 11 (updated #234 — SUCCESS)
- Status: SUCCESS — safeoutputs MCP accessible via HTTP at http://host.docker.internal:80/mcp/safeoutputs

## MCP Access Pattern (WORKING)
- Initialize: POST http://host.docker.internal:80/mcp/safeoutputs with Authorization header (no Bearer prefix)
- Get Mcp-Session-Id from response headers
- Call tools: POST with Mcp-Session-Id header, method: tools/call
- GitHub MCP (read-only): POST http://host.docker.internal:80/mcp/github — use issue_read (with method param), list_issues, list_pull_requests, pull_request_read (with method param)
- issue_read requires: owner, repo, issue_number, method ("get", "get_comments", etc.)

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN, updated 2026-04-26

## PRs Open (KeyBot)
- #252: fix: propagate CancellationToken in UmaTokenHandler.CloneRequestAsync - OPEN draft (up to date)
- #251: improve: refactor KeycloakRolesClaimsTransformation to reduce duplication - OPEN draft (up to date)
- #250: fix: propagate RequestAborted in UmaAuthorizationMiddlewareResultHandler - OPEN draft (up to date)
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113 - OPEN draft (REBASED and pushed 2026-04-26)
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115 - OPEN draft (REBASED and pushed 2026-04-26)

## Recently Merged
- #249: fix: propagate HttpContext.RequestAborted in authorization handlers (2026-04-15)
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync (2026-04-12)
- #241: fix: correct nameof() in DecisionRequirementHandler (2026-04-10)
- #228: fix: correct nameof() in requirement handler log messages (2026-04-02)
- #248: UMA Support (merged)
- #247: 3.0.0 release preparation (merged)
- #245: feat: add client_secret_jwt sample (merged, closes #174)

## Issues Commented On
- #198: DPoP support (2026-04-01 - no new activity since; enhancement label added 2026-04-25)
- #196: Organization-scoped token exchange (2026-04-08 - no new activity since; enhancement label added 2026-04-25)
- #113: WithDatabase for Keycloak Aspire (2026-04-16 - no new activity since; PR #230 rebased 2026-04-26)
- #115: Docker issuer mismatch (2026-03-30 - no new activity since; PR #224 rebased 2026-04-26)
- #242: KeyBot infrastructure failure (automated issue, do not comment again)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: WORKING via HTTP — initialize session, get Mcp-Session-Id, call tools
- Authorization header format: "bJ0AJ..." (no Bearer prefix) — NOTE: token changes each run, use GH_AW_MCP_CONFIG
- dotnet csharpier: use `dotnet tool restore` first, then `dotnet csharpier format <file>`
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)
- push_to_pull_request_branch: branch param = EXACT local branch name that tracks remote PR branch; requires "message" param
- push_to_pull_request_branch: tool fetches origin/<branch> to compute incremental patch — local branch MUST track correct remote
- push_to_pull_request_branch: rename local branch to match remote PR branch name before pushing (not -rebased suffix)
- issue_read tool: requires "method" param ("get", "get_comments", "get_sub_issues", "get_labels")
- GitHub MCP issue/PR data: content wrapped in array with 'content' and 'isError' fields

## Backlog
- DPoP support (issue #198) - Phase 1 (client-side) actionable
- Organization-scoped token exchange (issue #196) - tracking Keycloak upstream
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
