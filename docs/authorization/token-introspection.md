# Token Introspection

Keycloak 24+ supports [lightweight access tokens](https://www.keycloak.org/docs/latest/server_admin/#_using_lightweight_access_token) — valid signed JWTs that are stripped of business claims like `realm_access`, `resource_access`, and `preferred_username`. This reduces token size but means role-based authorization will silently fail (403 Forbidden) because the claims are missing from the JWT.

`AddKeycloakTokenIntrospection()` solves this by calling the Keycloak [token introspection endpoint](https://www.keycloak.org/docs/latest/securing_apps/#token-introspection-endpoint) to resolve the full claim set and enrich the `ClaimsPrincipal` before authorization runs.

*Table of Contents*:
[[toc]]

## Setup

```bash
dotnet add package Keycloak.AuthServices.Authorization
```

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);

// Register introspection BEFORE authorization
builder.Services.AddKeycloakTokenIntrospection(builder.Configuration);

builder.Services
    .AddKeycloakAuthorization(options =>
    {
        builder.Configuration.GetSection("Keycloak").Bind(options);
        options.EnableRolesMapping = RolesClaimTransformationSource.All;
    })
    .AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRealmRoles("Admin"));
```

> [!IMPORTANT]
> `AddKeycloakTokenIntrospection()` must be called **before** `AddKeycloakAuthorization()` so that introspected claims are available when role mapping runs.

## Configuration

Token introspection binds to the same `"Keycloak"` configuration section. A confidential client with `credentials.secret` is required:

```json
{
  "Keycloak": {
    "realm": "Test",
    "auth-server-url": "http://localhost:8080/",
    "resource": "test-client",
    "credentials": {
      "secret": "your-client-secret"
    }
  }
}
```

### Cache Duration

Introspection results are cached per token using `HybridCache` (keyed by `jti` claim or SHA256 hash of the token). Default cache duration is 60 seconds:

```csharp
builder.Services.AddKeycloakTokenIntrospection(options =>
{
    builder.Configuration.GetSection("Keycloak").Bind(options);
    options.CacheDuration = TimeSpan.FromSeconds(120);
});
```

### Startup Validation

The library validates at startup that `AuthServerUrl`, `Realm`, `Resource`, and `Credentials.Secret` are all set. Missing values cause a fail-fast exception.

## How It Works

The introspection pipeline runs as an `IClaimsTransformation`:

1. **Skip check** — if `realm_access` or `resource_access` claims are already present, the token is not lightweight and introspection is skipped.
2. **Token extraction** — the bearer token is extracted directly from the `Authorization` header.
3. **Cache lookup** — `HybridCache.GetOrCreateAsync` checks for a cached response (keyed by `jti` or SHA256 hash).
4. **Introspection call** — on cache miss, POSTs to `/realms/{realm}/protocol/openid-connect/token/introspect` with `token`, `client_id`, and `client_secret`.
5. **Claim enrichment** — introspected claims are added to the `ClaimsIdentity`.
6. **Role mapping** — `KeycloakRolesClaimsTransformation` then maps roles from the enriched claims.

### Default Claim Mapping

| Claim category | Behavior |
|---------------|----------|
| **Skipped** (`active`, `iat`, `exp`, `nbf`, `iss`, `sub`, `aud`, `jti`, `typ`, `azp`, `auth_time`, `session_state`, `acr`, `sid`) | Not added — these are infrastructure claims already present from JWT validation |
| **JSON claims** (`resource_access`, `realm_access`, `organization`) | Stored as a single claim with `ValueType = "JSON"` and the raw JSON as value |
| **Array values** | Each array element becomes a separate claim |
| **Object values** | Stored as a single claim with `ValueType = "JSON"` |
| **Scalar values** | Stored as a single string claim |
| **Existing claims** | Claims already present on the identity are not duplicated |

## Extending Introspection

Use `OnTokenIntrospected` to customize claim mapping after the default enrichment runs:

```csharp
builder.Services.AddKeycloakTokenIntrospection(options =>
{
    builder.Configuration.GetSection("Keycloak").Bind(options);

    options.OnTokenIntrospected = (identity, claims) =>
    {
        // Add a custom claim from the introspection response
        if (claims.TryGetValue("department", out var dept))
        {
            identity.AddClaim(new Claim("department", dept.GetString()!));
        }

        // Remove a claim added by default enrichment
        var unwanted = identity.FindFirst("some_claim");
        if (unwanted is not null)
        {
            identity.RemoveClaim(unwanted);
        }
    };
});
```

The delegate receives the `ClaimsIdentity` (already enriched by default logic) and the raw `Dictionary<string, JsonElement>` from the introspection response.

## Resilience

`AddKeycloakTokenIntrospection()` returns `IHttpClientBuilder`, so you can chain resilience policies:

```csharp
builder.Services
    .AddKeycloakTokenIntrospection(builder.Configuration)
    .AddStandardResilienceHandler();
```

## Sample

<<< @/../samples/TokenIntrospection/Program.cs

See sample source code: [keycloak-authorization-services-dotnet/tree/main/samples/TokenIntrospection](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/TokenIntrospection)
