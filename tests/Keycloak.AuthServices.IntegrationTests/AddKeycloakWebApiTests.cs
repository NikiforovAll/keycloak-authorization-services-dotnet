namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Alba.Security;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;
public class AddKeycloakWebApiTests(KeycloakFixture fixture, ITestOutputHelper testOutputHelper)
    : AuthenticationScenario(fixture)
{
    private const string Endpoint1 = "/endpoints/1";
    private static readonly string AppSettings = "appsettings.json";

    [Fact]
    public async Task AddKeycloakWebApi_FromConfiguration_Ok()
    {
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithConfiguration(AppSettings);

                x.ConfigureServices(
                    (context, services) =>
                        services
                            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddKeycloakWebApi(
                                context.Configuration,
                                options => options.WithKeycloakFixture(this.Keycloak)
                            )
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings))
        );

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }

    [Fact]
    public async Task AddKeycloakWebApi_FromConfigurationSection_Ok()
    {
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithConfiguration(AppSettings);

                x.ConfigureServices(
                    (context, services) =>
                        services
                            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddKeycloakWebApi(
                                context.Configuration.GetSection(
                                    KeycloakAuthenticationOptions.Section
                                ),
                                options => options.WithKeycloakFixture(this.Keycloak)
                            )
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings))
        );

        await host.Scenario(_ =>
        {
            _.Get.Url(Endpoint1);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }
}
