namespace Keycloak.AuthServices.Authentication.Claims;

using System.Security.Claims;
using System.Text.Json;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication;

/// <summary>
/// Transforms keycloak roles in the resource_access claim to jwt role claims.
/// Note, realm roles are not mapped atm.
/// </summary>
/// <example>
/// Example of keycloack resource_access claim
/// "resource_access": {
///     "api": {
///         "roles": [ "role1", "role2" ]
///     },
///     "account": {
///         "roles": [
///             "view-profile"
///         ]
///     }
/// },
/// </example>
/// <seealso cref="IClaimsTransformation" />
public class KeycloakRolesClaimsTransformation : IClaimsTransformation
{
    private readonly string roleClaimType;
    private readonly RolesClaimTransformationSource roleSource;
    private readonly string audience;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakRolesClaimsTransformation"/> class.
    /// </summary>
    /// <param name="roleClaimType">Type of the role claim.</param>
    /// <param name="roleSource"><see cref="RolesClaimTransformationSource"/></param>
    /// <param name="audience">The audience.</param>
    public KeycloakRolesClaimsTransformation(
        string roleClaimType,
        RolesClaimTransformationSource roleSource,
        string audience)
    {
        this.roleClaimType = roleClaimType;
        this.roleSource = roleSource;
        this.audience = audience;
    }

    /// <summary>
    /// Provides a central transformation point to change the specified principal.
    /// Note: this will be run on each AuthenticateAsync call, so its safer to
    /// return a new ClaimsPrincipal if your transformation is not idempotent.
    /// </summary>
    /// <param name="principal">The <see cref="ClaimsPrincipal" /> to transform.</param>
    /// <returns>
    /// The transformed principal.
    /// </returns>
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var result = principal.Clone();
        if (result.Identity is not ClaimsIdentity identity)
        {
            return Task.FromResult(result);
        }

        if (this.roleSource == RolesClaimTransformationSource.ResourceAccess)
        {
            var resourceAccessValue = principal.FindFirst("resource_access")?.Value;
            if (string.IsNullOrWhiteSpace(resourceAccessValue))
            {
                return Task.FromResult(result);
            }

            using var resourceAccess = JsonDocument.Parse(resourceAccessValue);
            var containsAudienceRoles = resourceAccess
                .RootElement
                .TryGetProperty(this.audience, out var rolesElement);

            if (!containsAudienceRoles)
            {
                return Task.FromResult(result);
            }

            var clientRoles = rolesElement.GetProperty("roles");

            foreach (var role in clientRoles.EnumerateArray())
            {
                var value = role.GetString();

                var matchingClaim = identity.Claims.FirstOrDefault(claim => 
                    claim.Type.Equals(this.roleClaimType, StringComparison.InvariantCultureIgnoreCase) && 
                    claim.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase)); 

                if (matchingClaim is null && !string.IsNullOrWhiteSpace(value))
                {
                    identity.AddClaim(new Claim(this.roleClaimType, value));
                }
            }

            return Task.FromResult(result);
        }

        if (this.roleSource == RolesClaimTransformationSource.Realm)
        {
            var realmAccessValue = principal.FindFirst("realm_access")?.Value;
            if (string.IsNullOrWhiteSpace(realmAccessValue))
            {
                return Task.FromResult(result);
            }

            using var realmAccess = JsonDocument.Parse(realmAccessValue);

            var containsRoles = realmAccess
                .RootElement
                .TryGetProperty("roles", out var rolesElement);

            if (containsRoles)
            {
                foreach (var role in rolesElement.EnumerateArray())
                {
                    var value = role.GetString();

                    var matchingClaim = identity.Claims.FirstOrDefault(claim => 
                        claim.Type.Equals(this.roleClaimType, StringComparison.InvariantCultureIgnoreCase) && 
                        claim.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));

                    if (matchingClaim is null && !string.IsNullOrWhiteSpace(value))
                    {
                        identity.AddClaim(new Claim(this.roleClaimType, value));
                    }
                }

                return Task.FromResult(result);
            }
        }

        return Task.FromResult(result);
    }
}
