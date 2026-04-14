namespace Keycloak.AuthServices.Authorization.Tests;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

public class HttpContextAccessTokenProviderTests
{
    private const string TestToken = "test-access-token";

    [Fact]
    public async Task GetAccessTokenAsync_NullHttpContext_ReturnsNull()
    {
        var (provider, _) = CreateProvider(new KeycloakAuthorizationServerOptions(), null);

        var token = await provider.GetAccessTokenAsync();

        token.Should().BeNull();
    }

    [Fact]
    public async Task GetAccessTokenAsync_UsesSourceAuthenticationScheme()
    {
        var options = new KeycloakAuthorizationServerOptions
        {
            SourceAuthenticationScheme = "Bearer",
        };
        var (provider, _) = CreateProvider(
            options,
            BuildHttpContext("Bearer", "access_token", TestToken)
        );

        var token = await provider.GetAccessTokenAsync();

        token.Should().Be(TestToken);
    }

    [Fact]
    public async Task GetAccessTokenAsync_WrongScheme_ReturnsNull()
    {
        var options = new KeycloakAuthorizationServerOptions
        {
            SourceAuthenticationScheme = "Bearer",
        };
        // Token stored under "Cookies" but provider looks under "Bearer"
        var (provider, _) = CreateProvider(
            options,
            BuildHttpContext("Cookies", "access_token", TestToken)
        );

        var token = await provider.GetAccessTokenAsync();

        token.Should().BeNull();
    }

    [Fact]
    public async Task GetAccessTokenAsync_CustomSourceAuthenticationScheme_UsesIt()
    {
        var options = new KeycloakAuthorizationServerOptions
        {
            SourceAuthenticationScheme = "CustomScheme",
        };
        var (provider, _) = CreateProvider(
            options,
            BuildHttpContext("CustomScheme", "access_token", TestToken)
        );

        var token = await provider.GetAccessTokenAsync();

        token.Should().Be(TestToken);
    }

    private static (HttpContextAccessTokenProvider, IHttpContextAccessor) CreateProvider(
        KeycloakAuthorizationServerOptions options,
        HttpContext? httpContext
    )
    {
        var accessor = new FakeHttpContextAccessor(httpContext);
        var provider = new HttpContextAccessTokenProvider(
            accessor,
            Options.Create(options),
            NullLogger<HttpContextAccessTokenProvider>.Instance
        );
        return (provider, accessor);
    }

    private static HttpContext BuildHttpContext(string scheme, string tokenName, string token)
    {
        var props = new AuthenticationProperties();
        props.StoreTokens([new AuthenticationToken { Name = tokenName, Value = token }]);

        var authResult = AuthenticateResult.Success(
            new AuthenticationTicket(new System.Security.Claims.ClaimsPrincipal(), props, scheme)
        );

        var authService = new FakeAuthenticationService(scheme, authResult);

        var services = new ServiceCollection();
        services.AddSingleton<IAuthenticationService>(authService);
        services.AddSingleton<IAuthenticationSchemeProvider>(new FakeSchemeProvider(scheme));

        return new DefaultHttpContext { RequestServices = services.BuildServiceProvider() };
    }

    private sealed class FakeHttpContextAccessor(HttpContext? context) : IHttpContextAccessor
    {
        public HttpContext? HttpContext { get; set; } = context;
    }

    private sealed class FakeAuthenticationService(string expectedScheme, AuthenticateResult result)
        : IAuthenticationService
    {
        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme) =>
            Task.FromResult(scheme == expectedScheme ? result : AuthenticateResult.NoResult());

        public Task ChallengeAsync(
            HttpContext context,
            string? scheme,
            AuthenticationProperties? properties
        ) => Task.CompletedTask;

        public Task ForbidAsync(
            HttpContext context,
            string? scheme,
            AuthenticationProperties? properties
        ) => Task.CompletedTask;

        public Task SignInAsync(
            HttpContext context,
            string? scheme,
            System.Security.Claims.ClaimsPrincipal principal,
            AuthenticationProperties? properties
        ) => Task.CompletedTask;

        public Task SignOutAsync(
            HttpContext context,
            string? scheme,
            AuthenticationProperties? properties
        ) => Task.CompletedTask;
    }

    private sealed class FakeSchemeProvider(string defaultScheme) : IAuthenticationSchemeProvider
    {
        public void AddScheme(AuthenticationScheme scheme) { }

        public Task<IEnumerable<AuthenticationScheme>> GetAllSchemesAsync() =>
            Task.FromResult<IEnumerable<AuthenticationScheme>>([]);

        public Task<AuthenticationScheme?> GetDefaultAuthenticateSchemeAsync() =>
            Task.FromResult<AuthenticationScheme?>(
                new AuthenticationScheme(defaultScheme, null, typeof(FakeHandler))
            );

        public Task<AuthenticationScheme?> GetDefaultChallengeSchemeAsync() =>
            Task.FromResult<AuthenticationScheme?>(null);

        public Task<AuthenticationScheme?> GetDefaultForbidSchemeAsync() =>
            Task.FromResult<AuthenticationScheme?>(null);

        public Task<AuthenticationScheme?> GetDefaultSignInSchemeAsync() =>
            Task.FromResult<AuthenticationScheme?>(null);

        public Task<AuthenticationScheme?> GetDefaultSignOutSchemeAsync() =>
            Task.FromResult<AuthenticationScheme?>(null);

        public Task<IEnumerable<AuthenticationScheme>> GetRequestHandlerSchemesAsync() =>
            Task.FromResult<IEnumerable<AuthenticationScheme>>([]);

        public Task<AuthenticationScheme?> GetSchemeAsync(string name) =>
            Task.FromResult<AuthenticationScheme?>(
                new AuthenticationScheme(name, null, typeof(FakeHandler))
            );

        public void RemoveScheme(string name) { }
    }

    private sealed class FakeHandler : IAuthenticationHandler
    {
        public Task<AuthenticateResult> AuthenticateAsync() =>
            Task.FromResult(AuthenticateResult.NoResult());

        public Task ChallengeAsync(AuthenticationProperties? properties) => Task.CompletedTask;

        public Task ForbidAsync(AuthenticationProperties? properties) => Task.CompletedTask;

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context) =>
            Task.CompletedTask;
    }
}
