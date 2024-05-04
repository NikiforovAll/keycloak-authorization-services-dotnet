namespace Keycloak.AuthServices.Sdk;

using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Protection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Adds HTTP Client SDKs
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="configureClient"></param>
    /// <param name="keycloakClientSectionName"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string keycloakClientSectionName = KeycloakAdminClientOptions.Section
    ) =>
        services.AddKeycloakAdminHttpClient(
            options => configuration.BindKeycloakOptions(options, keycloakClientSectionName),
            configureClient
        );

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        IConfigurationSection configurationSection,
        Action<HttpClient>? configureClient = default
    ) =>
        services.AddKeycloakAdminHttpClient(
            options => configurationSection.BindKeycloakOptions(options),
            configureClient
        );

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureKeycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>v
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        Action<KeycloakAdminClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        services.Configure(configureKeycloakOptions);

        services.AddTransient<IKeycloakRealmClient>(sp => sp.GetRequiredService<IKeycloakClient>());
        services.AddTransient<IKeycloakUserClient>(sp => sp.GetRequiredService<IKeycloakClient>());
        services.AddTransient<IKeycloakGroupClient>(sp => sp.GetRequiredService<IKeycloakClient>());

        return services
            .AddHttpClient(
                "keycloak_admin_api",
                (sp, http) =>
                {
                    var keycloakOptions = sp.GetRequiredService<
                        IOptions<KeycloakAdminClientOptions>
                    >();

                    http.BaseAddress = new Uri(keycloakOptions.Value.KeycloakUrlRealm);
                    configureClient?.Invoke(http);
                }
            )
            .AddTypedClient<IKeycloakClient, KeycloakClient>();
    }

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="keycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>v
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        KeycloakAdminClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        void configureKeycloakOptions(KeycloakAdminClientOptions options)
        {
            options.Realm = keycloakOptions.Realm;
            options.AuthServerUrl = keycloakOptions.AuthServerUrl;
            options.Resource = keycloakOptions.Resource;
            options.Credentials = keycloakOptions.Credentials;
            // redundant
            options.SslRequired = keycloakOptions.SslRequired;
            options.VerifyTokenAudience = keycloakOptions.VerifyTokenAudience;
            options.TokenClockSkew = keycloakOptions.TokenClockSkew;
        }

        return services.AddKeycloakAdminHttpClient(configureKeycloakOptions, configureClient);
    }

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="configureClient"></param>
    /// <param name="keycloakClientSectionName"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string keycloakClientSectionName = KeycloakProtectionClientOptions.Section
    ) =>
        services.AddKeycloakProtectionHttpClient(
            options => configuration.BindKeycloakOptions(options, keycloakClientSectionName),
            configureClient
        );

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        IConfigurationSection configurationSection,
        Action<HttpClient>? configureClient = default
    ) =>
        services.AddKeycloakProtectionHttpClient(
            options => configurationSection.BindKeycloakOptions(options),
            configureClient
        );

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureKeycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>v
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        Action<KeycloakProtectionClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        services.Configure(configureKeycloakOptions);

        return services
            .AddHttpClient(
                "keycloak_protection_api",
                (sp, http) =>
                {
                    var keycloakOptions = sp.GetRequiredService<
                        IOptions<KeycloakProtectionClientOptions>
                    >();

                    http.BaseAddress = new Uri(keycloakOptions.Value.KeycloakUrlRealm);
                    configureClient?.Invoke(http);
                }
            )
            .AddTypedClient<IKeycloakProtectionClient, KeycloakProtectionClient>();
    }

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="keycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>v
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        KeycloakProtectionClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        void configureKeycloakOptions(KeycloakProtectionClientOptions options)
        {
            options.Realm = keycloakOptions.Realm;
            options.AuthServerUrl = keycloakOptions.AuthServerUrl;
            options.Resource = keycloakOptions.Resource;
            options.Credentials = keycloakOptions.Credentials;
            // redundant
            options.SslRequired = keycloakOptions.SslRequired;
            options.VerifyTokenAudience = keycloakOptions.VerifyTokenAudience;
            options.TokenClockSkew = keycloakOptions.TokenClockSkew;
        }

        return services.AddKeycloakProtectionHttpClient(configureKeycloakOptions, configureClient);
    }
}
