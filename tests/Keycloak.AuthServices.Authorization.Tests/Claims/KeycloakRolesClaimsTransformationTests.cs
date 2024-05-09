namespace Keycloak.AuthServices.Authorization.Tests.Claims;

using System.Security.Claims;
using FluentAssertions;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.Claims;

public class KeycloakRolesClaimsTransformationTests
{
    [Theory]
    [InlineData(RolesClaimTransformationSource.Realm)]
    [InlineData(RolesClaimTransformationSource.ResourceAccess)]
    public async Task ClaimsTransformationShouldMap(RolesClaimTransformationSource roleSource)
    {
        var target = new KeycloakRolesClaimsTransformation(ClaimTypes.Role, roleSource, ClientId);
        var claimsPrincipal = GetClaimsPrincipal(MyRealmClaimValue, MyResourceClaimValue);

        // This should work many times
        for (var testCount = 0; testCount < 3; testCount++)
        {
            claimsPrincipal = await target.TransformAsync(claimsPrincipal);
            switch (roleSource)
            {
                case RolesClaimTransformationSource.Realm:
                    claimsPrincipal.HasClaim(ClaimTypes.Role, RealmRoleUserClaim).Should().BeTrue();
                    claimsPrincipal.HasClaim(ClaimTypes.Role, RealmRoleSuperUserClaim).Should().BeTrue();
                    break;
                case RolesClaimTransformationSource.ResourceAccess:
                    claimsPrincipal.HasClaim(ClaimTypes.Role, AppRoleUserClaim).Should().BeTrue();
                    claimsPrincipal.HasClaim(ClaimTypes.Role, AppRoleSuperUserClaim).Should().BeTrue();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roleSource), roleSource, "Unexpected role source");
            }
            claimsPrincipal.Claims.Count(item => ClaimTypes.Role == item.Type).Should().Be(2);
        }
    }

    [Fact]
    public async Task ClaimsTransformationShouldHandleNoneSource()
    {
        var target = new KeycloakRolesClaimsTransformation(
            ClaimTypes.Role,
            RolesClaimTransformationSource.None,
            ClientId
        );
        var claimsPrincipal = GetClaimsPrincipal(MyRealmClaimValue, MyResourceClaimValue);

        claimsPrincipal = await target.TransformAsync(claimsPrincipal);
        claimsPrincipal.Claims.Count(item => ClaimTypes.Role == item.Type).Should().Be(0);
    }

    [Fact]
    public async Task ClaimsTransformationShouldHandleAllSource()
    {
        var target = new KeycloakRolesClaimsTransformation(
            ClaimTypes.Role,
            RolesClaimTransformationSource.All,
            ClientId
        );
        var claimsPrincipal = GetClaimsPrincipal(MyRealmClaimValue, MyResourceClaimValue);

        claimsPrincipal = await target.TransformAsync(claimsPrincipal);
        claimsPrincipal.HasClaim(ClaimTypes.Role, AppRoleUserClaim).Should().BeTrue();
        claimsPrincipal.HasClaim(ClaimTypes.Role, AppRoleSuperUserClaim).Should().BeTrue();
        claimsPrincipal.HasClaim(ClaimTypes.Role, RealmRoleUserClaim).Should().BeTrue();
        claimsPrincipal.HasClaim(ClaimTypes.Role, RealmRoleSuperUserClaim).Should().BeTrue();
        claimsPrincipal.Claims.Count(item => ClaimTypes.Role == item.Type).Should().Be(4);
    }

    [Fact]
    public async Task ClaimsTransformationShouldHandleMissingResourceClaim()
    {
        var target = new KeycloakRolesClaimsTransformation(
            ClaimTypes.Role,
            RolesClaimTransformationSource.ResourceAccess,
            ClientId
        );
        var claimsPrincipal = GetClaimsPrincipalClaim(MyResourceClaimValue2);

        claimsPrincipal = await target.TransformAsync(claimsPrincipal);
        claimsPrincipal.Claims.Count(item => ClaimTypes.Role == item.Type).Should().Be(0);
    }

    private const string MyResourceClaimValue = /*lang=json,strict*/
        """
            {
                "my-client": {
                    "roles": [
                        "my_client_app_role_user",
                        "my_client_app_role_super_user"
                    ]
                }
            }
        """;

    private const string MyResourceClaimValue2 = /*lang=json,strict*/
        """
            {
                "account": {
                    "roles": [
                        "manage-account",
                        "manage-account-links"
                    ]
                }
            }
        """;

    // The resource_access claim type
    private const string ResourceClaimType = "resource_access";

    // The realm_access claim type
    private const string RealmClaimType = "realm_access";
    private const string MyRealmClaimValue = /*lang=json,strict*/
        """
        {
            "roles": [
                "realm_role_user",
                "realm_role_super_user"
            ]
        }
        """;

    // Fake claim values
    private const string AppRoleUserClaim = "my_client_app_role_user";
    private const string AppRoleSuperUserClaim = "my_client_app_role_super_user";
    private const string RealmRoleUserClaim = "realm_role_user";
    private const string RealmRoleSuperUserClaim = "realm_role_super_user";

    // The issuer/original issuer
    private const string MyUrl = "https://keycloak.mydomain.com/realms/my_realm";

    // The value type should be JSON
    private const string JsonValueType = "JSON";

    // The client for the resource_access
    private const string ClientId = "my-client";

    // Get a claims principal that has all the appropriate claim details required for testing
    private static ClaimsPrincipal GetClaimsPrincipal(
        string realmClaimValue,
        string resourceClaimValue
    ) =>
        new(
            new ClaimsIdentity(
                [
                    new Claim(ResourceClaimType, resourceClaimValue, JsonValueType, MyUrl, MyUrl),
                    new Claim(RealmClaimType, realmClaimValue, JsonValueType, MyUrl, MyUrl),
                ]
            )
        );

    // Get a claims principal that has all the appropriate claim details required for testing
    private static ClaimsPrincipal GetClaimsPrincipalClaim(string claimValue) =>
        new(
            new ClaimsIdentity(
                [new Claim(RealmClaimType, claimValue, JsonValueType, MyUrl, MyUrl),]
            )
        );
}
