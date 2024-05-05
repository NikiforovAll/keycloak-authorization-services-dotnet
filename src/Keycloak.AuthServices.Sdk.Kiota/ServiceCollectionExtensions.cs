namespace Keycloak.AuthServices.Sdk.Kiota;

using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Kiota.Admin;
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
    /// <summary>
    /// Adds <see cref="KeycloakAdminApiClient"/> for Keycloak Admin API.
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
    /// Adds <see cref="KeycloakAdminApiClient"/> for Keycloak Admin API.
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
    /// Adds <see cref="KeycloakAdminApiClient"/> for Keycloak Admin API.
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

        return services
            .AddHttpClient(
                "keycloak_admin_api_kiota",
                (sp, http) =>
                {
                    var keycloakOptions = sp.GetRequiredService<
                        IOptions<KeycloakAdminClientOptions>
                    >();

                    http.BaseAddress = new Uri(keycloakOptions.Value.AuthServerUrl);
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

                    return new KeycloakAdminApiClient(requestAdapter);
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
    /// Adds <see cref="KeycloakAdminApiClient"/> for Keycloak Admin API.
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
}
