namespace Keycloak.AuthServices.Sdk.Tests.Admin;

using System.Net;
using Refit;
using RichardSzalay.MockHttp;
using Sdk.Admin;
using Sdk.Admin.Models;

public class KeycloakUserClientTests
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakUserClient keycloakUserClient;

    public KeycloakUserClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakUserClient = RestService.For<IKeycloakUserClient>(httpClient);
    }

    [Fact]
    public async Task CreateUserShouldCallCorrectEndpoint()
    {
        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/users")
            .Respond(HttpStatusCode.Created);

        await this.keycloakUserClient.CreateUser("master", new()
        {
            Username = "email@example.com"
        });

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateUserShouldThrowBadRequestApiExceptionWhenRequestIsInvalid()
    {
        const string errorMessage = /*lang=json,strict*/ "{\"errorMessage\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/users")
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakUserClient.CreateUser("master", new User()));

        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.Equal(errorMessage, exception.Content);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendVerifyEmailShouldCallCorrectEndpoint()
    {
        var userId = Guid.NewGuid();

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/users/{userId}/send-verify-email")
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

        var url = $"{BaseAddress}/admin/realms/master/users/{userId}/send-verify-email?client_id={clientId}&redirect_uri={redirectUri}";
        this.handler.Expect(HttpMethod.Put, url).Respond(HttpStatusCode.NoContent);

        await this.keycloakUserClient.SendVerifyEmail("master", userId.ToString(), clientId, redirectUri);

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendVerifyEmailShouldThrowNotFoundApiExceptionWhenUserDoesNotExist()
    {
        var userId = Guid.NewGuid();
        const string errorMessage = /*lang=json,strict*/ "{\"error\":\"User not found\"}";

        this.handler.Expect(HttpMethod.Put, $"{BaseAddress}/admin/realms/master/users/{userId}/send-verify-email")
            .Respond(HttpStatusCode.NotFound, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakUserClient.SendVerifyEmail("master", userId.ToString()));

        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        Assert.Equal(errorMessage, exception.Content);

        this.handler.VerifyNoOutstandingExpectation();
    }
}
