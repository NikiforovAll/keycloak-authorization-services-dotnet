namespace Keycloak.AuthServices.Authorization.Tests;

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

public class AuthorizationServerRegistrationTests
{
    [Fact]
    public void AddAuthorizationServer_RegistersDefaultTokenProvider()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider.Should().NotBeNull();
        provider.Should().BeOfType<HttpContextAccessTokenProvider>();
    }

    [Fact]
    public void AddAuthorizationServer_WithCookieAuth_AutoDetectsCookieProvider()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        // Cookie auth registered before AddAuthorizationServer (typical Web App setup)
        services.AddAuthentication().AddCookie();

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider.Should().NotBeNull();
        provider.Should().BeOfType<CookieAccessTokenProvider>();
    }

    [Fact]
    public void AddAuthorizationServer_WithCookieAuth_UsesDetectedSchemeName()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddAuthentication().AddCookie("MyCookies");

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider.Should().BeOfType<CookieAccessTokenProvider>()
            .Which.CookieScheme.Should().Be("MyCookies");
    }

    [Fact]
    public void AddAuthorizationServer_CustomProviderTakesPrecedence()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddScoped<IKeycloakAccessTokenProvider, FakeTokenProvider>();

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider.Should().NotBeNull();
        provider.Should().BeOfType<FakeTokenProvider>();
    }

    [Fact]
    public void AddAuthorizationServer_CustomProviderTakesPrecedenceEvenWithCookieAuth()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddAuthentication().AddCookie();
        services.AddScoped<IKeycloakAccessTokenProvider, FakeTokenProvider>();

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider.Should().BeOfType<FakeTokenProvider>();
    }

    [Fact]
    public void AddAuthorizationServer_WithKeycloakWebApiAuth_RegistersDefaultTokenProvider()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        // Real Keycloak Web API authentication (JWT Bearer) — no cookie scheme registered
        services.AddKeycloakWebApiAuthentication(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider.Should().NotBeNull();
        provider.Should().BeOfType<HttpContextAccessTokenProvider>();
    }

    [Fact]
    public void AddAuthorizationServer_WithKeycloakWebAppAuth_AutoDetectsCookieProvider()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        // Real Keycloak Web App authentication (OIDC + Cookies) — cookie scheme is auto-registered
        services
            .AddAuthentication()
            .AddKeycloakWebApp(options =>
            {
                options.AuthServerUrl = "https://keycloak.example.com/";
                options.Realm = "test";
                options.Resource = "test-client";
            });

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider.Should().NotBeNull();
        provider.Should().BeOfType<CookieAccessTokenProvider>();
    }

    [Fact]
    public void AddAuthorizationServer_WithKeycloakWebAppAuth_CustomCookieScheme_UsesDetectedSchemeName()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services
            .AddAuthentication()
            .AddKeycloakWebApp(
                options =>
                {
                    options.AuthServerUrl = "https://keycloak.example.com/";
                    options.Realm = "test";
                    options.Resource = "test-client";
                },
                cookieScheme: "MyKeycloakCookies"
            );

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider
            .Should()
            .BeOfType<CookieAccessTokenProvider>()
            .Which.CookieScheme.Should()
            .Be("MyKeycloakCookies");
    }

    [Fact]
    public void AddAuthorizationServer_WithKeycloakWebAppAuth_CustomProviderTakesPrecedence()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddScoped<IKeycloakAccessTokenProvider, FakeTokenProvider>();

        services
            .AddAuthentication()
            .AddKeycloakWebApp(options =>
            {
                options.AuthServerUrl = "https://keycloak.example.com/";
                options.Realm = "test";
                options.Resource = "test-client";
            });

        services.AddAuthorizationServer(options =>
        {
            options.AuthServerUrl = "https://keycloak.example.com/";
            options.Realm = "test";
            options.Resource = "test-client";
        });

        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();

        var provider = scope.ServiceProvider.GetService<IKeycloakAccessTokenProvider>();
        provider.Should().NotBeNull();
        provider.Should().BeOfType<FakeTokenProvider>();
    }

    private sealed class FakeTokenProvider : IKeycloakAccessTokenProvider
    {
        public Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<string?>("fake-token");
    }
}
