namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Alba.Security;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class AuthorizationServerPolicyTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";

    [Fact]
    public async Task RequireProtectedResource_DefaultResource_Verified()
    {
        var policyName = "RequireProtectedResource";
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.UseConfiguration(AppSettings);

                x.ConfigureServices(
                    (context, services) =>
                    {
                        #region RequireProtectedResource_DefaultResource_Verified
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
                                        resource: "urn:test-client:resources:default",
                                        scope: string.Empty
                                    )
                            );

                        services
                            .AddAuthorizationServer(context.Configuration)
                            .AddStandardResilienceHandler(); // an example of how to extend IKeycloakProtectionClient by adding Polly

                        #endregion RequireProtectedResource_DefaultResource_Verified

                        services.PostConfigure<JwtBearerOptions>(options =>
                            options.WithKeycloakFixture(this.Keycloak)
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings))
        );

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(TestUsersRegistry.Admin.UserName, TestUsersRegistry.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(
                TestUsersRegistry.Tester.UserName,
                TestUsersRegistry.Tester.Password
            );
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    private static string RunPolicyBuyName(string policyName) =>
        $"/endpoints/RunPolicyBuyName?policy={policyName}";
}
