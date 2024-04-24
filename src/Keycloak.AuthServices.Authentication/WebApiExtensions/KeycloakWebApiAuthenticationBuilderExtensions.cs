namespace Keycloak.AuthServices.Authentication;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for <see cref="AuthenticationBuilder"/> for startup initialization of web APIs.
/// </summary>
public static class KeycloakWebApiAuthenticationBuilderExtensions
{
    /// <summary>
    /// Protects the web API with Keycloak
    /// This method expects the configuration file will have a section, named "Keycloak" as default, with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configuration">The configuration instance.</param>
    /// <param name="configSectionName">The configuration section with the necessary settings to initialize authentication options.</param>
    /// <param name="jwtBearerScheme">The JWT bearer scheme name to be used. By default it uses "Bearer".</param>
    /// Set to true if you want to debug, or just understand the JWT bearer events.
    /// </param>
    /// <returns>The authentication builder to chain.</returns>
    public static KeycloakWebApiAuthenticationBuilder AddKeycloakWebApi(
        this AuthenticationBuilder builder,
        IConfiguration configuration,
        string configSectionName = KeycloakAuthenticationOptions.Section,
        string jwtBearerScheme = JwtBearerDefaults.AuthenticationScheme
    )
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(configSectionName);

        var configurationSection = configuration.GetSection(configSectionName);

        return builder.AddKeycloakWebApi(configurationSection, jwtBearerScheme);
    }

    /// <summary>
    /// Protects the web API with Microsoft identity platform (formerly Azure AD v2.0).
    /// This method expects the configuration file will have a section, named "AzureAd" as default, with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configurationSection">The configuration second from which to fill-in the options.</param>
    /// <param name="jwtBearerScheme">The JWT bearer scheme name to be used. By default it uses "Bearer".</param>
    /// Set to true if you want to debug, or just understand the JWT bearer events.
    /// </param>
    /// <returns>The authentication builder to chain.</returns>
    public static KeycloakWebApiAuthenticationBuilder AddKeycloakWebApi(
        this AuthenticationBuilder builder,
        IConfigurationSection configurationSection,
        string jwtBearerScheme = JwtBearerDefaults.AuthenticationScheme
    )
    {
        ArgumentNullException.ThrowIfNull(configurationSection);
        ArgumentNullException.ThrowIfNull(builder);

        AddKeycloakWebApiImplementation(
            builder,
            options => configurationSection.Bind(options),
            jwtBearerScheme
        );

        return new KeycloakWebApiAuthenticationBuilder(
            builder.Services,
            jwtBearerScheme,
            options => configurationSection.Bind(options),
            options => configurationSection.Bind(options),
            configurationSection
        );
    }

    /// <summary>
    /// Protects the web API with Microsoft identity platform (formerly Azure AD v2.0).
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configureJwtBearerOptions">The action to configure <see cref="JwtBearerOptions"/>.</param>
    /// <param name="configureKeycloakOptions">The action to configure the <see cref="KeycloakAuthenticationOptions"/>.</param>
    /// <param name="jwtBearerScheme">The JWT bearer scheme name to be used. By default it uses "Bearer".</param>
    /// Set to true if you want to debug, or just understand the JWT bearer events.</param>
    /// <returns>The authentication builder to chain.</returns>
    public static KeycloakWebApiAuthenticationBuilder AddKeycloakWebApi(
        this AuthenticationBuilder builder,
        Action<JwtBearerOptions> configureJwtBearerOptions,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        string jwtBearerScheme = JwtBearerDefaults.AuthenticationScheme
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configureJwtBearerOptions);
        ArgumentNullException.ThrowIfNull(configureKeycloakOptions);

        AddKeycloakWebApiImplementation(builder, configureJwtBearerOptions, jwtBearerScheme);

        return new KeycloakWebApiAuthenticationBuilder(
            services: builder.Services,
            jwtBearerAuthenticationScheme: jwtBearerScheme,
            configureJwtBearerOptions: configureJwtBearerOptions,
            configureKeycloakOptions: configureKeycloakOptions,
            configurationSection: null
        );
    }

    private static void AddKeycloakWebApiImplementation(
        AuthenticationBuilder builder,
        Action<JwtBearerOptions> configureJwtBearerOptions,
        string jwtBearerScheme
    )
    {
        // TODO: add merging logic

        builder.AddJwtBearer(jwtBearerScheme, configureJwtBearerOptions);
    }
}
