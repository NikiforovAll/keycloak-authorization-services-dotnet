namespace Keycloak.AuthServices.Sdk.Tests;

using Admin;
using RichardSzalay.MockHttp;

public class KeycloakGroupClientTests : IDisposable
{
    private const string BaseAddress = "http://localhost:8080";
    private const string MediaType = "application/json";

    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakGroupClient groupClient;

    public KeycloakGroupClientTests()
    {
        var httpClient = this.handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        this.groupClient = new KeycloakClient(httpClient);
    }

    public void Dispose() => this.handler.Dispose();
}
