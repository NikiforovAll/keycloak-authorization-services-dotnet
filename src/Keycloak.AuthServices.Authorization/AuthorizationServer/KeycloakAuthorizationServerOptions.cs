namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using Common;

/// <summary>
/// Defines a set of options used to perform Authorization Server calls
/// </summary>
public sealed class KeycloakAuthorizationServerOptions : KeycloakInstallationOptions
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
    /// Controls if <see cref="AccessTokenPropagationHandler"/> is added to the <see cref="IAuthorizationServerClient"/>
    /// </summary>
    public static bool DisableHeaderPropagation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use the protected resource policy provider.
    /// </summary>
    /// <remarks>
    /// When set to true, the protected resource policy provider will be used to dynamically register policies based on their names.
    /// </remarks>
    public bool UseProtectedResourcePolicyProvider { get; set; }

    /// <summary>
    /// Represents the mode for validating scopes.
    /// </summary>
    public ScopesValidationMode ScopesValidationMode { get; set; } = ScopesValidationMode.AllOf;
}

/// <summary>
/// Specifies the validation mode for multiple scopes.
/// </summary>
public enum ScopesValidationMode
{
    /// <summary>
    /// Specifies that all of the scopes must be valid.
    /// </summary>
    AllOf,

    /// <summary>
    /// Specifies that at least one of the scopes must be valid.
    /// </summary>
    AnyOf,
}
