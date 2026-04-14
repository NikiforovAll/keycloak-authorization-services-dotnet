namespace Keycloak.AuthServices.IntegrationTests;

using System.Net;
using Alba;
using Alba.Security;
using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class UmaPermissionTicketChallengeTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task WhenForbidden_WithDecisionRequirement_ReturnsUmaChallenge()
    {
        var keycloakOptions = ReadKeycloakAuthenticationOptions(AppSettings, this.BaseAddress);
        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_uma_token");

        await using var host = await AlbaHost.For<Program>(
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

                        services
                            .AddAuthorization()
                            .AddKeycloakAuthorization()
                            .AddUmaPermissionTicketChallenge()
                            .AddAuthorizationBuilder()
                            .AddPolicy(
                                "UmaProtected",
                                policy => policy.RequireProtectedResource("uma-document", "read")
                            );

                        services
                            .AddAuthorizationServer(context.Configuration)
                            .AddStandardResilienceHandler();

                        services.AddDistributedMemoryCache();
                        services
                            .AddClientCredentialsTokenManagement()
                            .AddClient(
                                tokenClientName,
                                client =>
                                {
                                    client.ClientId = ClientId.Parse(keycloakOptions.Resource);
                                    client.ClientSecret = ClientSecret.Parse(
                                        keycloakOptions.Credentials.Secret
                                    );
                                    client.TokenEndpoint = new Uri(
                                        keycloakOptions.KeycloakTokenEndpoint
                                    );
                                }
                            );

                        services
                            .AddKeycloakProtectionHttpClient(context.Configuration)
                            .AddClientCredentialsTokenHandler(tokenClientName);

                        services.PostConfigure<JwtBearerOptions>(options =>
                            options.RequireHttpsMetadata = false
                        );
                    }
                );
            },
            UserPasswordFlow(ReadKeycloakAuthenticationOptions(AppSettings), this.BaseAddress)
        );

        // /uma/protected requires uma-document#read via DecisionRequirement (route-level auth)
        // Tester doesn't have permission → middleware handler fires → UMA challenge
        var result = await host.Scenario(_ =>
        {
            _.Get.Url("/uma/protected");
            _.UserAndPasswordIs(TestUsers.Tester.UserName, TestUsers.Tester.Password);
            _.StatusCodeShouldBe(HttpStatusCode.Unauthorized);
        });

        var wwwAuthenticate = result.Context.Response.Headers["WWW-Authenticate"].ToString();
        wwwAuthenticate.Should().Contain("UMA");
        wwwAuthenticate.Should().Contain("ticket=");
        wwwAuthenticate.Should().Contain("as_uri=");
    }
}
