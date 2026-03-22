# Troubleshooting & Recipes

## Common Issues

### 401 Unauthorized

1. Enable debug logging: `"Keycloak.AuthServices": "Debug"`
2. Verify access token is in the `Authorization: Bearer <token>` header
3. Check audience mapper — token `aud` must include your `resource` (client ID). Either:
   - Add Audience Mapper in Keycloak (Client → Client Scopes → `{client_id}-dedicated` → Mappers → Audience)
   - Or set `"verify-token-audience": false`
4. For dev: ensure `"ssl-required": "none"` and HTTPS metadata is not required

### 403 Forbidden

1. Enable trace logging: `"Keycloak.AuthServices.Authorization": "Trace"`
2. For RBAC: verify `ClaimsPrincipal` has `realm_access` and `resource_access` claims
3. For Authorization Server: ensure Keycloak is accessible and client has Authorization enabled
4. Check policy evaluation in Keycloak admin → Authorization → Evaluate tab

### Keycloak Slow to Respond

- Keycloak becomes bottleneck in Authorization Server scenarios
- Add resilience handlers: `.AddStandardResilienceHandler()`
- Consider cluster setup for production
- Use OpenTelemetry to identify bottlenecks

## Recipes

### Debug Logging

```json
{
  "Logging": {
    "Keycloak.AuthServices": "Debug",
    "Keycloak.AuthServices.Authorization": "Trace"
  }
}
```

### Get Options from DI

```csharp
// Authentication (named options, per scheme)
var authOptions = sp.GetRequiredService<IOptionsMonitor<KeycloakAuthenticationOptions>>()
    .Get(JwtBearerDefaults.AuthenticationScheme);

// Authorization
var authzOptions = sp.GetRequiredService<IOptionsMonitor<KeycloakAuthorizationOptions>>()
    .CurrentValue;
```

### Get Options Before DI Is Built

```csharp
using Keycloak.AuthServices.Common;

var options = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;
```

### Swagger UI with Keycloak

Using NSwag:

```csharp
builder.Services.AddOpenApiDocument((document, sp) =>
{
    var keycloakOptions = sp
        .GetRequiredService<IOptionsMonitor<KeycloakAuthenticationOptions>>()
        ?.Get(JwtBearerDefaults.AuthenticationScheme)!;

    document.AddSecurity(
        OpenIdConnectDefaults.AuthenticationScheme,
        [],
        new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.OpenIdConnect,
            OpenIdConnectUrl = keycloakOptions.OpenIdConnectUrl,
        }
    );

    document.OperationProcessors.Add(
        new OperationSecurityScopeProcessor(OpenIdConnectDefaults.AuthenticationScheme)
    );
});

app.UseSwaggerUi(ui =>
{
    var opts = builder.Configuration
        .GetKeycloakOptions<KeycloakAuthenticationOptions>()!;
    ui.OAuth2Client = new OAuth2ClientSettings
    {
        ClientId = opts.Resource,
        ClientSecret = opts.Credentials?.Secret,
    };
});
```

### HTTP Client Resilience

Globally (all HTTP clients):

```csharp
builder.Services.ConfigureHttpClientDefaults(http =>
    http.AddStandardResilienceHandler());
```

Per client:

```csharp
builder.Services
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(builder.Configuration)
    .AddStandardResilienceHandler();
```

