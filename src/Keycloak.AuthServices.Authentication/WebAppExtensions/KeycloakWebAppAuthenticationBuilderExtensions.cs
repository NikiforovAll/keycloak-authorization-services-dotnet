namespace Keycloak.AuthServices.Authentication;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Extensions for the <see cref="AuthenticationBuilder"/> for startup initialization.
/// </summary>
public static class KeycloakWebAppAuthenticationBuilderExtensions
{
    /// <summary>
    /// Add authentication to a web app with Keycloak
    /// This method expects the configuration file will have a section, named "Keycloak" as default,
    /// with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configuration">The configuration instance.</param>
    /// <param name="configSectionName">The configuration section with the necessary settings to initialize authentication options.</param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <returns>The <see cref="KeycloakWebAppAuthenticationBuilder"/> builder for chaining.</returns>
    public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebApp(
        this AuthenticationBuilder builder,
        IConfiguration configuration,
        string configSectionName = KeycloakAuthenticationOptions.Section,
        string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
        string? cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        string? displayName = null
    )
    {
        ArgumentNullException.ThrowIfNull(configuration);

        if (string.IsNullOrEmpty(configSectionName))
        {
            throw new ArgumentException(null, nameof(configSectionName));
        }

        var configurationSection = configuration.GetSection(configSectionName);

        return builder.AddKeycloakWebApp(
            configurationSection,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );
    }

    /// <summary>
    /// Add authentication with Microsoft identity platform.
    /// This method expects the configuration file will have a section, named "AzureAd" as default, with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configurationSection">The configuration section from which to get the options.</param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <returns>The authentication builder for chaining.</returns>
    public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebApp(
        this AuthenticationBuilder builder,
        IConfigurationSection configurationSection,
        string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
        string? cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        string? displayName = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configurationSection);

        return builder.AddKeycloakWebAppWithConfiguration(
            configureKeycloakOptions: options => configurationSection.Bind(options),
            configureCookieAuthenticationOptions: null,
            openIdConnectScheme: openIdConnectScheme,
            cookieScheme: cookieScheme,
            displayName: displayName,
            configurationSection: configurationSection
        );
    }

    /// <summary>
    /// Add authentication with Microsoft identity platform.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configureKeycloakOptions">The action to configure <see cref="KeycloakAuthenticationOptions"/>.</param>
    /// <param name="configureCookieAuthenticationOptions">The action to configure <see cref="CookieAuthenticationOptions"/>.</param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <returns>The authentication builder for chaining.</returns>
    public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebApp(
        this AuthenticationBuilder builder,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions = null,
        string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
        string? cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        string? displayName = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AddKeycloakWebAppWithoutConfiguration(
            configureKeycloakOptions,
            configureCookieAuthenticationOptions,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );
    }

    /// <summary>
    /// Add authentication with Microsoft identity platform.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configureKeycloakOptions">The action to configure <see cref="KeycloakAuthenticationOptions"/>.</param>
    /// <param name="configureCookieAuthenticationOptions">The action to configure <see cref="CookieAuthenticationOptions"/>.</param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <param name="configurationSection">Configuration section.</param>
    /// <returns>The authentication builder for chaining.</returns>
    private static KeycloakWebAppAuthenticationBuilder AddKeycloakWebAppWithConfiguration(
        this AuthenticationBuilder builder,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions,
        string openIdConnectScheme,
        string? cookieScheme,
        string? displayName,
        IConfigurationSection configurationSection
    )
    {
        AddKeycloakWebAppInternal(
            builder,
            configureKeycloakOptions,
            configureCookieAuthenticationOptions,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );

        return new KeycloakWebAppAuthenticationBuilder(
            builder.Services,
            openIdConnectScheme,
            configureKeycloakOptions,
            configurationSection
        );
    }

    /// <summary>
    /// Add authentication with Microsoft identity platform.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configureKeycloakOptions">The action to configure <see cref="KeycloakAuthenticationOptions"/>.</param>
    /// <param name="configureCookieAuthenticationOptions">The action to configure <see cref="CookieAuthenticationOptions"/>.</param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <returns>The authentication builder for chaining.</returns>
    private static KeycloakWebAppAuthenticationBuilder AddKeycloakWebAppWithoutConfiguration(
        this AuthenticationBuilder builder,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions,
        string openIdConnectScheme,
        string? cookieScheme,
        string? displayName
    )
    {
        AddKeycloakWebAppInternal(
            builder,
            configureKeycloakOptions,
            configureCookieAuthenticationOptions,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );

        return new KeycloakWebAppAuthenticationBuilder(
            builder.Services,
            openIdConnectScheme,
            configureKeycloakOptions,
            null
        );
    }

    private static void AddKeycloakWebAppInternal(
        AuthenticationBuilder builder,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions,
        string openIdConnectScheme,
        string? cookieScheme,
        string? displayName
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configureKeycloakOptions);

        builder.Services.Configure(openIdConnectScheme, configureKeycloakOptions);

        if (!string.IsNullOrEmpty(cookieScheme))
        {
            Action<CookieAuthenticationOptions> emptyOption = option => { };
            builder.AddCookie(cookieScheme, configureCookieAuthenticationOptions ?? emptyOption);
        }

        if (!string.IsNullOrEmpty(displayName))
        {
            builder.AddOpenIdConnect(openIdConnectScheme, displayName: displayName, options => { });
        }
        else
        {
            builder.AddOpenIdConnect(openIdConnectScheme, options => { });
        }

        builder
            .Services.AddOptions<OpenIdConnectOptions>(openIdConnectScheme)
            .Configure<IServiceProvider, IOptions<KeycloakAuthenticationOptions>>(
                (options, serviceProvider, keycloakOptions) => {
                    // TODO:
                }
            );
    }
}
