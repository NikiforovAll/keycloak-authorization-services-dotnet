namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using System.Text.Json;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Users;
using Keycloak.AuthServices.Sdk.Utils;
using RichardSzalay.MockHttp;

public class KeycloakUserClientTests
{
    private const string BaseAddress = "http://localhost:8080";
    private const string JsonMediaType = "application/json";
    private const string PlaintextMediaType = "text/plain";
    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakUserClient keycloakUserClient;

    public KeycloakUserClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakUserClient = new KeycloakClient(httpClient);
    }

    [Theory, AutoData]
    public async Task GetUserShouldCallCorrectEndpoint(UserRepresentation userFixture)
    {
        var userId = userFixture.Id;

        this.handler.Expect(HttpMethod.Get, $"/admin/realms/master/users/{userId}")
            .Respond(HttpStatusCode.OK, JsonMediaType, JsonSerializer.Serialize(userFixture));

        var user = await this.keycloakUserClient.GetUserAsync("master", userId!.ToString());

        user.Id.Should().Be(userId.ToString());
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUserShouldShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"User not found\"}";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/users/{userId}")
            .Respond(HttpStatusCode.NotFound, JsonMediaType, errorMessage);

        var exception = await FluentActions
            .Invoking(() => this.keycloakUserClient.GetUserAsync("master", userId.ToString()))
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        exception.And.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        exception.And.Response?.Error.Should().Be("User not found");

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUsersShouldCallCorrectEndpoint()
    {
        var users = Enumerable
            .Range(0, 3)
            .Select(_ =>
            {
                var id = Guid.NewGuid();
                return (Id: id.ToString(), Representation: GetUserRepresentation(id));
            })
            .ToArray();

        var response = $"[{string.Join(",", users.Select(u => u.Representation))}]";

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/users")
            .Respond(HttpStatusCode.OK, JsonMediaType, response);

        var result = await this.keycloakUserClient.GetUsersAsync("master");

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

        var url = $"/admin/realms/master/users";
        var queryBuilder = new QueryBuilder
        {
            { "briefRepresentation", "False" },
            { "email", "email" },
            { "emailVerified", "False" },
            { "enabled", "False" },
            { "exact", "False" },
            { "first", "0" },
            { "firstName", "firstName" },
            { "idpAlias", "idpAlias" },
            { "idpUserId", "idpUserId" },
            { "lastName", "lastName" },
            { "max", "1" },
            { "q", "key1:value2 key2:value2" },
            { "search", "search" },
            { "username", "username" }
        };

        var response = $"[{GetUserRepresentation(Guid.NewGuid())}]";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .Respond(HttpStatusCode.OK, JsonMediaType, response);

        _ = await this.keycloakUserClient.GetUsersAsync("master", getUsersRequestParameters);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUserCountShouldCallCorrectEndpoint()
    {
        const int userAmount = 5;

        for (var i = 0; i < userAmount; ++i)
        {
            var id = Guid.NewGuid();
            GetUserRepresentation(id);
        }

#pragma warning disable CA1305 // use locale provider
        var response = userAmount.ToString();
#pragma warning restore CA1305 // use locale provider

        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master/users/count")
            .Respond(HttpStatusCode.OK, PlaintextMediaType, response);

        var result = await this.keycloakUserClient.GetUserCountAsync("master");

        result.Should().Be(userAmount);
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUserCountShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var getUserCountRequestParameters = new GetUserCountRequestParameters
        {
            Email = "email",
            EmailVerified = false,
            Enabled = false,
            FirstName = "firstName",
            LastName = "lastName",
            Query = "query",
            Search = "search",
            Username = "username"
        };

        const string url = "/admin/realms/master/users/count";
        var queryBuilder = new QueryBuilder
        {
            { "email", "email" },
            { "emailVerified", "False" },
            { "enabled", "False" },
            { "firstName", "firstName" },
            { "lastName", "lastName" },
            { "q", "query" },
            { "search", "search" },
            { "username", "username" }
        };

        const string response = "0";

        this.handler.Expect(HttpMethod.Get, url + queryBuilder.ToQueryString())
            .Respond(HttpStatusCode.BadRequest, PlaintextMediaType, response);

        _ = await this.keycloakUserClient.GetUserCountAsync(
            "master",
            getUserCountRequestParameters
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateUserShouldCallCorrectEndpoint()
    {
        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/users")
            .Respond(HttpStatusCode.Created);

        await this.keycloakUserClient.CreateUserAsync(
            "master",
            new() { Username = "email@example.com" }
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateUserShouldReturnBadRequestWhenRequestIsInvalid()
    {
        const string errorMessage = /*lang=json,strict*/
            "{\"errorMessage\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"/admin/realms/master/users")
            .Respond(HttpStatusCode.BadRequest, JsonMediaType, errorMessage);

        var exception = await FluentActions
            .Invoking(
                () => this.keycloakUserClient.CreateUserAsync("master", new UserRepresentation())
            )
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        exception.And.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateUserShouldCallCorrectEndpoint()
    {
        var userId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Put, $"/admin/realms/master/users/{userId}")
            .WithContent( /*lang=json,strict*/
                "{\"firstName\":\"FirstName\"}"
            )
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakUserClient.UpdateUserAsync(
            "master",
            userId.ToString(),
            new() { FirstName = "FirstName" }
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task UpdateUserShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid().ToString();
        const string errorMessage = /*lang=json,strict*/
            "{\"errorMessage\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Put, $"/admin/realms/master/users/{userId}")
            .Respond(HttpStatusCode.NotFound, JsonMediaType, errorMessage);

        var exception = await FluentActions
            .Invoking(
                () =>
                    this.keycloakUserClient.UpdateUserAsync(
                        "master",
                        userId,
                        new UserRepresentation()
                    )
            )
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        exception.And.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteUserShouldCallCorrectEndpoint()
    {
        var userId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Delete, $"/admin/realms/master/users/{userId}")
            .Respond(HttpStatusCode.NoContent);

        await this.keycloakUserClient.DeleteUserAsync("master", userId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteUserShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid().ToString();
        const string errorMessage = /*lang=json,strict*/
            "{\"errorMessage\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Delete, $"/admin/realms/master/users/{userId}")
            .Respond(HttpStatusCode.NotFound, JsonMediaType, errorMessage);

        var exception = await FluentActions
            .Invoking(() => this.keycloakUserClient.DeleteUserAsync("master", userId))
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        exception.And.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendVerifyEmailShouldCallCorrectEndpoint()
    {
        var userId = Guid.NewGuid().ToString();

        this.handler.Expect(
                HttpMethod.Put,
                $"/admin/realms/master/users/{userId}/send-verify-email"
            )
            .Respond(HttpStatusCode.OK);

        await this.keycloakUserClient.SendVerifyEmailAsync("master", userId.ToString());

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendVerifyEmailShouldCallCorrectEndpointWithOptionalQueryParameters()
    {
        var userId = Guid.NewGuid();
        const string clientId = "client-id";
        const string redirectUri = "https://localhost:5001";

        var queryBuilder = new QueryBuilder
        {
            { "client_id", clientId },
            { "redirect_uri", redirectUri },
        };

        this.handler.Expect(
                HttpMethod.Put,
                $"/admin/realms/master/users/{userId}/send-verify-email{queryBuilder.ToQueryString()}"
            )
            .Respond(HttpStatusCode.OK);

        await this.keycloakUserClient.SendVerifyEmailAsync(
            "master",
            userId.ToString(),
            clientId,
            redirectUri
        );

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendVerifyEmailShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/
            "{\"error\":\"User not found\"}";

        this.handler.Expect(
                HttpMethod.Put,
                $"/admin/realms/master/users/{userId}/send-verify-email"
            )
            .Respond(HttpStatusCode.NotFound, JsonMediaType, errorMessage);

        var exception = await FluentActions
            .Invoking(
                () => this.keycloakUserClient.SendVerifyEmailAsync("master", userId.ToString())
            )
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        exception.And.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task ExecuteActionsEmailAsyncShouldCallCorrectEndpoint()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var actions = new List<string> { "UPDATE_PASSWORD" };
        var realm = "master";
        var expectedUrl =
            $"{BaseAddress}/admin/realms/{realm}/users/{userId}/execute-actions-email";

        var request = new ExecuteActionsEmailRequest
        {
            ClientId = "client-id",
            Lifespan = 1800,
            RedirectUri = "http://localhost:3000/callback",
            Actions = actions
        };

        this.handler.Expect(HttpMethod.Put, expectedUrl).Respond(HttpStatusCode.OK);

        // Act
        await this.keycloakUserClient.ExecuteActionsEmailAsync(realm, userId, request);

        // Assert
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetUserGroupsAsyncShouldCallCorrectEndpoint()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var realm = "master";
        var expectedUrl = $"/admin/realms/{realm}/users/{userId}/groups";

        this.handler.Expect(HttpMethod.Get, expectedUrl)
            .Respond(
                HttpStatusCode.OK,
                JsonMediaType,
                JsonSerializer.Serialize(Array.Empty<GroupRepresentation>())
            );

        // Act
        await this.keycloakUserClient.GetUserGroupsAsync(realm, userId, new());

        // Assert
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task JoinGroupAsyncShouldCallCorrectEndpoint()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var groupId = Guid.NewGuid().ToString();
        var realm = "master";
        var expectedUrl = $"/admin/realms/{realm}/users/{userId}/groups/{groupId}";

        this.handler.Expect(HttpMethod.Put, expectedUrl).Respond(HttpStatusCode.NoContent);

        // Act
        await this.keycloakUserClient.JoinGroupAsync(realm, userId, groupId);

        // Assert
        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task LeaveGroupAsyncShouldCallCorrectEndpoint()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var groupId = Guid.NewGuid().ToString();
        var realm = "master";
        var expectedUrl = $"/admin/realms/{realm}/users/{userId}/groups/{groupId}";

        this.handler.Expect(HttpMethod.Delete, expectedUrl).Respond(HttpStatusCode.NoContent);

        // Act
        await this.keycloakUserClient.LeaveGroupAsync(realm, userId, groupId);

        // Assert
        this.handler.VerifyNoOutstandingExpectation();
    }

    private static string GetUserRepresentation(Guid userId) =>
        /*lang=json,strict*/"""
            {
                "id": "{userId}",
                "createdTimestamp": 1670000000000,
                "username": "email@domain.com",
                "enabled": true,
                "totp": false,
                "emailVerified": true,
                "firstName": "Test",
                "lastName": "User",
                "email": "email@domain.com",
                "disableableCredentialTypes": [],
                "requiredActions": [],
                "notBefore": 0,
                "access": {
                    "manageGroupMembership": true,
                    "view": true,
                    "mapRoles": true,
                    "impersonate": true,
                    "manage": true
                }
            }
            """.Replace("{userId}", userId.ToString());
}
