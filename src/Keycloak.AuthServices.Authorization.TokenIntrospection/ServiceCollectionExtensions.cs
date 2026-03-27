namespace Keycloak.AuthServices.Authorization.TokenIntrospection;

using Keycloak.AuthServices.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Extension methods for adding Keycloak token introspection services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Keycloak token introspection support for lightweight access tokens.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration instance.</param>
    /// <param name="configureClient">Optional HTTP client configuration.</param>
    /// <param name="configSectionName">Configuration section name. Defaults to "Keycloak".</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for further HTTP client configuration (e.g. resilience policies).</returns>
    public static IHttpClientBuilder AddKeycloakTokenIntrospection(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string configSectionName = KeycloakTokenIntrospectionOptions.Section
    )
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentException.ThrowIfNullOrEmpty(configSectionName);

        return services.AddKeycloakTokenIntrospection(
            configuration.GetSection(configSectionName),
            configureClient
        );
    }

    /// <summary>
    /// Adds Keycloak token introspection support for lightweight access tokens.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configurationSection">The configuration section.</param>
    /// <param name="configureClient">Optional HTTP client configuration.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for further HTTP client configuration (e.g. resilience policies).</returns>
    public static IHttpClientBuilder AddKeycloakTokenIntrospection(
        this IServiceCollection services,
        IConfigurationSection configurationSection,
        Action<HttpClient>? configureClient = default
    )
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configurationSection);

        return services.AddKeycloakTokenIntrospection(
            options => configurationSection.BindKeycloakOptions(options),
            configureClient
        );
    }

    /// <summary>
    /// Adds Keycloak token introspection support for lightweight access tokens.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Options configuration delegate.</param>
    /// <param name="configureClient">Optional HTTP client configuration.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for further HTTP client configuration (e.g. resilience policies).</returns>
    public static IHttpClientBuilder AddKeycloakTokenIntrospection(
        this IServiceCollection services,
        Action<KeycloakTokenIntrospectionOptions> configureOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        services
            .AddOptions<KeycloakTokenIntrospectionOptions>()
            .Configure(configureOptions)
            .ValidateOnStart();

        services.AddSingleton<
            IValidateOptions<KeycloakTokenIntrospectionOptions>,
            KeycloakTokenIntrospectionOptionsValidator
        >();

        services.AddHybridCache();
        services.AddHttpContextAccessor();

        services.AddTransient<
            IKeycloakTokenIntrospectionTransformation,
            KeycloakTokenIntrospectionClaimsTransformation
        >();

        return services
            .AddHttpClient<IKeycloakTokenIntrospectionClient, KeycloakTokenIntrospectionClient>()
            .ConfigureHttpClient(
                (serviceProvider, client) =>
                {
                    var keycloakOptions = serviceProvider
                        .GetService<IOptions<KeycloakTokenIntrospectionOptions>>()
                        ?.Value;

                    if (!string.IsNullOrWhiteSpace(keycloakOptions?.KeycloakUrlRealm))
                    {
                        client.BaseAddress = new Uri(keycloakOptions.KeycloakUrlRealm);
                    }

                    configureClient?.Invoke(client);
                }
            );
    }
}
