namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using System.Text.Json;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Organizations;
using Keycloak.AuthServices.Sdk.Utils;
using RichardSzalay.MockHttp;

public class KeycloakOrganizationClientTests
{
    private const string BaseAddress = "http://localhost:8080";
    private const string MediaType = "application/json";
    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakOrganizationClient keycloakOrganizationClient;

    public KeycloakOrganizationClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakOrganizationClient = new KeycloakClient(httpClient);
    }

    [Fact]
    public async Task GetOrganizationsShouldReturnOrganizations()
    {
        var orgs = Enumerable
            .Range(0, 3)
            .Select(_ =>
            {
                var id = Guid.NewGuid();
                return (Id: id.ToString(), Representation: GetOrganizationRepresentation(id));
            })
            .ToArray();

        var response = $"[{string.Join(",", orgs.Select(o => o.Representation))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationsAsync("master");

        result.Select(o => o.Id).Should().BeEquivalentTo(orgs.Select(o => o.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationsShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var parameters = new GetOrganizationsRequestParameters
        {
            Search = "example.com",
            First = 0,
            Max = 10,
        };

        var queryBuilder = new QueryBuilder
        {
            { "search", "example.com" },
            { "first", "0" },
            { "max", "10" },
        };

        this.handler.Expect(HttpMethod.Get, $"/admin/realms/master/organizations{queryBuilder.ToQueryString()}")
            .Respond(HttpStatusCode.OK, MediaType, "[]");

        _ = await this.keycloakOrganizationClient.GetOrganizationsAsync("master", parameters);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationCountShouldCallCorrectEndpoint()
    {
        const int orgCount = 7;
#pragma warning disable CA1305 // use locale provider
        var response = orgCount.ToString();
#pragma warning restore CA1305 // use locale provider

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/count")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationCountAsync("master");

        result.Should().Be(orgCount);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationShouldCallCorrectEndpoint()
    {
        var orgId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Get, $"/admin/realms/master/organizations/{orgId}")
            .Respond(HttpStatusCode.OK, MediaType, GetOrganizationRepresentation(orgId));

        var result = await this.keycloakOrganizationClient.GetOrganizationAsync("master", orgId.ToString());

        result.Id.Should().Be(orgId.ToString());
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationShouldThrowNotFoundApiExceptionWhenOrganizationDoesNotExist()
    {
        var orgId = Guid.NewGuid().ToString();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"Organization not found\"}";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/{orgId}")
            .Respond(HttpStatusCode.NotFound, MediaType, errorMessage);

        var exception = await FluentActions
            .Invoking(() => this.keycloakOrganizationClient.GetOrganizationAsync("master", orgId))
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        exception.And.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        exception.And.Response?.Error.Should().Be("Organization not found");
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateOrganizationShouldCallCorrectEndpoint()
    {
        var orgId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/organizations")
            .Respond(HttpStatusCode.Created);

        await this.keycloakOrganizationClient.CreateOrganizationAsync("master", new() { Name = $"Org-{orgId}" });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateOrganizationShouldCallCorrectEndpoint()
    {
        var orgId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Put, $"/admin/realms/master/organizations/{orgId}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakOrganizationClient.UpdateOrganizationAsync("master", orgId.ToString(), new() { Name = $"Org-{orgId}" });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteOrganizationShouldCallCorrectEndpoint()
    {
        var orgId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Delete, $"/admin/realms/master/organizations/{orgId}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakOrganizationClient.DeleteOrganizationAsync("master", orgId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMembersShouldReturnMembers()
    {
        var orgId = Guid.NewGuid();
        var members = Enumerable
            .Range(0, 3)
            .Select(_ =>
            {
                var id = Guid.NewGuid();
                return (Id: id.ToString(), Representation: GetOrganizationMemberRepresentation(id));
            })
            .ToArray();

        var response = $"[{string.Join(",", members.Select(m => m.Representation))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/{orgId}/members")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationMembersAsync("master", orgId.ToString());

        result.Select(m => m.Id).Should().BeEquivalentTo(members.Select(m => m.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMembersShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var orgId = Guid.NewGuid();
        var parameters = new GetOrganizationMembersRequestParameters
        {
            MembershipType = "UNMANAGED",
            Search = "user",
            First = 0,
            Max = 20,
        };

        var queryBuilder = new QueryBuilder
        {
            { "membershipType", "UNMANAGED" },
            { "search", "user" },
            { "first", "0" },
            { "max", "20" },
        };

        this.handler.Expect(HttpMethod.Get, $"/admin/realms/master/organizations/{orgId}/members{queryBuilder.ToQueryString()}")
            .Respond(HttpStatusCode.OK, MediaType, "[]");

        _ = await this.keycloakOrganizationClient.GetOrganizationMembersAsync("master", orgId.ToString(), parameters);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMemberCountShouldCallCorrectEndpoint()
    {
        const int memberCount = 15;
#pragma warning disable CA1305 // use locale provider
        var response = memberCount.ToString();
#pragma warning restore CA1305 // use locale provider

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/org1/members/count")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationMemberCountAsync("master", "org1");

        result.Should().Be(memberCount);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMemberShouldCallCorrectEndpoint()
    {
        var orgId = Guid.NewGuid();
        var memberId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Get, $"/admin/realms/master/organizations/{orgId}/members/{memberId}")
            .Respond(HttpStatusCode.OK, MediaType, GetOrganizationMemberRepresentation(memberId));

        var result = await this.keycloakOrganizationClient.GetOrganizationMemberAsync("master", orgId.ToString(), memberId.ToString());

        result.Id.Should().Be(memberId.ToString());
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task AddOrganizationMemberShouldCallCorrectEndpoint()
    {
        const string realm = "master";
        const string orgId = "org-1";
        const string userId = "user-1";

        this.handler
            .Expect(HttpMethod.Post, $"/admin/realms/{realm}/organizations/{orgId}/members")
            .WithContent($"\"{userId}\"")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakOrganizationClient.AddOrganizationMemberAsync(realm, orgId, userId);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task RemoveOrganizationMemberShouldCallCorrectEndpoint()
    {
        var orgId = Guid.NewGuid();
        var memberId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Delete, $"/admin/realms/master/organizations/{orgId}/members/{memberId}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakOrganizationClient.RemoveOrganizationMemberAsync("master", orgId.ToString(), memberId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMemberGroupsShouldCallCorrectEndpoint()
    {
        var orgId = Guid.NewGuid();
        var memberId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Get, $"/admin/realms/master/organizations/{orgId}/members/{memberId}/groups")
            .Respond(
                HttpStatusCode.OK,
                MediaType,
                JsonSerializer.Serialize(Array.Empty<GroupRepresentation>())
            );

        var result = await this.keycloakOrganizationClient.GetOrganizationMemberGroupsAsync("master", orgId.ToString(), memberId.ToString());

        result.Should().BeEmpty();
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUserOrganizationsShouldCallCorrectEndpoint()
    {
        var memberId = Guid.NewGuid();
        var orgs = Enumerable
            .Range(0, 2)
            .Select(_ =>
            {
                var id = Guid.NewGuid();
                return (Id: id.ToString(), Representation: GetOrganizationRepresentation(id));
            })
            .ToArray();

        var response = $"[{string.Join(",", orgs.Select(o => o.Representation))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/members/{memberId}/organizations")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetUserOrganizationsAsync("master", memberId.ToString());

        result.Select(o => o.Id).Should().BeEquivalentTo(orgs.Select(o => o.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    private static string GetOrganizationRepresentation(Guid orgId) =>
        /*lang=json,strict*/"""
        {
            "id": "{orgId}",
            "name": "Org-{orgId}",
            "alias": "org-{orgId}",
            "enabled": true,
            "description": "Description for org-{orgId}",
            "redirectUrl": "https://org-{orgId}.example.com"
        }
        """.Replace("{orgId}", orgId.ToString());

    private static string GetOrganizationMemberRepresentation(Guid memberId) =>
        /*lang=json,strict*/"""
        {
            "id": "{memberId}",
            "username": "user-{memberId}",
            "email": "user-{memberId}@example.com",
            "firstName": "First-{memberId}",
            "lastName": "Last-{memberId}",
            "enabled": true,
            "membershipType": "UNMANAGED"
        }
        """.Replace("{memberId}", memberId.ToString());
}
