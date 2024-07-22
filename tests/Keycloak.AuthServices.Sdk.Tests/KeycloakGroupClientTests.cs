namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using Admin;
using RichardSzalay.MockHttp;

public class KeycloakGroupClientTests
{
    private const string BaseAddress = "http://localhost:8080";
    private const string MediaType = "application/json";
    private readonly MockHttpMessageHandler handler = new();
    private readonly IKeycloakGroupClient keycloakGroupClient;

    public KeycloakGroupClientTests()
    {
        var httpClient = handler.ToHttpClient();
        httpClient.BaseAddress = new Uri(BaseAddress);

        keycloakGroupClient = new KeycloakClient(httpClient);
    }

    [Fact]
    public async Task DeleteGroupShouldCallCorrectEndpoint()
    {
        var groupId = Guid.NewGuid();

        handler.Expect(HttpMethod.Delete, $"/admin/realms/master/groups/{groupId}")
            .Respond(HttpStatusCode.NoContent);

        await keycloakGroupClient.DeleteGroupAsync("master", groupId.ToString());

        handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task DeleteGroupShouldThrowNotFoundApiExceptionWhenGroupDoesNotExist()
    {
        var groupId = Guid.NewGuid().ToString();
        const string errorMessage = /*lang=json,strict*/
            "{\"errorMessage\":\"Group name is missing\"}";

        handler.Expect(HttpMethod.Delete, $"/admin/realms/master/groups/{groupId}")
            .Respond(HttpStatusCode.NotFound, MediaType, errorMessage);

        var exception = await FluentActions
            .Invoking(() => keycloakGroupClient.DeleteGroupAsync("master", groupId))
            .Should()
            .ThrowAsync<KeycloakHttpClientException>();

        exception.And.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        handler.VerifyNoOutstandingExpectation();
    }
}
