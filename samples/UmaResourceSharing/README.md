# UMA Resource Sharing Sample

This sample demonstrates the **UMA (User-Managed Access)** permission ticket flow using Keycloak Authorization Services for .NET.

## Architecture

```
┌──────────────┐     ┌──────────────────┐     ┌───────────┐
│  Client App  │────>│  Resource Server  │────>│ Keycloak  │
│ (Blazor+Mud) │<────│  (Minimal API)    │<────│           │
└──────────────┘     └──────────────────┘     └───────────┘
```

- **AppHost** — Aspire orchestration (Keycloak container + apps)
- **ResourceServer** — Minimal API protected with UMA. Uses `AddUmaPermissionTicketChallenge()` to return `WWW-Authenticate: UMA` on authorization failure
- **ClientApp** — Blazor Server with MudBlazor UI. `UmaTokenHandler` intercepts 401 UMA challenges, exchanges tickets for RPTs

## UMA Flow

1. User requests a protected resource
2. Resource Server returns `401` with `WWW-Authenticate: UMA as_uri="...", ticket="..."`
3. Client App extracts the ticket and exchanges it for an **RPT** via the Keycloak token endpoint (`grant_type=urn:ietf:params:oauth:grant-type:uma-ticket`)
4. Client App retries the request with the RPT

## Test Users

| Username | Password | Role              |
|----------|----------|-------------------|
| alice    | alice    | Resource Owner    |
| bob      | bob      | Requesting Party  |

## Running

```bash
dotnet run --project AppHost
```

The Aspire dashboard opens automatically. Navigate to the Client App URL to interact with the UMA flow.
