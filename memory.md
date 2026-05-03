# KeyBot Memory

## Last Run
- Date: 2026-05-03
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/25270793228
- Tasks: Task 4 (NuGet package updates PR), Task 2 (no new activity on issues), Task 11 (created May issue)
- Status: SUCCESS

## Monthly Summary Issue
- Issue #234: "[KeyBot] Monthly Activity 2026-04" - CLOSED
- "[KeyBot] Monthly Activity 2026-05" - OPEN (created this run, issue number TBD)

## PRs Open (KeyBot)
- (new, this run): chore: update NuGet packages (May 2026) - branch keybot/eng-nuget-updates-20260503 - OPEN draft

## Recently Merged
- #249: fix: propagate HttpContext.RequestAborted in authorization handlers (2026-04-15)
- #244: fix: handle non-JSON error bodies in EnsureResponseAsync (2026-04-12)
- #252, #251, #250, #230, #224: unknown status (were open last run)

## Issues Commented On
- #198: DPoP support (2026-04-01 - no new activity since)
- #196: Organization-scoped token exchange (2026-04-08 - no new activity since)
- #242: KeyBot infrastructure failure (do not comment again)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: WORKING via tool calls (tools available directly in this environment)
- dotnet csharpier: use `dotnet tool restore` first, then `dotnet csharpier format <file>`
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)
- samples/Directory.Packages.props overrides src package versions — must update both files
- Package versions: src/Directory.Packages.props + tests/Directory.Packages.props + samples/Directory.Packages.props

## Package Versions (as of this run)
- Aspire.Hosting: 13.2.4
- Microsoft.Extensions.*/Microsoft.AspNetCore.*: 10.0.7
- Microsoft.IdentityModel.Protocols.OpenIdConnect: 8.18.0
- System.IdentityModel.Tokens.Jwt: 8.18.0
- OpenTelemetry: 1.15.3
- Microsoft.Extensions.Caching.Hybrid: 10.5.0
- Microsoft.Extensions.Http.Resilience: 10.5.0
- Microsoft.Kiota.Bundle: 1.22.1
- Microsoft.NET.Test.Sdk: 18.5.1
- coverlet.collector: 10.0.0

## Backlog
- DPoP support (issue #198) - Phase 1 (client-side) actionable
- Organization-scoped token exchange (issue #196) - tracking Keycloak upstream
- UMA Phase 1: IKeycloakProtectionClient.CreatePermissionTicketAsync convenience helpers
- Find issue number for "[KeyBot] Monthly Activity 2026-05" on next run
