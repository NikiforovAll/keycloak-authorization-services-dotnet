# Access Token Management

Keycloak comes with a fully functional Admin REST API with all features provided by the Admin Console. To invoke the API you need to obtain an access token with the appropriate permissions.

Please refer to [official docs](https://www.keycloak.org/docs/latest/server_development/#authenticating-with-a-service-account) for more details on how to setup Service Account.

## Configure Service Account

We need to create a *Service Account* in **master** realm, configure a special *Audience Mapper* that adds **security-admin-console** audience to the token, and assign *Service Account Role* - **"admin"**.

Create a service account client called **"admin-api"** and enable *Client Authentication* and *Service Account Roles*.

Then, [download adapter config](/configuration/configuration-keycloak#download-adapter-config) from Keycloak and added it to "appsettings.json" to "Keycloak section. Here is how it looks like:

```json
{
  "Keycloak": {
    "realm": "master",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "admin-api",
    "credentials": {
      "secret": "k9LYTWKfbNOyfzFt2ZZsFl3Z4x4aAecf"
    },
    "confidential-port": 0
  }
}
```

💡 See [admin-api export file](/admin-rest-api/admin-api.spec) if you want to import it and see how it looks in Keycloak.

## Add Token Management

Luckily, there is a production-ready library called [DuendeSoftware/Duende.AccessTokenManagement](https://github.com/DuendeSoftware/Duende.AccessTokenManagement) that retrieves and caches tokens.

To install it run:

```bash
dotnet add package Duende.AccessTokenManagement
```

See [the docs](https://github.com/DuendeSoftware/Duende.AccessTokenManagement/wiki/worker-applications) on how to configure and use this library to use Service Accounts.

### Example

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/Admin/KeycloakRealmClientTests.cs#GetRealmAsync_RealmExists_Success {3-14,18 cs:line-numbers}
