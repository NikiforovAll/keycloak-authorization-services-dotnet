namespace Keycloak.AuthServices.Authentication;

using Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds keycloak authentication services, configuration is automatically wired from keycloak.json file.
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
            .GetSection(KeycloakConfigurationProvider.ConfigurationPrefix)
            .Get<KeycloakInstallationOptions>();

        services.AddOptions<KeycloakInstallationOptions>()
            .Bind(configuration);

        services.AddSingleton(options);

        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = options.TokenClockSkew,
            ValidateAudience = options.VerifyTokenAudience,
            ValidateIssuer = true,
        };

        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                opts.Authority = options.KeycloakUrlRealm;
                opts.Audience = options.Resource;
                opts.TokenValidationParameters = validationParameters;
                opts.RequireHttpsMetadata = true;
                opts.SaveToken = true;
                configureOptions?.Invoke(opts);
            });
    }

    public static IHostBuilder ConfigureKeycloakConfigurationSource(
        this IHostBuilder hostBuilder, string fileName = "keycloak.json") =>
            hostBuilder.ConfigureAppConfiguration((_, builder) =>
            {
                var source = new KeycloakConfigurationSource
                {
                    Path = fileName,
                    Optional = false
                };
                builder.Sources.Insert(0, source);
            });
}
