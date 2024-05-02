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
    public static IWebHostBuilder WithConfiguration(
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
            builder.SetMinimumLevel(LogLevel.Trace);

            builder.Services.AddSingleton<ILoggerProvider>(
                new XUnitLoggerProvider(
                    testOutputHelper,
                    new XUnitLoggerOptions
                    {
                        IncludeScopes = true,
                        IncludeCategory = true,
                        IncludeLogLevel = true
                    }
                )
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

    public static void WithLocalKeycloakInstallation(
        this JwtBearerOptions options,
        string realm = "Test"
    )
    {
        options.Authority = $"localhost:8080/realms/{realm}";
        options.RequireHttpsMetadata = false;
    }

    public static (IServiceCollection services, IConfiguration configuration1) KeycloakSetup(
        string fileName,
        ITestOutputHelper testOutputHelper
    )
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), fileName), optional: false)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));

        services.AddSingleton<ILoggerProvider>(
            new XUnitLoggerProvider(
                testOutputHelper,
                new XUnitLoggerOptions
                {
                    IncludeScopes = true,
                    IncludeCategory = true,
                    IncludeLogLevel = true,
                }
            )
        );

        return (services, configuration);
    }

    public static KeycloakAuthenticationOptions ReadKeycloakAuthenticationOptions(string fileName)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(fileName)
            .AddEnvironmentVariables()
            .Build();

        var keycloakAuthenticationOptions =
            configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;

        return keycloakAuthenticationOptions;
    }

    public static OpenConnectUserPassword UserPasswordFlow(
        KeycloakAuthenticationOptions keycloakAuthenticationOptions
    ) =>
        new()
        {
            ClientId = keycloakAuthenticationOptions.Resource,
            ClientSecret = keycloakAuthenticationOptions.Credentials.Secret,
            UserName = TestUsers.Tester.UserName,
            Password = TestUsers.Tester.Password,
        };
}
