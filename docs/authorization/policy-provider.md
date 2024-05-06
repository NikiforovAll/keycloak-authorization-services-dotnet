# Policy Provider

You can add automatic policy registration via `ProtectedResourcePolicyProvider` based on a simple convention. It means that you don't need to register policies manually.

The expected policy format is: `<resource>#<scope1>,<scope2>`, e.g: `my-workspace:workspaces:read,workspaces:delete`.

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
    .RequireAuthorization("my-workspace:workspaces:read"); // [!code highlight]

app.Run();
```
