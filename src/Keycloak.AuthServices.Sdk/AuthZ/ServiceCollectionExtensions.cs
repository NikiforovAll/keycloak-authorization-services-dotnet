namespace Keycloak.AuthServices.Sdk.AuthZ;

using Admin;
using Common;
using HttpMidleware;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
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
