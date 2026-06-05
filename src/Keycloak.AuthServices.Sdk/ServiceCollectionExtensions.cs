// ReSharper disable MemberCanBePrivate.Global
namespace Keycloak.AuthServices.Sdk;

using Common;
using Admin;
using Protection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Adds HTTP Client SDKs
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds <see cref="IKeycloakClient"/>, <see cref="IKeycloakRealmClient"/>, <see cref="IKeycloakUserClient"/>, <see cref="IKeycloakGroupClient"/> HTTP clients for Keycloak Admin API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configuration">The IConfiguration instance to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <param name="keycloakClientSectionName">The name of the configuration section containing the Keycloak client options.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = null,
        string keycloakClientSectionName = KeycloakAdminClientOptions.Section
    ) =>
        services.AddKeycloakAdminHttpClient(
            options => configuration.BindKeycloakOptions(options, keycloakClientSectionName),
            configureClient
        );

    /// <summary>
    /// Adds <see cref="IKeycloakClient"/>, <see cref="IKeycloakRealmClient"/>, <see cref="IKeycloakUserClient"/>, <see cref="IKeycloakGroupClient"/> HTTP clients for Keycloak Admin API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configurationSection">The IConfigurationSection to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        IConfigurationSection configurationSection,
        Action<HttpClient>? configureClient = null
    ) =>
        services.AddKeycloakAdminHttpClient(
            configurationSection.BindKeycloakOptions,
            configureClient
        );

    /// <summary>
    /// Adds <see cref="IKeycloakClient"/>, <see cref="IKeycloakRealmClient"/>, <see cref="IKeycloakUserClient"/>, <see cref="IKeycloakGroupClient"/> for Keycloak Admin API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configureKeycloakOptions">An action to configure the Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        Action<KeycloakAdminClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = null
    )
    {
        services
            .AddOptions<KeycloakAdminClientOptions>()
            .Configure(configureKeycloakOptions)
            .ValidateOnStart();

        services.AddSingleton<
            IValidateOptions<KeycloakAdminClientOptions>,
            KeycloakAdminClientOptionsValidator
        >();

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

                    http.BaseAddress = new Uri(keycloakOptions.Value.AuthServerUrl!);
                    configureClient?.Invoke(http);
                }
            )
            .AddTypedClient<IKeycloakClient, KeycloakClient>();
    }

    /// <summary>
    /// Adds <see cref="IKeycloakClient"/>, <see cref="IKeycloakRealmClient"/>, <see cref="IKeycloakUserClient"/>, <see cref="IKeycloakGroupClient"/> HTTP clients for Keycloak Admin API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="keycloakOptions">The Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        KeycloakAdminClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = null
    )
    {
        return services.AddKeycloakAdminHttpClient(ConfigureKeycloakOptions, configureClient);

        void ConfigureKeycloakOptions(KeycloakAdminClientOptions options)
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
    }

    /// <summary>
    /// Adds <see cref="IKeycloakProtectionClient"/>, <see cref="IKeycloakProtectedResourceClient"/>, <see cref="IKeycloakPolicyClient"/> HTTP clients for Protection API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configuration">The IConfiguration instance to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <param name="keycloakClientSectionName">The name of the configuration section containing the Keycloak client options.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = null,
        string keycloakClientSectionName = KeycloakProtectionClientOptions.Section
    ) =>
        services.AddKeycloakProtectionHttpClient(
            options => configuration.BindKeycloakOptions(options, keycloakClientSectionName),
            configureClient
        );

    /// <summary>
    /// Adds <see cref="IKeycloakProtectionClient"/>, <see cref="IKeycloakProtectedResourceClient"/>, <see cref="IKeycloakPolicyClient"/> HTTP clients for Protection API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configurationSection">The IConfigurationSection to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        IConfigurationSection configurationSection,
        Action<HttpClient>? configureClient = null
    ) =>
        services.AddKeycloakProtectionHttpClient(
            configurationSection.BindKeycloakOptions,
            configureClient
        );

    /// <summary>
    /// Adds <see cref="IKeycloakProtectionClient"/>, <see cref="IKeycloakProtectedResourceClient"/>, <see cref="IKeycloakPolicyClient"/> HTTP clients for Protection API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configureKeycloakOptions">An action to configure the Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        Action<KeycloakProtectionClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = null
    )
    {
        services
            .AddOptions<KeycloakProtectionClientOptions>()
            .Configure(configureKeycloakOptions)
            .ValidateOnStart();

        services.AddSingleton<
            IValidateOptions<KeycloakProtectionClientOptions>,
            KeycloakProtectionClientOptionsValidator
        >();

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

                    http.BaseAddress = new Uri(keycloakOptions.Value.AuthServerUrl!);
                    configureClient?.Invoke(http);
                }
            )
            .AddTypedClient<IKeycloakProtectionClient, KeycloakProtectionClient>();
    }

    /// <summary>
    /// Adds <see cref="IKeycloakProtectionClient"/>, <see cref="IKeycloakProtectedResourceClient"/>, <see cref="IKeycloakPolicyClient"/> HTTP clients for Protection API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="keycloakOptions">The Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        KeycloakProtectionClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = null
    )
    {
        return services.AddKeycloakProtectionHttpClient(ConfigureKeycloakOptions, configureClient);

        void ConfigureKeycloakOptions(KeycloakProtectionClientOptions options)
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
    }

    /// <summary>
    /// Adds <see cref="IUmaTicketExchangeClient"/> HTTP client for UMA ticket exchange at the Keycloak token endpoint.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configuration">The IConfiguration instance to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <param name="keycloakClientSectionName">The name of the configuration section containing the Keycloak client options.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakUmaTicketExchangeHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = null,
        string keycloakClientSectionName = KeycloakProtectionClientOptions.Section
    ) =>
        services.AddKeycloakUmaTicketExchangeHttpClient(
            options => configuration.BindKeycloakOptions(options, keycloakClientSectionName),
            configureClient
        );

    /// <summary>
    /// Adds <see cref="IUmaTicketExchangeClient"/> HTTP client for UMA ticket exchange at the Keycloak token endpoint.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configurationSection">The IConfigurationSection to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakUmaTicketExchangeHttpClient(
        this IServiceCollection services,
        IConfigurationSection configurationSection,
        Action<HttpClient>? configureClient = null
    ) =>
        services.AddKeycloakUmaTicketExchangeHttpClient(
            configurationSection.BindKeycloakOptions,
            configureClient
        );

    /// <summary>
    /// Adds <see cref="IUmaTicketExchangeClient"/> HTTP client for UMA ticket exchange at the Keycloak token endpoint.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configureKeycloakOptions">An action to configure the Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKeycloakUmaTicketExchangeHttpClient(
        this IServiceCollection services,
        Action<KeycloakProtectionClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = null
    )
    {
        services
            .AddOptions<KeycloakProtectionClientOptions>()
            .Configure(configureKeycloakOptions)
            .ValidateOnStart();

        services.AddSingleton<
            IValidateOptions<KeycloakProtectionClientOptions>,
            KeycloakProtectionClientOptionsValidator
        >();

        return services
            .AddHttpClient(
                "keycloak_uma_ticket_exchange",
                (_, http) =>
                {
                    configureClient?.Invoke(http);
                }
            )
            .AddTypedClient<IUmaTicketExchangeClient, UmaTicketExchangeClient>();
    }
}
