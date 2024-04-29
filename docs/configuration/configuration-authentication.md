# Configure Authentication

**Keycloak.AuthServices.Authentication** provides robust authentication mechanisms for both web APIs and web applications. For web APIs, it supports JWT Bearer token authentication, which allows clients to authenticate to the API by providing a JWT token in the Authorization header of their requests. For web applications, it supports OpenID Connect, a simple identity layer on top of the OAuth 2.0 protocol

---

[[toc]]

## Web API

Here is what library does for you:

* Adds and configures `AddJwtBearer` based on provided configuration.
* Registers `IOptions<KeycloakAuthenticationOptions>` and `IOptions<JwtBearerOptions>`.
* Registers `KeycloakRolesClaimsTransformation` so special Keycloak role claims are added to `ClaimsPrincipal`. See [Keycloak Claims Transformation](#keycloak-claims-transformation)

### ServiceCollection Extensions

The **Keycloak.AuthServices.Authentication** library will automatically retrieve the configuration values under the "Keycloak" section. You can access these values in your code to configure the authentication process. This section is likely defined in your application's configuration file, such as *appsettings.json*

```json
{
  "Keycloak": {
    "realm": "Test",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "test-client",
    "verify-token-audience": false,
    "credentials": {
      "secret": ""
    }
  }
}
```

Simply add:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiAuthenticationTests.cs#AddKeycloakWebApiAuthentication_FromConfiguration

This default assumption of the "Keycloak" section allows you to easily configure the library without explicitly specifying the section name every time. However, if you have a different section name or want to customize the configuration retrieval process, the library provides additional methods and options to handle that.

::: code-group
<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiAuthenticationTests.cs#AddKeycloakWebApiAuthentication_FromConfigurationSection [specify configuration section]

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiAuthenticationTests.cs#AddKeycloakWebApiAuthentication_FromConfiguration2 [specify section name]

:::

Not everything you want to do can be configured with `KeycloakAuthenticationOptions`, for more fine-grained configuration use next method overload that takes `Action<JwtBearerOptions>`:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiAuthenticationTests.cs#AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides

Typically, ASP.NET Core expects to find these (default) options under the `Authentication:Schemes:{SchemeName}`. See [Configuring Authentication **Strategies**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-8.0#configuring-authentication-strategy) for more details. Here is how to configure [JwtBearerOptions](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.jwtbeareroptions):

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiAuthenticationTests.cs#AddKeycloakWebApiAuthentication_FromConfigurationWithOverrides

```json
{
  "Keycloak": {
    "ssl-required": "internal",
    "resource": "test-client",
    "verify-token-audience": true,
    "credentials": {
      "secret": "Tgx4lvbyhho7oNFmiIupDRVA8ioQY7PW"
    },
    "confidential-port": 0
  },
  "Authentication": {
    "DefaultScheme": "Bearer",
    "Schemes": {
      "Bearer": {
        "ValidAudiences": [
          "default-test-client-new"
        ],
        "RequireHttpsMetadata": true,
        "Authority": "http://localhost:8080/realms/DefaultTest",
        "TokenValidationParameters": {
          "ValidateAudience": false
        }
      }
    }
  }
}
```

> [!NOTE]
> `KeycloakAuthenticationOptions` ("Keycloak") takes precedence over `Authentication:Schemes:{SchemeName}` ("Bearer") in the case of default configuration

### AuthenticationBuilder Extensions

For situations when you want to override *Authentication Scheme* or you just prefer more verbose way of defining your project's *Authentication* you can use `AuthenticationBuilder` extension methods:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiTests.cs#AddKeycloakWebApiAuthentication_FromConfiguration

Use `IConfigurationSection`:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiTests.cs#AddKeycloakWebApiAuthentication_FromConfigurationSection

Inline declaration:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiTests.cs#AddKeycloakWebApiAuthentication_FromInline

Inline declaration with `JwtBearerOptions` overrides:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiTests.cs#AddKeycloakWebApiAuthentication_FromInline2

## Web App

In the context of web development, a web application (web app) refers to a software application that runs on a web server and is accessed by users through a web browser.

OpenID Connect (OIDC) is a protocol that allows web applications to authenticate and authorize users. It is built on top of the OAuth 2.0 protocol, which is a widely used authorization framework. OIDC adds an identity layer to OAuth 2.0, enabling web apps to obtain information about the authenticated user.

Here is what library does for you:

* Adds and configures `OpenIdConnect` based on provided configuration.
* Registers `IOptions<KeycloakAuthenticationOptions>`, `IOptions<OpenIdConnectOptions>`, and `IOptions<CookieAuthenticationOptions>`.
* Registers `KeycloakRolesClaimsTransformation` so special Keycloak role claims are added to `ClaimsPrincipal`. See [Keycloak Claims Transformation](#keycloak-claims-transformation)

### ServiceCollection Extensions 🚧

From configuration:

```csharp
public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebAppAuthentication(
    this IServiceCollection services,
    IConfiguration configuration,
    string configSectionName = KeycloakAuthenticationOptions.Section,
    string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
    string cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
    string? displayName = null
)
```

### AuthenticationBuilder Extensions 🚧

From configuration:

```csharp
public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebApp(
    this AuthenticationBuilder builder,
    IConfiguration configuration,
    string configSectionName = KeycloakAuthenticationOptions.Section,
    string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
    string cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
    string? displayName = null
)
```

Inline:

```csharp
public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebApp(
    this AuthenticationBuilder builder,
    Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
    Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions = null,
    Action<OpenIdConnectOptions>? configureOpenIdConnectOptions = null,
    string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
    string? cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
    string? displayName = null
)
```

See [source code](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/blob/main/src/Keycloak.AuthServices.Authentication/WebAppExtensions/KeycloakWebAppAuthenticationBuilderExtensions.cs) for more details.

## Adapter File Configuration Provider

Using *appsettings.json* is a recommended and it is an idiomatic approach for .NET, but if you want a standalone "adapter" (installation) file - *keycloak.json*. You can use `ConfigureKeycloakConfigurationSource`. It adds dedicated configuration source.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureKeycloakConfigurationSource("keycloak.json"); // [!code focus]

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!").RequireAuthorization();

app.Run();
```

Here is an example of **keycloak.json** adapter file:

```json
{
  "realm": "Test",
  "auth-server-url": "http://localhost:8088/",
  "ssl-required": "external",
  "resource": "test-client",
  "verify-token-audience": true
}

```

## Keycloak Claims Transformation

Keycloak roles can be automatically transformed to [AspNetCore Roles](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles). This feature is disabled by default.

Specify `KeycloakAuthenticationOptions.RolesSource` to enable it. E.g.:

```json
{
  "Keycloak": {
    "RolesSource": "Realm"
  }
}
```

There are three options to determine a source for the roles:

```csharp
public enum RolesClaimTransformationSource
{
    /// <summary>
    /// No Transformation. Default
    /// </summary>
    None,

    /// <summary>
    /// Use realm roles as source
    /// </summary>
    Realm,

    /// <summary>
    /// Use client roles as source
    /// </summary>
    ResourceAccess
}
```

Here is an example of decoded JWT token:

```json
{
  "exp": 1714057504,
  "iat": 1714057204,
  "jti": "7250d2a9-e5a1-442f-9e76-5e6b78bb2760",
  "iss": "http://localhost:8080/realms/Test",
  "aud": [
    "test-client",
    "account"
  ],
  "sub": "bf0b3371-ccdc-44f6-8861-ce25cbfcac39",
  "typ": "Bearer",
  "azp": "test-client",
  "session_state": "563332d2-111a-4ef2-b6a0-ebc1d3ae9a1e",
  "acr": "1",
  "allowed-origins": [
    "/*"
  ],
  "realm_access": {
    "roles": [
      "default-roles-test",
      "offline_access",
      "uma_authorization"
    ]
  },
  "resource_access": {
    "account": {
      "roles": [
        "manage-account",
        "manage-account-links",
        "view-profile"
      ]
    }
  },
  "scope": "profile email",
  "sid": "563332d2-111a-4ef2-b6a0-ebc1d3ae9a1e",
  "email_verified": false,
  "name": "Test Test",
  "preferred_username": "test",
  "given_name": "Test",
  "family_name": "Test",
  "email": "test@test.com"
}
```

If we specify `KeycloakAuthenticationOptions.RolesSource = RolesClaimTransformationSource.Realm` the roles are taken from $token.realm_access.roles.

Result = ["default-roles-test","offline_access","uma_authorization"]

If we specify `KeycloakAuthenticationOptions.RolesSource = RolesClaimTransformationSource.ResourceAccess` and `KeycloakAuthenticationOptions.RolesResource="account"` the roles are taken from $token.realm_access.account.roles.

Result = ["manage-account","manage-account-links","view-profile"]

The target claim can be configured `KeycloakAuthenticationOptions.RoleClaimType`, the default value is "role".
