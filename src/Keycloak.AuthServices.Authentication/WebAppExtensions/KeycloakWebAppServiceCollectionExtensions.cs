namespace Keycloak.AuthServices.Authentication.WebAppExtensions;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension for IServiceCollection for startup initialization.
/// </summary>
public static partial class KeycloakWebAppServiceCollectionExtensions
{
    /// <summary>
    /// Add authentication with Microsoft identity platform.
    /// This method expects the configuration file will have a section, (by default named "AzureAd"), with the necessary settings to
    /// initialize the authentication options.
    /// </summary>
    /// <param name="services">Service collection to which to add authentication.</param>
    /// <param name="configuration">The IConfiguration object.</param>
    /// <param name="configSectionName">The name of the configuration section with the necessary
    /// settings to initialize authentication options.</param>
    /// <param name="openIdConnectScheme">Optional name for the open id connect authentication scheme
    /// (by default OpenIdConnectDefaults.AuthenticationScheme). This can be specified when you want to support
    /// several OpenIdConnect identity providers.</param>
    /// <param name="cookieScheme">Optional name for the cookie authentication scheme
    /// (by default CookieAuthenticationDefaults.AuthenticationScheme).</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <returns>The authentication builder to chain extension methods.</returns>

    public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebAppAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        string configSectionName = KeycloakAuthenticationOptions.Section,
        string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
        string cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        string? displayName = null
    )
    {
        var builder = services.AddAuthentication(openIdConnectScheme);

        return builder.AddKeycloakWebApp(
            configuration,
            configSectionName,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );
    }
}
