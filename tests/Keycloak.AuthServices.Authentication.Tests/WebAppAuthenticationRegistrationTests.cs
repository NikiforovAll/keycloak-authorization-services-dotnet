namespace Keycloak.AuthServices.Authentication.Tests;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public class WebAppAuthenticationRegistrationTests
{
    private static IConfiguration BuildConfig(
        string realm = "test",
        string authServerUrl = "https://keycloak.example.com/",
        string? resource = "test-client",
        string? secret = null,
        string sslRequired = "none",
        string? metadataAddress = null
    )
    {
        var dict = new Dictionary<string, string?>
        {
            ["Keycloak:realm"] = realm,
            ["Keycloak:auth-server-url"] = authServerUrl,
            ["Keycloak:resource"] = resource,
            ["Keycloak:ssl-required"] = sslRequired,
        };

        if (secret is not null)
        {
            dict["Keycloak:credentials:secret"] = secret;
        }

        if (metadataAddress is not null)
        {
            dict["Keycloak:metadataAddress"] = metadataAddress;
        }

        return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
    }

    private static OpenIdConnectOptions ResolveOidcOptions(
        IServiceCollection services,
        string scheme = OpenIdConnectDefaults.AuthenticationScheme
    )
    {
        using var sp = services.BuildServiceProvider();
        return sp.GetRequiredService<IOptionsMonitor<OpenIdConnectOptions>>().Get(scheme);
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithConfig_SetsAuthorityFromRealm()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(
            BuildConfig(realm: "my-realm", authServerUrl: "https://auth.example.com/")
        );

        var opts = ResolveOidcOptions(services);

        opts.Authority.Should().Be("https://auth.example.com/realms/my-realm/");
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithConfig_SetsClientIdFromResource()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(BuildConfig(resource: "my-webapp"));

        var opts = ResolveOidcOptions(services);

        opts.ClientId.Should().Be("my-webapp");
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithSecret_SetsClientSecret()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(BuildConfig(secret: "super-secret"));

        var opts = ResolveOidcOptions(services);

        opts.ClientSecret.Should().Be("super-secret");
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_SslRequiredNone_DisablesHttpsMetadata()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(BuildConfig(sslRequired: "none"));

        var opts = ResolveOidcOptions(services);

        opts.RequireHttpsMetadata.Should().BeFalse();
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_SslRequiredExternal_EnablesHttpsMetadata()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(BuildConfig(sslRequired: "external"));

        var opts = ResolveOidcOptions(services);

        opts.RequireHttpsMetadata.Should().BeTrue();
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_DefaultSslRequired_EnablesHttpsMetadata()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(BuildConfig(sslRequired: ""));

        var opts = ResolveOidcOptions(services);

        opts.RequireHttpsMetadata.Should().BeTrue();
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithMetadataAddress_AppendedToAuthority()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        const string oauthMetadataPath = ".well-known/oauth-authorization-server";

        services.AddKeycloakWebAppAuthentication(
            BuildConfig(
                realm: "test",
                authServerUrl: "https://keycloak.example.com/",
                metadataAddress: oauthMetadataPath
            )
        );

        var opts = ResolveOidcOptions(services);

        opts.MetadataAddress.Should()
            .Be($"https://keycloak.example.com/realms/test/{oauthMetadataPath}");
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithDefaultCookieScheme_SetsSignInScheme()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(BuildConfig());

        var opts = ResolveOidcOptions(services);

        opts.SignInScheme.Should().Be(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithCallbacks_AuthorityAndClientIdSet()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(BuildConfig());

        var opts = ResolveOidcOptions(services);

        opts.Authority.Should().NotBeNullOrEmpty();
        opts.ClientId.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithCustomOidcOptions_CallbackIsApplied()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services
            .AddAuthentication()
            .AddKeycloakWebApp(
                BuildConfig().GetSection("Keycloak"),
                configureOpenIdConnectOptions: opts => opts.ResponseType = "code token"
            );

        var opts = ResolveOidcOptions(services);

        opts.ResponseType.Should().Be("code token");
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithActionCallbacks_ConfiguresOidcOptions()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services
            .AddAuthentication()
            .AddKeycloakWebApp(configureKeycloakOptions: opts =>
            {
                opts.AuthServerUrl = "https://keycloak.example.com/";
                opts.Realm = "test-realm";
                opts.Resource = "my-client";
            });

        var opts = ResolveOidcOptions(services);

        opts.Authority.Should().Be("https://keycloak.example.com/realms/test-realm/");
        opts.ClientId.Should().Be("my-client");
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_WithCustomOidcScheme_OptionsRegisteredUnderScheme()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        const string customScheme = "MyOidcScheme";

        services.AddKeycloakWebAppAuthentication(
            BuildConfig(realm: "test", authServerUrl: "https://keycloak.example.com/"),
            openIdConnectScheme: customScheme
        );

        var opts = ResolveOidcOptions(services, customScheme);

        opts.Authority.Should().Be("https://keycloak.example.com/realms/test/");
    }

    [Fact]
    public void AddKeycloakWebAppAuthentication_ScopeContainsOpenId()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebAppAuthentication(BuildConfig());

        var opts = ResolveOidcOptions(services);

        opts.Scope.Should().Contain("openid");
    }
}
