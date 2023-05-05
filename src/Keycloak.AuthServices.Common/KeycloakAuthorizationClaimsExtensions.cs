namespace Keycloak.AuthServices.Common;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json;

/// <summary>
/// Utils
/// </summary>
public static class KeycloakAuthorizationClaimsExtensions
{
    private const string ClaimValueType = "JSON";

    /// <summary>
    /// Try to get the authorization collection out of the claims.
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="authorizationCollection"></param>
    /// <returns></returns>
    public static bool TryGetAuthorizationCollection(this IEnumerable<Claim> claims,
        [MaybeNullWhen(false)] out AuthorizationCollection authorizationCollection)
    {
        authorizationCollection = default;

        try
        {
            var claim = claims.SingleOrDefault(x =>
                x.Type.Equals(KeycloakConstants.AuthorizationClaimType, StringComparison.OrdinalIgnoreCase)
                && x.ValueType.Equals(ClaimValueType, StringComparison.OrdinalIgnoreCase));

            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return false;
            }

            var result = JsonSerializer.Deserialize<AuthorizationCollection>(claim.Value);

            if (result == null)
            {
                return false;
            }

            authorizationCollection = result;

            return true;
        }
        catch
        {
            // ignored
        }

        return false;
    }
}
