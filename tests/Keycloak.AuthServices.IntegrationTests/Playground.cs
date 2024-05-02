namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Alba.Security;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class Playground(ITestOutputHelper testOutputHelper)
{
    private static readonly string AppSettings = "appsettings.json";

    [Fact(Skip = "Playground Test")]
    public async Task PlaygroundRequireProtectedResource_Scopes_Verified()
    {
        var policyName = "RequireProtectedResource";
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

                        services
                            .AddAuthorization()
                            .AddKeycloakAuthorization()
                            .AddAuthorizationBuilder()
                            .AddPolicy(
                                policyName,
                                policy =>
                                    policy.RequireProtectedResource(
                                        resource: "my-workspace",
                                        scopes: ["workspace:read", "workspace:delete"],
                                        scopesValidationMode: ScopesValidationMode.AllOf
                                    )
                            );

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
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }

    private static string RunPolicyBuyName(string policyName) =>
        $"/endpoints/RunPolicyBuyName?policy={policyName}";
}
