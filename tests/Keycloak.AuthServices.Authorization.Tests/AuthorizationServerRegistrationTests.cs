namespace Keycloak.AuthServices.Authorization.Tests;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
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

    private sealed class FakeTokenProvider : IKeycloakAccessTokenProvider
    {
        public Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<string?>("fake-token");
    }
}
