namespace Keycloak.AuthServices.IntegrationTests;

using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Protection;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class KeycloakProtectionClientTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task GetResourcesIdsAsync_Success()
    {
        var (services, _) = ProtectionHttpClientSetup(
            AppSettings,
            testOutputHelper,
            this.BaseAddress
        );

        #region GetResourcesIdsAsync_Success

        var sp = services.BuildServiceProvider();

        var client = sp.GetRequiredService<IKeycloakProtectionClient>();

        var resources = await client.GetResourcesIdsAsync("Test");

        resources.Should().NotBeNull();
        #endregion GetResourcesIdsAsync_Success
    }

    [Fact]
    public async Task GetResourcesAsync_Success()
    {
        var (services, configuration) = KeycloakSetup(AppSettings, testOutputHelper);
        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_protection_api_token");

        // NOTE: the code is verbose for demonstration/docs

        #region GetResourcesAsync_Success
        var keycloakOptions = configuration.GetKeycloakOptions<KeycloakProtectionClientOptions>()!;
        keycloakOptions.AuthServerUrl = this.BaseAddress;

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(
                tokenClientName,
                client =>
                {
                    client.ClientId = ClientId.Parse(keycloakOptions.Resource);
                    client.ClientSecret = ClientSecret.Parse(keycloakOptions.Credentials.Secret);
                    client.TokenEndpoint = new Uri(keycloakOptions.KeycloakTokenEndpoint);
                }
            );

        services
            .AddKeycloakProtectionHttpClient(keycloakOptions)
            .AddClientCredentialsTokenHandler(tokenClientName);

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<IKeycloakProtectionClient>();

        var resources = await client.GetResourcesAsync("Test");
        #endregion GetResourcesAsync_Success

        resources.Should().NotBeNull();
    }
}
