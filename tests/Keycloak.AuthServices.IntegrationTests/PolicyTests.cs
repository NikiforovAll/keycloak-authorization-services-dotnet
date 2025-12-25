namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Alba.Security;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class PolicyTests(KeycloakFixture fixture, ITestOutputHelper testOutputHelper)
    : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task RequireRealmRoles_AdminRole_Verified()
    {
        var policyName = "RequireRealmRole";
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

                x.ConfigureServices(
                    (context, services) =>
                    {
                        #region RequireRealmRoles_AdminRole_Verified
                        services
                            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddKeycloakWebApi(context.Configuration);

                        services
                            .AddAuthorization()
                            .AddKeycloakAuthorization()
                            .AddAuthorizationBuilder()
                            .AddPolicy(
                                policyName,
                                policy => policy.RequireRealmRoles(KeycloakRoles.Admin)
                            );

                        #endregion RequireRealmRoles_AdminRole_Verified

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
    public async Task RequireClientRoles_TestClientRole_Verified()
    {
        var policyName = "RequireResourceRolesForClient";
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

                x.ConfigureServices(
                    (context, services) =>
                    {
                        #region RequireClientRoles_TestClientRole_Verified
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
                                    policy.RequireResourceRolesForClient(
                                        "test-client",
                                        [KeycloakRoles.TestClientRole]
                                    )
                            );

                        #endregion RequireClientRoles_TestClientRole_Verified

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
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }

    [Fact]
    public async Task RequireClientRoles_TestClientRoleWithInlineConfiguration_Verified()
    {
        var policyName = "RequireResourceRoles";
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

                x.ConfigureServices(
                    (context, services) =>
                    {
                        #region RequireClientRoles_TestClientRoleWithInlineConfiguration_Verified
                        services
                            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddKeycloakWebApi(context.Configuration);

                        services
                            .AddAuthorization()
                            .AddKeycloakAuthorization(options =>
                                options.RolesResource = "test-client"
                            )
                            .AddAuthorizationBuilder()
                            .AddPolicy(
                                policyName,
                                policy => policy.RequireResourceRoles(KeycloakRoles.TestClientRole)
                            );

                        #endregion RequireClientRoles_TestClientRoleWithInlineConfiguration_Verified

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
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }

    [Fact]
    public async Task RequireClientRoles_TestClientRoleWithConfiguration_Verified()
    {
        var policyName = "RequireResourceRoles";
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

                x.ConfigureServices(
                    (context, services) =>
                    {
                        #region RequireClientRoles_TestClientRoleWithConfiguration_Verified
                        services
                            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddKeycloakWebApi(context.Configuration);

                        services
                            .AddAuthorization()
                            .AddKeycloakAuthorization(context.Configuration)
                            .AddAuthorizationBuilder()
                            .AddPolicy(
                                policyName,
                                policy => policy.RequireResourceRoles(KeycloakRoles.TestClientRole)
                            );

                        #endregion RequireClientRoles_TestClientRoleWithConfiguration_Verified

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
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }

    [Fact]
    public async Task RequireRealmRoles_AdminRoleWithMapping_Verified()
    {
        var policyName = "RequireRole";
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

                x.ConfigureServices(
                    (context, services) =>
                    {
                        #region RequireRealmRoles_AdminRoleWithMapping_Verified
                        services
                            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddKeycloakWebApi(context.Configuration);

                        services
                            .AddAuthorization()
                            .AddKeycloakAuthorization(options =>
                            {
                                options.EnableRolesMapping = RolesClaimTransformationSource.Realm;
                                // Note, this should correspond to role configured with KeycloakAuthenticationOptions
                                options.RoleClaimType = KeycloakConstants.RoleClaimType;
                            })
                            .AddAuthorizationBuilder()
                            .AddPolicy(
                                policyName,
                                policy => policy.RequireRole(KeycloakRoles.Admin)
                            );

                        #endregion RequireRealmRoles_AdminRoleWithMapping_Verified

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
    public async Task RequireClientRoles_TestClientRoleWithMapping_Verified()
    {
        var policyName = "RequireRole";
        await using var host = await AlbaHost.For<Program>(
            x =>
            {
                x.WithLogging(testOutputHelper);
                x.WithKeycloakConfiguration(AppSettings, this.BaseAddress);

                x.ConfigureServices(
                    (context, services) =>
                    {
                        #region RequireClientRoles_TestClientRoleWithMapping_Verified
                        services
                            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddKeycloakWebApi(context.Configuration);

                        services
                            .AddAuthorization()
                            .AddKeycloakAuthorization(options =>
                            {
                                options.EnableRolesMapping =
                                    RolesClaimTransformationSource.ResourceAccess;
                                options.RolesResource = "test-client";
                            })
                            .AddAuthorizationBuilder()
                            .AddPolicy(
                                policyName,
                                policy => policy.RequireRole(KeycloakRoles.TestClientRole)
                            );

                        #endregion RequireClientRoles_TestClientRoleWithMapping_Verified

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
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });
    }

    private static string RunPolicyByName(string policyName) =>
        $"/endpoints/RunPolicyByName?policy={policyName}";
}
