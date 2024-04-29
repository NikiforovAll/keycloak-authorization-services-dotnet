namespace Keycloak.AuthServices.IntegrationTests;

using Alba.Security;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Testcontainers.Keycloak;
using Xunit.Abstractions;

public static class Utils
{
    public const string DefaultUserName = "test";
    public const string DefaultPassword = "test";

    public static IWebHostBuilder UseConfiguration(
        this IWebHostBuilder hostBuilder,
        string fileName
    ) =>
        hostBuilder.ConfigureAppConfiguration(x =>
            x.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), fileName), optional: false)
        );

    public static IWebHostBuilder WithLogging(
        this IWebHostBuilder hostBuilder,
        ITestOutputHelper testOutputHelper
    ) =>
        hostBuilder.ConfigureLogging(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Debug);

            builder.Services.AddSingleton<ILoggerProvider>(
                new XUnitLoggerProvider(testOutputHelper, appendScope: true)
            );
        });

    public static void WithKeycloakFixture(
        this JwtBearerOptions options,
        KeycloakContainer keycloakContainer
    )
    {
        options.Authority = $"{keycloakContainer.GetBaseAddress().TrimEnd('/')}/realms/Test";
        options.RequireHttpsMetadata = false;
    }

    public static KeycloakAuthenticationOptions ReadKeycloakAuthenticationOptions(string fileName)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(fileName)
            .AddEnvironmentVariables()
            .Build();

        var keycloakAuthenticationOptions = configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .Get<KeycloakAuthenticationOptions>(KeycloakFormatBinder.Instance)!;

        return keycloakAuthenticationOptions;
    }

    public static OpenConnectUserPassword UserPasswordFlow(
        KeycloakAuthenticationOptions keycloakAuthenticationOptions
    ) =>
        new()
        {
            ClientId = keycloakAuthenticationOptions.Resource,
            ClientSecret = keycloakAuthenticationOptions.Credentials.Secret,
            UserName = DefaultUserName,
            Password = DefaultPassword,
        };
}
