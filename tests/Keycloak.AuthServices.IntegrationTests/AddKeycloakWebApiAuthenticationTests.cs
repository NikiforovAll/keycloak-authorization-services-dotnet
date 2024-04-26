namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class AddKeycloakWebApiAuthenticationTests(KeycloakFixture fixture)
    : AuthenticationScenario(fixture)
{
    private const string Endpoint1 = "/endpoints/1";
    private static readonly string AppSettings = "appsettings.json";

    [Fact]
    public async Task AddKeycloakWebApiAuthentication_FromConfiguration_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.UseConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfiguration_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApiAuthentication_FromConfiguration_Setup(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        // #region AddKeycloakWebApiAuthentication_FromConfiguration
        services.AddKeycloakWebApiAuthentication(configuration);
        // #endregion AddKeycloakWebApiAuthentication_FromConfiguration
    }

    [Fact]
    public async Task AddKeycloakWebApiAuthentication_FromConfiguration2_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.UseConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfiguration2_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApiAuthentication_FromConfiguration2_Setup(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        // #region AddKeycloakWebApiAuthentication_FromConfiguration2
        services.AddKeycloakWebApiAuthentication(
            configuration,
            KeycloakAuthenticationOptions.Section
        );
        // #endregion AddKeycloakWebApiAuthentication_FromConfiguration2
    }

    [Fact]
    public async Task AddKeycloakWebApiAuthentication_FromConfigurationWithOverrides_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.UseConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfigurationWithOverrides_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApiAuthentication_FromConfigurationWithOverrides_Setup(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        // #region AddKeycloakWebApiAuthentication_FromConfigurationWithOverrides
        services.AddKeycloakWebApiAuthentication(
            configuration,
            (JwtBearerOptions options) =>
            {
                options.RequireHttpsMetadata = false;
                options.Audience = "test-client";
            }
        );
        // #endregion AddKeycloakWebApiAuthentication_FromConfigurationWithOverrides
    }

    [Fact]
    public async Task AddKeycloakWebApiAuthentication_FromConfigurationSection_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.UseConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfigurationSection_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApiAuthentication_FromConfigurationSection_Setup(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        // #region AddKeycloakWebApiAuthentication_FromConfigurationSection
        services.AddKeycloakWebApiAuthentication(
            configuration.GetSection(KeycloakAuthenticationOptions.Section)
        );
        // #endregion AddKeycloakWebApiAuthentication_FromConfigurationSection
    }
}
