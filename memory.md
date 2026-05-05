# KeyBot Memory

## Last Run
- Date: 2026-05-05
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/25358570202
- Tasks: Task 10 (UmaTicketExchangeClient tests PR), Task 3 (no fixable issues found), Task 11 (updated May summary)
- Status: SUCCESS

## Monthly Summary Issue
- Issue #255: "[KeyBot] Monthly Activity 2026-05" - OPEN, updated this run

## PRs Open (KeyBot)
- (this run): test: add unit tests for UmaTicketExchangeClient — branch keybot/test-uma-ticket-exchange-client-20260505 — OPEN draft
- #254: chore: update NuGet packages (May 2026) — OPEN

## Recently Merged / Closed
- #252, #251, #250, #230, #224: status unknown (need to check)
- #249: fix: propagate HttpContext.RequestAborted (2026-04-15)
- #248: UMA Support (merged)
- #247: 3.0.0 release preparation (merged)

## Issues Commented On
- #198: DPoP support (2026-04-01 - no new activity since)
- #196: Organization-scoped token exchange (2026-04-08 - no new activity since)
- #242: KeyBot infrastructure failure (do not comment again)

## Technical Notes
- KeycloakUrlRealm includes trailing slash
- JwtBearerOptions.Authority = KeycloakUrlRealm (with trailing slash)
- safeoutputs MCP: WORKING via tool calls
- dotnet csharpier: use `dotnet tool restore` first, then `dotnet csharpier format <file>`
- dotnet cake Test: only runs Authorization.Tests; SDK tests need separate dotnet test call
- 3.0.0 breaking changes: removed AddKeycloakAuthentication, moved extension namespaces
- ClaimsPrincipal.Clone() in .NET shares ClaimsIdentity references (not deep copy)
- samples/Directory.Packages.props overrides src package versions — must update both files
- Package versions: src/Directory.Packages.props + tests/Directory.Packages.props + samples/Directory.Packages.props

## Package Versions (as of 2026-05-03 run)
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
- Add tests for KeycloakTokenIntrospectionClient (no tests for the HTTP client itself, only transformation)
