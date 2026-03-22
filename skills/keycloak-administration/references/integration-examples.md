# Integration Examples

## .NET (Keycloak.AuthServices)

### JWT Bearer Authentication (Web API)

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/protected", () => "Hello!").RequireAuthorization();
app.Run();
```

```json
// appsettings.json
{
  "Keycloak": {
    "AuthServerUrl": "https://keycloak.example.com",
    "Realm": "my-realm",
    "Resource": "my-client",
    "VerifyTokenAudience": true
  }
}
```

### OIDC Authentication (Web App)

```csharp
builder.Services.AddKeycloakWebAppAuthentication(builder.Configuration);
```

### Authorization with Roles

```csharp
builder.Services.AddKeycloakAuthorization(builder.Configuration)
    .AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRealmRoles("admin"))
    .AddPolicy("EditorOnly", policy => policy.RequireResourceRoles("editor"));
```

### Resource Protection (Authorization Server)

```csharp
builder.Services.AddAuthorizationServer(builder.Configuration);

// Attribute-based
[ProtectedResource("documents", "read")]
public class DocumentsController : ControllerBase { }
```

### Admin SDK

```csharp
builder.Services.AddKeycloakAdminHttpClient(builder.Configuration);

// Inject and use
app.MapGet("/users", async (IKeycloakUserClient client) =>
    await client.GetUsers("my-realm"));
```

## Spring Boot

```yaml
spring:
  security:
    oauth2:
      resourceserver:
        jwt:
          issuer-uri: https://keycloak.example.com/realms/my-realm
```

```java
@Configuration
@EnableWebSecurity
public class SecurityConfig {
    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        http.authorizeHttpRequests(authz -> authz
                .requestMatchers("/public/**").permitAll()
                .anyRequest().authenticated())
            .oauth2ResourceServer(oauth2 -> oauth2.jwt(Customizer.withDefaults()));
        return http.build();
    }
}
```

## Node.js / Express

```javascript
const { Issuer, Strategy } = require('openid-client');

const keycloakIssuer = await Issuer.discover(
  'https://keycloak.example.com/realms/my-realm'
);

const client = new keycloakIssuer.Client({
  client_id: 'node-app',
  client_secret: 'secret',
  redirect_uris: ['http://localhost:3000/callback'],
});

// Use with passport.js
passport.use('oidc', new Strategy({ client }, (tokenSet, userinfo, done) => {
  return done(null, userinfo);
}));
```

## Token Validation (Any Platform)

### JWKS Endpoint

All platforms can validate tokens using the JWKS endpoint:

```
GET {AuthServerUrl}/realms/{realm}/protocol/openid-connect/certs
```

### Discovery Endpoint

Get all endpoints and configuration:

```
GET {AuthServerUrl}/realms/{realm}/.well-known/openid-configuration
```

### Manual Token Validation Steps

1. Fetch JWKS from realm
2. Decode JWT header to get `kid` (key ID)
3. Find matching public key in JWKS
4. Verify signature
5. Validate claims: `iss`, `aud`, `exp`, `nbf`
