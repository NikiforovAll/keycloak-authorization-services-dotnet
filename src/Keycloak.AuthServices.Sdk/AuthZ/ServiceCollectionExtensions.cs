namespace Keycloak.AuthServices.Sdk.AuthZ;

using Admin;
using HttpMiddleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using AdminServiceCollectionExtensions = Admin.ServiceCollectionExtensions;

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
    /// Adds keycloak confidential client and underlying token management
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
        string? keycloakClientSectionName = default)
    {
        KeycloakProtectionClientOptions options = new();

        configuration
            .GetSection(keycloakClientSectionName ?? KeycloakAdminClientOptions.Section)
            .Bind(options);

        return services.AddKeycloakProtectionHttpClient(options, configureClient);
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
            }).AddHeaderPropagation();
    }

    /// <summary>
    /// Adds keycloak confidential policy client and underlying token management
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="configureClient"></param>
    /// <param name="keycloakClientSectionName"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakPolicyHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string? keycloakClientSectionName = default)
    {
        KeycloakProtectionClientOptions options = new();

        configuration
            .GetSection(keycloakClientSectionName ?? KeycloakAdminClientOptions.Section)
            .Bind(options);

        return services.AddKeycloakPolicyHttpClient(options, configureClient);
    }
}
