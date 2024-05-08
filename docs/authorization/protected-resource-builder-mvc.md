# Protected Resource Builder MVC

This article demonstrates how to add **Protected Resource Builder** to an MVC project. Here is how to add resource authorization:

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

<<< @/../tests/TestWebApiWithControllers/Controllers/WorkspacesController.cs#WorkspaceAPI{3,7,11,15,19 cs:line-numbers}
