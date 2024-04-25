# Keycloak.AuthServices

[![Build](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/workflows/build.yml)
[![CodeQL](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/workflows/codeql-analysis.yml)
[![NuGet](https://img.shields.io/nuget/dt/Keycloak.AuthServices.Authentication.svg)](https://nuget.org/packages/Keycloak.AuthServices.Authentication)
[![contributionswelcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/nikiforovall/keycloak-authorization-services-dotnet)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/nikiforovall/keycloak-authorization-services-dotnet/blob/main/LICENSE.md)

Easy Authentication and Authorization with Keycloak in .NET and ASP.NET Core.

| Package                                | Version                                                                                                                                              | Description                                                  |
| -------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------ |
| `Keycloak.AuthServices.Authentication` | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Authentication.svg)](https://nuget.org/packages/Keycloak.AuthServices.Authentication) | Keycloak Authentication JWT + OICD                           |
| `Keycloak.AuthServices.Authorization`  | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Authorization.svg)](https://nuget.org/packages/Keycloak.AuthServices.Authorization)   | Authorization Services. Use Keycloak as authorization server |
| `Keycloak.AuthServices.Sdk`            | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Sdk.svg)](https://nuget.org/packages/Keycloak.AuthServices.Sdk)                       | HTTP API integration with Keycloak                           |

[![GitHub Actions Build History](https://buildstats.info/github/chart/nikiforovall/keycloak-authorization-services-dotnet?branch=main&includeBuildsFromPullRequest=false)](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions)

## Documentation

See the docs: <https://nikiforovall.github.io/keycloak-authorization-services-dotnet/>.

## Installation

```bash
dotnet add package Keycloak.AuthServices.Authentication
dotnet add package Keycloak.AuthServices.Authorization
dotnet add package Keycloak.AuthServices.Sdk
```

## Getting Started

See the docs: <https://nikiforovall.github.io/keycloak-authorization-services-dotnet/>.

```csharp
// Program.cs
using Keycloak.AuthServices.Authentication; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration); 
builder.Services.AddAuthorization(); 

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapGet("/", () => "Hello World!").RequireAuthorization(); 

app.Run();
```

In this example, configuration is based on `appsettings.json`.

```jsonc
//appsettings.json
{
    "Keycloak": {
        "realm": "Test",
        "auth-server-url": "http://localhost:8080/",
        "ssl-required": "none",
        "resource": "test-client",
        "verify-token-audience": false,
        "credentials": {
        "secret": ""
        },
        "confidential-port": 0
    }
}
```

You can always fetch the corresponding authentication options like this:

```csharp
var authenticationOptions = configuration
    .GetSection(KeycloakAuthenticationOptions.Section)
    .Get<KeycloakAuthenticationOptions>(KeycloakFormatBinder.Instance);

services.AddKeycloakAuthentication(authenticationOptions);
```

Note, the default case convention for `KeycloakAuthenticationOptions` configuration is PascalCase, but you can change it by specifying `KeycloakFormatBinder.Instance` binding options to retrieve it based on original kebab-case. See [changes in 2.0.0](https://nikiforovall.github.io/keycloak-authorization-services-dotnet/keycloak-authorization-services-dotnet/migration.html#key-changes-in-2-0-0)

## Example Authorization

With `Keycloak.AuthServices.Authorization`, you can implement role-based authorization in your application. This package allows you to define policies based on roles. Also, you can use Keycloak as Authorization Server. It is a powerful way to organize and apply authorization polices centrally.

```csharp
var builder = WebApplication.CreateBuilder(args);

var host = builder.Host;
var configuration = builder.Configuration;
var services = builder.Services;

services.AddKeycloakWebApiAuthentication(configuration);

services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminAndUser", builder =>
        {
            builder
                .RequireRealmRoles("User") // Realm role is fetched from token
                .RequireResourceRoles("Admin"); // Resource/Client role is fetched from token
        });
    })
    .AddKeycloakAuthorization(configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/hello", () => "[]")
    .RequireAuthorization("AdminAndUser");

app.Run();
```

## Build and Development

`dotnet cake --target build`

`dotnet pack -o ./Artefacts`

## Blog Posts

For more information and real world examples, please see my blog posts related to Keycloak and .NET <https://nikiforovall.github.io/tags.html#keycloak-ref>

* <https://nikiforovall.github.io/aspnetcore/dotnet/2022/08/24/dotnet-keycloak-auth.html>
* <https://nikiforovall.github.io/dotnet/keycloak/2022/12/28/keycloak-authorization-server.html>
* <https://nikiforovall.github.io/blazor/dotnet/2022/12/08/dotnet-keycloak-blazorwasm-auth.html>

## Reference

* <https://github.com/keycloak/keycloak-documentation/blob/main/authorization_services/topics/service-authorization-uma-authz-process.adoc>
* <https://www.keycloak.org/docs/latest/authorization_services/index.html>
