namespace Keycloak.AuthServices.Common;

/// <summary>
/// Installation options provided by Keycloak
/// </summary>
/// <remarks>
/// <seealso cref="/.well-known/openid-configuration"/>
/// </remarks>
public class KeycloakInstallationOptions
{
    public string AuthServerUrl { get; set; } = string.Empty;

    public string Realm { get; set; } = string.Empty;

    public string Resource { get; set; } = string.Empty;

    public bool VerifyTokenAudience { get; set; }

    public KeycloakClientInstallationCredentials Credentials { get; set; } = new();

    /// <summary>
    ///     Optional
    /// </summary>
    /// <remarks>
    ///     - Default: 0 seconds
    /// </remarks>
    public TimeSpan TokenClockSkew { get; set; } = TimeSpan.Zero;

    public string KeycloakUrlRealm => $"{NormalizeUrl(this.AuthServerUrl)}/realms/{this.Realm.ToLowerInvariant()}";

    private static string NormalizeUrl(string url)
    {
        var urlNormalized = !url.EndsWith('/') ? url : url.TrimEnd('/');

        return urlNormalized.ToLowerInvariant();
    }
}

public class KeycloakClientInstallationCredentials
{
    public string Secret { get; set; } = string.Empty;
}

public static class ConfigurationConstants
{
    public const string ConfigurationPrefix = "Keycloak";
}
