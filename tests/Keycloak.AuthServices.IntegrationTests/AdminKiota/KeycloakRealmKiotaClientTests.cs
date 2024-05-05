namespace Keycloak.AuthServices.IntegrationTests;

using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Kiota;
using Keycloak.AuthServices.Sdk.Kiota.Admin;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

/// <remarks>
/// Used for demonstration/docs, this is why this class is so verbose
/// </remarks>
public class KeycloakRealmKiotaClientTests(ITestOutputHelper testOutputHelper)
    : AuthenticationScenarioNoKeycloak()
{
    private static readonly string AppSettings = "appsettings.Master.json";

    [Fact]
    public async Task GetRealmAsyncKiota_RealmExists_Success()
    {
        // NOTE, Used for demonstration/docs, this is why this class is so verbose
        var (services, configuration) = KeycloakSetup(AppSettings, testOutputHelper);
        var tokenClientName = "keycloak_admin_api_token";

        #region GetRealmAsync_RealmExists_Success
        var options = configuration.GetKeycloakOptions<KeycloakAdminClientOptions>()!;

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(
                tokenClientName,
                client =>
                {
                    client.ClientId = options.Resource;
                    client.ClientSecret = options.Credentials.Secret;
                    client.TokenEndpoint = options.KeycloakTokenEndpoint;
                }
            );

        services
            .AddKeycloakAdminHttpClient(configuration)
            .AddClientCredentialsTokenHandler(tokenClientName);

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<KeycloakAdminApiClient>();

        var realmName = "Test";
        var realm = await client.Admin.Realms[realmName].GetAsync();

        realm.Should().NotBeNull();
        realm!.Realm.Should().Be(realmName);

        #endregion GetRealmAsync_RealmExists_Success

    }
}
