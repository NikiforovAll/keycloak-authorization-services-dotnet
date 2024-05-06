namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Alba.Security;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class DynamicProtectedResourcePolicyTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";

    [Fact]
    public async Task RequireProtectedResource_DynamicProtectedResource_Verified()
    {
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithConfiguration(AppSettings);

                x.ConfigureServices(
                    (context, services) =>
                    {
                        services
                            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddKeycloakWebApi(context.Configuration);

                        services.AddAuthorization().AddKeycloakAuthorization();

                        services.AddAuthorizationServer(context.Configuration);

                        services.PostConfigure<JwtBearerOptions>(options =>
                            options.WithLocalKeycloakInstallation()
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings))
        );
        await host.Scenario(_ =>
        {
            _.Get.Url("/endpoints/DynamicProtectedResourcePolicy");
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url("/endpoints/DynamicProtectedResourcePolicy");
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }
}
