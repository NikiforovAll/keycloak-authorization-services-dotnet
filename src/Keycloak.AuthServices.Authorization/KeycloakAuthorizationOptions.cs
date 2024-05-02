namespace Keycloak.AuthServices.Authorization;

using Common;

/// <summary>
/// Defines a set of options used to perform authentication
/// </summary>
public class KeycloakAuthorizationOptions : KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name.
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;

    /// <summary>
    /// Determines the source for roles
    /// </summary>
    public RolesClaimTransformationSource EnableRolesMapping { get; set; } =
        RolesClaimTransformationSource.None;

    /// <summary>
    /// The name of the resource to be used for Roles mapping
    /// </summary>
    public string? RolesResource { get; set; }

    /// <summary>
    /// Gets or sets the claim type used for roles.
    /// </summary>
    public string RoleClaimType { get; set; } = KeycloakConstants.RoleClaimType;
}

/// <summary>
/// RolesClaimTransformationSource
/// </summary>
public enum RolesClaimTransformationSource
{
    /// <summary>
    /// Specifies that no transformation should be applied from the source.
    /// </summary>
    None,

    /// <summary>
    /// Specifies that transformation should be applied to the realm.
    /// </summary>
    Realm,

    /// <summary>
    /// Specifies that transformation should be applied to the resource access.
    /// </summary>
    ResourceAccess
}
