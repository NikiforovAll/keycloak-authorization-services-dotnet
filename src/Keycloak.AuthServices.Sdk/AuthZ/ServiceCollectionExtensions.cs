namespace Keycloak.AuthServices.Authorization;

using Sdk.AuthZ;
using Sdk.HttpMiddleware;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using AdminServiceCollectionExtensions = Sdk.Admin.ServiceCollectionExtensions;

/// <summary>
/// Registers HTTP Client SDKs for integration with Keycloak
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds keycloak confidential client and underlying token management
    /// </summary>
    /// <param name="services"></param>
    /// <param name="keycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakProtectionHttpClient(
        this IServiceCollection services,
        KeycloakProtectionClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = default)
    {
        services.AddSingleton(keycloakOptions);
        services.AddHttpContextAccessor();

        return services.AddHttpClient<IKeycloakProtectionClient, KeycloakProtectionClient>()
            .ConfigureHttpClient(client =>
            {
                var baseUrl = new Uri(keycloakOptions.KeycloakUrlRealm.TrimEnd('/') + "/");
                client.BaseAddress = baseUrl;
                configureClient?.Invoke(client);
            }).AddHeaderPropagation();
    }

    /// <summary>
    /// Adds keycloak confidential policy client and underlying token management
    /// </summary>
    /// <param name="services"></param>
    /// <param name="keycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakPolicyHttpClient(
        this IServiceCollection services,
        KeycloakProtectionClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = default)
    {
        services.AddSingleton(keycloakOptions);
        services.AddHttpContextAccessor();

        return services.AddRefitClient<IKeycloakPolicyClient>(AdminServiceCollectionExtensions.GetKeycloakClientRefitSettings())
            .ConfigureHttpClient(client =>
            {
                var baseUrl = new Uri(keycloakOptions.AuthServerUrl.TrimEnd('/'));
                client.BaseAddress = baseUrl;
                configureClient?.Invoke(client);
            })
            .AddClientAccessTokenHandler("keycloak_policy_api");
    }

    /// <summary>
    /// Adds keycloak confidential permission ticket client and underlying token management
    /// </summary>
    /// <param name="services"></param>
    /// <param name="keycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakPermissionTicketHttpClient(
        this IServiceCollection services,
        KeycloakProtectionClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = default)
    {
        services.AddSingleton(keycloakOptions);
        services.AddHttpContextAccessor();

        return services.AddRefitClient<IKeycloakPermissionTicketClient>(AdminServiceCollectionExtensions.GetKeycloakClientRefitSettings())
            .ConfigureHttpClient(client =>
            {
                var baseUrl = new Uri(keycloakOptions.AuthServerUrl.TrimEnd('/'));
                client.BaseAddress = baseUrl;
                configureClient?.Invoke(client);
            })
            .AddClientAccessTokenHandler("keycloak_uma_api");
    }
}
