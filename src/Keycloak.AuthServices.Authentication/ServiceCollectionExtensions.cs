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
            .GetSection(ConfigurationConstants.ConfigurationPrefix)
            .Get<KeycloakInstallationOptions>();

        services.AddOptions<KeycloakInstallationOptions>()
            .Bind(configuration);

        services.AddSingleton(options);

        const string roleClaimType = "role";
        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = options.TokenClockSkew,
            ValidateAudience = options.VerifyTokenAudience,
            ValidateIssuer = true,
            NameClaimType = "preferred_username",
            RoleClaimType = roleClaimType, // TODO: clarify how keycloak writes roles
        };

        // options.Resource == Audience
        services.AddTransient<IClaimsTransformation>(_ =>
            new KeycloakRolesClaimsTransformation(roleClaimType, options.Resource));

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
