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

public class ProtectedResourcePolicyTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task RequireProtectedResource_SingleResourceSingleScopeSingleEndpoint_Verified()
    {
        await using var host = await AlbaHost.For<Program>(
            SetupAuthorizationServer(testOutputHelper),
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        var requestUrl = RunEndpoint(
            "SingleResourceSingleScopeSingleEndpoint",
            "workspaces#workspace:delete"
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_SingleResourceMultipleScopesSingleEndpoint_Verified()
    {
        await using var host = await AlbaHost.For<Program>(
            SetupAuthorizationServer(testOutputHelper),
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        var requestUrl = RunEndpoint(
            "SingleResourceMultipleScopesSingleEndpoint",
            "workspaces#workspace:read",
            "workspaces#workspace:delete"
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_SingleResourceMultipleScopesEndpointHierarchy_Verified()
    {
        await using var host = await AlbaHost.For<Program>(
            SetupAuthorizationServer(testOutputHelper),
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        var requestUrl = RunEndpoint(
            "SingleResourceMultipleScopesEndpointHierarchy",
            "workspaces#workspace:read",
            "workspaces#workspace:delete"
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_MultipleResourcesMultipleScopesSingleEndpoint_Verified()
    {
        await using var host = await AlbaHost.For<Program>(
            SetupAuthorizationServer(testOutputHelper),
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        // tests/TestWebApi/Program.cs#MultipleResourcesMultipleScopesSingleEndpoint
        var requestUrl = RunEndpoint(
            "MultipleResourcesMultipleScopesSingleEndpoint",
            "workspaces#workspace:read",
            "my-workspace#workspace:delete"
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_MultipleResourcesMultipleScopesEndpointHierarchy_Verified()
    {
        await using var host = await AlbaHost.For<Program>(
            SetupAuthorizationServer(testOutputHelper),
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        // tests/TestWebApi/Program.cs#MultipleResourcesMultipleScopesEndpointHierarchy
        var requestUrl = RunEndpoint(
            "MultipleResourcesMultipleScopesEndpointHierarchy",
            "workspaces#workspace:read",
            "my-workspace#workspace:delete"
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_SingleResourceIgnoreAllResourcesEndpointHierarchy_Verified()
    {
        await using var host = await AlbaHost.For<Program>(
            SetupAuthorizationServer(testOutputHelper),
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        // tests/TestWebApi/Program.cs#SingleResourceIgnoreProtectedResourceEndpointHierarchy
        var requestUrl1 = RunEndpoint(
            "SingleResourceIgnoreProtectedResourceEndpointHierarchy1",
            "my-workspace#workspace:read"
        );
        var requestUrl2 = RunEndpoint(
            "SingleResourceIgnoreProtectedResourceEndpointHierarchy2",
            "my-workspace#workspace:delete,workspace:read"
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl1);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl1);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl2);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl2);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_SingleDynamicResourceSingleScopeSingleEndpoint_Verified()
    {
        await using var host = await AlbaHost.For<Program>(
            SetupAuthorizationServer(testOutputHelper),
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        // tests/TestWebApi/Program.cs#MultipleResourcesMultipleScopesEndpointHierarchy
        var requestUrl = RunEndpoint(
            "SingleDynamicResourceSingleScopeSingleEndpoint/my-workspace",
            "my-workspace:workspace:delete"
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(requestUrl);
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
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
                    services
                        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddKeycloakWebApi(context.Configuration);

                    services.AddAuthorization().AddKeycloakAuthorization();

                    services.AddAuthorizationServer(context.Configuration);

                    services.PostConfigure<JwtBearerOptions>(options =>
                        options.RequireHttpsMetadata = false
                    );
                }
            );
        };

    private static string RunEndpoint(string path, params string[] resources) =>
        $"/pr/{path}?resource={string.Join(';', resources)}";
}
