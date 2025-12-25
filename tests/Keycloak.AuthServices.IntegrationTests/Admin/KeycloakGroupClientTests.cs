namespace Keycloak.AuthServices.IntegrationTests;

using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class KeycloakGroupClientTests(KeycloakFixture fixture, ITestOutputHelper testOutputHelper)
    : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.Master.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task GetGroupsAsync_RealmExists_Success()
    {
        var (services, _) = AdminHttpClientSetup(AppSettings, testOutputHelper, this.BaseAddress);

        #region GetGroupsAsync_RealmExists_Success

        var sp = services.BuildServiceProvider();

        var client = sp.GetRequiredService<IKeycloakGroupClient>();

        var groups = await client.GetGroupsAsync("Test");

        groups.Should().NotBeNull();
        #endregion GetGroupsAsync_RealmExists_Success
    }

    [Theory, AutoData]
    public async Task CreateGroupAsync_NewGroup_Success(string groupName)
    {
        var (services, _) = AdminHttpClientSetup(AppSettings, testOutputHelper, this.BaseAddress);

        #region CreateGroupAsync_NewGroup_Success

        var sp = services.BuildServiceProvider();

        var client = sp.GetRequiredService<IKeycloakGroupClient>();

        await client.CreateGroupAsync("Test", new() { Name = groupName });

        var groups = await client.GetGroupsAsync("Test", new() { Search = groupName });

        var group = await client.GetGroupAsync("Test", groups.First().Id!);

        group.Should().NotBeNull();
        #endregion CreateGroupAsync_NewGroup_Success
    }
}
