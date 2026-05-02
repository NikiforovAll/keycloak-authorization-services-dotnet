# Aspire Support

[.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview) is an opinionated, cloud ready stack for building observable, production ready, distributed applications.

`Keycloak.AuthServices.Aspire.Hosting` adds a support to run Keycloak Instance as a component. It is intended to be used together with `Keycloak.AuthServices`.

See working example here. [Examples/Aspire + Web API](/examples/aspire-web-api)

## Add to existing application

Install [Keycloak.AuthServices.Aspire.Hosting](https://www.nuget.org/packages/Keycloak.AuthServices.Aspire.Hosting) package to "AppHost":

```bash
dotnet add package Keycloak.AuthServices.Aspire.Hosting
```

Modify the `AppHost/Program.cs`:

```csharp
// AppHost/Program.cs
var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder // [!code focus]
    .AddKeycloakContainer("keycloak"); // [!code focus]

var realm = keycloak.AddRealm("Test"); // [!code focus]

builder.AddProject<Projects.Api>("api").WithReference(keycloak).WithReference(realm); // [!code focus]

builder.Build().Run();
```

Here is what it does:

1. Starts the instance of Keycloak as docker container.
2. `WithReference(keycloak)` adds Keycloak server instance to Service Discovery.
3. `WithReference(realm)` adds `Keycloak__Realm` and `Keycloak__AuthServerUrl` environment variables.

From this point you are almost finished, the only this is left is to configure Authentication missing parts:

```csharp
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.AddServiceDefaults();

services.AddKeycloakWebApiAuthentication( // [!code focus]
    configuration, // [!code focus]
    options => // [!code focus]
    { // [!code focus]
        options.Audience = "workspaces-client"; // [!code focus]
        options.RequireHttpsMetadata = false; // [!code focus]
    } // [!code focus]
); // [!code focus]
services.AddAuthorization(); // [!code focus]

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/hello", () => "Hello World!").RequireAuthorization();

app.Run();

```

Run:

```bash
dotnet run --project ./AppHost
```

### Import configuration files

You can reference import files and bind Keycloak data volumes to persist Keycloak configuration and share it with others.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithDataVolume()
    .WithImport("./KeycloakConfiguration/Test-realm.json")
    .WithImport("./KeycloakConfiguration/Test-users-0.json");

var realm = keycloak.AddRealm("Test");

builder.AddProject<Projects.Api>("api").WithReference(keycloak).WithReference(realm);

builder.Build().Run();
```

> [!TIP]
> You can sync your current configuration via CLI

Inside docker container run:

```bash
/opt/keycloak/bin/kc.sh export --dir /opt/keycloak/data/import --realm Test
```

## Use an external database

By default the Keycloak container uses the embedded H2 database, which is not suitable for anything beyond local exploration. Wire an external database via one of the typed extensions:

```csharp
var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume()
    .AddDatabase("keycloak-db", databaseName: "keycloak");

var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithPostgresDatabase(postgres);
```

The extension reads the resolved Aspire connection string at deploy time, translates it to JDBC, and sets `KC_DB`, `KC_DB_URL`, `KC_DB_USERNAME`, and `KC_DB_PASSWORD` on the Keycloak container. It also applies `WaitFor(database)`, so Keycloak only starts after the database is ready.

Available overloads — one per Keycloak-supported vendor:

| Extension | `KC_DB` |
|---|---|
| `WithPostgresDatabase` | `postgres` |
| `WithMySqlDatabase` | `mysql` |
| `WithMariaDbDatabase` | `mariadb` |
| `WithMsSqlDatabase` | `mssql` |
| `WithOracleDatabase` | `oracle` |

Each takes any `IResourceBuilder<IResourceWithConnectionString>`, so you can also point Keycloak at a managed database resource (e.g. an Aspire connection-string-only reference for a hosted Postgres).

## Pin the issuer hostname

Keycloak in development mode derives the `iss` claim from whichever URL it is reached on. When API consumers run inside containers (e.g. `host.docker.internal`) but tokens were obtained via `localhost`, you'll see:

```
WWW-Authenticate: Bearer error="invalid_token", error_description="The issuer 'http://localhost:8080/realms/...' is invalid"
```

Pin the hostname so every issued token uses the same `iss` regardless of how the container is reached:

```csharp
var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithHostname("http://localhost:8080/");
```

This sets `KC_HOSTNAME` on the container. For non-Aspire scenarios see the [containerized API recipe](/qa/recipes#how-to-connect-a-containerized-api-to-keycloak).

## Start from Template

You can use [Keycloak.AuthServices.Templates](https://www.nuget.org/packages/Keycloak.AuthServices.Templates) to scaffold the new Aspire Project that has `Keycloak.AuthServices` integration configured.

Install template:

```bash
dotnet new install Keycloak.AuthServices.Templates
```

Scaffold a solution:

```bash
dotnet new keycloak-aspire-starter -o $dev/KeycloakAspireStarter --EnableKeycloakImport
```

See [Aspire Template](/devex/templates#aspire-web-api) for more details.
