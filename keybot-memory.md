# KeyBot Memory

## Last Run
- Date: 2026-03-29
- Run: https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/runs/23710183099
- Tasks: Task 8 (Performance), Task 1 (Issue Labelling), Task 11 (Monthly Summary)

## Monthly Summary Issue
- 2026-03: Created "[KeyBot] Monthly Activity 2026-03" issue (see issues list)

## PRs Created
- `keybot/perf-role-check-avoid-hashset-alloc-20260329`: perf: avoid HashSet allocation in role requirement handlers — replaces Intersect().Any() with Any(Contains)

## Issue Labelling Notes
- All open issues already have labels (unlabelled_issues=0 per task_selection.json)

## Backlog / Ideas
- #115: Documentation improvement for Docker container setup — good candidate for a docs PR
- #96: UMA Support — complex, needs investigation
- #198: DPoP support — blocked on ASP.NET Core ecosystem
- #104: Protected Resource Builder + MVC — bug, needs investigation
- Possible perf: VerificationPlan.GetEnumerator() allocates List per iteration
- Possible perf: KeycloakOrganizationClaimsExtensions.GetOrganizations() double-LINQ

## Issues Commented On
- None yet
