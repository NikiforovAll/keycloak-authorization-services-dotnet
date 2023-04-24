namespace Keycloak.AuthServices.Sdk.Admin;

using System.Text.Json;
using System.Text.Json.Serialization;
using Common;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

/// <summary>
/// Adds HTTP Client SDK
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds keycloak confidential client and underlying token management
    /// Configuration is based on conventional configuration registration.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="keycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>v
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        KeycloakAdminClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = default)
    {
        services.AddAccessTokenManagement(o =>
            o.Client.Clients.Add("keycloak_admin_api",
                new ClientCredentialsTokenRequest
                {
                    RequestUri = new Uri(
                        $"{keycloakOptions.KeycloakUrlRealm}/{KeycloakConstants.TokenEndpointPath}"),
                    ClientId = keycloakOptions.Resource,
                    ClientSecret = keycloakOptions.Credentials.Secret,
                }));

        services.AddTransient<IKeycloakRealmClient>(
            sp => sp.GetRequiredService<IKeycloakClient>());
        services.AddTransient<IKeycloakProtectedResourceClient>(
            sp => sp.GetRequiredService<IKeycloakClient>());
        services.AddTransient<IKeycloakUserClient>(
            sp => sp.GetRequiredService<IKeycloakClient>());
        services.AddTransient<IKeycloakGroupClient>(
            sp => sp.GetRequiredService<IKeycloakClient>());

        return services.AddRefitClient<IKeycloakClient>(GetKeycloakClientRefitSettings())
            .ConfigureHttpClient(client =>
            {
                var baseUrl = new Uri(keycloakOptions.AuthServerUrl.TrimEnd('/'));
                client.BaseAddress = baseUrl;
                configureClient?.Invoke(client);
            })
            .AddClientAccessTokenHandler("keycloak_admin_api");
    }

    /// <summary>
    /// Adds keycloak confidential client and underlying token management
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="configureClient"></param>
    /// <param name="keycloakClientSectionName"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string? keycloakClientSectionName = default)
    {
        KeycloakAdminClientOptions options = new();

        configuration
            .GetSection(keycloakClientSectionName ?? KeycloakAdminClientOptions.Section)
            .Bind(options);

        return services.AddKeycloakAdminHttpClient(options, configureClient);
    }

    internal static RefitSettings GetKeycloakClientRefitSettings() =>
        new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            })
        };
}
