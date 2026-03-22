# Admin REST API SDK

Two client options for the Keycloak Admin REST API:

| Package | Description |
|---------|-------------|
| `Keycloak.AuthServices.Sdk` | Hand-written typed clients — high quality, partial API coverage |
| `Keycloak.AuthServices.Sdk.Kiota` | Kiota-generated client — full API coverage |

## Hand-Written SDK

```bash
dotnet add package Keycloak.AuthServices.Sdk
```

### Registration

```csharp
builder.Services.AddKeycloakAdminHttpClient(builder.Configuration);
```

Registers: `IKeycloakClient` (umbrella), `IKeycloakRealmClient`, `IKeycloakUserClient`, `IKeycloakGroupClient`.

Returns `IHttpClientBuilder` for resilience and handler configuration:

```csharp
builder.Services
    .AddKeycloakAdminHttpClient(builder.Configuration)
    .AddStandardResilienceHandler()
    .AddHttpMessageHandler<TimingHandler>();
```

### Available Clients

- `IKeycloakRealmClient` — realm CRUD, export
- `IKeycloakUserClient` — user CRUD, roles, groups, credentials, sessions
- `IKeycloakGroupClient` — group CRUD, members
- `IKeycloakClient` — umbrella interface combining all above

### Usage

```csharp
app.MapGet("/users", async (IKeycloakUserClient client) =>
    await client.GetUsers("my-realm"));

app.MapGet("/realm", async (IKeycloakRealmClient client) =>
    await client.GetRealmAsync("my-realm"));
```

### Console App

```csharp
var services = new ServiceCollection();

var keycloakOptions = new KeycloakAdminClientOptions
{
    AuthServerUrl = "http://localhost:8080/",
    Realm = "master",
    Resource = "admin-api",
};
services.AddKeycloakAdminHttpClient(keycloakOptions);

var sp = services.BuildServiceProvider();
var client = sp.GetRequiredService<IKeycloakClient>();
var realm = await client.GetRealmAsync("Test");
```

## Kiota-Generated Client

```bash
dotnet add package Keycloak.AuthServices.Sdk.Kiota
```

### Registration

```csharp
builder.Services.AddKeycloakAdminHttpClient(builder.Configuration);
```

Registers `KeycloakAdminApiClient` — a fluent, fully-typed client generated from OpenAPI spec.

### Usage

```csharp
var client = sp.GetRequiredService<KeycloakAdminApiClient>();
var realm = await client.Admin.Realms["Test"].GetAsync();
var users = await client.Admin.Realms["Test"].Users.GetAsync();
```

## Access Token Management

Admin API requires authentication. Use `Duende.AccessTokenManagement` for service account tokens:

```bash
dotnet add package Duende.AccessTokenManagement
```

### Service Account Setup

1. Create client `"admin-api"` in **master** realm with Client Authentication + Service Account Roles enabled
2. Add Audience Mapper for `"security-admin-console"`
3. Assign Service Account Role: `"admin"`

### Configuration

```json
{
  "Keycloak": {
    "realm": "master",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "admin-api",
    "credentials": {
      "secret": "your-client-secret"
    }
  }
}
```

### Token Management Integration

```csharp
builder.Services.AddDistributedMemoryCache();
builder.Services.AddClientCredentialsTokenManagement()
    .AddClient("keycloak", client =>
    {
        var keycloakOptions = builder.Configuration
            .GetKeycloakOptions<KeycloakAdminClientOptions>()!;
        client.ClientId = keycloakOptions.Resource;
        client.ClientSecret = keycloakOptions.Credentials?.Secret;
        client.TokenEndpoint = $"{keycloakOptions.AuthServerUrl.TrimEnd('/')}" +
            $"/realms/{keycloakOptions.Realm}/protocol/openid-connect/token";
    });

builder.Services
    .AddKeycloakAdminHttpClient(builder.Configuration)
    .AddClientCredentialsTokenHandler("keycloak");
```
