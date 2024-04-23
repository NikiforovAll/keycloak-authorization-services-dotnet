﻿namespace Keycloak.AuthServices.Authorization;

using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    /// <param name="keycloakOptions"></param>
    public static IServiceCollection AddKeycloakAuthorization(
        this IServiceCollection services,
        KeycloakProtectionClientOptions keycloakOptions
    )
    {
        services.AddKeycloakProtectionHttpClient(keycloakOptions);

        services.AddSingleton<IAuthorizationHandler, DecisionRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, RptRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, RealmAccessRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, ResourceAccessRequirementHandler>();

        return services;
    }

    /// <summary>
    /// Adds keycloak authorization services from predefined configuration provided by keycloak.json
    /// Alternatively, you can create custom section in appsettings.json and provide section name
    /// by adding <paramref name="keycloakClientSectionName"/>
    /// </summary>
    /// <param name="services">Source service collection</param>
    /// <param name="configuration">Configuration source</param>
    /// <param name="keycloakClientSectionName"></param>
    public static IServiceCollection AddKeycloakAuthorization(
        this IServiceCollection services,
        IConfiguration configuration,
        string? keycloakClientSectionName = default
    )
    {
        var options = configuration
            .GetSection(keycloakClientSectionName ?? KeycloakProtectionClientOptions.Section)
            .Get<KeycloakProtectionClientOptions>(KeycloakInstallationOptions.KeycloakFormatBinder)!;

        services.AddKeycloakAuthorization(options);
        return services;
    }
}
