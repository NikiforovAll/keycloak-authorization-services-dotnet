# Resource Protection & Authorization Server

Fine-grained access control using Keycloak Authorization Server. Delegates authorization decisions to Keycloak (PDP) while your app acts as PEP.

## Concepts

- **Resource**: entity to protect (e.g., "my-workspace")
- **Scope**: action on resource (e.g., "workspace:read", "workspace:write")
- **Permission**: rule linking resource + scope + policy
- **Policy**: condition evaluated by Keycloak (role-based, time-based, etc.)
- **PEP** (Policy Enforcement Point): your app — intercepts requests, checks authorization
- **PDP** (Policy Decision Point): Keycloak Authorization Server — evaluates policies

## Setup

```bash
dotnet add package Keycloak.AuthServices.Authorization
```

```csharp
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(builder.Configuration);

builder.Services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(builder.Configuration);
```

Authorization Server calls are made on behalf of the user via header propagation. `AddAuthorizationServer` adds `AccessTokenPropagationHandler` that uses `IKeycloakAccessTokenProvider` to obtain the user's token.

### IKeycloakAccessTokenProvider

The default `HttpContextAccessTokenProvider` reads the JWT from the current `HttpContext`. Override it for custom token sourcing (e.g., SignalR, long-lived connections):

```csharp
public interface IKeycloakAccessTokenProvider
{
    Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
```

Register a custom implementation before `AddAuthorizationServer`:

```csharp
services.AddScoped<IKeycloakAccessTokenProvider, MyCustomTokenProvider>();
```

Configuration options on `KeycloakAuthorizationServerOptions`:
- `SourceAuthenticationScheme` — auth scheme for token extraction (default: `"Bearer"`)
- `SourceTokenName` — token name (default: `"access_token"`)

## Protected Resource Builder

Simplest way to protect endpoints — no manual policy registration:

```csharp
app.MapGet("/workspaces", () => "Hello World!")
    .RequireProtectedResource("workspaces", "workspace:read");
```

`RequireProtectedResource` is an extension on `IEndpointConventionBuilder` — works with Minimal APIs, MVC, RazorPages.

### Dynamic Resources

Use path parameters in resource names:

```csharp
app.MapGet("/workspaces/{id}", (string id) => $"Workspace {id}")
    .RequireProtectedResource("workspaces/{id}", "workspace:read");
```

The `{id}` placeholder is resolved from the route parameter at runtime.

### Multiple Scopes

```csharp
app.MapDelete("/workspaces/{id}", (string id) => Results.NoContent())
    .RequireProtectedResource("workspaces/{id}", "workspace:read", "workspace:delete");
```

### Endpoint Hierarchy (Group-level + Endpoint-level)

```csharp
var workspaces = app.MapGroup("/workspaces")
    .RequireProtectedResource("workspaces", "workspace:read");

workspaces.MapGet("/", () => "List");
workspaces.MapDelete("/{id}", (string id) => Results.NoContent())
    .RequireProtectedResource("workspaces/{id}", "workspace:delete");
```

### Multiple Resources

```csharp
app.MapGet("/dashboard", () => "Dashboard")
    .RequireProtectedResource("workspaces", "workspace:read")
    .RequireProtectedResource("reports", "report:view");
```

### Ignore Protected Resources

Similar to `AllowAnonymous`:

```csharp
var group = app.MapGroup("/workspaces")
    .RequireProtectedResource("workspaces", "workspace:read");

group.MapGet("/public", () => "Public")
    .IgnoreProtectedResources();
```

## Policy-Based Approach

Instead of Protected Resource Builder, use standard ASP.NET policies:

```csharp
builder.Services
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(builder.Configuration)
    .AddAuthorizationBuilder()
    .AddPolicy("WorkspaceRead", policy =>
        policy.RequireProtectedResource(
            resource: "workspaces",
            scope: "workspace:read"
        )
    );

app.MapGet("/workspaces", () => "Hello").RequireAuthorization("WorkspaceRead");
```

## Policy Provider (Convention-Based)

Auto-register policies from naming convention `<resource>#<scope1>,<scope2>`:

```csharp
builder.Services.AddAuthorizationServer(options =>
{
    builder.Configuration.BindKeycloakOptions(options);
    options.UseProtectedResourcePolicyProvider = true;
});

app.MapGet("/", () => "Hello")
    .RequireAuthorization("my-workspace#workspaces:read");
```

### IProtectedResourcePolicyBuilder (Extensible Policy Construction)

The `ProtectedResourcePolicyProvider` delegates policy construction to `IProtectedResourcePolicyBuilder`. The default implementation parses the `<resource>#<scope1>,<scope2>` format:

```csharp
public interface IProtectedResourcePolicyBuilder
{
    AuthorizationPolicy? Build(string policyName);
}
```

Override with a custom implementation (e.g., caching):

```csharp
public class CachingPolicyBuilder : IProtectedResourcePolicyBuilder
{
    private readonly ConcurrentDictionary<string, AuthorizationPolicy?> cache = new();
    private readonly DefaultProtectedResourcePolicyBuilder inner = new();

    public AuthorizationPolicy? Build(string policyName) =>
        cache.GetOrAdd(policyName, inner.Build);
}

// Register before AddAuthorizationServer
services.AddSingleton<IProtectedResourcePolicyBuilder, CachingPolicyBuilder>();
```

## Pluggable Parameter Resolvers

The `{parameter}` syntax in `RequireProtectedResource("resource/{id}", ...)` is resolved by `IParameterResolver` implementations. All three built-in resolvers are registered automatically by `AddKeycloakAuthorization()`.

```csharp
public interface IParameterResolver
{
    string? Resolve(string parameter, HttpContext httpContext, IServiceProvider serviceProvider);
}
```

**Built-in resolvers:**

| Resolver | Source | Example |
|----------|--------|---------|
| `RouteParameterResolver` (default) | Route values | `{id}` from `/workspaces/{id}` |
| `HeaderParameterResolver` | HTTP headers | `{X-Tenant}` from request header |
| `QueryParameterResolver` | Query string | `{tenant}` from `?tenant=acme` |

**Custom resolver:**

```csharp
public class ServiceResolver : IParameterResolver
{
    public string? Resolve(string parameter, HttpContext httpContext, IServiceProvider serviceProvider)
    {
        var tenantConfig = serviceProvider.GetService<TenantConfig>();
        return tenantConfig?.TenantId;
    }
}
```

Resolvers are used by both resource protection (`RequireProtectedResource`) and organization authorization (`RequireOrganizationMembership`). See [organization-authorization.md](organization-authorization.md) for resolver usage with organizations.

## ProtectedResource Attribute (Controllers)

```csharp
[ProtectedResource("documents", "read")]
public class DocumentsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("Documents");

    [HttpDelete("{id}")]
    [ProtectedResource("documents/{id}", "delete")]
    public IActionResult Delete(string id) => NoContent();
}
```

## Keycloak Configuration

1. Enable Authorization on the client in Keycloak admin console
2. Create Resources (e.g., "workspaces" with type "urn:workspaces")
3. Create Scopes (e.g., "workspace:read", "workspace:write")
4. Create Policies (e.g., "Require Admin Role" — role-based policy)
5. Create Permissions linking resources + scopes + policies
6. Use Keycloak's "Evaluate" tab to test permissions

See realm export files in `tests/Keycloak.AuthServices.IntegrationTests/KeycloakConfiguration/` for complete working examples.
