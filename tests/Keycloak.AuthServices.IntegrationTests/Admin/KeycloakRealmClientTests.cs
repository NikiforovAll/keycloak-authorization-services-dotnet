namespace Keycloak.AuthServices.IntegrationTests;

using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

/// <remarks>
/// Used for demonstration/docs, this is why this class is so verbose
/// </remarks>
public class KeycloakRealmClientTests(ITestOutputHelper testOutputHelper)
    : AuthenticationScenarioNoKeycloak()
{
    private static readonly string AppSettings = "appsettings.Master.json";

    [Fact]
    public async Task GetRealmAsync_RealmExistsInlineConfiguration_Success()
    {
        var (services, _) = KeycloakSetup(string.Empty, testOutputHelper);

        #region GetRealmAsync_RealmExistsInlineConfiguration_Success
        var tokenClientName = "keycloak_admin_api_token";

        var keycloakOptions = new KeycloakAdminClientOptions
        {
            AuthServerUrl = "http://localhost:8080/",
            Realm = "master",
            Resource = "admin-api",
        };

        services.AddDistributedMemoryCache();
        services
            .AddClientCredentialsTokenManagement()
            .AddClient(tokenClientName, client => BindKeycloak(client, keycloakOptions));

        services
            .AddKeycloakAdminHttpClient(keycloakOptions)
            .AddClientCredentialsTokenHandler(tokenClientName);

        var sp = services.BuildServiceProvider();

        var client = sp.GetRequiredService<IKeycloakRealmClient>();

        var response = await client.GetRealmWithResponseAsync("Test");

        var realm = response.GetResponseAsync<RealmRepresentation>();

        realm.Should().NotBeNull();

        static void BindKeycloak(
            Duende.AccessTokenManagement.ClientCredentialsClient client,
            KeycloakAdminClientOptions adminClientOptions
        )
        {
            client.ClientId = adminClientOptions.Resource;
            client.ClientSecret = adminClientOptions.Credentials.Secret;
            client.TokenEndpoint = adminClientOptions.KeycloakTokenEndpoint;
        }
        #endregion GetRealmAsync_RealmExistsInlineConfiguration_Success
    }

    [Fact]
    public async Task GetRealmAsync_RealmExists_Success()
    {
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
            .AddKeycloakAdminHttpClient(options)
            .AddClientCredentialsTokenHandler(tokenClientName);

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<IKeycloakRealmClient>();

        var realm = await client.GetRealmAsync("Test");
        #endregion GetRealmAsync_RealmExists_Success

        realm.Should().NotBeNull();
    }

    [Fact]
    public async Task GetRealmAsync_NoRealmExists_ExceptionThrown()
    {
        var (services, configuration) = KeycloakSetup(AppSettings, testOutputHelper);
        var tokenClientName = "keycloak_admin_api_token";

        #region GetRealmAsync_NoRealmExists_ExceptionThrown
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
            .AddKeycloakAdminHttpClient(options)
            .AddClientCredentialsTokenHandler(tokenClientName);

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<IKeycloakRealmClient>();

        var result = await FluentActions
            .Invoking(() => client.GetRealmAsync(Guid.NewGuid().ToString()))
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        result.And.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        #endregion GetRealmAsync_NoRealmExists_ExceptionThrown
    }
}
