namespace Keycloak.AuthServices.IntegrationTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public static class ConfigurationUtils
{
    public static IWebHostBuilder UseConfiguration(
        this IWebHostBuilder hostBuilder,
        string fileName
    ) =>
        hostBuilder.UseConfiguration(
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName)
                .AddEnvironmentVariables()
                .Build()
        );
}
