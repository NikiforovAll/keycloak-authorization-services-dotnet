# Organization Authorization

Since Keycloak 26, **Organizations** provide built-in multi-tenancy support. Users can belong to one or more organizations, and membership is included as an `organization` claim in JWT tokens.

`Keycloak.AuthServices.Authorization` provides first-class support for enforcing organization membership — declaratively via endpoint metadata or imperatively via `IAuthorizationService`.

*Table of Contents*:
[[toc]]

## Token Claim Format

Keycloak produces two claim formats depending on the Organization Membership Mapper configuration:

**Default (string array)** — when no ID or attributes are enabled:

```json
{
  "organization": ["acme-corp", "startup-co"]
}
```

**Rich JSON (map)** — when `addOrganizationId` or `addOrganizationAttributes` is enabled:

```json
{
  "organization": {
    "acme-corp": { "id": "a56bea03-..." },
    "startup-co": { "id": "uuid-2", "department": ["engineering"] }
  }
}
```

Both formats are handled transparently by the library.

> [!TIP]
> To include organization membership in tokens, request the `organization:*` scope. The plain `organization` scope alone does **not** include the claim.

## Setup

Register organization authorization in your service collection:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddKeycloakAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
```

`AddKeycloakAuthorization()` registers the `OrganizationRequirementHandler` and the built-in parameter resolvers (`RouteParameterResolver`, `HeaderParameterResolver`, `QueryParameterResolver`).

## Declarative Authorization

Use `RequireOrganizationMembership()` on endpoints — it works like `RequireProtectedResource()` but for organization membership.

### Any Organization

Require the user to belong to **any** organization:

```csharp
app.MapGet("/orgs", (ClaimsPrincipal user) =>
{
    var orgs = user.GetOrganizations();
    return Results.Ok(orgs.Select(o => new { o.Alias, o.Id }));
})
.RequireOrganizationMembership();
```

### Route-Based (Dynamic)

Resolve the organization from a route parameter:

```csharp
app.MapGet("/orgs/{orgId}/projects", (string orgId) =>
    Results.Ok(new { Organization = orgId, Projects = new[] { "project-alpha" } })
)
.RequireOrganizationMembership("{orgId}");
```

The `{orgId}` template is resolved at evaluation time using the default `RouteParameterResolver`.

### Static Organization

Require membership in a specific, hardcoded organization:

```csharp
app.MapGet("/acme/settings", () =>
    Results.Ok(new { Theme = "dark", Tier = "enterprise" })
)
.RequireOrganizationMembership("acme-corp");
```

### Header-Based (Custom Resolver)

Resolve the organization from a request header using a custom `IParameterResolver`:

```csharp
app.MapGet("/tenant/projects", () =>
    Results.Ok(new { Projects = new[] { "tenant-project" } })
)
.RequireOrganizationMembership<RouteHandlerBuilder, HeaderParameterResolver>(
    "{X-Organization}"
);
```

You can also use the runtime `Type` overload:

```csharp
.RequireOrganizationMembership("{X-Organization}", typeof(HeaderParameterResolver));
```

## Policy Builder

Compose organization membership with other requirements using the policy builder:

```csharp
builder.Services
    .AddKeycloakAuthorization()
    .AddAuthorizationBuilder()
    .AddPolicy("AcmeAdmin", policy => policy
        .RequireOrganizationMembership("acme-corp")
        .RequireRealmRoles("admin")
    );

// Use the policy
app.MapGet("/acme/admin", () => Results.Ok(new { Message = "Acme admin area" }))
    .RequireAuthorization("AcmeAdmin");
