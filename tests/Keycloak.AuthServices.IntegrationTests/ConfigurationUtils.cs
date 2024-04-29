namespace Keycloak.AuthServices.IntegrationTests;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public static class ConfigurationUtils
{
    public static IWebHostBuilder UseConfiguration(
        this IWebHostBuilder hostBuilder,
        string fileName
    ) =>
        hostBuilder.ConfigureAppConfiguration(x =>
            x.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), fileName), optional: false)
        );
}
