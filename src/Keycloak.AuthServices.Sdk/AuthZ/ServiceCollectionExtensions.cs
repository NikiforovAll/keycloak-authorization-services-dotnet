namespace Keycloak.AuthServices.Authorization;

using Keycloak.AuthServices.Sdk.AuthZ;
using Keycloak.AuthServices.Sdk.HttpMiddleware;
using Microsoft.Extensions.DependencyInjection;

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
}
