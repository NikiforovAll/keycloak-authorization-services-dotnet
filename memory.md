# KeyBot Memory

## Last Run
- Date: 2026-05-01
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/25237068468
- Tasks: Task 2 (no new human activity on issues), Task 6 (rebased and pushed PRs #224 and #230 onto main), Task 11 (created May issue, closed April #234)
- Status: SUCCESS

## MCP Access Pattern (WORKING)
- Initialize: POST http://host.docker.internal:80/mcp/safeoutputs with Authorization header (no Bearer prefix)
- Get Mcp-Session-Id from response headers
- Call tools: POST with Mcp-Session-Id header, method: tools/call
- GitHub MCP (read-only): POST http://host.docker.internal:80/mcp/github — use issue_read (with method param), list_issues, list_pull_requests, pull_request_read (with method param)
- pull_request_read: uses "pullNumber" param (not pull_request_number)
- issue_read requires: owner, repo, issue_number, method ("get", "get_comments", etc.)
- list_issues: response content[0].text is JSON string; items are dicts; labels is list of strings (NOT list of dicts)
- list_pull_requests: response content[0].text is JSON string; parse normally
- issue_read get_comments: outer is list of objects; first item has 'body' field containing comment text; comments are in nested content[0].text as JSON list
- issue_read get: outer is list; first item has fields including 'content' array; use subprocess curl not urllib (avoids 400 errors)

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - CLOSED (2026-05-01)
- New issue created for May 2026: "[KeyBot] Monthly Activity 2026-05" - number TBD (created via safeoutputs, check next run)

## PRs Open (KeyBot)
- #252: fix: propagate CancellationToken in UmaTokenHandler.CloneRequestAsync - OPEN draft (0 behind main)
- #251: improve: refactor KeycloakRolesClaimsTransformation to reduce duplication - OPEN draft (0 behind main)
- #250: fix: propagate RequestAborted in UmaAuthorizationMiddlewareResultHandler - OPEN draft (0 behind main)
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113 - OPEN draft (REBASED and pushed 2026-05-01)
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115 - OPEN draft (REBASED and pushed 2026-05-01)

## Recently Merged
- #249: fix: propagate HttpContext.RequestAborted in authorization handlers (2026-04-15)
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync (2026-04-12)
- #241: fix: correct nameof() in DecisionRequirementHandler (2026-04-10)
- #228: fix: correct nameof() in requirement handler log messages (2026-04-02)
- #248: UMA Support (merged)
- #247: 3.0.0 release preparation (merged)

## Issues Commented On
- #198: DPoP support (2026-04-01 - no new activity since)
- #196: Organization-scoped token exchange (2026-04-08 - no new activity since)
- #113: WithDatabase for Keycloak Aspire (2026-04-16 - no new activity since; PR #230 open)
- #115: Docker issuer mismatch (2026-03-30 - no new activity since; PR #224 open)
- #242: KeyBot infrastructure failure (do not comment again)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: WORKING via HTTP — initialize session, get Mcp-Session-Id, call tools
- Authorization header format: token from GH_AW_MCP_CONFIG (no Bearer prefix)
- Use subprocess curl for safeoutputs calls (urllib causes 400 errors with multiline bodies)
- dotnet csharpier: use `dotnet tool restore` first, then `dotnet csharpier format <file>`
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)
- push_to_pull_request_branch: branch param = EXACT local branch name (must match remote); requires "message" param
- push_to_pull_request_branch: tool fetches origin/<branch> to compute incremental patch
- PR branches #224 and #230 keep needing rebase every run while main advances
- list_issues labels: returns plain strings NOT dicts (e.g. ['enhancement', 'bug'])
- issue_read get_comments: outer list items have 'body' key with comment text directly
- issue_read get: inner content is nested, parse with json.loads on content[0].text then again
- GitHub MCP list_issues may have caching delay for newly created issues

## Backlog
- DPoP support (issue #198) - Phase 1 (client-side) actionable
- Organization-scoped token exchange (issue #196) - tracking Keycloak upstream
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
- Find issue number for newly created "[KeyBot] Monthly Activity 2026-05" on next run
