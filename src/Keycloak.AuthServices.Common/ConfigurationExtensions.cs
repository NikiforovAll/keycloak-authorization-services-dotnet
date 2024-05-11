namespace Keycloak.AuthServices.Common;

using Microsoft.Extensions.Configuration;

/// <summary>
///
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Retrieves the Keycloak options from the configuration section with the specified name.
    /// </summary>
    /// <typeparam name="T">The type of the Keycloak options.</typeparam>
    /// <param name="configuration">The configuration instance.</param>
    /// <param name="configSectionName">The name of the configuration section. Default is ConfigurationConstants.ConfigurationPrefix.</param>
    /// <returns>The Keycloak options instance.</returns>
    public static T? GetKeycloakOptions<T>(
        this IConfiguration configuration,
        string configSectionName = ConfigurationConstants.ConfigurationPrefix
    )
        where T : KeycloakInstallationOptions
    {
        ArgumentNullException.ThrowIfNull(configuration);

        return configuration.GetSection(configSectionName).Get<T>(KeycloakFormatBinder.Instance);
    }

    /// <summary>
    /// Retrieves the Keycloak options from the configuration section
    /// </summary>
    /// <typeparam name="T">The type of the Keycloak options.</typeparam>
    /// <returns>The Keycloak options instance.</returns>
    public static T? GetKeycloakOptions<T>(this IConfigurationSection configurationSection)
        where T : KeycloakInstallationOptions
    {
        ArgumentNullException.ThrowIfNull(configurationSection);

        return configurationSection.Get<T>(KeycloakFormatBinder.Instance);
    }

    /// <summary>
    /// Binds the Keycloak options from the configuration section with the specified name to the provided options instance.
    /// </summary>
    /// <typeparam name="T">The type of the Keycloak options.</typeparam>
    /// <param name="configuration">The configuration instance.</param>
    /// <param name="options">The options instance to bind the configuration to.</param>
    /// <param name="configSectionName">The name of the configuration section. Default is ConfigurationConstants.ConfigurationPrefix.</param>
    public static void BindKeycloakOptions<T>(
        this IConfiguration configuration,
        T options,
        string configSectionName = ConfigurationConstants.ConfigurationPrefix
    )
        where T : KeycloakInstallationOptions
    {
        ArgumentNullException.ThrowIfNull(configuration);

        configuration.GetSection(configSectionName).Bind(options, KeycloakFormatBinder.Instance);
    }

    /// <summary>
    /// Binds the Keycloak options from the configuration section to the provided options instance.
    /// </summary>
    /// <typeparam name="T">The type of the Keycloak options.</typeparam>
    /// <param name="configurationSection"></param>
    /// <param name="options">The options instance to bind the configuration to.</param>
    public static void BindKeycloakOptions<T>(
        this IConfigurationSection configurationSection,
        T options
    )
        where T : KeycloakInstallationOptions
    {
        ArgumentNullException.ThrowIfNull(configurationSection);

        configurationSection.Bind(options, KeycloakFormatBinder.Instance);
    }
}
