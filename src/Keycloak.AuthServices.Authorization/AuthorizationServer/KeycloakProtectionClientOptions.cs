namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using Common;

/// <summary>
/// Defines a set of options used to perform Authorization Server calls
/// </summary>
public sealed class KeycloakProtectionClientOptions : KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name.
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;

    /// <summary>
    /// Gets or sets the source authentication scheme used for header propagation.
    /// </summary>
    public string SourceAuthenticationScheme { get; set; } = "Bearer";

    /// <summary>
    /// Gets or sets a value indicating whether to use the protected resource policy provider.
    /// </summary>
    /// <remarks>
    /// When set to true, the protected resource policy provider will be used to dynamically register policies based on their names.
    /// </remarks>
    public bool UseProtectedResourcePolicyProvider { get; set; }
}
