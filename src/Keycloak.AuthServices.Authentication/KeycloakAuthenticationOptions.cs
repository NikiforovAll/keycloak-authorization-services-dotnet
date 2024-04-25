namespace Keycloak.AuthServices.Authentication;

using Common;

/// <summary>
/// Defines a set of options used to perform authentication
/// </summary>
public class KeycloakAuthenticationOptions : KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name.
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;

    /// <summary>
    /// Gets or sets the audience for the authentication. Takes priority over Resource
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    /// Gets or sets the claim type used for roles.
    /// </summary>
    public string RoleClaimType { get; set; } = "role";

    /// <summary>
    /// Gets or sets the claim type used for the name.
    /// </summary>
    public string NameClaimType { get; set; } = "preferred_username";

    /// <summary>
    /// RolesClaimTransformationSource
    /// </summary>
    public RolesClaimTransformationSource RolesSource { get; set; } =
        RolesClaimTransformationSource.ResourceAccess;
}
