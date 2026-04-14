namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using System.Text.Json;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;
using Keycloak.AuthServices.Sdk.Utils;
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

    [Fact]
    public async Task GetPermissionTicketsWithParametersShouldBuildCorrectQueryString()
    {
        var parameters = new GetPermissionTicketsRequestParameters
        {
            Granted = false,
            ReturnNames = true,
            Requester = "bob",
        };

        var url = $"/realms/{CurrentRealm}/authz/protection/permission/ticket";
        var queryBuilder = new QueryBuilder
        {
            { "granted", "false" },
            { "requester", "bob" },
            { "returnNames", "true" },
        };

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .Respond(HttpStatusCode.OK, "application/json", "[]");

        _ = await this.keycloakPermissionClient.GetPermissionTicketsAsync(CurrentRealm, parameters);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetPermissionTicketsShouldDeserializeNameFields()
    {
        var responseBody = /*lang=json,strict*/
            """
            [
                {
                    "id": "ticket-1",
                    "owner": "owner-id",
                    "resource": "res-id",
                    "scope": "scope-id",
                    "granted": false,
                    "requester": "req-id",
                    "scopeName": "read",
                    "resourceName": "shared-document",
                    "ownerName": "alice",
                    "requesterName": "bob"
                }
            ]
            """;

        this.handler.Expect(
                HttpMethod.Get,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission/ticket"
            )
            .Respond(HttpStatusCode.OK, "application/json", responseBody);

        var result = await this.keycloakPermissionClient.GetPermissionTicketsAsync(CurrentRealm);

        result.Should().HaveCount(1);
        result[0].ScopeName.Should().Be("read");
        result[0].ResourceName.Should().Be("shared-document");
        result[0].OwnerName.Should().Be("alice");
        result[0].RequesterName.Should().Be("bob");
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdatePermissionTicketShouldCallCorrectEndpoint()
    {
        this.handler.Expect(
                HttpMethod.Put,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission/ticket"
            )
            .Respond(HttpStatusCode.NoContent);

        var ticket = new PermissionTicket { Id = "ticket-1", Granted = true };

        await this.keycloakPermissionClient.UpdatePermissionTicketAsync(CurrentRealm, ticket);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdatePermissionTicketWithResponseShouldReturnHttpResponse()
    {
        this.handler.Expect(
                HttpMethod.Put,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission/ticket"
            )
            .Respond(HttpStatusCode.NoContent);

        var ticket = new PermissionTicket { Id = "ticket-1", Granted = true };

        var response = await this.keycloakPermissionClient.UpdatePermissionTicketWithResponseAsync(
            CurrentRealm,
            ticket
        );

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeletePermissionTicketShouldCallCorrectEndpoint()
    {
        var ticketId = "ticket-to-delete";

        this.handler.Expect(
                HttpMethod.Delete,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission/ticket/{ticketId}"
            )
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakPermissionClient.DeletePermissionTicketAsync(CurrentRealm, ticketId);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeletePermissionTicketWithResponseShouldReturnHttpResponse()
    {
        var ticketId = "ticket-to-delete";

        this.handler.Expect(
                HttpMethod.Delete,
                $"{BaseAddress}/realms/{CurrentRealm}/authz/protection/permission/ticket/{ticketId}"
            )
            .Respond(HttpStatusCode.NoContent);

        var response = await this.keycloakPermissionClient.DeletePermissionTicketWithResponseAsync(
            CurrentRealm,
            ticketId
        );

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        this.handler.VerifyNoOutstandingExpectation();
    }
}
