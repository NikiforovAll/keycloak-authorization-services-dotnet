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
    public async Task GetOrganizationsAsync_ByDefault_ReturnsOrganizations()
    {
        var orgs = GetOrganizationRepresentations(3);
        var response = $"[{string.Join(",", orgs.Select(o => JsonSerializer.Serialize(o)))})]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationsAsync("master");

        result.Select(o => o.Id).Should().BeEquivalentTo(orgs.Select(o => o.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationsAsync_WithSearch_PassesQueryString()
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
    public async Task GetOrganizationCountAsync_ReturnsCount()
    {
        const int orgCount = 7;
        var response = orgCount.ToString();

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/count")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationCountAsync("master");

        result.Should().Be(orgCount);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationAsync_ByDefault_ReturnsOrganization()
    {
        var orgId = Guid.NewGuid();
        var org = GetOrganizationRepresentation(orgId);
        var orgJson = JsonSerializer.Serialize(org);

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/{orgId}")
            .Respond(HttpStatusCode.OK, MediaType, orgJson);

        var result = await this.keycloakOrganizationClient.GetOrganizationAsync("master", orgId.ToString());

        result.Id.Should().Be(orgId.ToString());
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationAsync_NotFound_ThrowsKeycloakHttpClientException()
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
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateOrganizationAsync_ByDefault_PostsRepresentation()
    {
        var org = GetOrganizationRepresentation(Guid.NewGuid());
        var orgJson = JsonSerializer.Serialize(org);

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/organizations")
            .Respond(HttpStatusCode.Created, MediaType, orgJson);

        await this.keycloakOrganizationClient.CreateOrganizationAsync("master", org);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateOrganizationAsync_ByDefault_PutsRepresentation()
    {
        var orgId = Guid.NewGuid();
        var org = GetOrganizationRepresentation(orgId);
        var orgJson = JsonSerializer.Serialize(org);

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/organizations/{orgId}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakOrganizationClient.UpdateOrganizationAsync("master", orgId.ToString(), org);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteOrganizationAsync_ByDefault_DeletesResource()
    {
        var orgId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Delete, $"/admin/realms/master/organizations/{orgId}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakOrganizationClient.DeleteOrganizationAsync("master", orgId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMembersAsync_ByDefault_ReturnsMembers()
    {
        var orgId = Guid.NewGuid();
        var members = GetOrganizationMemberRepresentations(3);
        var response = $"[{string.Join(",", members.Select(m => JsonSerializer.Serialize(m)))})]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/{orgId}/members")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationMembersAsync("master", orgId.ToString());

        result.Select(m => m.Id).Should().BeEquivalentTo(members.Select(m => m.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMembersAsync_WithParameters_PassesQueryString()
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
    public async Task GetOrganizationMemberCountAsync_ReturnsCount()
    {
        const int memberCount = 15;
        var response = memberCount.ToString();

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/org1/members/count")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationMemberCountAsync("master", "org1");

        result.Should().Be(memberCount);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMemberAsync_ByDefault_ReturnsMember()
    {
        var orgId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var member = GetOrganizationMemberRepresentation(memberId);
        var memberJson = JsonSerializer.Serialize(member);

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/{orgId}/members/{memberId}")
            .Respond(HttpStatusCode.OK, MediaType, memberJson);

        var result = await this.keycloakOrganizationClient.GetOrganizationMemberAsync("master", orgId.ToString(), memberId.ToString());

        result.Id.Should().Be(memberId.ToString());
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task AddOrganizationMemberAsync_ByDefault_PostsUserIdAsJsonString()
    {
        const string realm = "master";
        const string orgId = "org-1";
        const string userId = "user-1";

        this.handler
            .Expect(HttpMethod.Post, $"/admin/realms/{realm}/organizations/{orgId}/members")
            .WithRequestBody($"\"{userId}\"")
            .RespondWith(new MockHttpResponse { StatusCode = HttpStatusCode.NoContent });

        await this.keycloakOrganizationClient.AddOrganizationMemberAsync(realm, orgId, userId);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task RemoveOrganizationMemberAsync_ByDefault_DeletesResource()
    {
        var orgId = Guid.NewGuid();
        var memberId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Delete, $"/admin/realms/master/organizations/{orgId}/members/{memberId}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakOrganizationClient.RemoveOrganizationMemberAsync("master", orgId.ToString(), memberId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetOrganizationMemberGroupsAsync_ByDefault_ReturnsGroups()
    {
        var orgId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var groups = GetGroupRepresentations(2);
        var response = $"[{string.Join(",", groups.Select(g => JsonSerializer.Serialize(g)))})]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/{orgId}/members/{memberId}/groups")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetOrganizationMemberGroupsAsync("master", orgId.ToString(), memberId.ToString());

        result.Select(g => g.Id).Should().BeEquivalentTo(groups.Select(g => g.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUserOrganizationsAsync_ByDefault_ReturnsOrganizations()
    {
        var memberId = Guid.NewGuid();
        var orgs = GetOrganizationRepresentations(2);
        var response = $"[{string.Join(",", orgs.Select(o => JsonSerializer.Serialize(o)))})]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/organizations/members/{memberId}/organizations")
            .Respond(HttpStatusCode.OK, MediaType, response);

        var result = await this.keycloakOrganizationClient.GetUserOrganizationsAsync("master", memberId.ToString());

        result.Select(o => o.Id).Should().BeEquivalentTo(orgs.Select(o => o.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    private static OrganizationRepresentation GetOrganizationRepresentation(Guid id) => new()
    {
        Id = id.ToString(),
        Name = $"Org-{id}",
        Alias = $"org-{id}",
        Enabled = true,
        Description = $"Description for org-{id}",
        RedirectUrl = $"https://org-{id}.example.com",
        Attributes = null,
        Domains = null
    };

    private static IEnumerable<OrganizationRepresentation> GetOrganizationRepresentations(int count) => Enumerable
        .Range(0, count)
        .Select(i => GetOrganizationRepresentation(Guid.NewGuid()));

    private static OrganizationMemberRepresentation GetOrganizationMemberRepresentation(Guid id) => new()
    {
        Id = id.ToString(),
        Username = $"user-{id}",
        Email = $"user-{id}@example.com",
        FirstName = $"First-{id}",
        LastName = $"Last-{id}",
        Enabled = true,
        MembershipType = "UNMANAGED"
    };

    private static IEnumerable<OrganizationMemberRepresentation> GetOrganizationMemberRepresentations(int count) => Enumerable
        .Range(0, count)
        .Select(i => GetOrganizationMemberRepresentation(Guid.NewGuid()));

    private static GroupRepresentation GetGroupRepresentation(Guid id) => new()
    {
        Id = id.ToString(),
        Name = $"Group-{id}",
        Description = $"Description for group-{id}",
        RealmId = "master",
        ParentId = null,
        Path = $"/Group-{id}",
        Attributes = null,
        ClientRoles = null,
        UserRoles = null
    };

    private static IEnumerable<GroupRepresentation> GetGroupRepresentations(int count) => Enumerable
        .Range(0, count)
        .Select(i => GetGroupRepresentation(Guid.NewGuid()));
}