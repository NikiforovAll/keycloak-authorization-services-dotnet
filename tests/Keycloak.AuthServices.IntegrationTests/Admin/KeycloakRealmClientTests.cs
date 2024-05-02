namespace Keycloak.AuthServices.IntegrationTests;

using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class KeycloakRealmClientTests(ITestOutputHelper testOutputHelper)
    : AuthenticationScenarioNoKeycloak()
{
    private static readonly string AppSettings = "appsettings.Master.json";

    [Fact]
    public async Task GetRealmAsync_RealmExists_Success()
    {
        var (services, configuration) = KeycloakSetup(AppSettings, testOutputHelper);

        #region GetRealmAsync_RealmExists_Success
        var tokenClientName = "keycloak_admin_api_token";

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(
                tokenClientName,
                client =>
                {
                    var options = configuration.GetKeycloakOptions<KeycloakAdminClientOptions>()!;

                    client.ClientId = options.Resource;
                    client.ClientSecret = options.Credentials.Secret;
                    client.TokenEndpoint = options.KeycloakTokenEndpoint;
                }
            );

        services
            .AddKeycloakAdminHttpClient(configuration)
            .AddClientCredentialsTokenHandler(tokenClientName);

        var sp = services.BuildServiceProvider();

        var client = sp.GetRequiredService<IKeycloakRealmClient>();

        var realm = await client.GetRealmAsync("Test");

        realm.Should().NotBeNull();
        #endregion GetRealmAsync_RealmExists_Success
    }
}
