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
    /// Gets or sets the audience for the authentication. Takes priority over Resource.
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    /// Gets or sets the claim type used for roles.
    /// </summary>
    public string RoleClaimType { get; set; } = KeycloakConstants.RoleClaimType;

    /// <summary>
    /// Gets or sets the claim type used for the name.
    /// </summary>
    public string NameClaimType { get; set; } = KeycloakConstants.NameClaimType;

    /// <summary>
    /// Gets the OpenId Connect URL to discover OAuth2 configuration values.
    /// </summary>
    public string? OpenIdConnectUrl =>
        string.IsNullOrWhiteSpace(this.KeycloakUrlRealm)
            ? default
            : $"{this.KeycloakUrlRealm}{KeycloakConstants.OpenIdConnectConfigurationPath}";
}
