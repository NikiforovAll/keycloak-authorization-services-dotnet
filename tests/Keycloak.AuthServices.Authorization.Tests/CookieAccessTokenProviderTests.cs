namespace Keycloak.AuthServices.Authorization.Tests;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

public class CookieAccessTokenProviderTests
{
    private const string TestToken = "test-access-token";

    [Fact]
    public async Task GetAccessTokenAsync_NullHttpContext_ReturnsNull()
    {
        var provider = CreateProvider(null);

        var token = await provider.GetAccessTokenAsync();

        token.Should().BeNull();
    }

    [Fact]
    public async Task GetAccessTokenAsync_TokenStoredUnderCookieScheme_ReturnsToken()
    {
        var provider = CreateProvider(BuildHttpContext("Cookies", "access_token", TestToken));

        var token = await provider.GetAccessTokenAsync();

        token.Should().Be(TestToken);
    }

    [Fact]
    public async Task GetAccessTokenAsync_DefaultSchemeIsCookies()
    {
        // Default constructor uses CookieAuthenticationDefaults.AuthenticationScheme = "Cookies"
        var provider = CreateProvider(
            BuildHttpContext("Cookies", "access_token", TestToken),
            cookieScheme: "Cookies"
        );

        var token = await provider.GetAccessTokenAsync();

        token.Should().Be(TestToken);
    }

    [Fact]
    public async Task GetAccessTokenAsync_CustomCookieScheme_UsesCustomScheme()
    {
        var provider = CreateProvider(
            BuildHttpContext("MyCustomCookies", "access_token", TestToken),
            cookieScheme: "MyCustomCookies"
        );

        var token = await provider.GetAccessTokenAsync();

        token.Should().Be(TestToken);
    }

    [Fact]
    public async Task GetAccessTokenAsync_CustomTokenName_UsesCustomTokenName()
    {
        var provider = CreateProvider(
            BuildHttpContext("Cookies", "my_token", TestToken),
            tokenName: "my_token"
        );

        var token = await provider.GetAccessTokenAsync();

        token.Should().Be(TestToken);
    }

    [Fact]
    public async Task GetAccessTokenAsync_TokenNotStoredUnderScheme_ReturnsNull()
    {
        // Token is stored under "Bearer" but provider looks under "Cookies"
        var provider = CreateProvider(BuildHttpContext("Bearer", "access_token", TestToken));

        var token = await provider.GetAccessTokenAsync();

        token.Should().BeNull();
    }

    [Fact]
    public async Task GetAccessTokenAsync_DoesNotUseBearer_WhenCookieSchemeConfigured()
    {
        // Sanity: cookie provider must not fall back to Bearer scheme
        var provider = CreateProvider(
            BuildHttpContext("Bearer", "access_token", TestToken),
            cookieScheme: "Cookies"
        );

        var token = await provider.GetAccessTokenAsync();

        token.Should().BeNull();
    }

    private static CookieAccessTokenProvider CreateProvider(
        HttpContext? httpContext,
        string cookieScheme = "Cookies",
        string tokenName = "access_token"
    )
    {
        var accessor = new FakeHttpContextAccessor(httpContext);
        return new CookieAccessTokenProvider(
            accessor,
            NullLogger<CookieAccessTokenProvider>.Instance,
            cookieScheme,
            tokenName
        );
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

    private sealed class FakeAuthenticationService(
        string expectedScheme,
        AuthenticateResult result
    ) : IAuthenticationService
    {
        public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme) =>
            Task.FromResult(
                scheme == expectedScheme ? result : AuthenticateResult.NoResult()
            );

        public Task ChallengeAsync(HttpContext context, string? scheme, AuthenticationProperties? properties) => Task.CompletedTask;
        public Task ForbidAsync(HttpContext context, string? scheme, AuthenticationProperties? properties) => Task.CompletedTask;
        public Task SignInAsync(HttpContext context, string? scheme, System.Security.Claims.ClaimsPrincipal principal, AuthenticationProperties? properties) => Task.CompletedTask;
        public Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties) => Task.CompletedTask;
    }

    private sealed class FakeSchemeProvider(string defaultScheme) : IAuthenticationSchemeProvider
    {
        public void AddScheme(AuthenticationScheme scheme) { }
        public Task<IEnumerable<AuthenticationScheme>> GetAllSchemesAsync() => Task.FromResult<IEnumerable<AuthenticationScheme>>([]);
        public Task<AuthenticationScheme?> GetDefaultAuthenticateSchemeAsync() =>
            Task.FromResult<AuthenticationScheme?>(new AuthenticationScheme(defaultScheme, null, typeof(FakeHandler)));
        public Task<AuthenticationScheme?> GetDefaultChallengeSchemeAsync() => Task.FromResult<AuthenticationScheme?>(null);
        public Task<AuthenticationScheme?> GetDefaultForbidSchemeAsync() => Task.FromResult<AuthenticationScheme?>(null);
        public Task<AuthenticationScheme?> GetDefaultSignInSchemeAsync() => Task.FromResult<AuthenticationScheme?>(null);
        public Task<AuthenticationScheme?> GetDefaultSignOutSchemeAsync() => Task.FromResult<AuthenticationScheme?>(null);
        public Task<IEnumerable<AuthenticationScheme>> GetRequestHandlerSchemesAsync() => Task.FromResult<IEnumerable<AuthenticationScheme>>([]);
        public Task<AuthenticationScheme?> GetSchemeAsync(string name) =>
            Task.FromResult<AuthenticationScheme?>(new AuthenticationScheme(name, null, typeof(FakeHandler)));
        public void RemoveScheme(string name) { }
    }

    private sealed class FakeHandler : IAuthenticationHandler
    {
        public Task<AuthenticateResult> AuthenticateAsync() => Task.FromResult(AuthenticateResult.NoResult());
        public Task ChallengeAsync(AuthenticationProperties? properties) => Task.CompletedTask;
        public Task ForbidAsync(AuthenticationProperties? properties) => Task.CompletedTask;
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context) => Task.CompletedTask;
    }
}
