# Introduction

Welcome to the **Keycloak.AuthServices** documentation!

## What is Keycloak?

[Keycloak](https://www.keycloak.org/) is an open-source identity and access management solution. It provides features like single sign-on, user authentication, and authorization for web applications and services. Keycloak allows you to secure your applications by managing user identities, roles, and permissions. It supports various authentication mechanisms, including username/password, social logins, and multi-factor authentication. Additionally, Keycloak provides integration with popular identity providers like Google, Facebook, and LDAP directories. It is widely used in enterprise applications to ensure secure and seamless user authentication and authorization.

> [!NOTE]
> Keycloak is a Cloud Native Computing Foundation incubation project

Here is good [getting-started](https://www.keycloak.org/getting-started/getting-started-docker) from the Keycloak's official [documentation](https://www.keycloak.org/documentation) website.

## What is Keycloak.AuthServices?

Keycloak.AuthServices is a [family of packages](https://www.nuget.org/packages?q=Keycloak.AuthServices) that provides you everything you need to integrate and use Keycloak. From Single-Sign On and OpenId Connect (OIDC) to Keycloak Admin REST API integration.

## Packages

| Package                                                                                                     | Description                                                                                                                                                                                                                                                                                                                                                                     |
| ----------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Keycloak.AuthServices.Authentication](https://www.nuget.org/packages/Keycloak.AuthServices.Authentication) | As an OAuth2, OpenID Connect, and SAML compliant server, Keycloak can secure any application and service as long as the technology stack they are using supports any of these protocols. This library provides AspNetCore integration for JwtBearer and OpenIdConnect.                                                                                                          |
| [Keycloak.AuthServices.Authorization](https://www.nuget.org/packages/Keycloak.AuthServices.Authorization)   | Provides Role-Based Access Control (RBAC) and integration with the Keycloak Authorization Server.                                                                                                                                                                                                                                                                               |
| [Keycloak.AuthServices.Sdk](https://www.nuget.org/packages/Keycloak.AuthServices.Sdk)                       | Provides integration with the [Keycloak Admin REST API](https://www.keycloak.org/docs-api/21.1.1/rest-api/), allowing you to manage users, groups, and realms programmatically. Provides integration with the [Protection REST API](https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_api) - a UMA-compliant set of endpoints. |
| [Keycloak.AuthServices.Sdk.Kiota](https://www.nuget.org/packages/Keycloak.AuthServices.Sdk.Kiota)     | Provides integration with the [Keycloak Admin REST API](https://www.keycloak.org/docs-api/21.1.1/rest-api/). The client is generated based on Open API Spec |
