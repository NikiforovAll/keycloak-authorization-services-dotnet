namespace Keycloak.AuthServices.Authentication;

using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension for IServiceCollection for startup initialization of web APIs.
/// </summary>
public static partial class KeycloakWebApiServiceCollectionExtensions
{
    /// <summary>
    /// Protects the web API with Keycloak.
    /// This method expects the configuration file will have a section, named "Keycloak" as default, with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="services">The service collection to which to add authentication.</param>
    /// <param name="configuration">The Configuration object.</param>
    /// <param name="configSectionName">The configuration section with the necessary settings to initialize authentication options. Default value is "KeycloakAuthenticationOptions.Section".</param>
    /// <param name="jwtBearerScheme">The JwtBearer scheme name to be used. Default value is "JwtBearerDefaults.AuthenticationScheme".</param>
    /// <returns>The authentication builder to chain extension methods.</returns>
    public static KeycloakWebApiAuthenticationBuilder AddKeycloakWebApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        string configSectionName = KeycloakAuthenticationOptions.Section,
        string jwtBearerScheme = JwtBearerDefaults.AuthenticationScheme
    )
    {
        var builder = services.AddAuthentication(jwtBearerScheme);

        return builder.AddKeycloakWebApi(configuration, configSectionName, jwtBearerScheme);
    }

    /// <summary>
    /// Protects the web API with Keycloak.
    /// This method expects the configuration file will have a section, named "Keycloak" as default, with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="services">The service collection to which to add authentication.</param>
    /// <param name="configuration">The Configuration object.</param>
    /// <param name="configureJwtBearerOptions">A delegate to configure the JwtBearerOptions.</param>
    /// <param name="configSectionName">The configuration section with the necessary settings to initialize authentication options. Default value is "KeycloakAuthenticationOptions.Section".</param>
    /// <param name="jwtBearerScheme">The JwtBearer scheme name to be used. Default value is "JwtBearerDefaults.AuthenticationScheme".</param>
    /// <returns>The authentication builder to chain extension methods.</returns>
    public static KeycloakWebApiAuthenticationBuilder AddKeycloakWebApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<JwtBearerOptions> configureJwtBearerOptions,
        string configSectionName = KeycloakAuthenticationOptions.Section,
        string jwtBearerScheme = JwtBearerDefaults.AuthenticationScheme
    )
    {
        var builder = services.AddAuthentication(jwtBearerScheme);

        return builder.AddKeycloakWebApi(
            opt =>
                configuration
                    .GetSection(configSectionName)
                    .Bind(opt, KeycloakInstallationOptions.KeycloakFormatBinder),
            configureJwtBearerOptions,
            jwtBearerScheme
        );
    }
}
