namespace Keycloak.AuthServices.Authentication;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension for IServiceCollection for startup initialization of web APIs.
/// </summary>
public static partial class KeycloakWebApiServiceCollectionExtensions
{
    /// <summary>
    /// Protects the web API with Keycloak
    /// This method expects the configuration file will have a section, named "Keycloak" as default, with the necessary settings to initialize authentication options.
    /// </summary>
    /// <param name="services">Service collection to which to add authentication.</param>
    /// <param name="configuration">The Configuration object.</param>
    /// <param name="configSectionName">The configuration section with the necessary settings to initialize authentication options.</param>
    /// <param name="jwtBearerScheme">The JwtBearer scheme name to be used. By default it uses "Bearer".</param>
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
}
