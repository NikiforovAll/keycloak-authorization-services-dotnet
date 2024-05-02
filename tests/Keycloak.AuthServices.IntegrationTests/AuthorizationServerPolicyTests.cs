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
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_Scopes_Verified()
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
                        #region RequireProtectedResource_Scopes_Verified
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
                                        scope: "workspace:delete"
                                    )
                            );

                        services.AddAuthorizationServer(context.Configuration);

                        #endregion RequireProtectedResource_Scopes_Verified

                        services.PostConfigure<JwtBearerOptions>(options =>
                            options.WithLocalKeycloakInstallation()
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings))
        );
        #region RequireProtectedResource_Scopes_Verified_Assertion

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
        #endregion RequireProtectedResource_Scopes_Verified_Assertion
    }

    [Fact]
    public async Task RequireProtectedResource_MultipleScopesAllOf_Verified()
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
                        #region RequireProtectedResource_MultipleScopesAllOf_Verified
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
                                        scopes: ["workspace:delete", "workspace:read"],
                                        scopesValidationMode: ScopesValidationMode.AllOf
                                    )
                            );

                        services.AddAuthorizationServer(context.Configuration);

                        #endregion RequireProtectedResource_MultipleScopesAllOf_Verified

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

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_MultipleScopesAnyOf_Verified()
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
                        #region RequireProtectedResource_MultipleScopesAnyOf_Verified
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
                                        scopes: ["workspace:delete", "workspace:read"],
                                        scopesValidationMode: ScopesValidationMode.AnyOf
                                    )
                            );

                        services.AddAuthorizationServer(context.Configuration);

                        #endregion RequireProtectedResource_MultipleScopesAnyOf_Verified

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

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }

    [Fact]
    public async Task RequireProtectedResource_MultipleScopesMissingScope_Verified()
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
                        #region RequireProtectedResource_MultipleScopesMissingScope_Verified
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
                                        scopes:
                                        [
                                            "workspace:read",
                                            "workspace:delete",
                                            "workspace:unknown"
                                        ],
                                        scopesValidationMode: ScopesValidationMode.AllOf
                                    )
                            );

                        services.AddAuthorizationServer(context.Configuration);

                        #endregion RequireProtectedResource_MultipleScopesMissingScope_Verified

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
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyBuyName(policyName));
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    private static string RunPolicyBuyName(string policyName) =>
        $"/endpoints/RunPolicyBuyName?policy={policyName}";
}
