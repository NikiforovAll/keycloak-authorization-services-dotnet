namespace Keycloak.AuthServices.IntegrationTests.ConfigurationTests;

using System.Net;
using Alba;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class AddKeycloakWebApiTests : AuthenticationScenarioNoKeycloak
{
    private const string Endpoint1 = "/endpoints/1";
    private static readonly string AppSettings = "appsettings.json";

    [Fact]
    public async Task AddKeycloakWebApi_FromConfiguration_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.UseConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApi_FromConfiguration_Setup(services, context.Configuration)
            );
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApi_FromConfiguration_Setup(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        // #region AddKeycloakWebApiAuthentication_FromConfiguration
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddKeycloakWebApi(configuration);
        // #endregion AddKeycloakWebApiAuthentication_FromConfiguration
    }

    [Fact]
    public async Task AddKeycloakWebApi_FromConfigurationSection_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.UseConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApi_FromConfigurationSection_Setup(
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

    private static void AddKeycloakWebApi_FromConfigurationSection_Setup(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        // #region AddKeycloakWebApiAuthentication_FromConfigurationSection
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddKeycloakWebApi(configuration.GetSection(KeycloakAuthenticationOptions.Section));
        // #endregion AddKeycloakWebApiAuthentication_FromConfigurationSection
    }

    [Fact]
    public async Task AddKeycloakWebApi_FromInline_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.UseConfiguration(AppSettings);
            x.ConfigureServices(AddKeycloakWebApi_FromInline_Setup);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApi_FromInline_Setup(IServiceCollection services)
    {
        // #region AddKeycloakWebApiAuthentication_FromInline
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddKeycloakWebApi(options =>
            {
                options.Resource = "test-client";
                options.Realm = "Test";
                options.SslRequired = "none";
                options.AuthServerUrl = "http://localhost:8080/";
            });
        // #endregion AddKeycloakWebApiAuthentication_FromInline
    }

    [Fact]
    public async Task AddKeycloakWebApi_FromInline2_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.UseConfiguration(AppSettings);
            x.ConfigureServices(AddKeycloakWebApi_FromInline2_Setup);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApi_FromInline2_Setup(IServiceCollection services)
    {
        // #region AddKeycloakWebApiAuthentication_FromInline2
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddKeycloakWebApi(
                options =>
                {
                    options.Resource = "test-client";
                    options.Realm = "Test";
                    options.AuthServerUrl = "http://localhost:8080/";
                },
                options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = "test-client";
                }
            );
        // #endregion AddKeycloakWebApiAuthentication_FromInline2
    }
}
