namespace Keycloak.AuthServices.IntegrationTests;

using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Kiota;
using Keycloak.AuthServices.Sdk.Kiota.Admin;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

/// <remarks>
/// Used for demonstration/docs, this is why this class is so verbose
/// </remarks>
public class KeycloakRealmKiotaClientTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.Master.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task GetRealmAsyncKiota_RealmExists_Success()
    {
        // NOTE, Used for demonstration/docs, this is why this class is so verbose
        var (services, configuration) = KeycloakSetup(AppSettings, testOutputHelper);
        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_admin_api_token");

        #region GetRealmAsyncKiota_RealmExists_Success
        var options = configuration.GetKeycloakOptions<KeycloakAdminClientOptions>()!;
        options.AuthServerUrl = this.BaseAddress;

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(
                tokenClientName,
                client =>
                {
                    client.ClientId = ClientId.Parse(options.Resource);
                    client.ClientSecret = ClientSecret.Parse(options.Credentials.Secret);
                    client.TokenEndpoint = new Uri(options.KeycloakTokenEndpoint);
                }
            );

        services
            .AddKeycloakAdminHttpClient(options)
            .AddClientCredentialsTokenHandler(tokenClientName);

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<KeycloakAdminApiClient>();

        var realmName = "Test";
        var realm = await client.Admin.Realms[realmName].GetAsync();

        realm.Should().NotBeNull();
        realm!.Realm.Should().Be(realmName);

        #endregion GetRealmAsyncKiota_RealmExists_Success
    }
}