```

## Imperative Authorization

### Single Check

Use `IAuthorizationService` to check membership programmatically:

```csharp
app.MapGet("/check/{orgId}", async (
    string orgId,
    ClaimsPrincipal user,
    IAuthorizationService authorizationService) =>
{
    var result = await authorizationService.AuthorizeAsync(
        user, null, new OrganizationRequirement(orgId)
    );

    return result.Succeeded
        ? Results.Ok(new { OrgId = orgId, Access = true })
        : Results.Forbid();
});
```

### Filtering Resources

Filter a collection based on organization membership:

```csharp
app.MapGet("/workspaces", async (
    ClaimsPrincipal user,
    IAuthorizationService authorizationService) =>
{
    var allWorkspaces = new[]
    {
        new { Id = 1, Name = "Acme Platform", Org = "acme-corp" },
        new { Id = 2, Name = "Partner Portal", Org = "partner-inc" },
        new { Id = 3, Name = "Startup MVP", Org = "startup-co" },
    };

    var accessible = new List<object>();
    foreach (var workspace in allWorkspaces)
    {
        var result = await authorizationService.AuthorizeAsync(
            user, null, new OrganizationRequirement(workspace.Org)
        );

        if (result.Succeeded)
            accessible.Add(workspace);
    }

    return Results.Ok(accessible);
});
```

> [!TIP]
> For simple cases, you can also use the `ClaimsPrincipal` extension methods directly — `user.IsMemberOf("acme-corp")` or `user.GetOrganizations()` — without going through the authorization service.

## Claim Parsing Extensions

The `ClaimsPrincipal` extension methods are available in `Keycloak.AuthServices.Common.Claims`:

```csharp
using Keycloak.AuthServices.Common.Claims;

// Get all organizations
IReadOnlyList<OrganizationMembership> orgs = user.GetOrganizations();

// Check membership by alias
bool isMember = user.IsMemberOf("acme-corp");

// Check membership by ID (rich JSON format only)
bool isMemberById = user.IsMemberOfById("a56bea03-...");
```

The `OrganizationMembership` model:

```csharp
public class OrganizationMembership
{
    public string Alias { get; set; }
    public string? Id { get; set; }
    public IDictionary<string, string[]>? Attributes { get; set; }
}
```

## Multi-Tenancy Patterns

Keycloak Organizations are a **multi-tenancy primitive** — each organization represents a tenant, customer, or company. The library handles the authorization gate ("can this user access this tenant?"), while the application handles data isolation and tenant context.

### Library vs. Application Responsibility

| Concern | Who Handles |
|---|---|
| User belongs to tenant? | **Library** (`RequireOrganizationMembership`) |
| Which tenant is this request for? | **App** (from route, header, or claim) |
| Isolate data per tenant | **App** (EF Core filters, DB schemas) |
| Tenant-specific configuration | **App** (`IOptionsSnapshot` per org) |
| Org-scoped roles | **Not yet in Keycloak** ([keycloak#30180](https://github.com/keycloak/keycloak/issues/30180)) |

### Tenant Context Middleware

Extract the current tenant from the organization claim and make it available to the rest of the pipeline:

```csharp
app.Use(async (context, next) =>
{
    var orgs = context.User.GetOrganizations();
    if (orgs.Count > 0)
    {
        context.Features.Set(new TenantContext(orgs.First()));
    }
    await next();
});
```

### Data Isolation with EF Core

Use a global query filter to scope all queries to the current tenant:

```csharp
// In DbContext.OnModelCreating
builder.Entity<Project>()
    .HasQueryFilter(e => e.TenantId == _currentTenant.Id);
```

### Tenant Switching

Users who belong to multiple organizations can switch between them. Keycloak supports requesting a token scoped to a **single** organization using the `organization:<alias>` scope:

```
scope=openid organization:acme-corp
```

This produces a token with only that organization in the claim, which is useful when your application needs a single active tenant context.

### B2B Identity Federation

Each organization in Keycloak can be linked to an external identity provider (e.g., customer's Azure AD or Okta). Users from that org automatically federate through their own IdP — enabling B2B SSO without application-level changes.

## Keycloak Setup

Organizations must be enabled in Keycloak (KC 26+). See the [OrganizationAuthorization sample](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/OrganizationAuthorization) for a complete Docker Compose setup with realm import and seed script.

Key steps:

1. Enable organizations on the realm (`organizationsEnabled: true`)
2. Create organizations via Admin API or UI
3. Assign users to organizations
4. Ensure the `organization` client scope is assigned to your client
5. Request `organization:*` scope when obtaining tokens
