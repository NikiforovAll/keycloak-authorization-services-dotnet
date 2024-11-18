namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using Admin;
using RichardSzalay.MockHttp;

public class KeycloakClientRelativeAddressTests
{
    private const string BaseAddress = "http://localhost:8080/identity/";
    private const string MediaType = "application/json";
    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakRealmClient keycloakRealmClient;

    public KeycloakClientRelativeAddressTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.keycloakRealmClient = new KeycloakClient(httpClient);
    }

    [Fact]
    public async Task GetRealmShouldCallRealmRelativeEndpoint()
    {
        this.handler.Expect(HttpMethod.Get, "/identity/admin/realms/master")
            .Respond(HttpStatusCode.OK, MediaType, "{}");

        await this.keycloakRealmClient.GetRealmAsync("master");

        this.handler.VerifyNoOutstandingExpectation();
    }
}
