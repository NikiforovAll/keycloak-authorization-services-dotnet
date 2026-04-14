namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using System.Text.Json;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using RichardSzalay.MockHttp;

public class KeycloakPermissionClientTests
{
    private const string BaseAddress = "http://localhost:8080";
    private const string CurrentRealm = "master";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakPermissionClient keycloakPermissionClient;

    public KeycloakPermissionClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakPermissionClient = new KeycloakProtectionClient(httpClient);
    }

    [Fact]
    public async Task CreatePermissionTicketShouldCallCorrectEndpoint()
    {
        var ticketValue = Guid.NewGuid().ToString();
        var responseBody = JsonSerializer.Serialize(new { ticket = ticketValue });

        this.handler.Expect(
                HttpMethod.Post,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission"
            )
            .Respond(HttpStatusCode.OK, "application/json", responseBody);

        var permissions = new List<PermissionTicketRequest>
        {
            new()
            {
                ResourceId = "my-resource",
                ResourceScopes = new List<string> { "read", "write" },
            },
        };

        var result = await this.keycloakPermissionClient.CreatePermissionTicketAsync(
            CurrentRealm,
            permissions
        );

        result.Ticket.Should().Be(ticketValue);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreatePermissionTicketWithMultiplePermissionsShouldCallCorrectEndpoint()
    {
        var ticketValue = Guid.NewGuid().ToString();
        var responseBody = JsonSerializer.Serialize(new { ticket = ticketValue });

        this.handler.Expect(
                HttpMethod.Post,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission"
            )
            .Respond(HttpStatusCode.OK, "application/json", responseBody);

        var permissions = new List<PermissionTicketRequest>
        {
            new()
            {
                ResourceId = "resource-1",
                ResourceScopes = new List<string> { "read" },
            },
            new()
            {
                ResourceId = "resource-2",
                ResourceScopes = new List<string> { "write" },
            },
        };

        var result = await this.keycloakPermissionClient.CreatePermissionTicketAsync(
            CurrentRealm,
            permissions
        );

        result.Ticket.Should().Be(ticketValue);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetPermissionTicketsShouldCallCorrectEndpoint()
    {
        var responseBody = """
            [
                {"id": "ticket-1", "owner": "alice", "resource": "res-1", "scope": "read", "granted": true, "requester": "bob"},
                {"id": "ticket-2", "owner": "alice", "resource": "res-2", "scope": "write", "granted": false, "requester": "charlie"}
            ]
            """;

        this.handler.Expect(
                HttpMethod.Get,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission/ticket"
            )
            .Respond(HttpStatusCode.OK, "application/json", responseBody);

        var result = await this.keycloakPermissionClient.GetPermissionTicketsAsync(CurrentRealm);

        result.Should().HaveCount(2);
        result[0].Id.Should().Be("ticket-1");
        result[0].Owner.Should().Be("alice");
        result[0].Granted.Should().BeTrue();
        result[1].Id.Should().Be("ticket-2");
        result[1].Requester.Should().Be("charlie");
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreatePermissionTicketWithResponseShouldReturnHttpResponse()
    {
        this.handler.Expect(
                HttpMethod.Post,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission"
            )
            .Respond(HttpStatusCode.Created, "application/json", """{"ticket":"t1"}""");

        var permissions = new List<PermissionTicketRequest>
        {
            new()
            {
                ResourceId = "res",
                ResourceScopes = new List<string> { "read" },
            },
        };

        var response = await this.keycloakPermissionClient.CreatePermissionTicketWithResponseAsync(
            CurrentRealm,
            permissions
        );

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        this.handler.VerifyNoOutstandingExpectation();
    }
}
