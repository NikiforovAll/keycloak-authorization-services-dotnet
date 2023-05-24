namespace Keycloak.AuthServices.Authentication.Tests.Claims;

using System.Security.Claims;
using Authentication.Claims;
using Common;
using FluentAssertions;

public class KeycloakRolesClaimsTransformationTests
{
    [Theory]
    [InlineData(RolesClaimTransformationSource.Realm)]
    [InlineData(RolesClaimTransformationSource.ResourceAccess)]
    public async Task ClaimsTransformationShouldMap(RolesClaimTransformationSource roleSource)
    {
        // create the service
        var target = new KeycloakRolesClaimsTransformation(ClaimTypes.Role, roleSource, MyAudience);

        // The fixture
        var claimsPrincipal = GetClaimsPrincipal();

        // This should work many times
        for(var testCount = 0; testCount < 3; testCount++)
        {
            claimsPrincipal = await target.TransformAsync(claimsPrincipal);
            claimsPrincipal.HasClaim(ClaimTypes.Role, MyFirstClaim).Should().BeTrue();
            claimsPrincipal.HasClaim(ClaimTypes.Role, MySecondClaim).Should().BeTrue();
            claimsPrincipal.Claims.Count(item => ClaimTypes.Role == item.Type).Should().Be(2);
        }
    }

    // The resource_access claim type
    private const string MyResourceClaimType = "resource_access";
    private const string MyResourceClaimValue = @$"{{""{MyAudience}"":{{""roles"":[""{MyFirstClaim}"",""{MySecondClaim}""]}}}}";

    // The realm_access claim type
    private const string MyRealmClaimType = "realm_access";
    private const string MyRealmClaimValue = @$"{{""roles"":[""{MyFirstClaim}"",""{MySecondClaim}""]}}";

    // Fake claim values
    private const string MyFirstClaim = "my_client_app_role_user";
    private const string MySecondClaim = "my_client_app_role_super_user";

    // The issuer/original issuer
    private const string MyUrl = "https://keycloak.mydomain.com/realms/my_realm";

    // The value type should be JSON
    private const string MyValueType = "JSON";

    // The audience for the resource_access
    private const string MyAudience = "my-audience";

    // Get a claims principal that has all the appropriate claim details required for testing
    private static ClaimsPrincipal GetClaimsPrincipal() =>
        new(new ClaimsIdentity(new[]
        {
            new Claim(MyResourceClaimType, MyResourceClaimValue, MyValueType, MyUrl, MyUrl),
            new Claim(MyRealmClaimType, MyRealmClaimValue, MyValueType, MyUrl, MyUrl),
        }));

}
