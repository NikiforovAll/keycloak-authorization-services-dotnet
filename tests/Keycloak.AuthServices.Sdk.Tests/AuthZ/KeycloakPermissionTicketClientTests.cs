namespace Keycloak.AuthServices.Sdk.Tests.AuthZ;

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Extensions;
using FluentAssertions;
using Keycloak.AuthServices.Sdk.Admin.Requests.Groups;
using Microsoft.AspNetCore.Http.Extensions;
using Refit;
using RichardSzalay.MockHttp;
using Sdk.Admin;
using Sdk.Admin.Models;
using Sdk.Admin.Models.PermissionTickets;
using Sdk.Admin.Requests.Permissions;
using Sdk.Admin.Requests.Policy;
using Sdk.AuthZ;

public class KeycloakPermissionTicketClientTests
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakPermissionTicketClient keycloakPermissionTicketClient;

    public KeycloakPermissionTicketClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakPermissionTicketClient = RestService.For<IKeycloakPermissionTicketClient>(httpClient,
            ServiceCollectionExtensions.GetKeycloakClientRefitSettings());
    }

    [Fact]
    public async Task GetPermissionTicketsShouldCallCorrectEndpoint()
    {
        var permissionTickets = Enumerable.Range(0, 3).Select(_ =>
        {
            var accessToken = Guid.NewGuid().ToString();
            var expiresIn = long.MaxValue;
            return (AccessToken: accessToken, Response: GetPermissionTicketResponse(accessToken, expiresIn));
        }).ToArray();

        var response = $"[{string.Join(",", permissionTickets.Select(u => u.Response))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/realms/master/authz/protection/permission/ticket")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakPermissionTicketClient.GetPermissionTickets("master");

        result.Select(u => u.AccessToken).Should().BeEquivalentTo(permissionTickets.Select(u => u.AccessToken));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetPoliciesShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var getPermissionTicketsRequestParameters = new GetPermissionTicketsRequestParameters
        {
            ResourceId = "my_resource_id",
            Owner = "my_owner",
            Requester = "my_requester",
            ReturnNames = true,
            Granted = true,
            ScopeId = "my_scope_id",
            First = 0,
            Max = 1
        };

        var url = $"{BaseAddress}/realms/master/authz/protection/permission/ticket";
        var queryBuilder = new QueryBuilder
        {
            {"scopeId", getPermissionTicketsRequestParameters.ScopeId },
            {"resourceId", getPermissionTicketsRequestParameters.ResourceId },
            {"owner", getPermissionTicketsRequestParameters.Owner },
            {"requester", getPermissionTicketsRequestParameters.Requester },
            {"granted", getPermissionTicketsRequestParameters.Granted.Value.ToString() },
            {"returnNames", getPermissionTicketsRequestParameters.ReturnNames.Value.ToString() },
            {"first", getPermissionTicketsRequestParameters.First.Value.ToString() },
            {"max", getPermissionTicketsRequestParameters.Max.Value.ToString() }
        };

        var response = $"[{GetPermissionTicketResponse(default!, default)}]";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        _ = await this.keycloakPermissionTicketClient.GetPermissionTickets("master", getPermissionTicketsRequestParameters);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreatePermissionTicketShouldCallCorrectEndpoint()
    {
        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/realms/master/authz/protection/permission/ticket")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.Created);

        _ = await this.keycloakPermissionTicketClient.CreatePermissionTicket("master", GetPermissionTicket());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreatePermissionTicketShouldReturnBadRequestWhenRequestIsInvalid()
    {
        const string errorMessage = "{\"errorMessage\":\"Requester is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/realms/master/authz/protection/permission/ticket")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var response =
            await this.keycloakPermissionTicketClient.CreatePermissionTicket("master",
                new PermissionTicket(default!, default!));

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(errorMessage);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdatePermissionTicketShouldCallCorrectEndpoint()
    {
        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/realms/master/authz/protection/permission/ticket")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.Created);

        await this.keycloakPermissionTicketClient.UpdatePermissionTicket("master", GetPermissionTicket());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdatePermissionTicketShouldThrowNotFoundApiExceptionWhenTicketDoesNotExist()
    {
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"Resource does not exist\"}";
        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/realms/master/authz/protection/permission/ticket")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(() =>
            this.keycloakPermissionTicketClient.UpdatePermissionTicket("master", GetPermissionTicket()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteGroupShouldCallCorrectEndpoint()
    {
        var ticketId = Guid.NewGuid();
        this.handler.Expect(HttpMethod.Delete, $"{BaseAddress}/realms/master/authz/protection/permission/ticket/{ticketId}")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.Created);

        await this.keycloakPermissionTicketClient.DeletePermissionTicket("master", ticketId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    private static string GetPermissionTicketResponse(string accessToken, long expiresIn) => $@"{{
        ""access_token"":""{accessToken}"",
        ""expires_in"":{expiresIn},
        ""refresh_expires_in"":{expiresIn},
        ""refresh_token"":""{accessToken}"",
        ""token_type"":""Bearer"",
        ""not-before-policy"":0,
        ""scope"":""openid profile""
    }}";

    private static PermissionTicket GetPermissionTicket() =>
        new(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
        {
            Groups = new[] { "my_group" },
            Claims = new Dictionary<string, List<string>>()
            {
                { "my_custom_claim_value_type", new() { "my_custom_claim_value" } }
            },
            ResourceScopes = new() { new ResourceScope { Name = "view" } },
            Granted = true,
            Roles = new[] { "my_role" },
            ScopeName = "my_scope_name",
            Scopes = new[] { "scope_one", "scope_two" }
        };
}
