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

## Authentication

JWT token example:

```json
{
  "exp": 1642433984,
  "iat": 1642430384,
  "auth_time": 1642428609,
  "jti": "b89e599b-8c69-4707-b95d-af40b475de9c",
  "iss": "http://localhost:8088/auth/realms/authz",
  "aud": [
    "workspace-authz",
    "account"
  ],
  "sub": "eefae679-99a3-44d1-b746-67e357dca78a",
  "typ": "Bearer",
  "azp": "frontend",
  "session_state": "167dcc27-30d0-43d5-a80e-b291d4ffa824",
  "acr": "0",
  "realm_access": {
    "roles": [
      "SuperManager",
      "offline_access",
      "uma_authorization"
    ]
  },
  "resource_access": {
    "workspace-authz": {
      "roles": [
        "Manager"
      ]
    },
    "account": {
      "roles": [
        "manage-account",
        "manage-account-links",
        "view-profile"
      ]
    }
  },
  "scope": "openid email profile",
  "email_verified": false,
  "preferred_username": "test"
}
```
