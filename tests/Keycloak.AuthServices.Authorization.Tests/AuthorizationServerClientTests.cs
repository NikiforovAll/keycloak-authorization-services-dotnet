namespace Keycloak.AuthServices.Authorization.Tests;

using System.Net;
using System.Net.Http.Headers;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;

public class AuthorizationServerClientTests
{
    private const string TokenEndpoint = "protocol/openid-connect/token";
    private const string DecisionResponse = """{"result": true}""";

    [Fact]
    public async Task VerifyAccessToResource_WithExplicitToken_SetsAuthorizationHeader()
    {
        AuthenticationHeaderValue? capturedAuth = null;

        var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .When(HttpMethod.Post, $"*/{TokenEndpoint}")
            .With(req =>
            {
                capturedAuth = req.Headers.Authorization;
                return true;
            })
            .Respond(HttpStatusCode.OK, "application/json", DecisionResponse);

        var client = CreateClient(mockHttp);

        var result = await client.VerifyAccessToResource("workspace", "read", "my-fresh-token");

        result.Should().BeTrue();
        capturedAuth.Should().NotBeNull();
        capturedAuth!.Scheme.Should().Be("Bearer");
        capturedAuth.Parameter.Should().Be("my-fresh-token");
    }

    [Fact]
    public async Task VerifyAccessToResource_WithExplicitToken_UsesConfiguredScheme()
    {
        AuthenticationHeaderValue? capturedAuth = null;

        var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .When(HttpMethod.Post, $"*/{TokenEndpoint}")
            .With(req =>
            {
                capturedAuth = req.Headers.Authorization;
                return true;
            })
            .Respond(HttpStatusCode.OK, "application/json", DecisionResponse);

        var options = new KeycloakAuthorizationServerOptions
        {
            Resource = "test-client",
            SourceAuthenticationScheme = "DPoP",
        };
        var client = CreateClient(mockHttp, options);

        await client.VerifyAccessToResource("workspace", "read", "my-token");

        capturedAuth!.Scheme.Should().Be("DPoP");
    }

    [Fact]
    public async Task VerifyAccessToResource_WithExplicitToken_SendsUmaRequest()
    {
        string? capturedBody = null;

        var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .When(HttpMethod.Post, $"*/{TokenEndpoint}")
            .With(req =>
            {
                capturedBody = req.Content!.ReadAsStringAsync().GetAwaiter().GetResult();
                return true;
            })
            .Respond(HttpStatusCode.OK, "application/json", DecisionResponse);

        var client = CreateClient(mockHttp);

        await client.VerifyAccessToResource("workspace", "read", "token");

        capturedBody.Should().Contain("grant_type=urn");
        capturedBody.Should().Contain("permission=workspace%23read");
        capturedBody.Should().Contain("audience=test-client");
    }

    [Fact]
    public async Task VerifyAccessToResource_WithExplicitToken_AccessDenied_ReturnsFalse()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .When(HttpMethod.Post, $"*/{TokenEndpoint}")
            .Respond(
                HttpStatusCode.Forbidden,
                "application/json",
                """{"error": "access_denied"}"""
            );

        var client = CreateClient(mockHttp);

        var result = await client.VerifyAccessToResource("workspace", "write", "token");

        result.Should().BeFalse();
    }

    [Fact]
    public async Task VerifyAccessToResource_WithoutToken_Works()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .When(HttpMethod.Post, $"*/{TokenEndpoint}")
            .Respond(HttpStatusCode.OK, "application/json", DecisionResponse);

        var client = CreateClient(mockHttp);

        var result = await client.VerifyAccessToResource("workspace", "read");

        result.Should().BeTrue();
    }

    [Fact]
    public async Task VerifyAccessToResource_WithExplicitToken_NullToken_Throws()
    {
        var mockHttp = new MockHttpMessageHandler();
        var client = CreateClient(mockHttp);

        var act = () => client.VerifyAccessToResource("workspace", "read", (string)null!);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task VerifyAccessToResource_WithExplicitToken_EmptyOrWhitespace_Throws(
        string token
    )
    {
        var mockHttp = new MockHttpMessageHandler();
        var client = CreateClient(mockHttp);

        var act = () => client.VerifyAccessToResource("workspace", "read", token);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task VerifyAccessToResource_WithAudienceOverride_UsesAudienceInsteadOfConfig()
    {
        string? capturedBody = null;

        var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .When(HttpMethod.Post, $"*/{TokenEndpoint}")
            .With(req =>
            {
                capturedBody = req.Content!.ReadAsStringAsync().GetAwaiter().GetResult();
                return true;
            })
            .Respond(HttpStatusCode.OK, "application/json", DecisionResponse);

        var client = CreateClient(mockHttp);

        var result = await client.VerifyAccessToResource(
            "invoice",
            "read",
            scopesValidationMode: null,
            audience: "billing-api"
        );

        result.Should().BeTrue();
        capturedBody.Should().Contain("audience=billing-api");
        capturedBody.Should().NotContain("audience=test-client");
    }

    [Fact]
    public async Task VerifyAccessToResource_NullAudience_FallsBackToConfigResource()
    {
        string? capturedBody = null;

        var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .When(HttpMethod.Post, $"*/{TokenEndpoint}")
            .With(req =>
            {
                capturedBody = req.Content!.ReadAsStringAsync().GetAwaiter().GetResult();
                return true;
            })
            .Respond(HttpStatusCode.OK, "application/json", DecisionResponse);

        var client = CreateClient(mockHttp);

        await client.VerifyAccessToResource(
            "invoice",
            "read",
            scopesValidationMode: null,
            audience: null
        );

        capturedBody.Should().Contain("audience=test-client");
    }

    private static AuthorizationServerClient CreateClient(
        MockHttpMessageHandler mockHttp,
        KeycloakAuthorizationServerOptions? options = null
    )
    {
        options ??= new KeycloakAuthorizationServerOptions { Resource = "test-client" };

        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri("https://keycloak.example.com/realms/test/");

        return new AuthorizationServerClient(
            httpClient,
            Options.Create(options),
            NullLogger<AuthorizationServerClient>.Instance
        );
    }
}
