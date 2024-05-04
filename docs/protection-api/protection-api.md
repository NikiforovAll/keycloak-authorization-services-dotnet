# Protection API

The Protection API provides a UMA-compliant set of endpoints providing:

* Resource Management - With this endpoint, resource servers can manage their resources remotely and enable policy enforcers to query the server for the resources that need protection.

* Permission Management - In the UMA protocol, resource servers access this endpoint to create permission tickets. Keycloak also provides endpoints to manage the state of permissions and query permissions.

* Policy API - Keycloak leverages the UMA Protection API to allow resource servers to manage permissions for their users. In addition to the Resource and Permission APIs, Keycloak provides a Policy API from where permissions can be set to resources by resource servers on behalf of their users.

See documentation for more details: <https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_api>

## Add to your code

Install [Keycloak.AuthServices.Sdk](https://www.nuget.org/packages/Keycloak.AuthServices.Sdk):

```bash
dotnet add package Keycloak.AuthServices.Sdk
```

> [!IMPORTANT]
> Protection API is protected so you need to acquire access token somehow. See [Access Token Management](/admin-rest-api/access-token)

You can use `IKeycloakProtectionClient` from Web APIs, Worker, Console apps, etc. It is fully integrated with [IHttpClientFactory](https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory) and therefore you don't need to worry about `HttpClient` lifetime and the way you work with it.

To add it to DI, you can use convenience extensions method `AddKeycloakProtectionHttpClient`:

```csharp
public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
    this IServiceCollection services,
    KeycloakProtectionClientOptions keycloakOptions,
    Action<HttpClient>? configureClient = default
)
```

Here is how to use it:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/Protection/KeycloakProtectionClientTests.cs#GetResourcesAsync_Success {17,21,23 cs:line-numbers}
