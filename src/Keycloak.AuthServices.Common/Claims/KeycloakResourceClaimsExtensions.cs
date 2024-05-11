namespace Keycloak.AuthServices.Common.Claims;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using static Keycloak.AuthServices.Common.KeycloakConstants;

/// <summary>
/// Keycloak resource claims extensions.
/// </summary>
public static class KeycloakClaimsExtensions
{
    /// <summary>
    /// JsonClaimValueType
    /// </summary>
    public const string JsonClaimValueType = "JSON";

    /// <summary>
    /// Tries to get the value of a specific claim from the JWT claims.
    /// </summary>
    /// <typeparam name="T">The type of the claim value.</typeparam>
    /// <param name="claims">The JWT claims.</param>
    /// <param name="claimType">The claim type.</param>
    /// <param name="claimValueType">The claim value type.</param>
    /// <param name="result">The claim value if found, otherwise the default value of type T.</param>
    /// <returns>True if the claim value was found, false otherwise.</returns>
    public static bool TryGetClaimValue<T>(
        this IEnumerable<Claim> claims,
        string claimType,
        string? claimValueType,
        [MaybeNullWhen(false)] out T result
    )
    {
        var claim = claims.SingleOrDefault(x =>
            x.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase)
            && x.ValueType.Equals(claimValueType, StringComparison.OrdinalIgnoreCase)
        );

        if (claim == null || string.IsNullOrEmpty(claim.Value))
        {
            result = default;

            return false;
        }

        if (typeof(T).IsPrimitive || typeof(T) == typeof(string))
        {
            result = (T)Convert.ChangeType(claim.Value, typeof(T), CultureInfo.InvariantCulture);
        }
        else
        {
            result = JsonSerializer.Deserialize<T>(claim.Value)!;
        }

        return true;
    }

    /// <summary>
    /// Tries to get the resource collection from the JWT claims.
    /// </summary>
    /// <param name="claims">The JWT claims.</param>
    /// <param name="resourceAccessCollection">The resource access collection if found, otherwise null.</param>
    /// <returns>True if the resource collection was found, false otherwise.</returns>
    public static bool TryGetResourceCollection(
        this IEnumerable<Claim> claims,
        [MaybeNullWhen(false)] out ResourceAccessCollection resourceAccessCollection
    ) =>
        claims.TryGetClaimValue(
            ResourceAccessClaimType,
            JsonClaimValueType,
            out resourceAccessCollection
        );

    /// <summary>
    /// Tries to get the realm resource from the JWT claims.
    /// </summary>
    /// <param name="claims">The JWT claims.</param>
    /// <param name="resourcesAccess">The realm resource access if found, otherwise null.</param>
    /// <returns>True if the realm resource was found, false otherwise.</returns>
    public static bool TryGetRealmResource(
        this IEnumerable<Claim> claims,
        [MaybeNullWhen(false)] out ResourceAccess resourcesAccess
    ) => claims.TryGetClaimValue(RealmAccessClaimType, JsonClaimValueType, out resourcesAccess);
}
