namespace Keycloak.AuthServices.IntegrationTests;

using Alba.Security;
using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
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

    public static IWebHostBuilder WithKeycloakConfiguration(
        this IWebHostBuilder hostBuilder,
        string fileName,
        string baseAddress
    ) =>
        hostBuilder.ConfigureAppConfiguration(x =>
        {
            x.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), fileName), optional: false);
            x.AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["Keycloak:auth-server-url"] = baseAddress,
                    ["Keycloak:AuthServerUrl"] = baseAddress,
                }
            );
        });

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
                        IncludeLogLevel = true,
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
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), fileName), optional: true)
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

    public static (IServiceCollection services, IConfiguration configuration) AdminHttpClientSetup(
        string fileName,
        ITestOutputHelper testOutputHelper
    )
    {
        var (services, configuration) = KeycloakSetup(fileName, testOutputHelper);

        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_admin_api_token");
        var keycloakOptions = configuration.GetKeycloakOptions<KeycloakAdminClientOptions>()!;

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(tokenClientName, client => BindKeycloak(client, keycloakOptions));

        services
            .AddKeycloakAdminHttpClient(configuration)
            .AddClientCredentialsTokenHandler(tokenClientName);

        return (services, configuration);
    }

    public static (IServiceCollection services, IConfiguration configuration) AdminHttpClientSetup(
        string fileName,
        ITestOutputHelper testOutputHelper,
        string baseAddress
    )
    {
        var (services, configuration) = KeycloakSetup(fileName, testOutputHelper);

        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_admin_api_token");
        var keycloakOptions = configuration.GetKeycloakOptions<KeycloakAdminClientOptions>()!;
        keycloakOptions.AuthServerUrl = baseAddress;

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(tokenClientName, client => BindKeycloak(client, keycloakOptions));

        services
            .AddKeycloakAdminHttpClient(keycloakOptions)
            .AddClientCredentialsTokenHandler(tokenClientName);

        return (services, configuration);
    }

    public static (
        IServiceCollection services,
        IConfiguration configuration
    ) ProtectionHttpClientSetup(string fileName, ITestOutputHelper testOutputHelper)
    {
        var (services, configuration) = KeycloakSetup(fileName, testOutputHelper);

        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_protection_api_token");
        var keycloakOptions = configuration.GetKeycloakOptions<KeycloakProtectionClientOptions>()!;

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(tokenClientName, client => BindKeycloak(client, keycloakOptions));

        services
            .AddKeycloakProtectionHttpClient(configuration)
            .AddClientCredentialsTokenHandler(tokenClientName);

        return (services, configuration);
    }

    public static (
        IServiceCollection services,
        IConfiguration configuration
    ) ProtectionHttpClientSetup(
        string fileName,
        ITestOutputHelper testOutputHelper,
        string baseAddress
    )
    {
        var (services, configuration) = KeycloakSetup(fileName, testOutputHelper);

        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_protection_api_token");
        var keycloakOptions = configuration.GetKeycloakOptions<KeycloakProtectionClientOptions>()!;
        keycloakOptions.AuthServerUrl = baseAddress;

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(tokenClientName, client => BindKeycloak(client, keycloakOptions));

        services
            .AddKeycloakProtectionHttpClient(keycloakOptions)
            .AddClientCredentialsTokenHandler(tokenClientName);

        return (services, configuration);
    }

    private static void BindKeycloak(
        ClientCredentialsClient client,
        KeycloakAdminClientOptions keycloakOptions
    )
    {
        client.ClientId = ClientId.Parse(keycloakOptions.Resource);
        client.ClientSecret = ClientSecret.Parse(keycloakOptions.Credentials.Secret);
        client.TokenEndpoint = new Uri(keycloakOptions.KeycloakTokenEndpoint);
    }

    private static void BindKeycloak(
        ClientCredentialsClient client,
        KeycloakProtectionClientOptions keycloakOptions
    )
    {
        client.ClientId = ClientId.Parse(keycloakOptions.Resource);
        client.ClientSecret = ClientSecret.Parse(keycloakOptions.Credentials.Secret);
        client.TokenEndpoint = new Uri(keycloakOptions.KeycloakTokenEndpoint);
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

    public static KeycloakAuthenticationOptions ReadKeycloakAuthenticationOptions(
        string fileName,
        string baseAddress
    )
    {
        var options = ReadKeycloakAuthenticationOptions(fileName);
        options.AuthServerUrl = baseAddress;
        return options;
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

    public static OpenConnectUserPassword UserPasswordFlow(
        KeycloakAuthenticationOptions keycloakAuthenticationOptions,
        string baseAddress
    )
    {
        keycloakAuthenticationOptions.AuthServerUrl = baseAddress;
        return new OpenConnectUserPassword
        {
            ClientId = keycloakAuthenticationOptions.Resource,
            ClientSecret = keycloakAuthenticationOptions.Credentials.Secret,
            UserName = TestUsers.Tester.UserName,
            Password = TestUsers.Tester.Password,
        };
    }
}
