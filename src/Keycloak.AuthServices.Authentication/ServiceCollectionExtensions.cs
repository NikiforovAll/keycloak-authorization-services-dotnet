namespace Keycloak.AuthServices.Authentication;

using Claims;
using Common;
using Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Configures Authentication via Keycloak
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds keycloak authentication services.
    /// </summary>
    public static AuthenticationBuilder AddKeycloakAuthentication(
        this IServiceCollection services,
        KeycloakInstallationOptions keycloakOptions,
        Action<JwtBearerOptions>? configureOptions = default)
    {
        services.AddSingleton(keycloakOptions);

        const string roleClaimType = "role";
        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = keycloakOptions.TokenClockSkew,
            ValidateAudience = keycloakOptions.VerifyTokenAudience,
            ValidateIssuer = true,
            NameClaimType = "preferred_username",
            RoleClaimType = roleClaimType, // TODO: clarify how keycloak writes roles
        };

        // options.Resource == Audience
        services.AddTransient<IClaimsTransformation>(_ =>
            new KeycloakRolesClaimsTransformation(roleClaimType, keycloakOptions.Resource));

        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                var sslRequired = string.IsNullOrWhiteSpace(keycloakOptions.SslRequired)
                    || keycloakOptions.SslRequired
                        .Equals("external", StringComparison.OrdinalIgnoreCase);

                opts.Authority = keycloakOptions.KeycloakUrlRealm;
                opts.Audience = keycloakOptions.Resource;
                opts.TokenValidationParameters = validationParameters;
                opts.RequireHttpsMetadata = sslRequired;
                opts.SaveToken = true;
                configureOptions?.Invoke(opts);
            });
    }

    /// <summary>
    /// Adds keycloak authentication services. Configuration is automatically wired from keycloak.json file.
    /// </summary>
    /// <param name="services">Source service collection</param>
    /// <param name="configuration">Configuration source,
    /// make sure the <see cref="ConfigureKeycloakConfigurationSource"/> is configured</param>
    /// <param name="configureOptions">Configure overrides</param>
    /// <returns></returns>
    public static AuthenticationBuilder AddKeycloakAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<JwtBearerOptions>? configureOptions = default)
    {
        var options = configuration
            .GetSection(ConfigurationConstants.ConfigurationPrefix)
            .Get<KeycloakInstallationOptions>();

        services.AddOptions<KeycloakInstallationOptions>()
            .Bind(configuration);

        return services.AddKeycloakAuthentication(options, configureOptions);
    }

    /// <summary>
    /// Adds keycloak authentication services from configuration located in specified section <paramref name="keycloakClientSectionName"/>.
    /// </summary>
    /// <param name="services">Source service collection</param>
    /// <param name="configuration">Configuration source,
    /// make sure the <see cref="ConfigureKeycloakConfigurationSource"/> is configured</param>
    /// <param name="keycloakClientSectionName"></param>
    /// <param name="configureOptions">Configure overrides</param>
    /// <returns></returns>
    public static AuthenticationBuilder AddKeycloakAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        string keycloakClientSectionName,
        Action<JwtBearerOptions>? configureOptions = default)
    {
        var options = configuration
            .GetSection(keycloakClientSectionName)
            .Get<KeycloakInstallationOptions>() ?? new KeycloakInstallationOptions();

        return services.AddKeycloakAuthentication(options, configureOptions);
    }

    /// <summary>
    /// Adds configuration source based on keycloak.json
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static IHostBuilder ConfigureKeycloakConfigurationSource(
        this IHostBuilder hostBuilder, string fileName = "keycloak.json") =>
        hostBuilder.ConfigureAppConfiguration((_, builder) =>
        {
            var source = new KeycloakConfigurationSource { Path = fileName, Optional = false };
            builder.Sources.Insert(0, source);
        });
}
