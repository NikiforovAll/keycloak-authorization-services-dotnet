# Keycloak.AuthServices 

[![Build](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/actions/workflows/build.yml)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/nikiforovall/copy-paster/blob/main/LICENSE)
[![contributionswelcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/nikiforovall/copy-paster)

Package                                | Version                                                                                                                                  | Description
---------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------
`Keycloak.AuthServices.Authentication` | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Authentication.svg)](https://nuget.org/packages/Keycloak.AuthServices.Authentication)                         | Keycloak Authentication JWT + OICD
`Keycloak.AuthServices.Authorization`  | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Authorization.svg)](https://nuget.org/packages/Keycloak.AuthServices.Authorization) | Authorization Services. Use Keycloak as authorization server
`Keycloak.AuthServices.Sdk`            | [![Nuget](https://img.shields.io/nuget/v/Keycloak.AuthServices.Sdk.svg)](https://nuget.org/packages/Keycloak.AuthServices.Sdk)     | HTTP API integration with Keycloak

Easy Authentication and Authorization with Keycloak in .NET and ASP.NET Core.

## Keycloak.AuthServices.Authentication

[Keycloak.AuthServices.Authentication](src/Keycloak.AuthServices.Authentication/Keycloak.AuthServices.Authentication.csproj)

Add OpenID Connect + JWT Bearer token authentication.

```csharp
// add configuration from keycloak file
host.ConfigureKeycloakConfigurationSource("keycloak.json");
// add authentication services, OICD JwtBearerDefaults.AuthenticationScheme
services.AddKeycloakAuthentication(configuration, o =>
{
    o.RequireHttpsMetadata = false;
});
```

Client roles are automatically transformed into user role claims [KeycloakRolesClaimsTransformation](./src/Keycloak.AuthServices.Authentication/Claims/KeycloakRolesClaimsTransformation.cs).

See [Keycloak.AuthServices.Authentication - README.md](src/Keycloak.AuthServices.Authentication/README.md)

Keycloak installation file:

```jsonc
// confidential client
{
  "realm": "<realm>",
  "auth-server-url": "http://localhost:8088/auth/",
  "ssl-required": "external", // external | none
  "resource": "<clientId>",
  "verify-token-audience": true,
  "credentials": {
    "secret": ""
  }
}
// public client
{
  "realm": "<realm>",
  "auth-server-url": "http://localhost:8088/auth/",
  "ssl-required": "external",
  "resource": "<clientId>",
  "public-client": true,
  "confidential-port": 0
}
```

## Keycloak.AuthServices.Authorization

[Keycloak.AuthServices.Authorization](src/Keycloak.AuthServices.Authorization/Keycloak.AuthServices.Authorization.csproj)

```csharp
services.AddAuthorization(authOptions =>
{
    authOptions.AddPolicy("<policyName>", policyBuilder =>
    {
        // configure policies here
    });
}).AddKeycloakAuthorization(configuration);
```

See [Keycloak.AuthServices.Authorization - README.md](src/Keycloak.AuthServices.Authorization/README.md)

## Keycloak.AuthServices.Sdk

[Keycloak.AuthServices.Sdk](src/Keycloak.AuthServices.Sdk/Keycloak.AuthServices.Sdk.csproj)

Keycloak API clients.

| Service                          | Description                                                                  |
|----------------------------------|------------------------------------------------------------------------------|
| IKeycloakClient                  | Unified HTTP client - IKeycloakRealmClient, IKeycloakProtectedResourceClient |
| IKeycloakRealmClient             | Keycloak realm API                                                           |
| IKeycloakProtectedResourceClient | Protected resource API                                                       |
| IKeycloakProtectionClient        | Authorization server API, used by `AddKeycloakAuthorization`                 |

```csharp
// requires confidential client
services.AddKeycloakAdminHttpClient(keycloakOptions);

// based on token forwarding HttpClient middleware and IHttpContextAccessor
services.AddKeycloakProtectionHttpClient(keycloakOptions);
```

See [Keycloak.AuthServices.Sdk - README.md](src/Keycloak.AuthServices.Sdk/README.md)

## Build and Development

`dotnet cake --target build`

`dotnet pack -o ./Artefacts`

## Reference

* <https://github.com/thinktecture-labs/webinar-keycloak>
* <https://github.com/thinktecture-labs/webinar-keycloak-authorization>
* <https://github.com/elmankross/Jboss.AspNetCore.Authentication.Keycloak/>
* <https://github.com/mikemir/AspNetCore.KeycloakAuthentication/>
* <https://github.com/lvermeulen/Keycloak.Net>
* <https://github.com/keycloak/keycloak-documentation/blob/main/authorization_services/topics/service-authorization-uma-authz-process.adoc>
* <https://www.keycloak.org/docs/latest/authorization_services/index.html>
