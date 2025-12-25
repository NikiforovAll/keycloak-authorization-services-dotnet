namespace Keycloak.AuthServices.IntegrationTests;

using Duende.AccessTokenManagement;
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
public class KeycloakRealmClientTests(KeycloakFixture fixture, ITestOutputHelper testOutputHelper)
    : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.Master.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task GetRealmAsync_RealmExistsInlineConfiguration_Success()
    {
        var (services, _) = KeycloakSetup(string.Empty, testOutputHelper);

        #region GetRealmAsync_RealmExistsInlineConfiguration_Success
        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_admin_api_token");

        var keycloakOptions = new KeycloakAdminClientOptions
        {
            AuthServerUrl = this.BaseAddress,
            Realm = "master",
            Resource = "admin-api",
            Credentials = new() { Secret = "k9LYTWKfbNOyfzFt2ZZsFl3Z4x4aAecf" },
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
            ClientCredentialsClient client,
            KeycloakAdminClientOptions adminClientOptions
        )
        {
            client.ClientId = ClientId.Parse(adminClientOptions.Resource);
            client.ClientSecret = ClientSecret.Parse(adminClientOptions.Credentials.Secret);
            client.TokenEndpoint = new Uri(adminClientOptions.KeycloakTokenEndpoint);
        }
        #endregion GetRealmAsync_RealmExistsInlineConfiguration_Success
    }

    [Fact]
    public async Task GetRealmAsync_RealmExists_Success()
    {
        var (services, configuration) = KeycloakSetup(AppSettings, testOutputHelper);
        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_admin_api_token");

        #region GetRealmAsync_RealmExists_Success
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
        var client = sp.GetRequiredService<IKeycloakRealmClient>();

        var realm = await client.GetRealmAsync("Test");
        #endregion GetRealmAsync_RealmExists_Success

        realm.Should().NotBeNull();
    }

    [Fact]
    public async Task GetRealmAsync_NoRealmExists_ExceptionThrown()
    {
        var (services, configuration) = KeycloakSetup(AppSettings, testOutputHelper);
        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_admin_api_token");

        #region GetRealmAsync_NoRealmExists_ExceptionThrown
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
        var client = sp.GetRequiredService<IKeycloakRealmClient>();

        var result = await FluentActions
            .Invoking(() => client.GetRealmAsync(Guid.NewGuid().ToString()))
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        result.And.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        #endregion GetRealmAsync_NoRealmExists_ExceptionThrown
    }
}
