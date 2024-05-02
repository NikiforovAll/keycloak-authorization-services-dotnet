namespace Keycloak.AuthServices.IntegrationTests.ConfigurationTests;

using System.Net;
using Alba;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.IntegrationTests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public class AddKeycloakWebApiAuthenticationTests : AuthenticationScenarioNoKeycloak
{
    private const string Endpoint1 = "/endpoints/1";
    private static readonly string AppSettings = "appsettings.json";

    private static readonly JwtBearerOptions ExpectedAppSettingsJwtBearerOptions =
        new()
        {
            Authority = "http://localhost:8080/realms/Test/",
            Audience = "test-client",
            RequireHttpsMetadata = false,
            TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false },
        };

    private static readonly string AppSettingsWithOverrides = "appsettings.with-overrides.json";

    [Fact]
    public async Task AddKeycloakWebApiAuthentication_FromConfiguration_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.WithConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfiguration_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        host.Services.EnsureConfiguredJwtOptions(ExpectedAppSettingsJwtBearerOptions);

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
            x.WithConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfiguration2_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        host.Services.EnsureConfiguredJwtOptions(ExpectedAppSettingsJwtBearerOptions);

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
            x.WithConfiguration(AppSettingsWithOverrides);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfigurationWithOverrides_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        host.Services.EnsureConfiguredJwtOptions(
            new JwtBearerOptions
            {
                Audience = "test-client",
                Authority = "http://localhost:8080/realms/DefaultTest",
                RequireHttpsMetadata = false,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true
                }
            }
        );

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
        services.AddKeycloakWebApiAuthentication(configuration);
        // #endregion AddKeycloakWebApiAuthentication_FromConfigurationWithOverrides
    }

    [Fact]
    public async Task AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.WithConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        host.Services.EnsureConfiguredJwtOptions(ExpectedAppSettingsJwtBearerOptions);

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides_Setup(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        // #region AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides
        services.AddKeycloakWebApiAuthentication(
            configuration,
            (options) =>
            {
                options.RequireHttpsMetadata = false;
                options.Audience = "test-client";
            }
        );
        // #endregion AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides
    }

    [Fact]
    public async Task AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides2_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.WithConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides2_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        host.Services.EnsureConfiguredJwtOptions(ExpectedAppSettingsJwtBearerOptions);

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });
    }

    private static void AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides2_Setup(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        // #region AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides2
        services.AddKeycloakWebApiAuthentication(options =>
        {
            configuration.BindKeycloakOptions(options);

            options.SslRequired = "none";
            options.Audience = "test-client";
        });
        // #endregion AddKeycloakWebApiAuthentication_FromConfigurationWithInlineOverrides2
    }

    [Fact]
    public async Task AddKeycloakWebApiAuthentication_FromConfigurationSection_Unauthorized()
    {
        await using var host = await AlbaHost.For<Program>(x =>
        {
            x.WithConfiguration(AppSettings);
            x.ConfigureServices(
                (context, services) =>
                    AddKeycloakWebApiAuthentication_FromConfigurationSection_Setup(
                        services,
                        context.Configuration
                    )
            );
        });

        host.Services.EnsureConfiguredJwtOptions(ExpectedAppSettingsJwtBearerOptions);

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
