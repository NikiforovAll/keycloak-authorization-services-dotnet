namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Alba.Security;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class ProtectedResourceWithControllersPolicyTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task ProtectedResourceAttribute_DeleteWorkspaceAndAccessPublic_Verified()
    {
        await using var host = await AlbaHost.For<TestWebApiWithControllers.Program>(
            SetupAuthorizationServer(testOutputHelper),
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        var requestUrl = "/workspaces/my-workspace";
        await host.Scenario(_ =>
        {
            _.Delete.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        await host.Scenario(_ =>
        {
            _.Delete.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });

        var publicWorkspaceUrl = "/workspaces/public";

        await host.Scenario(_ =>
        {
            _.Get.Url(publicWorkspaceUrl);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(publicWorkspaceUrl);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }

    private Action<IWebHostBuilder> SetupAuthorizationServer(ITestOutputHelper testOutputHelper) =>
        x =>
        {
            x.WithLogging(testOutputHelper);
            x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

            x.ConfigureServices(
                (context, services) =>
                {
                    #region SetupProtectedResourcesMVC
                    services.AddControllers(options => options.AddProtectedResources());

                    services
                        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddKeycloakWebApi(context.Configuration);

                    services
                        .AddAuthorization()
                        .AddKeycloakAuthorization()
                        .AddAuthorizationServer(context.Configuration);
                    #endregion SetupProtectedResourcesMVC

                    services.PostConfigure<JwtBearerOptions>(options =>
                        options.RequireHttpsMetadata = false
                    );
                }
            );
        };
}
