namespace Keycloak.AuthServices.Authentication;

using Common;

/// <summary>
/// Defines a set of options used to perform authentication
/// </summary>
public class KeycloakAuthenticationOptions : KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;
}
