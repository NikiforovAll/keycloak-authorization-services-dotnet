namespace Keycloak.AuthServices.Common.Tests;

using Microsoft.Extensions.Configuration;

public class KeycloakInstallationOptionsTests
{
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
            .Get<KeycloakInstallationOptions>(KeycloakInstallationOptions.KeycloakFormatBinder);

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
}
