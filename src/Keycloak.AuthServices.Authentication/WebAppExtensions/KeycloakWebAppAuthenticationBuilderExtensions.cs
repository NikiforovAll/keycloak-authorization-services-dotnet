namespace Keycloak.AuthServices.Authentication;

using Keycloak.AuthServices.Authentication.Claims;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
        string cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
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
            null,
            null,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );
    }

    /// <summary>
    /// Add authentication with Keycloak
    /// This method expects the configuration file will have a section, named "AzureAd" as default, with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configurationSection">The configuration section from which to get the options.</param>
    /// <param name="configureCookieAuthenticationOptions"></param>
    /// <param name="configureOpenIdConnectOptions"></param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <returns>The authentication builder for chaining.</returns>
    public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebApp(
        this AuthenticationBuilder builder,
        IConfigurationSection configurationSection,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions = null,
        Action<OpenIdConnectOptions>? configureOpenIdConnectOptions = null,
        string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
        string? cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        string? displayName = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configurationSection);

        return builder.AddKeycloakWebAppWithConfiguration(
            configureKeycloakOptions: options =>
                configurationSection.Bind(options, KeycloakFormatBinder.Instance),
            configureCookieAuthenticationOptions: configureCookieAuthenticationOptions,
            configureOpenIdConnectOptions: configureOpenIdConnectOptions,
            openIdConnectScheme: openIdConnectScheme,
            cookieScheme: cookieScheme,
            displayName: displayName,
            configurationSection: configurationSection
        );
    }

    /// <summary>
    /// Add authentication with Keycloak
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configureKeycloakOptions">The action to configure <see cref="KeycloakAuthenticationOptions"/>.</param>
    /// <param name="configureCookieAuthenticationOptions">The action to configure <see cref="CookieAuthenticationOptions"/>.</param>
    /// <param name="configureOpenIdConnectOptions"></param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <returns>The authentication builder for chaining.</returns>
    public static KeycloakWebAppAuthenticationBuilder AddKeycloakWebApp(
        this AuthenticationBuilder builder,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions = null,
        Action<OpenIdConnectOptions>? configureOpenIdConnectOptions = null,
        string openIdConnectScheme = OpenIdConnectDefaults.AuthenticationScheme,
        string? cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        string? displayName = null
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.AddKeycloakWebAppWithoutConfiguration(
            configureKeycloakOptions,
            configureCookieAuthenticationOptions,
            configureOpenIdConnectOptions,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );
    }

    /// <summary>
    /// Add authentication with Keycloak
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configureKeycloakOptions">The action to configure <see cref="KeycloakAuthenticationOptions"/>.</param>
    /// <param name="configureCookieAuthenticationOptions">The action to configure <see cref="CookieAuthenticationOptions"/>.</param>
    /// <param name="configureOpenIdConnectOptions"></param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <param name="configurationSection">Configuration section.</param>
    /// <returns>The authentication builder for chaining.</returns>
    private static KeycloakWebAppAuthenticationBuilder AddKeycloakWebAppWithConfiguration(
        this AuthenticationBuilder builder,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions,
        Action<OpenIdConnectOptions>? configureOpenIdConnectOptions,
        string openIdConnectScheme,
        string? cookieScheme,
        string? displayName,
        IConfigurationSection configurationSection
    )
    {
        AddKeycloakWebAppInternal(
            builder,
            configureKeycloakOptions,
            configureOpenIdConnectOptions,
            configureCookieAuthenticationOptions,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );

        return new KeycloakWebAppAuthenticationBuilder(
            builder.Services,
            openIdConnectScheme,
            configureOpenIdConnectOptions,
            configureKeycloakOptions,
            configurationSection
        );
    }

    /// <summary>
    /// Add authentication with Keycloak
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configureKeycloakOptions">The action to configure <see cref="KeycloakAuthenticationOptions"/>.</param>
    /// <param name="configureCookieAuthenticationOptions">The action to configure <see cref="CookieAuthenticationOptions"/>.</param>
    /// <param name="configureOpenIdConnectOptions"></param>
    /// <param name="openIdConnectScheme">The OpenID Connect scheme name to be used. By default it uses "OpenIdConnect".</param>
    /// <param name="cookieScheme">The cookie-based scheme name to be used. By default it uses "Cookies".</param>
    /// <param name="displayName">A display name for the authentication handler.</param>
    /// <returns>The authentication builder for chaining.</returns>
    private static KeycloakWebAppAuthenticationBuilder AddKeycloakWebAppWithoutConfiguration(
        this AuthenticationBuilder builder,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions,
        Action<OpenIdConnectOptions>? configureOpenIdConnectOptions,
        string openIdConnectScheme,
        string? cookieScheme,
        string? displayName
    )
    {
        AddKeycloakWebAppInternal(
            builder,
            configureKeycloakOptions,
            configureOpenIdConnectOptions,
            configureCookieAuthenticationOptions,
            openIdConnectScheme,
            cookieScheme,
            displayName
        );

        return new KeycloakWebAppAuthenticationBuilder(
            builder.Services,
            openIdConnectScheme,
            configureOpenIdConnectOptions,
            configureKeycloakOptions,
            null
        );
    }

    private static void AddKeycloakWebAppInternal(
        AuthenticationBuilder builder,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        Action<OpenIdConnectOptions>? configureOpenIdConnectOptions,
        Action<CookieAuthenticationOptions>? configureCookieAuthenticationOptions,
        string openIdConnectScheme,
        string? cookieScheme,
        string? displayName
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configureKeycloakOptions);

        builder.Services.AddTransient<IClaimsTransformation>(sp =>
        {
            var keycloakOptions = sp.GetRequiredService<
                IOptions<KeycloakAuthenticationOptions>
            >().Value;

            return new KeycloakRolesClaimsTransformation(
                keycloakOptions.RoleClaimType,
                keycloakOptions.RolesSource,
                keycloakOptions.Resource
            );
        });

        builder.Services.Configure(openIdConnectScheme, configureKeycloakOptions);

        if (!string.IsNullOrEmpty(cookieScheme))
        {
            builder.AddCookie(cookieScheme, configureCookieAuthenticationOptions ?? (_ => { }));
        }

        if (!string.IsNullOrEmpty(displayName))
        {
            builder.AddOpenIdConnect(openIdConnectScheme, displayName: displayName, _ => { });
        }
        else
        {
            builder.AddOpenIdConnect(openIdConnectScheme, _ => { });
        }

        builder
            .Services.AddOptions<OpenIdConnectOptions>(openIdConnectScheme)
            .Configure<IServiceProvider, IOptionsMonitor<KeycloakAuthenticationOptions>>(
                (options, serviceProvider, keycloakOptionsMonitor) =>
                {
                    var keycloakOptions = keycloakOptionsMonitor.Get(openIdConnectScheme);

                    if (!string.IsNullOrWhiteSpace(keycloakOptions.KeycloakUrlRealm))
                    {
                        options.Authority = keycloakOptions.KeycloakUrlRealm;
                    }

                    if (!string.IsNullOrEmpty(cookieScheme))
                    {
                        options.SignInScheme = cookieScheme;
                    }

                    var sslRequired =
                        string.IsNullOrWhiteSpace(keycloakOptions.SslRequired)
                        || keycloakOptions.SslRequired.Equals(
                            "external",
                            StringComparison.OrdinalIgnoreCase
                        );
                    options.RequireHttpsMetadata = sslRequired;
                    options.ClientId = keycloakOptions.Resource;
                    options.ClientSecret = keycloakOptions.Credentials?.Secret;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = keycloakOptions.TokenClockSkew,
                        ValidateIssuer = true,
                        NameClaimType = keycloakOptions.NameClaimType,
                        RoleClaimType = keycloakOptions.RoleClaimType,
                    };

                    configureOpenIdConnectOptions?.Invoke(options);
                }
            );
    }
}
