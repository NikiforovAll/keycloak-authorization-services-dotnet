namespace Keycloak.AuthServices.IntegrationTests;

using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Kiota;
using Keycloak.AuthServices.Sdk.Kiota.Admin;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class KeycloakOrganizationKiotaClientTests(
    KeycloakFixture fixture,
    ITestOutputHelper testOutputHelper
) : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.Master.json";
    private string BaseAddress => fixture.BaseAddress;

    private KeycloakAdminApiClient CreateClient()
    {
        var (services, configuration) = KeycloakSetup(AppSettings, testOutputHelper);
        var tokenClientName = ClientCredentialsClientName.Parse("keycloak_admin_api_token");

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
        return sp.GetRequiredService<KeycloakAdminApiClient>();
    }

    [Fact]
    public async Task ListOrganizations_Empty_Success()
    {
        var client = this.CreateClient();

        var orgs = await client.Admin.Realms["Test"].Organizations.GetAsync();

        orgs.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAndGetOrganization_Success()
    {
        var client = this.CreateClient();
        var orgName = $"test-org-{Guid.NewGuid():N}";

        await client
            .Admin.Realms["Test"]
            .Organizations.PostAsync(new OrganizationRepresentation { Name = orgName });

        var orgs = await client
            .Admin.Realms["Test"]
            .Organizations.GetAsync(r => r.QueryParameters.Search = orgName);

        orgs.Should().ContainSingle();
        orgs![0].Name.Should().Be(orgName);

        var orgId = orgs[0].Id!;
        var org = await client.Admin.Realms["Test"].Organizations[orgId].GetAsync();

        org.Should().NotBeNull();
        org!.Name.Should().Be(orgName);
    }

    [Fact]
    public async Task OrganizationMembers_ListEmpty_Success()
    {
        var client = this.CreateClient();
        var orgName = $"test-org-members-{Guid.NewGuid():N}";

        await client
            .Admin.Realms["Test"]
            .Organizations.PostAsync(new OrganizationRepresentation { Name = orgName });

        var orgs = await client
            .Admin.Realms["Test"]
            .Organizations.GetAsync(r => r.QueryParameters.Search = orgName);
        var orgId = orgs![0].Id!;

        var members = await client.Admin.Realms["Test"].Organizations[orgId].Members.GetAsync();

        members.Should().NotBeNull();
        members.Should().BeEmpty();
    }

    [Fact]
    public async Task OrganizationInvitations_ListEmpty_Success()
    {
        var client = this.CreateClient();
        var orgName = $"test-org-invitations-{Guid.NewGuid():N}";

        await client
            .Admin.Realms["Test"]
            .Organizations.PostAsync(new OrganizationRepresentation { Name = orgName });

        var orgs = await client
            .Admin.Realms["Test"]
            .Organizations.GetAsync(r => r.QueryParameters.Search = orgName);
        var orgId = orgs![0].Id!;

        var invitations = await client
            .Admin.Realms["Test"]
            .Organizations[orgId]
            .Invitations.GetAsync();

        invitations.Should().NotBeNull();
        invitations.Should().BeEmpty();
    }

    [Fact]
    public async Task OrganizationCount_Success()
    {
        var client = this.CreateClient();

        var count = await client.Admin.Realms["Test"].Organizations.Count.GetAsync();

        count.Should().NotBeNull();
        count!.Value.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task DeleteOrganization_Success()
    {
        var client = this.CreateClient();
        var orgName = $"test-org-delete-{Guid.NewGuid():N}";

        await client
            .Admin.Realms["Test"]
            .Organizations.PostAsync(new OrganizationRepresentation { Name = orgName });

        var orgs = await client
            .Admin.Realms["Test"]
            .Organizations.GetAsync(r => r.QueryParameters.Search = orgName);
        var orgId = orgs![0].Id!;

        await client.Admin.Realms["Test"].Organizations[orgId].DeleteAsync();

        var orgsAfterDelete = await client
            .Admin.Realms["Test"]
            .Organizations.GetAsync(r => r.QueryParameters.Search = orgName);

        orgsAfterDelete.Should().BeEmpty();
    }
}
