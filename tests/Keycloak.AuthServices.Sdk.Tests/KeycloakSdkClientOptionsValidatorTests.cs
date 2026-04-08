namespace Keycloak.AuthServices.Sdk.Tests;

using Microsoft.Extensions.Options;

public class KeycloakAdminClientOptionsValidatorTests
{
    private readonly KeycloakAdminClientOptionsValidator sut = new();

    [Fact]
    public void Validate_WhenAllRequiredFieldsSet_ReturnsSuccess()
    {
        var options = new KeycloakAdminClientOptions
        {
            AuthServerUrl = "http://localhost:8080/",
            Realm = "test",
        };

        var result = this.sut.Validate(null, options);

        result.Succeeded.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("not-a-valid-url")]
    public void Validate_WhenAuthServerUrlInvalid_ReturnsFail(string? authServerUrl)
    {
        var options = new KeycloakAdminClientOptions
        {
            AuthServerUrl = authServerUrl,
            Realm = "test",
        };

        var result = this.sut.Validate(null, options);

        result.Failed.Should().BeTrue();
        result.Failures.Should().Contain(f => f.Contains("AuthServerUrl"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_WhenRealmMissing_ReturnsFail(string? realm)
    {
        var options = new KeycloakAdminClientOptions
        {
            AuthServerUrl = "http://localhost:8080/",
            Realm = realm!,
        };

        var result = this.sut.Validate(null, options);

        result.Failed.Should().BeTrue();
        result.Failures.Should().Contain(f => f.Contains("Realm"));
    }

    [Fact]
    public void Validate_WhenOptionsIsNull_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => this.sut.Validate(null, null!));
}

public class KeycloakProtectionClientOptionsValidatorTests
{
    private readonly KeycloakProtectionClientOptionsValidator sut = new();

    [Fact]
    public void Validate_WhenAllRequiredFieldsSet_ReturnsSuccess()
    {
        var options = new KeycloakProtectionClientOptions
        {
            AuthServerUrl = "http://localhost:8080/",
            Realm = "test",
        };

        var result = this.sut.Validate(null, options);

        result.Succeeded.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("not-a-valid-url")]
    public void Validate_WhenAuthServerUrlInvalid_ReturnsFail(string? authServerUrl)
    {
        var options = new KeycloakProtectionClientOptions
        {
            AuthServerUrl = authServerUrl,
            Realm = "test",
        };

        var result = this.sut.Validate(null, options);

        result.Failed.Should().BeTrue();
        result.Failures.Should().Contain(f => f.Contains("AuthServerUrl"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_WhenRealmMissing_ReturnsFail(string? realm)
    {
        var options = new KeycloakProtectionClientOptions
        {
            AuthServerUrl = "http://localhost:8080/",
            Realm = realm!,
        };

        var result = this.sut.Validate(null, options);

        result.Failed.Should().BeTrue();
        result.Failures.Should().Contain(f => f.Contains("Realm"));
    }

    [Fact]
    public void Validate_WhenOptionsIsNull_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => this.sut.Validate(null, null!));
}
