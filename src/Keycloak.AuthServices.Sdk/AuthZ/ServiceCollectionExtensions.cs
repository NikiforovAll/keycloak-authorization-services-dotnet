namespace Keycloak.AuthServices.Sdk.AuthZ;

using Admin;
using Common;
using HttpMiddleware;
using Microsoft.Extensions.DependencyInjection;

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
        KeycloakInstallationOptions keycloakOptions,
        Action<HttpClient>? configureClient = default)
    {
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
