namespace PhaseTwo.AuthServices.Sdk.Kiota;

using Keycloak.AuthServices.Common;

/// <summary>
/// Defines a set of options used to perform Admin HTTP Client calls
/// </summary>
public sealed class PhaseTwoAdminClientOptions : KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;
}
