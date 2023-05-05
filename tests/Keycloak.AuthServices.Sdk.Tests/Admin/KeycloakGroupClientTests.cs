namespace Keycloak.AuthServices.Sdk.Tests.Admin;
using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Refit;
using RichardSzalay.MockHttp;
using Sdk.Admin;
using Sdk.Admin.Requests.Groups;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Sdk.Admin.Models.Groups;

public class KeycloakGroupClientTests
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakGroupClient keycloakGroupClient;

    public KeycloakGroupClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakGroupClient = RestService.For<IKeycloakGroupClient>(httpClient,
            ServiceCollectionExtensions.GetKeycloakClientRefitSettings());
    }

    [Fact]
    public async Task GetGroupShouldCallCorrectEndpoint()
    {
        var groupId = Guid.NewGuid();
        var groupRepresentation = GetGroupRepresentation(groupId);

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/groups/{groupId}")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", groupRepresentation);

        var group = await this.keycloakGroupClient.GetGroup("master", groupId.ToString());

        group.Id.Should().Be(groupId.ToString());
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetGroupShouldThrowNotFoundApiExceptionWhenGroupDoesNotExist()
    {
        var groupId = Guid.NewGuid();
        const string errorMessage = "{\"error\":\"Group not found\"}";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/groups/{groupId}")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakGroupClient.GetGroup("master", groupId.ToString()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetGroupsShouldCallCorrectEndpoint()
    {
        var groups = Enumerable.Range(0, 3).Select(_ =>
        {
            var id = Guid.NewGuid();
            return (Id: id.ToString(), Representation: GetGroupRepresentation(id));
        }).ToArray();

        var response = $"[{string.Join(",", groups.Select(u => u.Representation))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/groups")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakGroupClient.GetGroups("master");

        result.Select(u => u.Id).Should().BeEquivalentTo(groups.Select(u => u.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetGroupsShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var getGroupsRequestParameters = new GetGroupsRequestParameters
        {
            BriefRepresentation = false,
            First = 0,
            Max = 1,
            Search = "search",
        };

        var url = $"{BaseAddress}/admin/realms/master/groups";
        var queryBuilder = new QueryBuilder
        {
            {"briefRepresentation", "False"},
            {"first", "0"},
            {"max", "1"},
            {"search", "search"}
        };

        var response = $"[{GetGroupRepresentation(Guid.NewGuid())}]";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        _ = await this.keycloakGroupClient.GetGroups("master", getGroupsRequestParameters);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateGroupShouldCallCorrectEndpoint()
    {
        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/groups")
            .Respond(HttpStatusCode.Created);

        await this.keycloakGroupClient.CreateGroup("master", new()
        {
            Name = "GroupName"
        });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateGroupShouldReturnBadRequestWhenRequestIsInvalid()
    {
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"Group name is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/groups")
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var response = await this.keycloakGroupClient.CreateGroup("master", new Group());

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(errorMessage);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateGroupShouldCallCorrectEndpoint()
    {
        var groupId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/groups/{groupId}")
            .WithContent(/*lang=json,strict*/ "{\"name\":\"GroupName\"}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakGroupClient.UpdateGroup("master", groupId.ToString(), new()
        {
            Name = "GroupName"
        });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateGroupShouldThrowNotFoundApiExceptionWhenGroupDoesNotExist()
    {
        var groupId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"Group name is missing\"}";

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/groups/{groupId}")
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(() =>
            this.keycloakGroupClient.UpdateGroup("master", groupId.ToString(), new Group()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateChildGroupShouldCallCorrectEndpoint()
    {
        var groupId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/groups/{groupId}/children")
            .Respond(HttpStatusCode.Created);

        await this.keycloakGroupClient.CreateChildGroup("master", groupId.ToString(), new()
        {
            Name = "GroupName"
        });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteGroupShouldCallCorrectEndpoint()
    {
        var groupId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Delete, $"{BaseAddress}/admin/realms/master/groups/{groupId}")
            .Respond(HttpStatusCode.OK);

        await this.keycloakGroupClient.DeleteGroup("master", groupId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteGroupShouldThrowApiNotFoundExceptionWhenGroupDoesNotExist()
    {
        var groupId = Guid.NewGuid();
        const string errorMessage = "{\"error\":\"Group not found\"}";

        this.handler.Expect(HttpMethod.Delete, $"{BaseAddress}/admin/realms/master/groups/{groupId}")
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakGroupClient.DeleteGroup("master", groupId.ToString()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    private static string GetGroupRepresentation(Guid groupId)
    {
        var ticks = DateTime.UtcNow.Ticks;
        var topLevelGroupName = $"top-level-group-{ticks}";
        var secondLevelGroupName = $"second-level-group-{ticks}";
        var thirdLevelGroupName = $"third-level-group-{ticks}";

        return $@"
        {{
            ""id"": ""{groupId}"",
            ""name"": ""{topLevelGroupName}"",
            ""path"": ""/{topLevelGroupName}"",
            ""subGroups"": [{{
                ""id"": ""{Guid.NewGuid()}"",
                ""name"": ""{secondLevelGroupName}"",
                ""path"": ""/{topLevelGroupName}/{secondLevelGroupName}"",
                ""subGroups"": [{{
                    ""id"": ""{Guid.NewGuid()}"",
                    ""name"": ""{thirdLevelGroupName}"",
                    ""path"": ""/{topLevelGroupName}/{secondLevelGroupName}/{thirdLevelGroupName}"",
                    ""subGroups"": []
                }}]
            }}]
        }}";
    }
}
