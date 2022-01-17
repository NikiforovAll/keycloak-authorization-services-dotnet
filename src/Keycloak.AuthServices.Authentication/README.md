# Keycloak.AuthServices.Authentication

Adds Keycloak OICD Authentication.

The configuration is based on well-known "Keycloak OICD JSON" format. So all you need to do is:

1. Create "keycloak.json"
2. Drop "keycloak.json" to the root of a project.
3. Add configuration provider `ConfigureKeycloakConfigurationSource`
4. Add Authentication Services `AddKeycloakAuthentication`

Add configuration provider:

```csharp
// filename is optional, you can override it
host.ConfigureKeycloakConfigurationSource("keycloak.json");
```

Add and configure authentication services:

```csharp
services.AddKeycloakAuthentication(configuration, o =>
{
    o.RequireHttpsMetadata = false;
});
```

`AddKeycloakAuthentication` accepts <https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions>, so you can control *JwtBearer* authentication.

## Installation

For more details see an example - [Program.cs](../../samples/SimpleExample/Program.cs).

"workspace-authz" is a "confidential client".

![install-keycloak](../../assets/install-keycloak.png)

Here is how non-confidential client installation configuration look like:
```csharp
{
  "realm": "authz",
  "auth-server-url": "http://localhost:8088/auth/",
  "ssl-required": "external",
  "resource": "frontend",
  "public-client": true,
  "confidential-port": 0
}
```
