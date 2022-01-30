namespace Keycloak.AuthServices.Common;

/// <summary>
/// Installation options provided by Keycloak
/// </summary>
/// <remarks>
/// See "/.well-known/openid-configuration"
/// </remarks>
public class KeycloakInstallationOptions
{
    /// <summary>
    /// Authorization server URL
    /// </summary>
    /// <example>
    /// "auth-server-url": "http://localhost:8088/auth/"
    /// </example>
    public string AuthServerUrl { get; set; } = string.Empty;

    /// <summary>
    /// Keycloak Realm
    /// </summary>
    public string Realm { get; set; } = string.Empty;

    /// <summary>
    /// Resource as client id
    /// </summary>
    /// <example>
    /// "resource": "client-id"
    /// </example>
    public string Resource { get; set; } = string.Empty;

    /// <summary>
    /// Audience verification
    /// </summary>
    public bool VerifyTokenAudience { get; set; } = true;

    /// <summary>
    /// Credentials, defined for private client
    /// </summary>
    public KeycloakClientInstallationCredentials Credentials { get; set; } = new();

    /// <summary>
    ///     Optional
    /// </summary>
    /// <remarks>
    ///     - Default: 0 seconds
    /// </remarks>
    public TimeSpan TokenClockSkew { get; set; } = TimeSpan.Zero;

    /// <summary>
    /// Require HTTPS
    /// </summary>
    public string SslRequired { get; set; } = "external";

    /// <summary>
    /// Realm URL
    /// </summary>
    public string KeycloakUrlRealm => $"{NormalizeUrl(this.AuthServerUrl)}/realms/{this.Realm}";

    private static string NormalizeUrl(string url)
    {
        var urlNormalized = !url.EndsWith('/') ? url : url.TrimEnd('/');

        // return urlNormalized.ToLowerInvariant();
        return urlNormalized;
    }
}

/// <summary>
/// Keycloak client credentials
/// </summary>
public class KeycloakClientInstallationCredentials
{
    /// <summary>
    /// Secret
    /// </summary>
    public string Secret { get; set; } = string.Empty;
}

/// <summary>
/// Configuration constant, used internally
/// </summary>
public static class ConfigurationConstants
{
    /// <summary>
    /// Configuration prefix
    /// </summary>
    public const string ConfigurationPrefix = "Keycloak";
}
