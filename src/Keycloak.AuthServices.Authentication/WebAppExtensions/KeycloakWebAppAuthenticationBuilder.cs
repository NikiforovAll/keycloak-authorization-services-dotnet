namespace Keycloak.AuthServices.Authentication;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Authentication builder specific for Microsoft identity platform.
/// </summary>
public class KeycloakWebAppAuthenticationBuilder : KeycloakBaseAuthenticationBuilder
{
    private readonly Action<OpenIdConnectOptions>? configureOpenIdConnectOptions;

    /// <summary>
    ///  Constructor.
    /// </summary>
    /// <param name="services"> The services being configured.</param>
    /// <param name="openIdConnectScheme">Default scheme used for OpenIdConnect.</param>
    /// <param name="configureOpenIdConnectOptions"></param>
    /// <param name="configureKeycloakOptions">Action called to configure
    /// the <see cref="KeycloakAuthenticationOptions"/>Microsoft identity options.</param>
    /// <param name="configurationSection">Optional configuration section.</param>
    internal KeycloakWebAppAuthenticationBuilder(
        IServiceCollection services,
        string openIdConnectScheme,
        Action<OpenIdConnectOptions>? configureOpenIdConnectOptions,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        IConfigurationSection? configurationSection
    )
        : base(services, configurationSection)
    {
        this.OpenIdConnectScheme = openIdConnectScheme;
        this.configureOpenIdConnectOptions = configureOpenIdConnectOptions;
        ArgumentNullException.ThrowIfNull(configureKeycloakOptions);

        this.Services.AddOptions<KeycloakAuthenticationOptions>(openIdConnectScheme)
            .Configure(configureKeycloakOptions);
    }

    /// <summary>
    /// Gets or sets the OpenIdConnect scheme.
    /// </summary>
    public string OpenIdConnectScheme { get; set; }
}
