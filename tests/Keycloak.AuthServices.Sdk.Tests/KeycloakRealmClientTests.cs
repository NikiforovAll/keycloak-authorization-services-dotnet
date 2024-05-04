namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;
using RichardSzalay.MockHttp;

public class KeycloakRealmClientTests
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakRealmClient keycloakRealmClient;

    public KeycloakRealmClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakRealmClient = new KeycloakClient(httpClient);
    }

    [Fact]
    public async Task GetRealmShouldCallRealmEndpoint()
    {
        this.handler.Expect(HttpMethod.Get, "/admin/realms/master")
            .Respond(HttpStatusCode.OK, "application/json", "{}");

        await this.keycloakRealmClient.GetRealmAsync("master");

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetRealmShouldThrowNotFoundApiExceptionWhenRealmDoesNotExist()
    {
        this.handler.Expect(HttpMethod.Get, "/admin/realms/nonexistent")
            .Respond(
                HttpStatusCode.NotFound,
                "application/json",
                /*lang=json,strict*/"{\"error\": \"Realm not found\"}"
            );

        var exception = await FluentActions
            .Invoking(() => this.keycloakRealmClient.GetRealmAsync("nonexistent"))
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        exception.And.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        this.handler.VerifyNoOutstandingExpectation();
    }
}
