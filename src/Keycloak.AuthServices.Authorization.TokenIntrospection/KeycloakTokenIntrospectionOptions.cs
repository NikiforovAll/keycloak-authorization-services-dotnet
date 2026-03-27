namespace Keycloak.AuthServices.Authorization.TokenIntrospection;

using System.Security.Claims;
using System.Text.Json;
using Keycloak.AuthServices.Common;

/// <summary>
/// Options for Keycloak token introspection.
/// Used to resolve full claim sets from lightweight access tokens.
/// </summary>
public class KeycloakTokenIntrospectionOptions : KeycloakInstallationOptions
{
    /// <summary>
    /// Default section name.
    /// </summary>
    public const string Section = ConfigurationConstants.ConfigurationPrefix;

    /// <summary>
    /// Cache duration for introspection results. Default is 60 seconds.
    /// </summary>
    public TimeSpan CacheDuration { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>
    /// Optional delegate invoked after the default claim enrichment logic runs.
    /// Use this to add, remove, or transform claims from the raw introspection response.
    /// </summary>
    public Action<
        ClaimsIdentity,
        Dictionary<string, JsonElement>
    >? OnTokenIntrospected { get; set; }
}
