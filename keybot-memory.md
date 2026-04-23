# KeyBot Memory

## Last Run
- Date: 2026-04-23
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/24862136711
- Tasks: Task 6 (rebased PRs #224 and #230 onto main locally — blocked from pushing by infra), Task 2 (no new issues), Task 11 (blocked by infra)
- Status: INFRASTRUCTURE FAILURE — safeoutputs MCP not available (--disable-builtin-mcps set in CLI; GH_AW_MCP_CONFIG empty)

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - OPEN, last comment 2026-04-16; needs update for runs 2026-04-17 (#252 created), 2026-04-22 (blocked), 2026-04-23 (this run, blocked)

## PRs Open (KeyBot)
- #252: fix: propagate CancellationToken in UmaTokenHandler.CloneRequestAsync - OPEN draft
- #251: improve: refactor KeycloakRolesClaimsTransformation to reduce duplication - OPEN draft
- #250: fix: propagate RequestAborted in UmaAuthorizationMiddlewareResultHandler - OPEN draft
- #230: feat(aspire): add IKeycloakDbAdapter and WithDatabase extension - closes #113 - OPEN draft
- #224: docs: add recipe for connecting containerized API to Keycloak - closes #115 - OPEN draft

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
- Update monthly summary #234 with runs 2026-04-17 (#252 created), 2026-04-22 (infra fail), 2026-04-23 (rebased #224 and #230, infra fail)
- Push rebased PRs #224 and #230 (rebases done locally, verified build OK; need push_to_pull_request_branch)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: BROKEN since 2026-04-22 — CLI started with --disable-builtin-mcps AND GH_AW_MCP_CONFIG is empty; safeoutputs tools not available as tool calls
- safeoutputs MCP: mcp-scripts server runs on localhost:3000 but only has keycloak_docs tool
- safeoutputs MCP: tools write to /home/runner/work/_temp/gh-aw/safeoutputs/outputs.jsonl (read-only for main agent)
- safeoutputs MCP: patch files go to /tmp/gh-aw/aw-{sanitized-branch-name}.patch
- dotnet csharpier: use `dotnet tool restore` first, then `dotnet csharpier format <file>`
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)
- push_to_pull_request_branch: branch param = full local branch name, pull_request_number = PR number

## Backlog
- DPoP support (issue #198) - Phase 1 (client-side) actionable
- Organization-scoped token exchange (issue #196) - tracking Keycloak upstream
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
