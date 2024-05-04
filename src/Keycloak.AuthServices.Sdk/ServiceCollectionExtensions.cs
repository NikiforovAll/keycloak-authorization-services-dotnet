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
    /// Adds an HttpClient for Keycloak Admin API to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configuration">The IConfiguration instance to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <param name="keycloakClientSectionName">The name of the configuration section containing the Keycloak client options.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
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
    /// Adds an HttpClient for Keycloak Admin API to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configurationSection">The IConfigurationSection to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
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
    /// Adds an HttpClient for Keycloak Admin API to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configureKeycloakOptions">An action to configure the Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
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
    /// Adds an HttpClient for Keycloak Admin API to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="keycloakOptions">The Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
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
    /// Adds an HttpClient for Keycloak Protection API to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configuration">The IConfiguration instance to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <param name="keycloakClientSectionName">The name of the configuration section containing the Keycloak client options.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
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
    /// Adds an HttpClient for Keycloak Protection API to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configurationSection">The IConfigurationSection to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
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
    /// Adds an HttpClient for Keycloak Protection API to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configureKeycloakOptions">An action to configure the Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        Action<KeycloakProtectionClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        services.Configure(configureKeycloakOptions);

        services.AddTransient<IKeycloakProtectedResourceClient>(sp =>
            sp.GetRequiredService<IKeycloakProtectionClient>()
        );
        services.AddTransient<IKeycloakPolicyClient>(sp =>
            sp.GetRequiredService<IKeycloakProtectionClient>()
        );

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
    /// Adds an HttpClient for Keycloak Protection API to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="keycloakOptions">The Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
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
