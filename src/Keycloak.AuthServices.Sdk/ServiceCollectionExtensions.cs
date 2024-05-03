namespace Keycloak.AuthServices.Sdk;

using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Adds HTTP Client SDK
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureKeycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>v
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        Action<KeycloakAdminClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        services.Configure<KeycloakAdminClientOptions>(configureKeycloakOptions);

        services.AddTransient<IKeycloakRealmClient>(sp => sp.GetRequiredService<IKeycloakClient>());
        services.AddTransient<IKeycloakProtectedResourceClient>(sp =>
            sp.GetRequiredService<IKeycloakClient>()
        );
        services.AddTransient<IKeycloakUserClient>(sp => sp.GetRequiredService<IKeycloakClient>());

        return services
            .AddHttpClient(
                "keycloak_admin_api",
                (sp, http) =>
                {
                    var keycloakOptions = sp.GetRequiredService<
                        IOptions<KeycloakAdminClientOptions>
                    >();

                    http.BaseAddress = new Uri(keycloakOptions.Value.KeycloakUrlRealm);
                    configureClient?.Invoke(http);
                }
            )
            .AddTypedClient<IKeycloakClient, KeycloakClient>();
    }

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="services"></param>
    /// <param name="keycloakAdminClientOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>v
    public static IHttpClientBuilder AddKeycloakAdminHttpClient(
        this IServiceCollection services,
        KeycloakAdminClientOptions keycloakAdminClientOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        void configureKeycloakOptions(KeycloakAdminClientOptions options)
        {
            options.Realm = keycloakAdminClientOptions.Realm;
            options.AuthServerUrl = keycloakAdminClientOptions.AuthServerUrl;
            options.Resource = keycloakAdminClientOptions.Resource;
            // redundant
            options.SslRequired = keycloakAdminClientOptions.SslRequired;
            options.VerifyTokenAudience = keycloakAdminClientOptions.VerifyTokenAudience;
            options.Credentials = keycloakAdminClientOptions.Credentials;
            options.TokenClockSkew = keycloakAdminClientOptions.TokenClockSkew;
        }

        return services.AddKeycloakAdminHttpClient(configureKeycloakOptions, configureClient);
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
        string keycloakClientSectionName = KeycloakAdminClientOptions.Section
    ) =>
        services.AddKeycloakAdminHttpClient(
            options => configuration.BindKeycloakOptions(options, keycloakClientSectionName),
            configureClient
        );
}
