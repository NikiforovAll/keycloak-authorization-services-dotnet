namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Alba.Security;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class AuthorizationServerPolicyTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task RequireProtectedResource_DefaultResource_Verified()
    {
        var policyName = "RequireProtectedResource";
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

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
                            .AddStandardResilienceHandler(); // an example of how to extend based on IHttpClientBuilder by adding Polly
                        #endregion RequireProtectedResource_DefaultResource_Verified

                        services.PostConfigure<JwtBearerOptions>(options =>
                            options.RequireHttpsMetadata = false
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
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
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

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
                            options.RequireHttpsMetadata = false
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );
        #region RequireProtectedResource_Scopes_Verified_Assertion

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
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
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

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
                            options.RequireHttpsMetadata = false
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
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
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

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
                            options.RequireHttpsMetadata = false
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
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
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

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
                                            "workspace:unknown",
                                        ],
                                        scopesValidationMode: ScopesValidationMode.AllOf
                                    )
                            );

                        services.AddAuthorizationServer(context.Configuration);

                        #endregion RequireProtectedResource_MultipleScopesMissingScope_Verified

                        services.PostConfigure<JwtBearerOptions>(options =>
                            options.RequireHttpsMetadata = false
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );
        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
            _.UserAndPasswordIs(TestUsers.Admin.UserName, TestUsers.Admin.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });

        await host.Scenario(_ =>
        {
            _.Get.Url(RunPolicyByName(policyName));
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Forbidden);
        });
    }

    private static string RunPolicyByName(string policyName) =>
        $"/endpoints/RunPolicyByName?policy={policyName}";
}
