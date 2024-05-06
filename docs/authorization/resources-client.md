# Use IAuthorizationServerClient

You can use `IAuthorizationServerClient` directly in your code, but beware that it depends on `IHttpContextAccessor`.

> [!TIP]
> In situations when you want to authorize access in your code I advise you to use [Microsoft.AspNetCore.Authorization.IAuthorizationService](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authorization.iauthorizationservice) and not `IAuthorizationServerClient` directly. In this case, you still need to define policies and use them by name like this:
>
> ```csharp
> var result = await authorizationService.AuthorizeAsync(claimsPrincipal, policyName);
> ```

If you need more control and for some reason you don't want to use *Policies* and `IAuthorizationService` you can use `IAuthorizationServerClient` directly.

## Add to your code

`AddAuthorizationServer` adds typed HTTP client - `IAuthorizationServerClient`

```csharp
public static IHttpClientBuilder AddAuthorizationServer(
    this IServiceCollection services,
    Action<KeycloakAuthorizationServerOptions> configureKeycloakOptions,
    Action<HttpClient>? configureClient = default
)
```

Here is an example of a endpoint:

```csharp

app.MapGet("RunAuthorizationServerClient", RunAuthorizationServerClient);

async Task<IResult> RunAuthorizationServerClient(
    [FromQuery] string resource,
    [FromQuery] string scopes,
    IAuthorizationServerClient client
)
{
    var success = await client.VerifyAccessToResource(resource, scopes);

    return success ? TypedResults.Ok() : TypedResults.Forbid();
}
```

## Use outside `IHttpContextAccessor`

There is a way to add just HTTP Client without all other Authorization Server capabilities provided by library.

## Add to your code

`AddAuthorizationServerClient` adds typed HTTP client - `IAuthorizationServerClient`

```csharp
public static IHttpClientBuilder AddAuthorizationServerClient(
    this IServiceCollection services,
    Action<HttpClient>? configureClient = default
)
```

Use it:

```csharp
var client = sp.GetRequiredService<IAuthorizationServerClient>();

bool success = await client.VerifyAccessToResource(
    "my-workspace",
    "workspace:read,workspace:delete"
);
```

> [!WARNING]
> `AddAuthorizationServerClient` doesn't add header propagation because it relies on `IHttpContextAccessor`, so you will need to provide access token using your custom implementation of [DelegatingHandler](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.delegatinghandler)

Something like:

```csharp
public class TokenDelegatingHandler : DelegatingHandler
{
    private readonly ITokenProvider tokenProvider;

    public TokenDelegatingHandler(ITokenProvider tokenProvider) =>
        this.tokenProvider = tokenProvider;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        string token = await tokenProvider.GetTokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken);
    }
}
```
