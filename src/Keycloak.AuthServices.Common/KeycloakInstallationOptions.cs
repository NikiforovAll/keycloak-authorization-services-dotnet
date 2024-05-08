namespace Keycloak.AuthServices.Common;

using System.Diagnostics;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Installation options provided by Keycloak
/// </summary>
/// <remarks>
/// See "/.well-known/openid-configuration"
/// </remarks>
public class KeycloakInstallationOptions
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? authServerUrl;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool? verifyTokenAudience;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private TimeSpan? tokenClockSkew;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? sslRequired;

    /// <summary>
    /// Authorization server URL
    /// </summary>
    /// <remarks>The value is normalized, the trailing '/' is ensured.</remarks>
    /// <example>
    /// "auth-server-url": "http://localhost:8088/auth/"
    /// </example>
    [ConfigurationKeyName("AuthServerUrl")]
    public string? AuthServerUrl
    {
        get => this.authServerUrl ?? NormalizeUrl(this.AuthServerUrl2);
        set => this.authServerUrl = NormalizeUrl(value);
    }

    [ConfigurationKeyName("auth-server-url")]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string AuthServerUrl2 { get; set; } = default!;

    /// <summary>
    /// Keycloak Realm
    /// </summary>
    public string Realm { get; set; } = default!;

    /// <summary>
    /// Resource as client id
    /// </summary>
    /// <example>
    /// "resource": "client-id"
    /// </example>
    public string Resource { get; set; } = default!;

    /// <summary>
    /// Audience verification
    /// </summary>
    [ConfigurationKeyName("VerifyTokenAudience")]
    public bool? VerifyTokenAudience
    {
        get => this.verifyTokenAudience ?? this.VerifyTokenAudience2;
        set => this.verifyTokenAudience = value;
    }

    [ConfigurationKeyName("verify-token-audience")]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
    [ConfigurationKeyName("TokenClockSkew")]
    public TimeSpan TokenClockSkew
    {
        get => this.tokenClockSkew ?? this.TokenClockSkew2 ?? TimeSpan.Zero;
        set => this.tokenClockSkew = value;
    }

    [ConfigurationKeyName("token-clock-skew")]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private TimeSpan? TokenClockSkew2 { get; set; }

    /// <summary>
    /// Require HTTPS
    /// </summary>
    [ConfigurationKeyName("SslRequired")]
    public string SslRequired
    {
        get => this.sslRequired ?? this.SslRequired2;
        set => this.sslRequired = value;
    }

    [ConfigurationKeyName("ssl-required")]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string SslRequired2 { get; set; } = default!;

    /// <summary>
    /// Realm URL
    /// </summary>
    public string KeycloakUrlRealm
    {
        get
        {
            if (string.IsNullOrWhiteSpace(this.AuthServerUrl))
            {
                return default!;
            }

            return $"{this.AuthServerUrl}realms/{this.Realm}/";
        }
    }

    /// <summary>
    /// Token endpoint URL including Realm
    /// </summary>
    public string KeycloakTokenEndpoint
    {
        get
        {
            if (string.IsNullOrWhiteSpace(this.KeycloakUrlRealm))
            {
                return default!;
            }

            return $"{this.KeycloakUrlRealm}{KeycloakConstants.TokenEndpointPath}";
        }
    }

    private static string? NormalizeUrl(string? url)
    {
        if (url is null)
        {
            return url;
        }

        if (!url.EndsWith('/'))
        {
            url += "/";
        }

        return url;
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
