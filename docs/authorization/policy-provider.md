# Policy Provider

You can add automatic policy registration via `ProtectedResourcePolicyProvider` based on a simple convention. It means that you don't need to register policies manually.

The expected policy format is: `<resource>#<scope1>,<scope2>`, e.g: `my-workspace#workspaces:read,workspaces:delete`.

## Usage

The example below demonstrates how to automatically register policies based on policy name:

```csharp:line-numbers
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(context.Configuration);

services
    .AddAuthorization()
    .AddKeycloakAuthorization();

services.AddAuthorizationServer(options => { // [!code highlight]
    configuration.BindKeycloakOptions(options); // [!code highlight]
    options.UseProtectedResourcePolicyProvider = true; // [!code highlight]
}); // [!code highlight]

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapGet("/", () => "Hello World!")
    .RequireAuthorization("my-workspace#workspaces:read"); // [!code highlight]

app.Run();
```

## Extensibility

The `ProtectedResourcePolicyProvider` delegates policy construction to `IProtectedResourcePolicyBuilder`. A `DefaultProtectedResourcePolicyBuilder` is registered automatically, but you can replace it with your own implementation — for example, to add caching:

```csharp
public class CachingPolicyBuilder : IProtectedResourcePolicyBuilder
{
    private readonly ConcurrentDictionary<string, AuthorizationPolicy?> cache = new();
    private readonly DefaultProtectedResourcePolicyBuilder inner = new();

    public AuthorizationPolicy? Build(string policyName) =>
        cache.GetOrAdd(policyName, inner.Build);
}
```

Register it before calling `AddAuthorizationServer`:

```csharp
services.AddSingleton<IProtectedResourcePolicyBuilder, CachingPolicyBuilder>();

services.AddAuthorizationServer(options =>
{
    configuration.BindKeycloakOptions(options);
    options.UseProtectedResourcePolicyProvider = true;
});
```

Because `DefaultProtectedResourcePolicyBuilder` is registered with `TryAddSingleton`, your registration takes precedence.
