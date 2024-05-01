namespace Keycloak.AuthServices.Authentication;

using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Configures Authentication via Keycloak
/// </summary>
[Obsolete("This class will be removed")]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds keycloak authentication services.
    /// </summary>
    [Obsolete(
        "This method will be removed. Use AddKeycloakWebApiAuthentication. Furthermore, the way KeycloakAuthenticationOptions is changed and you need to specify KeycloakFormatBinder.Instance to correctly bind the instance. See for more details https://nikiforovall.github.io/keycloak-authorization-services-dotnet/migration.html#key-changes-in-2-0-0"
    )]
    public static AuthenticationBuilder AddKeycloakAuthentication(
        this IServiceCollection services,
        KeycloakAuthenticationOptions keycloakOptions,
        Action<JwtBearerOptions>? configureOptions = default
    )
    {
        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = keycloakOptions.TokenClockSkew,
            ValidateAudience = keycloakOptions.VerifyTokenAudience ?? true,
            ValidateIssuer = true,
            NameClaimType = keycloakOptions.NameClaimType,
            RoleClaimType = keycloakOptions.RoleClaimType,
        };

        return services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                var sslRequired =
                    string.IsNullOrWhiteSpace(keycloakOptions.SslRequired)
                    || keycloakOptions.SslRequired.Equals(
                        "external",
                        StringComparison.OrdinalIgnoreCase
                    );

                opts.Authority = keycloakOptions.KeycloakUrlRealm;
                opts.Audience = keycloakOptions.Resource;
                opts.TokenValidationParameters = validationParameters;
                opts.RequireHttpsMetadata = sslRequired;
                opts.SaveToken = true;
                configureOptions?.Invoke(opts);
            });
    }

    /// <summary>
    /// Adds keycloak authentication services from configuration located in specified default section.
    /// </summary>
    /// <param name="services">Source service collection</param>
    /// <param name="configuration">Configuration source</param>
    /// <param name="configureOptions">Configure overrides</param>
    /// <returns></returns>
    [Obsolete("This method will be removed. Use AddKeycloakWebApiAuthentication")]
    public static AuthenticationBuilder AddKeycloakAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<JwtBearerOptions>? configureOptions = default
    )
    {
        var authenticationOptions = configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .Get<KeycloakAuthenticationOptions>(KeycloakFormatBinder.Instance)!;

        return services.AddKeycloakAuthentication(authenticationOptions, configureOptions);
    }

    /// <summary>
    /// Adds keycloak authentication services from section
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="keycloakClientSectionName"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    [Obsolete("This method will be removed. Use AddKeycloakWebApiAuthentication")]
    public static AuthenticationBuilder AddKeycloakAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        string keycloakClientSectionName,
        Action<JwtBearerOptions>? configureOptions = default
    )
    {
        var authenticationOptions = configuration
            .GetSection(keycloakClientSectionName)
            .Get<KeycloakAuthenticationOptions>(KeycloakFormatBinder.Instance)!;

        return services.AddKeycloakAuthentication(authenticationOptions, configureOptions);
    }
}
