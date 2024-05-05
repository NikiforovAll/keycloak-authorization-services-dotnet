# Generated Client - Kiota <Badge type="warning" text="preview" />

This `KeycloakAdminApiClient` was generated using the *Kiota* library.

> [!NOTE]
> Kiota is a powerful command line tool developed by Microsoft that simplifies the process of generating API clients for calling any OpenAPI-described API. See [OpenAPI Support](/admin-rest-api/admin-api-openapi) for more details.

```csharp
/// <summary>
/// Adds <see cref="KeycloakAdminApiClient"/> for Keycloak Admin API.
/// </summary>
/// <returns>The IHttpClientBuilder for further configuration.</returns>
public static IHttpClientBuilder AddKeycloakAdminHttpClient(
    this IServiceCollection services,
    IConfiguration configuration,
    Action<HttpClient>? configureClient = default,
    string keycloakClientSectionName = KeycloakAdminClientOptions.Section
);
```

>[!TIP]
> Kiota supports partial client generation, you can generate only the functionality you need. In this case, you can use source code of this library as an example.

## Example

This is an example of how to use `KeycloakAdminApiClient` together with [Access Token Management](/admin-rest-api/access-token).

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/AdminKiota/KeycloakRealmKiotaClientTests.cs#GetRealmAsyncKiota_RealmExists_Success
