namespace Keycloak.AuthServices.IntegrationTests;

using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Microsoft.Extensions.DependencyInjection;
using static Keycloak.AuthServices.IntegrationTests.Utils;

public class PermissionTicketTests(KeycloakFixture fixture, ITestOutputHelper testOutputHelper)
    : AuthenticationScenario(fixture)
{
    private static readonly string AppSettings = "appsettings.json";
    private string BaseAddress => fixture.BaseAddress;

    [Fact]
    public async Task CreatePermissionTicketAsync_UmaResource_ReturnsTicket()
    {
        var (services, _) = ProtectionHttpClientSetup(
            AppSettings,
            testOutputHelper,
            this.BaseAddress
        );

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<IKeycloakProtectionClient>();

        var permissions = new List<PermissionTicketRequest>
        {
            new() { ResourceId = "uma-document", ResourceScopes = ["read"] },
        };

        var result = await client.CreatePermissionTicketAsync("Test", permissions);

        result.Should().NotBeNull();
        result.Ticket.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task CreatePermissionTicketWithResponseAsync_UmaResource_ReturnsSuccess()
    {
        var (services, _) = ProtectionHttpClientSetup(
            AppSettings,
            testOutputHelper,
            this.BaseAddress
        );

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<IKeycloakProtectionClient>();

        var permissions = new List<PermissionTicketRequest>
        {
            new()
            {
                ResourceId = "uma-document",
                ResourceScopes = new List<string> { "read", "write" },
            },
        };

        var response = await client.CreatePermissionTicketWithResponseAsync("Test", permissions);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task CreateResourceWithUma_ThenCreatePermissionTicket_Success()
    {
        var (services, _) = ProtectionHttpClientSetup(
            AppSettings,
            testOutputHelper,
            this.BaseAddress
        );

        var sp = services.BuildServiceProvider();
        var client = sp.GetRequiredService<IKeycloakProtectionClient>();

        var resource = new Resource($"uma-test-{Guid.NewGuid():N}", ["read"])
        {
            Type = "urn:test-documents",
            DisplayName = "Dynamic UMA Resource",
            OwnerManagedAccess = true,
        };

        var created = await client.CreateResourceAsync("Test", resource);
        created.Should().NotBeNull();
        created.Id.Should().NotBeNullOrEmpty();

        try
        {
            var permissions = new List<PermissionTicketRequest>
            {
                new()
                {
                    ResourceId = created.Id!,
                    ResourceScopes = new List<string> { "read" },
                },
            };

            var ticket = await client.CreatePermissionTicketAsync("Test", permissions);
            ticket.Should().NotBeNull();
            ticket.Ticket.Should().NotBeNullOrEmpty();
        }
        finally
        {
            await client.DeleteResourceAsync("Test", created.Id!);
        }
    }
}
