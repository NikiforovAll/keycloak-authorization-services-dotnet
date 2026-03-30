# KeyBot Memory

## Last Run
- Date: 2026-03-30
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/23770866377
- Tasks: Task 2 (Issue Comment on #115), Task 3 (Docs PR for #115), Task 11 (Monthly Summary)

## Monthly Summary Issue
- 2026-03: Issue #220 "[KeyBot] Monthly Activity 2026-03" (updated)

## PRs Created
- #221 (open, draft): `perf: avoid HashSet allocation in role requirement handlers`
- #223 (open, draft): `fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval` — fixes #104
- New (pending): `docs: add recipe for connecting containerized API to Keycloak` — closes #115

## Issues Commented On
- #104: Deep-dive analysis (2026-03-29) + fix in PR #223
- #135: Multi-client guidance (2026-03-29) — Keycloak audience mappers, ValidAudiences, multiple schemes
- #115: Docker issuer mismatch (2026-03-30) — KC_HOSTNAME, host.docker.internal, ValidIssuers solutions

## Technical Notes
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used for GetTokenAsync. Falls back to SourceAuthenticationScheme when null.
- Docker container issue (#115): Keycloak iss claim derives from request URL in dev mode. Fix via KC_HOSTNAME env var or explicit ValidIssuer/ValidIssuers config.

## Backlog / Ideas
- #96: UMA Support — complex, needs investigation
- #198: DPoP support — blocked on ASP.NET Core ecosystem (dotnet/aspnetcore#58016)
- #113: Aspire DB integration — design discussion open, no contributor yet
- #104: Consider additive `AddAuthorizationServerForWebApp()` convenience extension
- Possible perf: VerificationPlan.GetEnumerator() allocates List per iteration
- Possible perf: KeycloakOrganizationClaimsExtensions.GetOrganizations() double-LINQ
