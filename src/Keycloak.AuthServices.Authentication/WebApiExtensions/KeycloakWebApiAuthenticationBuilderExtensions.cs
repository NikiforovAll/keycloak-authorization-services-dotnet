﻿namespace Keycloak.AuthServices.Authentication;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
    /// Protects the web API with Keycloak
    /// This method expects the configuration file will have a section, named "AzureAd" as default, with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configurationSection">The configuration second from which to fill-in the options.</param>
    /// <param name="jwtBearerScheme">The JWT bearer scheme name to be used. By default it uses "Bearer".</param>
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
    /// Protects the web API with Keycloak
    /// </summary>
    /// <param name="builder">The <see cref="AuthenticationBuilder"/> to which to add this configuration.</param>
    /// <param name="configureJwtBearerOptions">The action to configure <see cref="JwtBearerOptions"/>.</param>
    /// <param name="configureKeycloakOptions">The action to configure the <see cref="KeycloakAuthenticationOptions"/>.</param>
    /// <param name="jwtBearerScheme">The JWT bearer scheme name to be used. By default it uses "Bearer".</param>
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
        builder.AddJwtBearer(jwtBearerScheme, _ => { });

        builder
            .Services.AddOptions<JwtBearerOptions>(jwtBearerScheme)
            .Configure<IServiceProvider, IOptionsMonitor<KeycloakAuthenticationOptions>>(
                (options, serviceProvider, keycloakOptionsMonitor) =>
                {
                    var keycloakOptions = keycloakOptionsMonitor.Get(jwtBearerScheme);

                    // Ensure a well-formed authority was provided
                    if (!string.IsNullOrWhiteSpace(keycloakOptions.KeycloakUrlRealm))
                    {
                        options.Authority = keycloakOptions.KeycloakUrlRealm;
                    }

                    var sslRequired =
                        string.IsNullOrWhiteSpace(keycloakOptions.SslRequired)
                        || keycloakOptions.SslRequired.Equals(
                            "external",
                            StringComparison.OrdinalIgnoreCase
                        );
                    options.RequireHttpsMetadata = sslRequired;

                    var validationParameters = new TokenValidationParameters
                    {
                        ClockSkew = keycloakOptions.TokenClockSkew,
                        ValidateAudience = keycloakOptions.VerifyTokenAudience ?? true,
                        ValidateIssuer = true,
                        NameClaimType = keycloakOptions.NameClaimType,
                        RoleClaimType = keycloakOptions.RoleClaimType,
                    };
                    options.Audience = keycloakOptions.Audience ?? keycloakOptions.Resource;
                    options.TokenValidationParameters = validationParameters;
                    options.SaveToken = true;

                    options.Events ??= new JwtBearerEvents();

                    configureJwtBearerOptions?.Invoke(options);
                }
            );
    }
}
