# Organization Authorization (Multi-Tenancy)

Keycloak 26+ Organizations provide built-in multi-tenancy. The library offers declarative and imperative authorization based on organization membership claims.

## Setup

```csharp
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddKeycloakAuthorization(); // Registers organization handlers + parameter resolvers
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
```

`AddKeycloakAuthorization()` automatically registers:
- `OrganizationRequirementHandler`
- `RouteParameterResolver`, `HeaderParameterResolver`, `QueryParameterResolver`
- Default `$OrganizationPolicy` policy

## Token Claim Formats

**Vanilla format** — multiple string-valued claims:
```json
{ "organization": ["acme-corp", "startup-co"] }
```

**Rich JSON format** — single JSON claim with optional id/attributes (requires "Add organization id" / "Add organization attributes" mapper options):
```json
{
  "organization": {
    "acme-corp": { "id": "a56bea03-...", "tier": ["premium"] },
    "startup-co": { "id": "uuid-2", "department": ["engineering"] }
  }
}
```

The library auto-detects which format is in use — transparent to the developer.

## OrganizationMembership Model

```csharp
public class OrganizationMembership
{
    public required string Alias { get; init; }
    public string? Id { get; init; }
    public IReadOnlyDictionary<string, string[]>? Attributes { get; init; }
}
```

## ClaimsPrincipal Extension Methods

```csharp
using Keycloak.AuthServices.Common.Claims;

IReadOnlyList<OrganizationMembership> orgs = user.GetOrganizations();
IReadOnlyList<OrganizationMembership> orgs = user.GetOrganizations(claimType: "custom");

bool isMember = user.IsMemberOf("acme-corp");
bool isMember = user.IsMemberOf("acme-corp", claimType: "custom");

bool isMemberById = user.IsMemberOfById("a56bea03-...");
bool isMemberById = user.IsMemberOfById("a56bea03-...", claimType: "custom");
```

## Declarative Authorization (Endpoint Extensions)

**Any organization membership:**
```csharp
app.MapGet("/orgs", (ClaimsPrincipal user) =>
{
    var orgs = user.GetOrganizations();
    return Results.Ok(orgs.Select(o => new { o.Alias, o.Id }));
})
.RequireOrganizationMembership();
```

**Static organization:**
```csharp
app.MapGet("/acme/settings", () => Results.Ok(new { Theme = "dark" }))
    .RequireOrganizationMembership("acme-corp");
```

**Route-based (dynamic):**
```csharp
app.MapGet("/orgs/{orgId}/projects", (string orgId) =>
    Results.Ok(new { Organization = orgId, Projects = new[] { "project-alpha" } })
)
.RequireOrganizationMembership("{orgId}");
```

**Header-based (custom resolver):**
```csharp
app.MapGet("/tenant/projects", () =>
    Results.Ok(new { Projects = new[] { "tenant-project" } })
)
.RequireOrganizationMembership<RouteHandlerBuilder, HeaderParameterResolver>("{X-Organization}");

// Or using runtime Type overload:
.RequireOrganizationMembership("{X-Organization}", typeof(HeaderParameterResolver));
```

## Policy Builder API

Combine organization requirements with other requirements:

```csharp
builder.Services
    .AddKeycloakAuthorization()
    .AddAuthorizationBuilder()
    .AddPolicy("AcmeAdmin", policy => policy
        .RequireOrganizationMembership("acme-corp")
        .RequireRealmRoles("admin")
    );

app.MapGet("/acme/admin", () => Results.Ok(new { Message = "Acme admin area" }))
    .RequireAuthorization("AcmeAdmin");
```

## Imperative Authorization

**Single check:**
```csharp
app.MapGet("/check/{orgId}", async (
    string orgId,
    ClaimsPrincipal user,
    IAuthorizationService authorizationService) =>
{
    var result = await authorizationService.AuthorizeAsync(
        user, null, new OrganizationRequirement(orgId)
    );
    return result.Succeeded ? Results.Ok(new { OrgId = orgId, Access = true }) : Results.Forbid();
});
```

**Resource filtering:**
```csharp
app.MapGet("/workspaces", async (
    ClaimsPrincipal user,
    IAuthorizationService authorizationService) =>
{
    var allWorkspaces = new[]
    {
        new { Id = 1, Name = "Acme Platform", Org = "acme-corp" },
        new { Id = 2, Name = "Partner Portal", Org = "partner-inc" },
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

## Configuration

```csharp
builder.Services.AddKeycloakAuthorization(options =>
{
    options.OrganizationClaimType = "custom-org-claim"; // Default: "organization"
});
```

## Pluggable Parameter Resolvers

The `{parameter}` syntax in `RequireOrganizationMembership("{param}")` and `RequireProtectedResource("resource/{param}", ...)` is resolved by `IParameterResolver` implementations.

**Built-in resolvers:**

| Resolver | Source | Example |
|----------|--------|---------|
| `RouteParameterResolver` (default) | Route values | `{orgId}` from `/orgs/{orgId}` |
| `HeaderParameterResolver` | HTTP headers | `{X-Organization}` from header |
| `QueryParameterResolver` | Query string | `{tenant}` from `?tenant=acme` |

**Custom resolver:**
```csharp
public interface IParameterResolver
{
    string? Resolve(string parameter, HttpContext httpContext, IServiceProvider serviceProvider);
}

public class CustomResolver : IParameterResolver
{
    public string? Resolve(string parameter, HttpContext httpContext, IServiceProvider serviceProvider)
    {
        var config = serviceProvider.GetService<TenantConfig>();
        return config?.TenantId;
    }
}
```

## Keycloak Setup

- Keycloak 26+ required
- Enable organizations: `organizationsEnabled: true` in realm config
- Assign `organization` scope to client
- Request `organization:*` scope in token requests
- Create organizations and assign users via Admin API or UI

## Multi-Tenancy Patterns

| Concern | Handler |
|---------|---------|
| User belongs to tenant? | **Library** (`RequireOrganizationMembership`) |
| Which tenant is this request for? | **App** (from route, header, claim) |
| Isolate data per tenant | **App** (EF Core filters, DB schemas) |
| Tenant-specific configuration | **App** (`IOptionsSnapshot` per org) |

**Tenant switching:** Users with multiple organizations can request org-specific tokens using `organization:<alias>` scope for single-tenant context.

**B2B identity federation:** Each organization can link to an external identity provider for automatic federation without app changes.
