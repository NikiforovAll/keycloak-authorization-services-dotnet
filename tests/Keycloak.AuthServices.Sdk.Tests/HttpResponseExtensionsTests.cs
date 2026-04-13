namespace Keycloak.AuthServices.Sdk.Tests;

using System.Net;
using System.Net.Http;
using FluentAssertions;
using Keycloak.AuthServices.Sdk;
using Xunit;

public class HttpResponseExtensionsTests
{
    [Fact]
    public async Task EnsureResponseAsyncShouldThrowKeycloakExceptionForJsonErrorBody()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
        {
            Content = new StringContent(
                """{"error": "unauthorized_client", "error_description": "Invalid credentials"}""",
                System.Text.Encoding.UTF8,
                "application/json"
            ),
        };

        var act = () => response.EnsureResponseAsync();

        var exception = await act.Should().ThrowAsync<KeycloakHttpClientException>();
        exception.And.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        exception.And.Response.Error.Should().Be("unauthorized_client");
    }

    [Fact]
    public async Task EnsureResponseAsyncShouldThrowKeycloakExceptionForHtmlErrorBody()
    {
        var htmlBody = "<html><body><h1>502 Bad Gateway</h1></body></html>";

        using var response = new HttpResponseMessage(HttpStatusCode.BadGateway)
        {
            Content = new StringContent(htmlBody, System.Text.Encoding.UTF8, "text/html"),
        };

        // Should throw KeycloakHttpClientException, NOT JsonException
        var act = () => response.EnsureResponseAsync();

        var exception = await act.Should().ThrowAsync<KeycloakHttpClientException>();
        exception.And.StatusCode.Should().Be((int)HttpStatusCode.BadGateway);
        exception.And.Response.ErrorDescription.Should().Be(htmlBody);
        exception.And.Response.Error.Should().Be(HttpStatusCode.BadGateway.ToString());
    }

    [Fact]
    public async Task EnsureResponseAsyncShouldThrowKeycloakExceptionForEmptyBody()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
        {
            Content = new StringContent(string.Empty),
        };

        var act = () => response.EnsureResponseAsync();

        var exception = await act.Should().ThrowAsync<KeycloakHttpClientException>();
        exception.And.StatusCode.Should().Be((int)HttpStatusCode.ServiceUnavailable);
        exception.And.Response.Error.Should().Be("Something went wrong");
    }

    [Fact]
    public async Task EnsureResponseAsyncShouldNotThrowForSuccessfulResponse()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("""{"id": "realm-id"}"""),
        };

        var act = () => response.EnsureResponseAsync();

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task EnsureResponseAsyncShouldThrowKeycloakExceptionForWhitespaceBody()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("   "),
        };

        var act = () => response.EnsureResponseAsync();

        var exception = await act.Should().ThrowAsync<KeycloakHttpClientException>();
        exception.And.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        exception.And.Response.Error.Should().Be("Something went wrong");
    }
}
