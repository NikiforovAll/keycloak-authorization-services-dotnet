namespace Keycloak.AuthServices.Sdk.Tests.Admin;

using System.Net;
using Refit;
using RichardSzalay.MockHttp;
using Sdk.Admin;

public class KeycloakRealmClientTests : IDisposable
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
        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/master").Respond(HttpStatusCode.OK);

        await this.keycloakRealmClient.GetRealm("master");

        this.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetRealmShouldThrowNotFoundApiExceptionWhenRealmDoesNotExist()
    {
        this.handler.Expect(HttpMethod.Get, $"{BaseAddress}/admin/realms/nonexistent")
            .Respond(HttpStatusCode.NotFound);

        var exception = await Assert.ThrowsAsync<ApiException>(
            () => this.keycloakRealmClient.GetRealm("nonexistent"));

        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        this.handler.VerifyNoOutstandingExpectation();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.handler.Dispose();
        }
    }
}
