# KeyBot Memory

## Last Run
- Date: 2026-03-29
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/23720606447
- Tasks: Task 2 (Issue Comment on #135), Task 3 (Fix #104), Task 11 (Monthly Summary)

## Monthly Summary Issue
- 2026-03: Issue #220 "[KeyBot] Monthly Activity 2026-03" (updated)

## PRs Created
- #221: `perf: avoid HashSet allocation in role requirement handlers` (open, draft)
- New (pending merge): `fix: add SourceTokenRetrievalScheme to support cookie-based Web App token retrieval` — fixes #104

## Issues Commented On
- #104: Deep-dive analysis (2026-03-29) + fix now implemented in PR. SourceTokenRetrievalScheme approach.
- #135: Multi-client guidance (2026-03-29) — Keycloak audience mappers, ValidAudiences, multiple schemes

## Issue Labelling Notes
- All open issues already have labels (unlabelled_issues=0)

## Technical Notes
- HttpContextAccessTokenProvider fix (#104): SourceTokenRetrievalScheme (nullable) controls which auth scheme is used for GetTokenAsync. Falls back to SourceAuthenticationScheme ("Bearer") when null. For cookie/OIDC apps: set SourceTokenRetrievalScheme = "Cookies". AccessTokenPropagationHandler still uses SourceAuthenticationScheme for the Authorization header.

## Backlog / Ideas
- #115: Documentation improvement for Docker container setup — good candidate for a docs PR
- #96: UMA Support — complex, needs investigation
- #198: DPoP support — blocked on ASP.NET Core ecosystem
- #104: Consider additive `AddAuthorizationServerForWebApp()` convenience extension
- Possible perf: VerificationPlan.GetEnumerator() allocates List per iteration
- Possible perf: KeycloakOrganizationClaimsExtensions.GetOrganizations() double-LINQ
