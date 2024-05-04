# HTTP Admin REST API

[Keycloak.AuthServices.Sdk](https://www.nuget.org/packages/Keycloak.AuthServices.Sdk) provides a typed HTTP Client to work with Keycloak Admin HTTP REST API.


The Admin REST API in Keycloak provides a programmatic way to manage and administer Keycloak instances. It allows you to perform various administrative tasks such as creating and managing realms, users, roles, clients, and more. To interact with the Admin REST API, you can use HTTP requests to send commands and retrieve data. The API follows the REST architectural style and is designed to be simple and intuitive to use.

> [!NOTE]
> See full list of API endpoints - [Admin REST API](https://www.keycloak.org/docs-api/latest/rest-api/#_overview)

Keycloak provides a comprehensive set of endpoints that cover a wide range of administrative operations. These endpoints are organized into different resource types, such as realms, users, roles, and clients, making it easy to navigate and manipulate the Keycloak configuration.

❗ To get started with the Admin REST API, you need to authenticate and obtain an access token. Once you have the token, you can include it in the Authorization header of your HTTP requests to authenticate and authorize your API calls.

> [!NOTE]
> See [Admin REST API - Server Development](https://www.keycloak.org/docs/latest/server_development/#admin-rest-api) documentation for more details.

## Add to your code

Install [Keycloak.AuthServices.Sdk](https://www.nuget.org/packages/Keycloak.AuthServices.Sdk):

```bash
dotnet add package Keycloak.AuthServices.Sdk
```

> [!IMPORTANT]
> Admin API is protected so you need to acquire access token somehow. See [Access Token Management](/admin-rest-api/access-token)

You can use `IKeycloakClient` from Web APIs, Worker, Console apps, etc. It is fully integrated with [IHttpClientFactory](https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory) and therefore you don't need to worry about `HttpClient` lifetime and the way you work with it.

To add it to DI, you can use convenience extensions method `AddKeycloakAdminHttpClient`:

```csharp
public static IHttpClientBuilder AddKeycloakAdminHttpClient(
    this IServiceCollection services,
    KeycloakAdminClientOptions keycloakAdminClientOptions,
    Action<HttpClient>? configureClient = default
)
```

It registers typed client with umbrella interface `IKeycloakClient` and adds `KeycloakAdminClientOptions` to DI so you can use it as `IOptions<KeycloakAdminClientOptions>` in your code.

> [!NOTE]
> 💡 `AddKeycloakAdminHttpClient` returns `IHttpClientBuilder` so you can proceed and configure underlying `HttpClient`.
> 
>  For example, here is how to add Polly and some custom delegating handlers:
>```csharp
> services
>   .AddKeycloakAdminHttpClient(configuration)
>   .AddStandardResilienceHandler()
>   .AddHttpMessageHandler<TimingHandler>()
>   .AddHttpMessageHandler<ValidateHeaderHandler>();
>```

## Console App

Here is how to use it from a Console App:

```csharp
var services = new ServiceCollection();

var keycloakOptions = new KeycloakAdminClientOptions
{
    AuthServerUrl = "http://localhost:8080/",
    Realm = "master",
    Resource = "admin-api",
};
services.AddKeycloakAdminHttpClient(keycloakOptions);

var sp = services.BuildServiceProvider();
var client = sp.GetRequiredService<IKeycloakClient>();

var realm = await client.GetRealmAsync("Test");
```

> [!WARNING]
> In the code above the **key part** is missing - Authentication and Authorization. Because of that, you will receive **401 (Unauthorized)**. In the [next section](/admin-rest-api/access-token) I will show you how to obtain access token and successfully invoke Admin API endpoints.

Here is `IKeycloakClient`:

<<< @/../src/Keycloak.AuthServices.Sdk/Admin/IKeycloakClient.cs
