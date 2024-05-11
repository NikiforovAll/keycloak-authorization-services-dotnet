namespace Keycloak.AuthServices.IntegrationTests;

using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Protection;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class KeycloakProtectionClientTests(ITestOutputHelper testOutputHelper)
    : AuthenticationScenarioNoKeycloak()
{
    private static readonly string AppSettings = "appsettings.json";

    [Fact]
    public async Task GetResourcesIdsAsync_Success()
    {
        var (services, _) = ProtectionHttpClientSetup(AppSettings, testOutputHelper);

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
        var tokenClientName = "keycloak_protection_api_token";

        // NOTE: the code is verbose for demonstration/docs

        #region GetResourcesAsync_Success
        var keycloakOptions = configuration.GetKeycloakOptions<KeycloakProtectionClientOptions>()!;

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(
                tokenClientName,
                client =>
                {
                    client.ClientId = keycloakOptions.Resource;
                    client.ClientSecret = keycloakOptions.Credentials.Secret;
                    client.TokenEndpoint = keycloakOptions.KeycloakTokenEndpoint;
                }
            );

        services
            .AddKeycloakProtectionHttpClient(configuration)
            .AddClientCredentialsTokenHandler(tokenClientName);

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<IKeycloakProtectionClient>();

        var resources = await client.GetResourcesAsync("Test");
        #endregion GetResourcesAsync_Success

        resources.Should().NotBeNull();
    }
}
