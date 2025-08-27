namespace PhaseTwo.AuthServices.Sdk.Kiota;

using Admin;
using Keycloak.AuthServices.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

/// <summary>
/// Adds HTTP Client SDKs
/// </summary>
public static class ServiceCollectionExtensions
{
#pragma warning disable CS0419 // Ambiguous reference in cref attribute
    /// <summary>
    /// Adds <see cref="PhaseTwoAdminApiClient"/> for Keycloak Admin API alias for <see cref="AddPhaseTwoAdminHttpClient"/>. You can use it to resolve possible namespaces issues.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configuration">The IConfiguration instance to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <param name="keycloakClientSectionName">The name of the configuration section containing the Keycloak client options.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddKiotaPhaseTwoAdminHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string keycloakClientSectionName = PhaseTwoAdminClientOptions.Section
    ) =>
        services.AddPhaseTwoAdminHttpClient(
            configuration,
            configureClient,
            keycloakClientSectionName
        );
#pragma warning restore CS0419 // Ambiguous reference in cref attribute

    /// <summary>
    /// Adds <see cref="PhaseTwoAdminApiClient"/> for Keycloak Admin API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configuration">The IConfiguration instance to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <param name="keycloakClientSectionName">The name of the configuration section containing the Keycloak client options.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddPhaseTwoAdminHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string keycloakClientSectionName = PhaseTwoAdminClientOptions.Section
    ) =>
        services.AddPhaseTwoAdminHttpClient(
            options => configuration.BindKeycloakOptions(options, keycloakClientSectionName),
            configureClient
        );

    /// <summary>
    /// Adds <see cref="PhaseTwoAdminApiClient"/> for Keycloak Admin API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configurationSection">The IConfigurationSection to bind the Keycloak options from.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddPhaseTwoAdminHttpClient(
        this IServiceCollection services,
        IConfigurationSection configurationSection,
        Action<HttpClient>? configureClient = default
    ) =>
        services.AddPhaseTwoAdminHttpClient(
            configurationSection.BindKeycloakOptions,
            configureClient
        );

    /// <summary>
    /// Adds <see cref="PhaseTwoAdminApiClient"/> for Keycloak Admin API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="configureKeycloakOptions">An action to configure the Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddPhaseTwoAdminHttpClient(
        this IServiceCollection services,
        Action<PhaseTwoAdminClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        services.Configure(configureKeycloakOptions);

        return services
            .AddHttpClient(
                "phasetwo_admin_api_kiota",
                (sp, http) =>
                {
                    var keycloakOptions = sp.GetRequiredService<
                        IOptions<PhaseTwoAdminClientOptions>
                    >();

                    http.BaseAddress = new Uri(keycloakOptions.Value.AuthServerUrl!);
                    configureClient?.Invoke(http);
                }
            )
            .AddTypedClient(
                (httpClient, sp) =>
                {
                    var requestAdapter = new HttpClientRequestAdapter(
                        new AnonymousAuthenticationProvider(),
                        httpClient: httpClient
                    );

                    var options = sp.GetRequiredService<IOptions<PhaseTwoAdminClientOptions>>();
                    return new PhaseTwoAdminApiClient(requestAdapter, options);
                }
            )
            .ConfigurePrimaryHttpMessageHandler(_ =>
            {
                var defaultHandlers = KiotaClientFactory.CreateDefaultHandlers();
                var defaultHttpMessageHandler = KiotaClientFactory.GetDefaultHttpMessageHandler();

                return KiotaClientFactory.ChainHandlersCollectionAndGetFirstLink(
                    defaultHttpMessageHandler,
                    defaultHandlers.ToArray()
                )!;
            });
    }

    /// <summary>
    /// Adds <see cref="PhaseTwoAdminApiClient"/> for Keycloak Admin API.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the HttpClient to.</param>
    /// <param name="keycloakOptions">The Keycloak client options.</param>
    /// <param name="configureClient">An optional action to configure the HttpClient.</param>
    /// <returns>The IHttpClientBuilder for further configuration.</returns>
    public static IHttpClientBuilder AddPhaseTwoAdminHttpClient(
        this IServiceCollection services,
        PhaseTwoAdminClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        return services.AddPhaseTwoAdminHttpClient(ConfigureKeycloakOptions, configureClient);

        void ConfigureKeycloakOptions(PhaseTwoAdminClientOptions options)
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
}
