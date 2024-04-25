namespace Keycloak.AuthServices.Authentication;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Represents a base class for configuring Keycloak authentication in an application.
/// </summary>
public class KeycloakWebApiAuthenticationBuilder : KeycloakBaseAuthenticationBuilder
{
    private readonly Action<JwtBearerOptions>? configureJwtBearerOptions;

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
        Action<JwtBearerOptions>? configureJwtBearerOptions,
        Action<KeycloakAuthenticationOptions> configureKeycloakOptions,
        IConfigurationSection? configurationSection
    )
        : base(services, configurationSection)
    {
        this.JwtBearerAuthenticationScheme = jwtBearerAuthenticationScheme;
        this.configureJwtBearerOptions = configureJwtBearerOptions;
        ArgumentNullException.ThrowIfNull(configureKeycloakOptions);

        this.Services.AddOptions<KeycloakAuthenticationOptions>(jwtBearerAuthenticationScheme)
            .Configure(configureKeycloakOptions)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    /// <summary>
    /// Gets or sets the JWT bearer authentication scheme.
    /// </summary>
    public string JwtBearerAuthenticationScheme { get; set; }
}
