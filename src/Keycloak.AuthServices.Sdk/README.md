# Keycloak.AuthServices.Sdk

Keycloak API clients. Adds typed `HttpClient` to integrate with keycloak API.

| Service                          | Description                                                                  |
|----------------------------------|------------------------------------------------------------------------------|
| IKeycloakClient                  | Unified HTTP client - IKeycloakRealmClient, IKeycloakProtectedResourceClient |
| IKeycloakRealmClient             | Keycloak realm API                                                           |
| IKeycloakProtectedResourceClient | Protected resource API                                                       |
| IKeycloakProtectionClient        | Authorization server API, used by `AddKeycloakAuthorization`                 |

```csharp
// requires confidential client
services.AddKeycloakAdminHttpClient(keycloakOptions);

// based on token forwarding HttpClient middleware
services.AddKeycloakProtectionHttpClient(keycloakOptions);
```
