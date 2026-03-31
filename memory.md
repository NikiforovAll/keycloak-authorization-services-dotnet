# KeyBot Memory

## Last Run
- Date: 2026-03-31
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/23822437481
- Tasks: Task 3 (Fix RptRequirementHandler), Task 5 (VerificationPlan refactor), Task 11 (Monthly Summary)

## Monthly Summary Issue
- 2026-03: Issue #220 "[KeyBot] Monthly Activity 2026-03" (updated)

## PRs Created
- #221 (open, draft): `perf: avoid HashSet allocation in role requirement handlers`
- #223 (open, draft): `fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval` — fixes #104
- #224 (open, draft): `docs: add recipe for connecting containerized API to Keycloak` — closes #115
- New (pending): `fix: robustify RptRequirementHandler JSON parsing` — disposes JsonDocument, uses TryGetProperty, 9 tests
- New (pending): `refactor: improve VerificationPlan enumerator and clear logic` — yield return, Count check, Clear()

## Issues Commented On
- #104: Deep-dive analysis (2026-03-29) + fix in PR #223
- #135: Multi-client guidance (2026-03-29) — Keycloak audience mappers, ValidAudiences, multiple schemes
- #115: Docker issuer mismatch (2026-03-30) — KC_HOSTNAME, host.docker.internal, ValidIssuers solutions

## Technical Notes
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used for GetTokenAsync
- Docker container issue (#115): Keycloak iss claim derives from request URL in dev mode
- RptRequirementHandler fix: JsonDocument disposal + TryGetProperty for permissions/rsname/scopes
- VerificationPlan: yield return in GetEnumerator(), Count==0 for emptiness, Clear() instead of RemoveAll

## Backlog / Ideas
- #96: UMA Support — complex, needs investigation
- #198: DPoP support — blocked on ASP.NET Core ecosystem (dotnet/aspnetcore#58016)
- #113: Aspire DB integration — design discussion open, no contributor yet
- #174: Signed JWT client auth — awaiting user feedback on use case
- #104: Consider additive `AddAuthorizationServerForWebApp()` convenience extension
