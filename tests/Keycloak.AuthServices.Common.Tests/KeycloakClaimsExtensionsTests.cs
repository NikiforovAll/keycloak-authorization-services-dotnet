namespace Keycloak.AuthServices.Common.Tests;

using System.Security.Claims;
using System.Text.Json;
using Keycloak.AuthServices.Common.Claims;

public class KeycloakClaimsExtensionsTests
{
    [Fact]
    public void TryGetClaimValue_RetrieveStringValue_Success()
    {
        // Arrange
        List<Claim> claims = [new("claimType", "claimValue", ClaimValueTypes.String)];

        // Act
        var result = claims.TryGetClaimValue<string>(
            "claimType",
            ClaimValueTypes.String,
            out var claimValue
        );

        // Assert
        result.Should().BeTrue();
        claimValue.Should().Be("claimValue");
    }

    [Fact]
    public void TryGetClaimValue_RetrieveIntValue_Success()
    {
        // Arrange
        List<Claim> claims = [new Claim("claimType", "10", ClaimValueTypes.Integer)];

        // Act
        var result = claims.TryGetClaimValue<int>(
            "claimType",
            ClaimValueTypes.Integer,
            out var claimValue
        );

        // Assert
        result.Should().BeTrue();
        claimValue.Should().Be(10);
    }

    [Fact]
    public void TryGetResourceCollection_RetrieveResourceCollection_Success()
    {
        // Arrange
        var jsonResourceAccessCollection = JsonSerializer.Serialize(new ResourceAccessCollection());
        List<Claim> claims =
        [
            new Claim(
                KeycloakConstants.ResourceAccessClaimType,
                jsonResourceAccessCollection,
                KeycloakClaimsExtensions.JsonClaimValueType
            )
        ];

        // Act
        var result = claims.TryGetResourceCollection(out var retrievedResourceAccessCollection);

        // Assert
        result.Should().BeTrue();
        retrievedResourceAccessCollection.Should().NotBeNull();
    }

    [Fact]
    public void TryGetRealmResource_RetrieveRealmResource_Success()
    {
        // Arrange
        var jsonResourceAccess = JsonSerializer.Serialize(new ResourceAccess());
        List<Claim> claims =
        [
            new Claim(
                KeycloakConstants.RealmAccessClaimType,
                jsonResourceAccess,
                KeycloakClaimsExtensions.JsonClaimValueType
            )
        ];

        // Act
        var result = claims.TryGetRealmResource(out var retrievedResourceAccess);

        // Assert
        result.Should().BeTrue();
        retrievedResourceAccess.Should().NotBeNull();
    }

    [Fact]
    public void TryGetRealmResource_NoRealmResource_Failure()
    {
        // Arrange
        List<Claim> claims = [];

        // Act
        var result = claims.TryGetRealmResource(out var retrievedResourceAccess);

        // Assert
        result.Should().BeFalse();
        retrievedResourceAccess.Should().BeNull();
    }

    [Fact]
    public void TryGetResourceCollection_NoResourceCollection_Failure()
    {
        // Arrange
        List<Claim> claims = [];

        // Act
        var result = claims.TryGetResourceCollection(out var retrievedResourceAccessCollection);

        // Assert
        result.Should().BeFalse();
        retrievedResourceAccessCollection.Should().BeNull();
    }
}
