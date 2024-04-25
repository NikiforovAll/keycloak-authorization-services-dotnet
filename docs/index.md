---
layout: home

hero:
  name: "Keycloak AuthServices .NET"
  text: ""
  tagline: "Everything you need know to work with Keycloak in .NET"
  actions:
    - theme: brand
      text: Getting Started
      link: /getting-started
    - theme: alt
      text: Configuration
      link: /configuration
    - theme: alt
      text: Authentication
      link: /authentication
    - theme: alt
      text: Authorization
      link: /authorization
    - theme: alt
      text: Admin REST HTTP API
      link: /admin-rest-api

features:
  - title: 🔒Authentication
    details: Keycloak.AuthServices provides robust authentication mechanisms for both web APIs and web applications. For web APIs, it supports JWT Bearer token authentication, which allows clients to authenticate to the API by providing a JWT token in the Authorization header of their requests. For web applications, it supports OpenID Connect, a simple identity layer on top of the OAuth 2.0 protocol, which allows clients to verify the identity of the end-user based on the authentication performed by an authorization server, as well as to obtain basic profile information about the end-user.
  - title: 🗝️Authorization
    details: Keycloak.AuthServices allows authorization based role-based access control (RBAC). It also enables Keycloak to function as an Authorization Server, enforcing execution policies based on permissions of authenticated users. This ensures secure access to resources, with the ability to define fine-grained permissions and policies.
  - title: ⚙️ HTTP REST Admin API integration
    details: Keycloak.AuthServices includes an SDK client that integrates with the Keycloak Admin HTTP REST API. This allows developers to manage and configure Keycloak instances programmatically, providing a high degree of flexibility and automation.
---
