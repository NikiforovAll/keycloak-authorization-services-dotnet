# Configure Authentication

**Keycloak.AuthServices.Authentication** provides robust authentication mechanisms for both web APIs and web applications. For web APIs, it supports JWT Bearer token authentication, which allows clients to authenticate to the API by providing a JWT token in the Authorization header of their requests. For web applications, it supports OpenID Connect, a simple identity layer on top of the OAuth 2.0 protocol

---

*Table of Contents*:
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

Here is a trick to bind options from configuration an override directly in the same code:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/ConfigurationTests/AddKeycloakWebApiAuthenticationTests.cs#AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides2{3}

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

## Web App <Badge type="warning" text="beta" />

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

