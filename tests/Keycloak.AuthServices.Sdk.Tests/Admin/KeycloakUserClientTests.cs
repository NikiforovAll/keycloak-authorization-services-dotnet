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
    public async Task CreateUserShouldCallUserEndpoint()
    {
        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/users")
            .Respond(HttpStatusCode.Created);

        await this.keycloakUserClient.CreateUser("master", new User {Username = "email@example.com"});

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task CreateUserShouldThrowBadRequestApiExceptionWhenRequestIsInvalid()
    {
        const string errorMessage = "{\"errorMessage\":\"User name is missing\"}";

        this.handler.Expect(HttpMethod.Post, $"{BaseAddress}/admin/realms/master/users")
            .Respond(HttpStatusCode.BadRequest, "application/json", errorMessage);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakUserClient.CreateUser("master", new User()));

        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.Equal(errorMessage, exception.Content);

        this.handler.VerifyNoOutstandingExpectation();
    }
}
