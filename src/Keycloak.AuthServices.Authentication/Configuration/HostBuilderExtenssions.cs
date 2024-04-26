namespace Keycloak.AuthServices.Authentication.Configuration;

using Keycloak.AuthServices.Common;
using Microsoft.Extensions.Hosting;

/// <summary>
/// HostBuilder Extensions
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Adds configuration source based on adapter config.
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <param name="fileName"></param>
    /// <param name="section"></param>
    /// <returns></returns>
    public static IHostBuilder ConfigureKeycloakConfigurationSource(
        this IHostBuilder hostBuilder,
        string fileName = "keycloak.json",
        string section = ConfigurationConstants.ConfigurationPrefix
    ) =>
        hostBuilder.ConfigureAppConfiguration(
            (_, builder) =>
            {
                var source = new KeycloakConfigurationSource
                {
                    Path = fileName,
                    Optional = false,
                    ConfigurationPrefix = section
                };
                builder.Sources.Insert(0, source);
            }
        );
}
