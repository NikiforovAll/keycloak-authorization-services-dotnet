# Developer Experience

## .NET Aspire Integration

```bash
dotnet add package Keycloak.AuthServices.Aspire.Hosting
```

### AppHost Setup

```csharp
// AppHost/Program.cs
var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloakContainer("keycloak");
var realm = keycloak.AddRealm("Test");

builder.AddProject<Projects.Api>("api")
    .WithReference(keycloak)
    .WithReference(realm);

builder.Build().Run();
```

What this does:
1. Starts Keycloak as Docker container
2. `WithReference(keycloak)` — adds Keycloak to Service Discovery
3. `WithReference(realm)` — sets `Keycloak__Realm` and `Keycloak__AuthServerUrl` env vars

### Import Configuration Files

```csharp
var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithDataVolume()
    .WithImport("./KeycloakConfiguration/Test-realm.json")
    .WithImport("./KeycloakConfiguration/Test-users-0.json");
```

Export current config from inside the container:

```bash
/opt/keycloak/bin/kc.sh export --dir /opt/keycloak/data/import --realm Test
```

### API Project Configuration

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddKeycloakWebApiAuthentication(
    builder.Configuration,
    options =>
    {
        options.Audience = "workspaces-client";
        options.RequireHttpsMetadata = false;
    }
);
builder.Services.AddAuthorization();
```

## Project Templates

```bash
dotnet new install Keycloak.AuthServices.Templates
```

### Available Templates

| Template | Short Name | Description |
|----------|-----------|-------------|
| Keycloak WebApi | `keycloak-webapi` | Single Web API project with Keycloak auth |
| Keycloak Aspire Starter | `keycloak-aspire-starter` | Full Aspire solution with Keycloak |

### Web API Template

```bash
dotnet new keycloak-webapi -o MyKeycloakApi
```

Creates: `Program.cs`, `appsettings.json`, OpenAPI config, `Directory.Build.props`.

### Aspire Template

```bash
dotnet new keycloak-aspire-starter -o MyAspireSolution --EnableKeycloakImport
```

Creates full solution: AppHost, API, ServiceDefaults, with optional realm import files.

## OpenTelemetry

```bash
dotnet add package Keycloak.AuthServices.OpenTelemetry
```

```csharp
builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddKeycloakAuthServicesInstrumentation()
    )
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddKeycloakAuthServicesInstrumentation()
    )
    .UseOtlpExporter();
```

### Metrics

Monitor authorization decisions:

```bash
dotnet counters monitor --name MyApp --counters Keycloak.AuthServices.Authorization
```

```text
[Keycloak.AuthServices.Authorization]
    keycloak.authservices.requirements.fail (Count)
        requirement=ParameterizedProtectedResourceRequirement    3
    keycloak.authservices.requirements.succeed (Count)
        requirement=ParameterizedProtectedResourceRequirement    5
        requirement=RealmAccessRequirement                      16
```

## Logging

```json
{
  "Logging": {
    "Keycloak.AuthServices": "Debug",
    "Keycloak.AuthServices.Authorization": "Trace"
  }
}
```
