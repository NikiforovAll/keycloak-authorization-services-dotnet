---
layout: home

hero:
  name: "Keycloak AuthServices .NET"
  text: ""
  tagline: "Everything you need know to work with Keycloak in .NET"
  actions:
    - theme: brand
      text: Getting Started
      link: /introduction
    - theme: alt
      text: Authentication
      link: /configuration/configuration-authentication
    - theme: alt
      text: Authorization
      link: /authorization/authorization-server
    - theme: alt
      text: HTTP Admin REST API
      link: /admin-rest-api/admin-rest-api

features:
  - title: 🔒Authentication
    details: Keycloak.AuthServices provides robust authentication mechanisms for both web APIs and web applications. For web APIs, it supports JWT Bearer token authentication, which allows clients to authenticate to the API by providing a JWT token in the Authorization header of their requests. For web applications, it supports OpenID Connect, a simple identity layer on top of the OAuth 2.0 protocol, which allows clients to verify the identity of the end-user, obtain basic profile information about the end-user, etc.
  - title: 🗝️Authorization
    details: Keycloak.AuthServices allows authorization based role-based access control (RBAC). It also enables Keycloak to function as an Authorization Server, enforcing execution policies based on permissions of authenticated users. This ensures secure access to resources, with the ability to define fine-grained permissions and policies.
  - title: ⚙️ HTTP Admin REST API integration
    details: Keycloak.AuthServices includes an SDK client that integrates with the Keycloak Admin API. This allows developers to manage and configure Keycloak instances programmatically, providing a high degree of flexibility and automation.
---
