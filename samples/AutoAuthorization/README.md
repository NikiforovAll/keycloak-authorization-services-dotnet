# URI based keycloak authorization

The extension is cabable of performing an automated authorization based on the uri, which is called. Therefore no additional authorization configuration will be needed in the compiled sources. The authorization to and protection of the endpoints is completely configured in keycloak as authentication and authorization provider. This has the advantage, that it can be changed completely without touching the API server, which is providing those endpoints.

In this example the details about how this works will be shown.

## Basic Principle

**URI = Resource name**

Essentially the URI, which is protected by the resource, is used as the resource's name. This way the API server can use the called URI path from the endpoint as resourcename for an authorization request. 

This has implication of the appicability of the URI based keycloak authorization:
Assuming the endpoint "http://example.com/api/endpoint" should be protected by authorization the following works
| URI example                              | Works     |
|------------------------------------------|-----------|
| http://example.com/api/endpoint          | yes       |
| http://example.com/api/endpoint/         | yes       |
| http://example.com/api/endpoint?var=1    | yes       |
| http://example.com/api/endpoint/var/1    | no        |

## Run example

### Prerequisition
KeyCloak server (tested with version 21.0.2) is running.

### Import demo realm into Keycloak
Being in the Keycloak Adminpanel create a Realm from [this config file](realm-export.json)
(Accept suggested realm name or adapt [settings](appsettings.Development.json) and use appropriate URI to fetch token in later step)

### Create User
Create a user with name {username} in the new realm. The user needs the role **usermanager** from the test-api client assigned.
Set credentials {password} for it.

### Fetch token
Do the following POST request:
```
curl -X POST \
  http://localhost:8080/realms/Test/protocol/openid-connect/token \
  -H "Content-Type: application/x-www-form-urlencoded" \
  --data "grant_type=password" \
  --data "client_id=test-client" \
  --data "username={username}" \
  --data "password={password}"
```
Copy the returned access_token

### Test endpoint
Execute `dotnet run` in root dir of this example.
Doing the following GET request:
```
curl -X GET \
  https://localhost:7182/api/auto-auth \
  -H "Authorization: Bearer ${access_token}" \
```
should result in `OK("Auto Authorization works.")` being returned.

## Usage

### Setup authentication

The following snippet is just an example of how this could look like. Configure it as you like.

```csharp
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeycloakAuthentication(configuration);

builder.Services.AddAuthorization(authorizationOptions => authorizationOptions.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build())
        .AddKeycloakAuthorization(configuration);

...

app.UseAuthentication();

app.UseAuthorization();

...
```

### Add Middleware

Add to your Program.cs
```csharp
using Keycloak.AuthServices.Authorization;

app.UseUriBasedKeycloakEndpointProtection();
```

### Configure Keycloak resources

The Keycloak resource must be set up like this:
```json
"authorizationSettings": {
        "allowRemoteResourceManagement": true,
        "policyEnforcementMode": "ENFORCING",
        "resources": [
          {
            "name": "/api/auto-auth",
            "type": "urn:test-api:resources:usermanagement",
            "ownerManagedAccess": true,
            "displayName": "",
            "attributes": {},

            "uris": [],
            "scopes": [
              {
                "name": "GET"
              }
            ],
            "icon_uri": ""
          }
        ],
        "policies": [
          {
            "name": "allow usermanager",
            "description": "",
            "type": "role",
            "logic": "POSITIVE",
            "decisionStrategy": "UNANIMOUS",
            "config": {
              "roles": "[{\"id\":\"test-api/usermanager\",\"required\":true}]"
            }
          },
          {
            "name": "allow role usermanager to urn:test-api:resources:usermanagement",
            "description": "",
            "type": "resource",
            "logic": "POSITIVE",
            "decisionStrategy": "UNANIMOUS",
            "config": {
              "defaultResourceType": "urn:test-api:resources:usermanagement",
              "applyPolicies": "[\"allow usermanager\"]"
            }
          }
        ],
        "scopes": [
          {
            "name": "GET",
            "iconUri": "",
            "displayName": "GET"
          }
        ],
        "decisionStrategy": "UNANIMOUS"
      }
```
Where `/api/auto-auth` is the endpoint, which should be accessible only for users with the role `test-api/usermanager`.
