namespace Keycloak.AuthServices.Authorization.Claims;

using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

/// <summary>
/// Transforms keycloak roles in the resource_access claim to jwt role claims.
/// Note, realm roles are not mapped atm.
/// </summary>
/// <example>
/// Example of Keycloak resource_access claim
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
    private readonly ILogger logger;
    private readonly bool tokenIntrospectionEnabled;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakRolesClaimsTransformation"/> class.
    /// </summary>
    /// <param name="roleClaimType">Type of the role claim.</param>
    /// <param name="roleSource"><see cref="RolesClaimTransformationSource"/></param>
    /// <param name="resource">The audience.</param>
    /// <param name="logger">Optional logger for diagnostics.</param>
    /// <param name="tokenIntrospectionEnabled">Whether token introspection is registered.</param>
    public KeycloakRolesClaimsTransformation(
        string roleClaimType,
        RolesClaimTransformationSource roleSource,
        string resource,
        ILogger<KeycloakRolesClaimsTransformation>? logger = null,
        bool tokenIntrospectionEnabled = false
    )
    {
        this.roleClaimType = roleClaimType;
        this.roleSource = roleSource;
        this.audience = resource;
        this.logger = logger ?? NullLogger<KeycloakRolesClaimsTransformation>.Instance;
        this.tokenIntrospectionEnabled = tokenIntrospectionEnabled;
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
        ArgumentNullException.ThrowIfNull(principal);

        if (this.roleSource == RolesClaimTransformationSource.None)
        {
            return Task.FromResult(principal);
        }

        if (principal.Identity is not ClaimsIdentity identity)
        {
            return Task.FromResult(principal);
        }

        var result = principal.Clone();

        var existingRoles = new HashSet<string>(
            identity
                .Claims.Where(c =>
                    c.Type.Equals(this.roleClaimType, StringComparison.InvariantCultureIgnoreCase)
                )
                .Select(c => c.Value),
            StringComparer.InvariantCultureIgnoreCase
        );

        if (this.roleSource.HasFlag(RolesClaimTransformationSource.ResourceAccess))
        {
            var resourceAccessValue = principal.FindFirst("resource_access")?.Value;
            if (!string.IsNullOrWhiteSpace(resourceAccessValue))
            {
                using var resourceAccess = JsonDocument.Parse(resourceAccessValue);
                if (
                    resourceAccess.RootElement.TryGetProperty(
                        this.audience,
                        out var audienceElement
                    ) && audienceElement.TryGetProperty("roles", out var clientRoles)
                )
                {
                    this.AddRolesFromJsonElement(identity, clientRoles, existingRoles);
                }
            }
            else if (!this.tokenIntrospectionEnabled)
            {
                this.logger.LogRolesMappingMissingClaimsNoIntrospection("resource_access");
            }
        }

        if (this.roleSource.HasFlag(RolesClaimTransformationSource.Realm))
        {
            var realmAccessValue = principal.FindFirst("realm_access")?.Value;
            if (!string.IsNullOrWhiteSpace(realmAccessValue))
            {
                using var realmAccess = JsonDocument.Parse(realmAccessValue);
                if (realmAccess.RootElement.TryGetProperty("roles", out var realmRoles))
                {
                    this.AddRolesFromJsonElement(identity, realmRoles, existingRoles);
                }
            }
            else if (!this.tokenIntrospectionEnabled)
            {
                this.logger.LogRolesMappingMissingClaimsNoIntrospection("realm_access");
            }
        }

        return Task.FromResult(result);
    }

    private void AddRolesFromJsonElement(
        ClaimsIdentity identity,
        JsonElement rolesElement,
        HashSet<string> existingRoles
    )
    {
        foreach (var role in rolesElement.EnumerateArray())
        {
            var value = role.GetString();
            if (!string.IsNullOrWhiteSpace(value) && existingRoles.Add(value))
            {
                identity.AddClaim(new Claim(this.roleClaimType, value));
            }
        }
    }
}
