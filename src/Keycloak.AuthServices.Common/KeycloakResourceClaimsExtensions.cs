namespace Keycloak.AuthServices.Common;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json;

public static class KeycloakResourceClaimsExtensions
{
    private const string ClaimValueType = "JSON";

    public static bool TryGetResourceCollection(
        this IEnumerable<Claim> claims,
        [MaybeNullWhen(false)] out ResourceAccessCollection resourceAccessCollection)
    {
        var claim = claims.SingleOrDefault(x =>
            x.Type.Equals(KeycloakConstants.ResourceAccessClaimType, StringComparison.OrdinalIgnoreCase)
            && x.ValueType.Equals(ClaimValueType, StringComparison.OrdinalIgnoreCase));

        if (claim == null || string.IsNullOrEmpty(claim.Value))
        {
            resourceAccessCollection = default;
            return false;
        }

        var resourcesAccess = JsonSerializer.Deserialize<ResourceAccessCollection>(claim.Value);

        resourceAccessCollection = resourcesAccess!;
        return true;
    }

    public static bool TryGetRealmResource(
        this IEnumerable<Claim> claims,
        [MaybeNullWhen(false)] out ResourceAccess resourcesAccess)
    {
        var claim = claims.SingleOrDefault(x =>
            x.Type.Equals(KeycloakConstants.RealmAccessClaimType, StringComparison.OrdinalIgnoreCase)
            && x.ValueType.Equals(ClaimValueType, StringComparison.OrdinalIgnoreCase));

        if (claim == null || string.IsNullOrEmpty(claim.Value))
        {
            resourcesAccess = default;
            return false;
        }

        resourcesAccess = JsonSerializer.Deserialize<ResourceAccess>(claim.Value)!;
        return true;
    }
}
