namespace Keycloak.AuthServices.Common;

using Microsoft.Extensions.Configuration;

/// <summary>
/// Installation options provided by Keycloak
/// </summary>
/// <remarks>
/// See "/.well-known/openid-configuration"
/// </remarks>
public class KeycloakInstallationOptions
{
    private string? authServerUrl;
    private bool? verifyTokenAudience;
    private TimeSpan? tokenClockSkew;
    private string? sslRequired;

    /// <summary>
    /// Authorization server URL
    /// </summary>
    /// <example>
    /// "auth-server-url": "http://localhost:8088/auth/"
    /// </example>
    [ConfigurationKeyName("auth-server-url")]
    public string AuthServerUrl
    {
        get => this.authServerUrl ?? this.AuthServerUrl2;
        set => this.authServerUrl = value;
    }

    [ConfigurationKeyName("AuthServerUrl")]
    private string AuthServerUrl2 { get; set; } = default!;

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
    [ConfigurationKeyName("verify-token-audience")]
    public bool? VerifyTokenAudience
    {
        get => this.verifyTokenAudience ?? this.VerifyTokenAudience2;
        set => this.verifyTokenAudience = value;
    }
    [ConfigurationKeyName("VerifyTokenAudience")]
    private bool? VerifyTokenAudience2 { get; set; }

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
    [ConfigurationKeyName("token-clock-skew")]
    public TimeSpan TokenClockSkew
    {
        get => this.tokenClockSkew ?? this.TokenClockSkew2 ?? TimeSpan.Zero;
        set => this.tokenClockSkew = value;
    }
    [ConfigurationKeyName("TokenClockSkew")]
    private TimeSpan? TokenClockSkew2 { get; set; }

    /// <summary>
    /// Require HTTPS
    /// </summary>
    [ConfigurationKeyName("ssl-required")]
    public string SslRequired
    {
        get => this.sslRequired ?? this.SslRequired2;
        set => this.sslRequired = value;
    }
    [ConfigurationKeyName("SslRequired")]
    private string SslRequired2 { get; set; } = default!;

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
