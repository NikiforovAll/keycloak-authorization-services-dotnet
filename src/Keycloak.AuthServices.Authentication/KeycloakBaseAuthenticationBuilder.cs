namespace Keycloak.AuthServices.Authentication;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Represents a base class for configuring Keycloak authentication in an application.
/// </summary>
public class KeycloakBaseAuthenticationBuilder
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="services">The services being configured.</param>
    /// <param name="configurationSection">Optional configuration section.</param>
    protected KeycloakBaseAuthenticationBuilder(
        IServiceCollection services,
        IConfigurationSection? configurationSection = null
    )
    {
        this.Services = services;
        this.ConfigurationSection = configurationSection;
    }

    /// <summary>
    /// Gets the services being configured.
    /// </summary>
    public IServiceCollection Services { get; private set; }

    /// <summary>
    /// Gets the configuration section from which to bind options.
    /// </summary>
    /// <remarks>
    /// It can be null if the configuration happens with delegates rather than configuration.
    /// </remarks>
    protected IConfigurationSection? ConfigurationSection { get; set; }
}
