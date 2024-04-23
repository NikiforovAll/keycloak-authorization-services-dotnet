namespace Keycloak.AuthServices.Common;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json;
using static Keycloak.AuthServices.Common.KeycloakConstants;

/// <summary>
/// Keycloak resource claims extensions.
/// </summary>
public static class KeycloakResourceClaimsExtensions
{
    private const string ClaimValueType = "JSON";

    /// <summary>
    /// Tries to get the resource collection from the JWT claims.
    /// </summary>
    /// <param name="claims">The JWT claims.</param>
    /// <param name="resourceAccessCollection">The resource access collection if found, otherwise null.</param>
    /// <returns>True if the resource collection was found, false otherwise.</returns>
    public static bool TryGetResourceCollection(
        this IEnumerable<Claim> claims,
        [MaybeNullWhen(false)] out ResourceAccessCollection resourceAccessCollection
    )
    {
        var claim = claims.SingleOrDefault(x =>
            x.Type.Equals(ResourceAccessClaimType, StringComparison.OrdinalIgnoreCase)
            && x.ValueType.Equals(ClaimValueType, StringComparison.OrdinalIgnoreCase)
        );

        if (claim == null || string.IsNullOrEmpty(claim.Value))
        {
            resourceAccessCollection = default;
            return false;
        }

        var resourcesAccess = JsonSerializer.Deserialize<ResourceAccessCollection>(claim.Value);

        resourceAccessCollection = resourcesAccess!;
        return true;
    }

    /// <summary>
    /// Tries to get the realm resource from the JWT claims.
    /// </summary>
    /// <param name="claims">The JWT claims.</param>
    /// <param name="resourcesAccess">The realm resource access if found, otherwise null.</param>
    /// <returns>True if the realm resource was found, false otherwise.</returns>
    public static bool TryGetRealmResource(
        this IEnumerable<Claim> claims,
        [MaybeNullWhen(false)] out ResourceAccess resourcesAccess
    )
    {
        var claim = claims.SingleOrDefault(x =>
            x.Type.Equals(RealmAccessClaimType, StringComparison.OrdinalIgnoreCase)
            && x.ValueType.Equals(ClaimValueType, StringComparison.OrdinalIgnoreCase)
        );

        if (claim == null || string.IsNullOrEmpty(claim.Value))
        {
            resourcesAccess = default;
            return false;
        }

        resourcesAccess = JsonSerializer.Deserialize<ResourceAccess>(claim.Value)!;

        return true;
    }
}
