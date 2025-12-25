# Keycloak.AuthServices

[![Discord](https://img.shields.io/discord/1236946465318768670?color=blue&label=Chat%20on%20Discord)](https://discord.gg/S449PhBPRQ)
[![Build](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/dt/Keycloak.AuthServices.Authentication.svg)](https://nuget.org/packages/Keycloak.AuthServices.Authentication)
[![contributionswelcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/nikiforovall/keycloak-authorization-services-dotnet)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/nikiforovall/keycloak-authorization-services-dotnet/blob/main/LICENSE.md)

🔐 Easy Authentication and Authorization with Keycloak in .NET.

| Package                                | Version                                                                                                                                              | Description                                                  |
| -------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------ |
| `Keycloak.AuthServices.Authentication` | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Authentication.svg)](https://nuget.org/packages/Keycloak.AuthServices.Authentication) | Keycloak Authentication JWT + OIDC                           |
| `Keycloak.AuthServices.Authorization`  | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Authorization.svg)](https://nuget.org/packages/Keycloak.AuthServices.Authorization)   | Authorization Services. Use Keycloak as authorization server |
| `Keycloak.AuthServices.Sdk`            | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Sdk.svg)](https://nuget.org/packages/Keycloak.AuthServices.Sdk)                       | HTTP API integration with Keycloak                           |
| `Keycloak.AuthServices.Sdk.Kiota`      | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Sdk.Kiota.svg)](https://nuget.org/packages/Keycloak.AuthServices.Sdk.Kiota)           | HTTP API integration with Keycloak based on OpenAPI          |
| `Keycloak.AuthServices.OpenTelemetry`  | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.OpenTelemetry.svg)](https://nuget.org/packages/Keycloak.AuthServices.OpenTelemetry)   | OpenTelemetry support                                        |
| `Keycloak.AuthServices.Templates`      | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Templates.svg)](https://nuget.org/packages/Keycloak.AuthServices.Templates)           | `dotnet new` templates                                       |

## Documentation

For Developer Documentation see: <https://nikiforovall.github.io/keycloak-authorization-services-dotnet>

### API Reference

See: <https://nikiforovall.github.io/keycloak-authorization-services-dotnet-docs>

## Getting Started

Install packages:

```bash
dotnet add package Keycloak.AuthServices.Authentication
```

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

## Example - Add Authorization

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

### Example - Invoke Admin API

```csharp
var services = new ServiceCollection();
services.AddKeycloakAdminHttpClient(new KeycloakAdminClientOptions
{
    AuthServerUrl = "http://localhost:8080/",
    Realm = "master",
    Resource = "admin-api",
});

var sp = services.BuildServiceProvider();
var client = sp.GetRequiredService<IKeycloakRealmClient>();

var realm = await client.GetRealmAsync("Test");
```

## Build and Development

`dotnet cake --target build`

`dotnet cake --target test`

`dotnet pack -o ./Artefacts`
