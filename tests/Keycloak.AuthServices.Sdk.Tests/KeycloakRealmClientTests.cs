namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using RichardSzalay.MockHttp;
using Sdk.Admin;

#pragma warning disable CA1001 // Types that own disposable fields should be disposable
public class KeycloakRealmClientTests
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
{
    private const string BaseAddress = "http://localhost:8080";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakRealmClient keycloakRealmClient;

    public KeycloakRealmClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakRealmClient = RestService.For<IKeycloakRealmClient>(httpClient);
    }

    [Fact]
    public async Task GetRealmShouldCallRealmEndpoint()
    {
        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master")
            .Respond(HttpStatusCode.OK);

        await this.keycloakRealmClient.GetRealmAsync("master");

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetRealmShouldThrowNotFoundApiExceptionWhenRealmDoesNotExist()
    {
        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/nonexistent")
            .Respond(HttpStatusCode.NotFound);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakRealmClient.GetRealmAsync("nonexistent")
        );

        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        this.handler.VerifyNoOutstandingExpectation();
    }
}
