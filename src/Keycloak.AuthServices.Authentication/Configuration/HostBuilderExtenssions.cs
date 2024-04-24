namespace Keycloak.AuthServices.Authentication.Configuration;

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
    /// <returns></returns>
    public static IHostBuilder ConfigureKeycloakConfigurationSource(
        this IHostBuilder hostBuilder,
        string fileName = "keycloak.json"
    ) =>
        hostBuilder.ConfigureAppConfiguration(
            (_, builder) =>
            {
                var source = new KeycloakConfigurationSource { Path = fileName, Optional = false };
                builder.Sources.Insert(0, source);
            }
        );
}
