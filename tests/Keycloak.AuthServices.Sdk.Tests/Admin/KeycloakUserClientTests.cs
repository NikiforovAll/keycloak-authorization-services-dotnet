namespace Keycloak.AuthServices.Sdk.Tests.Admin;

using System.Net;
using Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Extensions;
using Refit;
using RichardSzalay.MockHttp;
using Sdk.Admin;
using Sdk.Admin.Models;
using Sdk.Admin.Models.Users;
using Sdk.Admin.Requests.Users;

public class KeycloakUserClientTests
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakUserClient keycloakUserClient;

    public KeycloakUserClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakUserClient = RestService.For<IKeycloakUserClient>(httpClient,
            ServiceCollectionExtensions.GetKeycloakClientRefitSettings());
    }

    [Fact]
    public async Task GetUserShouldCallCorrectEndpoint()
    {
        var userId = Guid.NewGuid();
        var userRepresentation = GetUserRepresentation(userId);

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/users/{userId}")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", userRepresentation);

        var user = await this.keycloakUserClient.GetUser("master", userId.ToString());

        user.Id.Should().Be(userId.ToString());
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUserShouldShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"error\":\"User not found\"}";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/users/{userId}")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakUserClient.GetUser("master", userId.ToString()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUsersShouldCallCorrectEndpoint()
    {
        var users = Enumerable.Range(0, 3).Select(_ =>
        {
            var id = Guid.NewGuid();
            return (Id: id.ToString(), Representation: GetUserRepresentation(id));
        }).ToArray();

        var response = $"[{string.Join(",", users.Select(u => u.Representation))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/users")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        var result = await this.keycloakUserClient.GetUsers("master");

        result.Select(u => u.Id).Should().BeEquivalentTo(users.Select(u => u.Id));
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUsersShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var getUsersRequestParameters = new GetUsersRequestParameters
        {
            BriefRepresentation = false,
            Email = "email",
            EmailVerified = false,
            Enabled = false,
            Exact = false,
            First = 0,
            FirstName = "firstName",
            IdpAlias = "idpAlias",
            IdpUserId = "idpUserId",
            LastName = "lastName",
            Max = 1,
            Query = "key1:value2 key2:value2",
            Search = "search",
            Username = "username"
        };

        var url = $"{BaseAddress}/admin/realms/master/users";
        var queryBuilder = new QueryBuilder
        {
            {"briefRepresentation", "False"},
            {"email", "email"},
            {"emailVerified", "False"},
            {"enabled", "False"},
            {"exact", "False"},
            {"first", "0"},
            {"firstName", "firstName"},
            {"idpAlias", "idpAlias"},
            {"idpUserId", "idpUserId"},
            {"lastName", "lastName"},
            {"max", "1"},
            {"q", "key1:value2 key2:value2"},
            {"search", "search"},
            {"username", "username"}
        };

        var response = $"[{GetUserRepresentation(Guid.NewGuid())}]";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .WithAcceptHeader()
            .Respond(HttpStatusCode.OK, "application/json", response);

        _ = await this.keycloakUserClient.GetUsers("master", getUsersRequestParameters);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateUserShouldCallCorrectEndpoint()
    {
        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/users")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.Created);

        await this.keycloakUserClient.CreateUser("master", new()
        {
            Username = "email@example.com"
        });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateUserShouldReturnBadRequestWhenRequestIsInvalid()
    {
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/users")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var response = await this.keycloakUserClient.CreateUser("master", new User());

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(errorMessage);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateUserShouldCallCorrectEndpoint()
    {
        var userId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/users/{userId}")
            .WithAcceptAndContentTypeHeaders()
            .WithContent(/*lang=json,strict*/ "{\"firstName\":\"FirstName\"}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakUserClient.UpdateUser("master", userId.ToString(), new()
        {
            FirstName = "FirstName"
        });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateUserShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/users/{userId}")
            .WithAcceptAndContentTypeHeaders()
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(() =>
            this.keycloakUserClient.UpdateUser("master", userId.ToString(), new User()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendVerifyEmailShouldCallCorrectEndpoint()
    {
        var userId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/users/{userId}/send-verify-email")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakUserClient.SendVerifyEmail("master", userId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendVerifyEmailShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var userId = Guid.NewGuid();
        const string clientId = "client-id";
        const string redirectUri = "https://localhost:5001";

        var url = $"{BaseAddress}/admin/realms/master/users/{userId}/send-verify-email"
                  + $"?client_id={clientId}"
                  + $"&redirect_uri={redirectUri}";

        this.handler.Expect(HttpMethod.Put, url)
            .WithAcceptHeader()
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakUserClient.SendVerifyEmail("master", userId.ToString(), clientId, redirectUri);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendVerifyEmailShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"error\":\"User not found\"}";

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/users/{userId}/send-verify-email")
            .WithAcceptHeader()
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakUserClient.SendVerifyEmail("master", userId.ToString()));

        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        exception.Content.Should().Be(errorMessage);
        this.handler.VerifyNoOutstandingExpectation();
    }

    private static string GetUserRepresentation(Guid userId) =>
        /*lang=json,strict*/ $@"{{
            ""id"": ""{userId}"",
            ""createdTimestamp"": 1670000000000,
            ""username"": ""email@domain.com"",
            ""enabled"": true,
            ""totp"": false,
            ""emailVerified"": true,
            ""firstName"": ""Test"",
            ""lastName"": ""User"",
            ""email"": ""email@domain.com"",
            ""disableableCredentialTypes"": [],
            ""requiredActions"": [],
            ""notBefore"": 0,
            ""access"": {{
                ""manageGroupMembership"": true,
                ""view"": true,
                ""mapRoles"": true,
                ""impersonate"": true,
                ""manage"": true
            }}
        }}";
}
