# Templates

You can use [dotnet-new](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new) command to scaffold various solution items. `Keyclaok.AuthServices` provides you with templates to get started with Keycloak and .NET

## Getting Started

Install:

```bash
dotnet new install Keycloak.AuthServices.Templates
```

List installed packages:

```bash
❯ dotnet new list keycloak
# These templates matched your input: 'keycloak'

# Template Name            Short Name               Language  Tags
# -----------------------  -----------------------  --------  -------------------------------------
# Keycloak Aspire Starter  keycloak-aspire-starter  [C#]      Common/.NET Aspire/Cloud/API/Keycloak
# Keycloak WebApi          keycloak-webapi          [C#]      Common/API/Keycloak
```

> [!NOTE]
> To successfully run commands below - replace `$dev` with the actual working directory.
>
> You can set shell variable by running next command: `export dev=~/projects`

## Web API

Use `dotnet new keycloak-webapi -h` to discover how to use this template.

### Example

Verify the output of the command by using `--dry-run` option:

```bash
❯ dotnet new keycloak-webapi -o $dev/KeycloakWebApi --dry-run
# File actions would have been taken:
#   Create: $dev\KeycloakWebApi\Directory.Build.props
#   Create: $dev\KeycloakWebApi\Directory.Packages.props
#   Create: $dev\KeycloakWebApi\Extensions.OpenApi.cs
#   Create: $dev\KeycloakWebApi\KeycloakWebApi.csproj
#   Create: $dev\KeycloakWebApi\Program.cs
#   Create: $dev\KeycloakWebApi\Properties\launchSettings.json
#   Create: $dev\KeycloakWebApi\README.md
#   Create: $dev\KeycloakWebApi\appsettings.Development.json
#   Create: $dev\KeycloakWebApi\appsettings.json
```

Generate:

```bash
❯ dotnet new keycloak-webapi -o $dev/KeycloakWebApi
# The template "Keycloak WebApi" was created successfully.
```

Run:

```bash
❯ dotnet run --project $dev/KeycloakWebApi
# Building...
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: https://localhost:7107
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: http://localhost:5064
# info: Microsoft.Hosting.Lifetime[0]
#       Application started. Press Ctrl+C to shut down.
# info: Microsoft.Hosting.Lifetime[0]
#       Hosting environment: Development
# info: Microsoft.Hosting.Lifetime[0]
#       Content root path: $dev\KeycloakWebApi
```

⚠️To finish the configuration process you will need to:

1. Start Keycloak instance, see the generated `README.md` file.
2. Create a *Realm*
3. Register a *Client*
4. Update `appsettings.Development.json` correspondingly.

## Aspire + Web API

Use `dotnet new keycloak-aspire-starter -h` to discover how to use this template.

### Example

Verify the output of the command by using `--dry-run` option:

```bash
❯ dotnet new keycloak-aspire-starter -o $dev/KeycloakAspireStarter --EnableKeycloakImport --dry-run
# File actions would have been taken:
#   Create: $dev\KeycloakAspireStarter\.gitignore
#   Create: $dev\KeycloakAspireStarter\Api\Api.csproj
#   Create: $dev\KeycloakAspireStarter\Api\Extensions.OpenApi.cs
#   Create: $dev\KeycloakAspireStarter\Api\Program.cs
#   Create: $dev\KeycloakAspireStarter\Api\Properties\launchSettings.json
#   Create: $dev\KeycloakAspireStarter\Api\appsettings.Development.json
#   Create: $dev\KeycloakAspireStarter\Api\appsettings.json
#   Create: $dev\KeycloakAspireStarter\AppHost\AppHost.csproj
#   Create: $dev\KeycloakAspireStarter\AppHost\KeycloakConfiguration\Test-realm.json
#   Create: $dev\KeycloakAspireStarter\AppHost\KeycloakConfiguration\Test-users-0.json
#   Create: $dev\KeycloakAspireStarter\AppHost\Program.cs
#   Create: $dev\KeycloakAspireStarter\AppHost\Properties\launchSettings.json
#   Create: $dev\KeycloakAspireStarter\AppHost\appsettings.Development.json
#   Create: $dev\KeycloakAspireStarter\AppHost\appsettings.json
#   Create: $dev\KeycloakAspireStarter\Directory.Build.props
#   Create: $dev\KeycloakAspireStarter\Directory.Packages.props
#   Create: $dev\KeycloakAspireStarter\KeycloakAspireStarter.sln
#   Create: $dev\KeycloakAspireStarter\README.md
#   Create: $dev\KeycloakAspireStarter\ServiceDefaults\Extensions.cs
#   Create: $dev\KeycloakAspireStarter\ServiceDefaults\ServiceDefaults.csproj
#   Create: $dev\KeycloakAspireStarter\global.json
```

Generate:

```bash
❯ dotnet new keycloak-aspire-starter -o $dev/KeycloakAspireStarter --EnableKeycloakImport
# The template "Keycloak Aspire Starter" was created successfully.
```

We want to enable automatically imported realm by adding `--EnableKeycloakImport` option.

Run:

```bash
❯ dotnet run --project $dev/KeycloakAspireStarter/AppHost/
# Building...
# info: Aspire.Hosting.DistributedApplication[0]
#       Aspire version: 8.0.1+a6e341ebbf956bbcec0dda304109815fcbae70c9
# info: Aspire.Hosting.DistributedApplication[0]
#       Distributed application starting.
# info: Aspire.Hosting.DistributedApplication[0]
#       Application host directory is: $dev\KeycloakAspireStarter\AppHost
# info: Aspire.Hosting.DistributedApplication[0]
#       Now listening on: http://localhost:15056
# info: Aspire.Hosting.DistributedApplication[0]
#       Distributed application started. Press Ctrl+C to shut down.
```

Additionally, open Keycloak master realm by navigating: <http://localhost:8080/>. Use `admin:admin` as credentials.
