namespace Keycloak.AuthServices.Sdk.Admin;

using Common;

/// <summary>
/// Defines a set of options used to perform Admin HTTP Client calls
/// </summary>
public sealed class KeycloakAdminClientOptions: KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;
}
