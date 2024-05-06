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
    /// <param name="configSectionName"></param>
    /// <returns></returns>
    public static IServiceCollection AddKeycloakAuthorization(
        this IServiceCollection services,
        IConfiguration configuration,
        string configSectionName = KeycloakAuthorizationOptions.Section
    )
    {
        var configurationSection = configuration.GetSection(configSectionName);

        return services.AddKeycloakAuthorization(configurationSection);
    }

    /// <summary>
    /// Adds keycloak authorization services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection"></param>
    /// <returns></returns>
    public static IServiceCollection AddKeycloakAuthorization(
        this IServiceCollection services,
        IConfigurationSection configurationSection
    ) =>
        services.AddKeycloakAuthorization(options =>
            configurationSection.BindKeycloakOptions(options)
        );

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
        configureKeycloakAuthorizationOptions ??= _ => { };
        services.Configure(configureKeycloakAuthorizationOptions);

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

        return services;
    }

    /// <summary>
    /// Adds Keycloak <see cref="IAuthorizationServerClient"/> client and auto header propagation
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="configSectionName"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddAuthorizationServer(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string configSectionName = KeycloakAuthorizationServerOptions.Section
    ) =>
        services.AddAuthorizationServer(
            configuration.GetSection(configSectionName),
            configureClient
        );

    /// <summary>
    /// Adds Keycloak <see cref="IAuthorizationServerClient"/> client and auto header propagation
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
    /// Adds Keycloak <see cref="IAuthorizationServerClient"/> client, <see cref="ProtectedResourcePolicyProvider"/> and auto header propagation
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureKeycloakOptions"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddAuthorizationServer(
        this IServiceCollection services,
        Action<KeycloakAuthorizationServerOptions> configureKeycloakOptions,
        Action<HttpClient>? configureClient = default
    )
    {
        services.Configure(configureKeycloakOptions);

        services.AddHttpContextAccessor();

        services.AddAuthorization(options =>
            options.AddPolicy(
                DynamicProtectedResourceRequirement.DynamicProtectedResourcePolicy,
                p => p.AddRequirements(new DynamicProtectedResourceRequirement())
            )
        );

        services.AddScoped<IAuthorizationHandler, DynamicProtectedResourceRequirementHandler>();
        // TODO: determine correct lifetime.
        services.AddSingleton<IAuthorizationHandler, DecisionRequirementHandler>();

        // (!) resolved locally, will not work with PostConfigure and IOptions pattern
        configureKeycloakOptions ??= _ => { };
        var keycloakOptions = new KeycloakAuthorizationServerOptions();
        configureKeycloakOptions.Invoke(keycloakOptions);

        if (keycloakOptions.UseProtectedResourcePolicyProvider)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, ProtectedResourcePolicyProvider>();
        }

        var builder = services.AddAuthorizationServerClient(configureClient);

        if (!KeycloakAuthorizationServerOptions.DisableHeaderPropagation)
        {
            builder.AddHeaderPropagation();
        }

        return builder;
    }

    /// <summary>
    /// Adds Keycloak <see cref="IAuthorizationServerClient"/> client
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureClient"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddAuthorizationServerClient(
        this IServiceCollection services,
        Action<HttpClient>? configureClient = default
    ) =>
        services
            .AddHttpClient<IAuthorizationServerClient, AuthorizationServerClient>()
            .ConfigureHttpClient(
                (serviceProvider, client) =>
                {
                    var keycloakOptions = serviceProvider
                        .GetService<IOptions<KeycloakAuthorizationServerOptions>>()
                        ?.Value;

                    if (!string.IsNullOrWhiteSpace(keycloakOptions?.KeycloakUrlRealm))
                    {
                        client.BaseAddress = new Uri(keycloakOptions.KeycloakUrlRealm);
                    }

                    configureClient?.Invoke(client);
                }
            );
}
