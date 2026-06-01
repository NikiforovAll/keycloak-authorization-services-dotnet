# Protected Resource Builder MVC

This article demonstrates how to add **Protected Resource Builder** to an MVC project. Here is how to add resource authorization:

> [!TIP]
> 💡See [Resource Authorization](/examples/resource-authorization) Reference Solution to see a real world example of how to use Protected Resource Builder.

## Register Protected Resource Builder for MVC

```csharp:line-numbers
services.AddControllers(options => options.AddProtectedResources()); // [!code focus]

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(context.Configuration);

services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(context.Configuration);
```

## Apply `ProtectedResourceAttribute`

> [!IMPORTANT]
> Make sure that the 'roles' scope under Clients > 'ClientId' > Client Scopes is set to Default else you will receive a Not Authorized Error. Alternatively requesting the 'roles' scope will also work.


<<< @/../tests/TestWebApiWithControllers/Controllers/WorkspacesController.cs#WorkspaceAPI{3,7,11,15,19 cs:line-numbers}
