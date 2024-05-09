# Protected Resource Builder

Using *Policies* is a standard and common approach. However, as the number of resources grows, organizing and managing these policies can become a challenge. To address this issue, we suggest using the **Protected Resource Builder** approach. This builder provides a convenient way to authorize resources, making it easier to manage and maintain authorization rules.

> [!TIP]
> 💡See [Resource Authorization](/examples/resource-authorization) Reference Solution to see a real world example of how to use Protected Resource Builder.

::: info
In most cases, we don't really need to build policies when working with *Authorization Server*, the authorization responsibility is delegated.
:::

## Add to your code

Here is an example of how to migrate from *Policies* to **Protected Resource Builder**:

```csharp
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(builder.Configuration);

services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(builder.Configuration);
services. // [!code --] 
    .AddAuthorizationBuilder() // [!code --]
    .AddPolicy( // [!code --]
        "WorkspaceRead", // [!code --]
        policy => policy.RequireProtectedResource( // [!code --]
            resource: "workspaces", // [!code --]
            scope: "workspace:read" // [!code --]
        ) // [!code --]
    ); // [!code --]

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/workspaces", () => "Hello World!").RequireAuthorization("WorkspaceRead");  // [!code --]
app.MapGet("/workspaces", () => "Hello World!") // [!code ++]
    .RequireProtectedResource("workspaces", "workspace:read"); // [!code ++]
app.Run();
```

With just one line, we can authorize access for `"workspaces#workspace:read"`, no policy registrations needed. 🚀

## Dynamic Resources

You can use path parameters in resource names by enclosing parameter name in '{}'.

In example below, we are substituting resource with value `{id}` with the actual value of path parameter.

<<< @/../tests/TestWebApi/Program.cs#SingleDynamicResourceSingleScopeSingleEndpoint

> [!NOTE]
> ☝️Currently, it is not possible to use body or query parameters. Please create an issue if it is something that you are interested in adding.

## Multiple Scopes

Here is how to check for multiple scopes simultaneously:

<<< @/../tests/TestWebApi/Program.cs#SingleResourceMultipleScopesSingleEndpointV2

Chained calls:

<<< @/../tests/TestWebApi/Program.cs#SingleResourceMultipleScopesSingleEndpoint

Endpoint hierarchy:

<<< @/../tests/TestWebApi/Program.cs#SingleResourceMultipleScopesEndpointHierarchy

Basically, you can define *Group-level* protected resources and *Endpoint-level* protected resources.

> [!NOTE]
> 💡 `RequireProtectedResource` is extension method over `IEndpointConventionBuilder`. It means you can use it outside of Minimal API. E.g.: MVC, RazorPages, etc. Here is the original design document: <https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/issues/87>

## Multiple Resources

You are not limited to use single resource:

<<< @/../tests/TestWebApi/Program.cs#MultipleResourcesMultipleScopesSingleEndpoint

Here is how to define top-level rule for "workspaces" in general and specific rule for particular workspace.

<<< @/../tests/TestWebApi/Program.cs#MultipleResourcesMultipleScopesEndpointHierarchy

## Ignore Resources

Similarly, to `AllowAnonymous` from `Microsoft.AspNetCore.Authorization` namespace, there are two methods to ignore what has been registered for protected resources: `IgnoreProtectedResources`, `IgnoreProtectedResource`.

```csharp
public static TBuilder AllowAnonymous<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder;
```

<<< @/../tests/TestWebApi/Program.cs#SingleResourceIgnoreProtectedResourceEndpointHierarchy

> [!TIP]
> See the integration tests [ProtectedResourcePolicyTests](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/tests/Keycloak.AuthServices.IntegrationTests/ProtectedResourcePolicyTests.cs) for more details.
