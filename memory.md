# KeyBot Memory

## Last Run
- Date: 2026-04-27
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/25023168225
- Tasks: Task 6 (rebased and pushed PRs #224 and #230 onto main — SUCCESS), Task 2 (added enhancement labels to #196 and #198), Task 11 (updated #234 — SUCCESS)
- Status: SUCCESS

## MCP Access Pattern (WORKING)
- Initialize: POST http://host.docker.internal:80/mcp/safeoutputs with Authorization header (no Bearer prefix)
- Get Mcp-Session-Id from response headers
- Call tools: POST with Mcp-Session-Id header, method: tools/call
- GitHub MCP (read-only): POST http://host.docker.internal:80/mcp/github — use issue_read (with method param), list_issues, list_pull_requests, pull_request_read (with method param)
- pull_request_read: uses "pullNumber" param (not pull_request_number)
- issue_read requires: owner, repo, issue_number, method ("get", "get_comments", etc.)
- list_issues: response wraps issues in content[].text as JSON string (parse twice)

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN, updated 2026-04-27

## PRs Open (KeyBot)
- #252: fix: propagate CancellationToken in UmaTokenHandler.CloneRequestAsync - OPEN draft (up to date)
- #251: improve: refactor KeycloakRolesClaimsTransformation to reduce duplication - OPEN draft (up to date)
- #250: fix: propagate RequestAborted in UmaAuthorizationMiddlewareResultHandler - OPEN draft (up to date)
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113 - OPEN draft (REBASED and pushed 2026-04-27)
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115 - OPEN draft (REBASED and pushed 2026-04-27)

## Recently Merged
- #249: fix: propagate HttpContext.RequestAborted in authorization handlers (2026-04-15)
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync (2026-04-12)
- #241: fix: correct nameof() in DecisionRequirementHandler (2026-04-10)
- #228: fix: correct nameof() in requirement handler log messages (2026-04-02)
- #248: UMA Support (merged)
- #247: 3.0.0 release preparation (merged)

## Issues Commented On
- #198: DPoP support (2026-04-01 - no new activity; enhancement label added 2026-04-27)
- #196: Organization-scoped token exchange (2026-04-08 - no new activity; enhancement label added 2026-04-27)
- #113: WithDatabase for Keycloak Aspire (2026-04-16 - no new activity; PR #230 rebased 2026-04-27)
- #115: Docker issuer mismatch (2026-03-30 - no new activity; PR #224 rebased 2026-04-27)
- #242: KeyBot infrastructure failure (do not comment again)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: WORKING via HTTP — initialize session, get Mcp-Session-Id, call tools
- Authorization header format: token from GH_AW_MCP_CONFIG (no Bearer prefix)
- dotnet csharpier: use `dotnet tool restore` first, then `dotnet csharpier format <file>`
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)
- push_to_pull_request_branch: branch param = EXACT local branch name; requires "message" param
- push_to_pull_request_branch: tool fetches origin/<branch> to compute incremental patch
- PR branches #224 and #230 keep rebasing 20 commits behind — need rebase every run while main advances
- list_issues response: content[].text is a JSON string — parse twice; issues and PRs mixed (filter by absence of 'pull_request' key)

## Backlog
- DPoP support (issue #198) - Phase 1 (client-side) actionable
- Organization-scoped token exchange (issue #196) - tracking Keycloak upstream
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
- private_key_jwt client auth support (issue #174)
