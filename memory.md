# KeyBot Memory

## Last Run
- Date: 2026-03-29
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/23710341168
- Tasks: Command mode — evaluate feature for issue #104

## Monthly Summary Issue
- 2026-03: Created "[KeyBot] Monthly Activity 2026-03" issue (see issues list)

## PRs Created
- `keybot/perf-role-check-avoid-hashset-alloc-20260329`: perf: avoid HashSet allocation in role requirement handlers — replaces Intersect().Any() with Any(Contains)

## Issue Labelling Notes
- All open issues already have labels (unlabelled_issues=0 per task_selection.json)

## Issues Commented On
- #104: Evaluated ProtectedResource + MVC feature (2026-03-29). Root cause: HttpContextAccessTokenProvider uses GetTokenAsync("Bearer", "access_token") which returns null for cookie-based apps. Fix: make SourceAuthenticationScheme nullable.

## Backlog / Ideas
- #104: Protected Resource Builder + MVC — evaluated (see comment). Needs maintainer decision on default scheme change vs explicit opt-in.
- #115: Documentation improvement for Docker container setup — good candidate for a docs PR
- #96: UMA Support — complex, needs investigation
- #198: DPoP support — blocked on ASP.NET Core ecosystem
- Possible perf: VerificationPlan.GetEnumerator() allocates List per iteration
- Possible perf: KeycloakOrganizationClaimsExtensions.GetOrganizations() double-LINQ

## Technical Notes
- HttpContextAccessTokenProvider: SourceAuthenticationScheme defaults to "Bearer"; GetTokenAsync("Bearer", "access_token") returns null for JWT Bearer since JwtBearerHandler doesn't store tokens in AuthenticationProperties
- For cookie/OIDC apps: GetTokenAsync("access_token") without scheme works when SaveTokens=true
- Fix: make SourceAuthenticationScheme nullable; use GetTokenAsync(tokenName) when null
