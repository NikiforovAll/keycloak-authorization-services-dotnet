namespace Keycloak.AuthServices.Sdk;

using Common;

/// <summary>
/// Defines a set of options used to perform Protection API HTTP Client calls
/// </summary>
public sealed class KeycloakProtectionClientOptions : KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;
}
