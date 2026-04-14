# UMA Resource Sharing Samples

This directory contains samples demonstrating **UMA 2.0 (User-Managed Access)** using Keycloak Authorization Services for .NET.

## What is UMA?

**User-Managed Access (UMA)** is an OAuth-based protocol that enables a resource owner to control access to their protected resources. Unlike traditional OAuth where the resource server decides access, UMA introduces an asynchronous approval model — a user can request access to a resource, and the resource owner can review and approve (or deny) that request later.

## Samples

| Sample                                                  | Architecture                      | Description                                                                                                                                                                                            |
| ------------------------------------------------------- | --------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| [Blazor + Resource Server](ClientApp/README.md)         | Blazor Server → WebAPI → Keycloak | Two-project setup. `UmaTokenHandler` (DelegatingHandler) transparently handles 401 UMA challenges between the Blazor client and a separate Resource Server API.                                        |
| [Razor Pages (Self-Contained)](RazorPagesApp/README.md) | Razor Pages → Keycloak            | Single-project setup. The app **is** the resource server — uses `IAuthorizationService` to check permissions inline and `IKeycloakProtectionClient` for permission ticket management. No separate API. |

## Shared Infrastructure

- **AppHost** — Aspire orchestration (Keycloak container + apps)
- **ServiceDefaults** — OpenTelemetry, health checks, service discovery
- **KeycloakConfiguration** — Pre-configured realm, clients, resources, users

## Test Users

| Username | Password | Role                                                  |
| -------- | -------- | ----------------------------------------------------- |
| `alice`  | `alice`  | Resource Owner — has full access to `shared-document` |
| `bob`    | `bob`    | Requesting Party — no access by default               |

## Running

```bash
dotnet run --project AppHost
```

The Aspire dashboard opens automatically. Navigate to either app to interact with the UMA flow.
