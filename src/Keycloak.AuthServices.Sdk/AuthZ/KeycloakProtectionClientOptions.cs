namespace Keycloak.AuthServices.Sdk.AuthZ;

using Common;

/// <summary>
/// Defines a set of options used to perform Authorization Server calls
/// </summary>
public sealed class KeycloakProtectionClientOptions : KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;
}
