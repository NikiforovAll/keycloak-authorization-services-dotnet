namespace Keycloak.AuthServices.Authentication;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Represents a base class for configuring Keycloak authentication in an application.
/// </summary>
public class KeycloakWebApiAuthenticationBuilder : KeycloakBaseAuthenticationBuilder
{
    private readonly Action<JwtBearerOptions, IServiceProvider>? configureJwtBearerOptions;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="services">The services being configured.</param>
    /// <param name="jwtBearerAuthenticationScheme">Default scheme used for OpenIdConnect.</param>
    /// <param name="configureJwtBearerOptions">Action called to configure the JwtBearer options.</param>
    /// <param name="configureKeycloakOptions">Action called to configure
    /// the <see cref="KeycloakAuthenticationOptions"/>Microsoft identity options.</param>
    /// <param name="configurationSection">Configuration section from which to
    /// get parameters.</param>
    internal KeycloakWebApiAuthenticationBuilder(
        IServiceCollection services,
        string jwtBearerAuthenticationScheme,
        Action<JwtBearerOptions, IServiceProvider>? configureJwtBearerOptions,
        Action<KeycloakAuthenticationOptions, IServiceProvider> configureKeycloakOptions,
        IConfigurationSection? configurationSection
    )
        : base(services, configurationSection)
    {
        this.JwtBearerAuthenticationScheme = jwtBearerAuthenticationScheme;
        this.configureJwtBearerOptions = configureJwtBearerOptions;
        ArgumentNullException.ThrowIfNull(configureKeycloakOptions);

        this.Services.AddSingleton<IConfigureOptions<KeycloakAuthenticationOptions>>(serviceProvider =>
            new ConfigureNamedOptions<KeycloakAuthenticationOptions>(jwtBearerAuthenticationScheme, options => configureKeycloakOptions(options, serviceProvider)));
    }

    /// <summary>
    /// Gets or sets the JWT bearer authentication scheme.
    /// </summary>
    public string JwtBearerAuthenticationScheme { get; set; }
}
