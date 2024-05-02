namespace Keycloak.AuthServices.Authorization;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Keycloak.AuthServices.Authorization.Claims;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Requirements;

/// <summary>
/// Add Keycloak authorization services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds keycloak authorization services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="keycloakClientSectionName"></param>
    /// <returns></returns>
    [Obsolete(
        "Method overload will be removed in future versions. Use AddKeycloakAuthorization together with AddAuthorizationServer instead"
    )]
    public static IServiceCollection AddKeycloakAuthorization(
        this IServiceCollection services,
        IConfiguration configuration,
        string keycloakClientSectionName = KeycloakProtectionClientOptions.Section
    )
    {
        services.AddAuthorizationServer(configuration, keycloakClientSectionName);

        services.AddSingleton<IAuthorizationHandler, RptRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, RealmAccessRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, ResourceAccessRequirementHandler>();

        return services;
    }

    /// <summary>
    /// Adds keycloak authorization services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureKeycloakAuthorizationOptions"></param>
    public static IServiceCollection AddKeycloakAuthorization(
        this IServiceCollection services,
        Action<KeycloakAuthorizationOptions>? configureKeycloakAuthorizationOptions = null
    )
    {
        services.AddSingleton<IAuthorizationHandler, RptRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, RealmAccessRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, ResourceAccessRequirementHandler>();

        services.AddTransient<IClaimsTransformation>(sp =>
        {
            var keycloakOptions = sp.GetRequiredService<
                IOptionsMonitor<KeycloakAuthorizationOptions>
            >().CurrentValue;

            return new KeycloakRolesClaimsTransformation(
                keycloakOptions.RoleClaimType,
                keycloakOptions.EnableRolesMapping,
                keycloakOptions.RolesResource ?? keycloakOptions.Resource
            );
        });
        configureKeycloakAuthorizationOptions ??= _ => { };

        services.Configure<KeycloakAuthorizationOptions>(configureKeycloakAuthorizationOptions);

        return services;
    }

    /// <summary>
    /// Adds Keycloak Protection client and auto header propagation
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="configSectionName"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddAuthorizationServer(
        this IServiceCollection services,
        IConfiguration configuration,
        string configSectionName = KeycloakProtectionClientOptions.Section,
        Action<HttpClient>? configureClient = default
    )
    {
        var configurationSection = configuration.GetSection(configSectionName);

        return services.AddAuthorizationServer(configurationSection, configureClient);
    }

    /// <summary>
    /// Adds Keycloak Protection client and auto header propagation
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddAuthorizationServer(
        this IServiceCollection services,
        IConfigurationSection configurationSection,
        Action<HttpClient>? configureClient = default
    ) =>
        services.AddAuthorizationServer(
            options => configurationSection.BindKeycloakOptions(options),
            configureClient
        );

    /// <summary>
    /// Adds Keycloak Protection client and auto header propagation
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureKeycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddAuthorizationServer(
        this IServiceCollection services,
        Action<KeycloakProtectionClientOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        services.AddHttpContextAccessor();

        services.AddSingleton<IAuthorizationHandler, DecisionRequirementHandler>();

        var keycloakOptions = new KeycloakProtectionClientOptions();
        configureKeycloakOptions.Invoke(keycloakOptions);

        if (keycloakOptions.UseProtectedResourcePolicyProvider)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, ProtectedResourcePolicyProvider>();
        }

        services.AddOptions<KeycloakProtectionClientOptions>().Configure(configureKeycloakOptions);

        return services
            .AddHttpClient<IKeycloakProtectionClient, KeycloakProtectionClient>()
            .ConfigureHttpClient(
                (serviceProvider, client) =>
                {
                    var keycloakOptions = serviceProvider
                        .GetRequiredService<IOptions<KeycloakProtectionClientOptions>>()
                        .Value;

                    client.BaseAddress = new Uri(keycloakOptions.KeycloakUrlRealm);

                    configureClient?.Invoke(client);
                }
            )
            .AddHeaderPropagation();
    }
}
