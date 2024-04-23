namespace Keycloak.AuthServices.Common.Tests;

using FluentAssertions;
using Microsoft.Extensions.Configuration;

public class KeycloakInstallationOptionsTests
{
    private static readonly KeycloakInstallationOptions _expected =
        new()
        {
            Realm = "Test",
            AuthServerUrl = "http://localhost:8080/",
            SslRequired = "none",
            Resource = "test-client",
            VerifyTokenAudience = false,
            Credentials = new() { Secret = "secret" },
        };

    [Fact]
    public void TestKebabCaseNotation()
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var authenticationOptions = configuration
            .GetSection("Keycloak1")
            .Get<KeycloakInstallationOptions>(options => options.BindNonPublicProperties = true);

        authenticationOptions.Should().BeEquivalentTo(_expected);
    }

    [Fact]
    public void TestPascalCaseNotation()
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var authenticationOptions = configuration
            .GetSection("Keycloak2")
            .Get<KeycloakInstallationOptions>();

        authenticationOptions.Should().BeEquivalentTo(_expected);
    }
}
