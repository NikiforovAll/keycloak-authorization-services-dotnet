# KeyBot Memory

## Last Run
- Date: 2026-04-24
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24914773883
- Tasks: Task 6 (rebased PRs #224 and #230 onto main; build OK), Task 2 (no new issues to comment), Task 11 (blocked by infra)
- Status: INFRASTRUCTURE FAILURE — safeoutputs, github, mcpscripts MCP all blocked by policy (4th consecutive run with this issue)

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN
- Needs update for runs: 2026-04-17 (#252 created), 2026-04-22 (infra fail), 2026-04-23 (infra fail), 2026-04-24 (infra fail, rebased #224 and #230)

## PRs Open (KeyBot)
- #252: fix: propagate CancellationToken in UmaTokenHandler.CloneRequestAsync - OPEN draft (ahead=1, behind=0, no action needed)
- #251: improve: refactor KeycloakRolesClaimsTransformation to reduce duplication - OPEN draft (ahead=1, behind=0, no action needed)
- #250: fix: propagate RequestAborted in UmaAuthorizationMiddlewareResultHandler - OPEN draft (ahead=1, behind=0, no action needed)
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113 - OPEN draft (REBASED locally this run onto main; branch=fix-pr-230-rebase, needs push)
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115 - OPEN draft (REBASED locally this run onto main; branch=fix-pr-224-rebase, needs push)

## Recently Merged
- #249: fix: propagate HttpContext.RequestAborted in authorization handlers (2026-04-15)
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync (2026-04-12)
- #241: fix: correct nameof() in DecisionRequirementHandler (2026-04-10)
- #228: fix: correct nameof() in requirement handler log messages (2026-04-02)
- #248: UMA Support (merged)
- #247: 3.0.0 release preparation (merged)
- #245: feat: add client_secret_jwt sample (merged, closes #174)

## Issues Commented On
- #198: DPoP support (2026-04-01 - no new activity since)
- #196: Organization-scoped token exchange (2026-04-08 - no new activity since)
- #113: WithDatabase for Keycloak Aspire (2026-04-16 - no new activity since; PR #230 open)
- #115: Docker issuer mismatch (2026-03-30 - no new activity since; PR #224 open)
- #242: KeyBot infrastructure failure (automated issue, do not comment again)

## Pending (blocked by infra)
- Add `enhancement` label to #196 and #198 (both currently only have `Renovation2026`)
- Update monthly summary #234 with runs 2026-04-17 (#252 created), 2026-04-22 (infra fail), 2026-04-23 (infra fail), 2026-04-24 (this run, rebased #224 and #230)
- Push rebased PRs #224 (branch: fix-pr-224-rebase) and #230 (branch: fix-pr-230-rebase) — rebases done locally, build verified OK; need push_to_pull_request_branch

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: BROKEN since 2026-04-22 — MCP servers blocked by policy ('github', 'mcpscripts', 'safeoutputs' all blocked)
- dotnet csharpier: use `dotnet tool restore` first, then `dotnet csharpier format <file>`
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)
- push_to_pull_request_branch: branch param = full local branch name, pull_request_number = PR number

## Backlog
- DPoP support (issue #198) - Phase 1 (client-side) actionable
- Organization-scoped token exchange (issue #196) - tracking Keycloak upstream
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
