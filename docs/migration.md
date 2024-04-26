# Migration Guide

## Key Changes in 2.0.0

* `RolesClaimTransformationSource` change to `None` from `ResourceAccess` meaning we no longer map to `AspNetCore` roles by default.
* Moved `IKeycloakProtectionClient` to `Keycloak.AuthServices.Authorization`. Removed `AddKeycloakProtectionHttpClient`, added `AddAuthorizationServer` instead.

```csharp
// Before
.AddKeycloakAuthorization(configuration)
// After
.AddKeycloakAuthorization().AddAuthorizationServer(configuration)
```

* Dropped namespace `Keycloak.AuthServices.Sdk.AuthZ`
* `AddKeycloakAuthentication` has been deprecated in favor of `AddKeycloakWebApiAuthentication`.
* Breaking change 💥: Changed default Configuration format from kebab-case to PascalCase:

It is possible to retrieve configuration by specifying special option `KeycloakFormatBinder.Instance`. The tests below explain the changes:

```csharp
private static readonly KeycloakInstallationOptions Expected =
    new()
    {
        Realm = "Test",
        AuthServerUrl = "http://localhost:8080/",
        SslRequired = "none",
        Resource = "test-client",
        VerifyTokenAudience = true,
        Credentials = new() { Secret = "secret" },
    };

[Fact]
public void TestKebabCaseNotation()
{
    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    var authenticationOptions = configuration
        .GetSection("Keycloak1")
        .Get<KeycloakInstallationOptions>(KeycloakFormatBinder.Instance);

    authenticationOptions.Should().BeEquivalentTo(Expected);
}

[Fact]
public void TestPascalCaseNotation()
{
    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    var authenticationOptions = configuration
        .GetSection("Keycloak2")
        .Get<KeycloakInstallationOptions>();

    authenticationOptions.Should().BeEquivalentTo(Expected);
}
```

```json
{
  "Keycloak1": {
    "realm": "Test",
    "auth-server-url": "http://localhost:8080/",
    "ssl-required": "none",
    "resource": "test-client",
    "verify-token-audience": true,
    "credentials": {
      "secret": "secret"
    }
  },
  "Keycloak2": {
    "Realm": "Test",
    "AuthServerUrl": "http://localhost:8080/",
    "SslRequired": "none",
    "Resource": "test-client",
    "VerifyTokenAudience": true,
    "Credentials": {
      "Secret": "secret"
    }
  }
}

```
