namespace Keycloak.AuthServices.Authentication;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Authentication builder specific for Microsoft identity platform.
/// </summary>
public class KeycloakWebAppAuthenticationBuilder : KeycloakBaseAuthenticationBuilder
{
    /// <summary>
    ///  Constructor.
    /// </summary>
    /// <param name="services"> The services being configured.</param>
    /// <param name="openIdConnectScheme">Default scheme used for OpenIdConnect.</param>
    /// <param name="configureKeycloakOptions">Action called to configure
    /// the <see cref="KeycloakAuthenticationOptions"/>Microsoft identity options.</param>
    /// <param name="configurationSection">Optional configuration section.</param>
    internal KeycloakWebAppAuthenticationBuilder(
        IServiceCollection services,
        string openIdConnectScheme,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        IConfigurationSection? configurationSection
    )
        : base(services, configurationSection)
    {
        ArgumentNullException.ThrowIfNull(configureKeycloakOptions);

        this.OpenIdConnectScheme = openIdConnectScheme;

        ArgumentNullException.ThrowIfNull(configureKeycloakOptions);

        this.Services.Configure(openIdConnectScheme, configureKeycloakOptions);
    }

    /// <summary>
    /// Gets or sets the OpenIdConnect scheme.
    /// </summary>
    public string OpenIdConnectScheme { get; set; }
}
