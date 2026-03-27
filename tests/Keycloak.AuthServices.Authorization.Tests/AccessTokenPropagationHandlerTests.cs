namespace Keycloak.AuthServices.Authorization.Tests;

using System.Net;
using System.Net.Http.Headers;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

public class AccessTokenPropagationHandlerTests
{
    private const string TestToken = "test-access-token";

    [Fact]
    public async Task SendAsync_NoExistingAuthHeader_SetsTokenFromProvider()
    {
        var provider = new FakeTokenProvider(TestToken);
        var handler = CreateHandler(provider);

        var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com/api");
        var response = await handler.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        request.Headers.Authorization.Should().NotBeNull();
        request.Headers.Authorization!.Scheme.Should().Be("Bearer");
        request.Headers.Authorization.Parameter.Should().Be(TestToken);
    }

    [Fact]
    public async Task SendAsync_ExistingAuthHeader_SkipsProvider()
    {
        var provider = new FakeTokenProvider(TestToken);
        var handler = CreateHandler(provider);

        var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com/api");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "pre-set-token");

        var response = await handler.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        request.Headers.Authorization.Parameter.Should().Be("pre-set-token");
        provider.CallCount.Should().Be(0);
    }

    [Fact]
    public async Task SendAsync_ProviderReturnsNull_DoesNotSetHeader()
    {
        var provider = new FakeTokenProvider(null);
        var handler = CreateHandler(provider);

        var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com/api");
        var response = await handler.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        request.Headers.Authorization.Should().BeNull();
    }

    [Fact]
    public async Task SendAsync_ProviderReturnsEmpty_DoesNotSetHeader()
    {
        var provider = new FakeTokenProvider(string.Empty);
        var handler = CreateHandler(provider);

        var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com/api");
        var response = await handler.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        request.Headers.Authorization.Should().BeNull();
    }

    [Fact]
    public async Task SendAsync_UsesConfiguredAuthenticationScheme()
    {
        var provider = new FakeTokenProvider(TestToken);
        var options = new KeycloakAuthorizationServerOptions
        {
            SourceAuthenticationScheme = "Custom",
        };
        var handler = CreateHandler(provider, options);

        var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com/api");
        await handler.SendAsync(request);

        request.Headers.Authorization!.Scheme.Should().Be("Custom");
        request.Headers.Authorization.Parameter.Should().Be(TestToken);
    }

    private static TestableAccessTokenPropagationHandler CreateHandler(
        IKeycloakAccessTokenProvider provider,
        KeycloakAuthorizationServerOptions? options = null
    )
    {
        options ??= new KeycloakAuthorizationServerOptions();
        return new TestableAccessTokenPropagationHandler(
            provider,
            Options.Create(options),
            NullLogger<AccessTokenPropagationHandler>.Instance
        );
    }

    private sealed class FakeTokenProvider(string? token) : IKeycloakAccessTokenProvider
    {
        public int CallCount { get; private set; }

        public Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            this.CallCount++;
            return Task.FromResult(token);
        }
    }

    private sealed class TestableAccessTokenPropagationHandler(
        IKeycloakAccessTokenProvider tokenProvider,
        IOptions<KeycloakAuthorizationServerOptions> options,
        Microsoft.Extensions.Logging.ILogger<AccessTokenPropagationHandler> logger
    ) : AccessTokenPropagationHandler(tokenProvider, options, logger)
    {
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            this.InnerHandler = new FakeInnerHandler();
            return await base.SendAsync(request, CancellationToken.None);
        }
    }

    private sealed class FakeInnerHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        ) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
    }
}
