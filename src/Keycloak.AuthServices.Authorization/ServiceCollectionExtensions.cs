namespace Keycloak.AuthServices.Authorization;

using Common;
using Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdk.AuthZ;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds keycloak authorization services
    /// </summary>
    /// <param name="services">Source service collection</param>
    /// <param name="configuration">Configuration source
    /// <param name="configureOptions">Configure overrides</param>
    /// <returns></returns>
    public static IServiceCollection AddKeycloakAuthorization(
        this IServiceCollection services,
        IConfiguration configuration,
        string? keycloakClientSectionName = default)
    {
        var keycloakOptions = configuration
            .GetSection(keycloakClientSectionName ?? ConfigurationConstants.ConfigurationPrefix)
            .Get<KeycloakInstallationOptions>();

        services.AddKeycloakProtectionHttpClient(keycloakOptions);

        services.AddSingleton<IAuthorizationHandler, DecisionRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, RptRequirementHandler>();
        return services;
    }

}
