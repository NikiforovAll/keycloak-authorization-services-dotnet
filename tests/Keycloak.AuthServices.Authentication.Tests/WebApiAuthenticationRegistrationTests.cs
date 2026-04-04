namespace Keycloak.AuthServices.Authentication.Tests;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public class WebApiAuthenticationRegistrationTests
{
    private static IConfiguration BuildConfig(
        string realm = "test",
        string authServerUrl = "https://keycloak.example.com/",
        string? resource = "test-client",
        string? audience = null,
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

        if (audience is not null)
        {
            dict["Keycloak:audience"] = audience;
        }

        if (metadataAddress is not null)
        {
            dict["Keycloak:metadataAddress"] = metadataAddress;
        }

        return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
    }

    private static JwtBearerOptions ResolveJwtBearerOptions(
        IServiceCollection services,
        string scheme = JwtBearerDefaults.AuthenticationScheme
    )
    {
        using var sp = services.BuildServiceProvider();
        return sp.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>().Get(scheme);
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_WithConfig_SetsAuthorityFromRealm()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebApiAuthentication(
            BuildConfig(realm: "my-realm", authServerUrl: "https://auth.example.com/")
        );

        var opts = ResolveJwtBearerOptions(services);

        opts.Authority.Should().Be("https://auth.example.com/realms/my-realm/");
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_WithResourceOnly_UsesResourceAsAudience()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebApiAuthentication(BuildConfig(resource: "my-api", audience: null));

        var opts = ResolveJwtBearerOptions(services);

        opts.Audience.Should().Be("my-api");
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_WithExplicitAudience_UsesAudienceOverResource()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebApiAuthentication(
            BuildConfig(resource: "my-api", audience: "explicit-audience")
        );

        var opts = ResolveJwtBearerOptions(services);

        opts.Audience.Should().Be("explicit-audience");
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_SslRequiredNone_DisablesHttpsMetadata()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebApiAuthentication(BuildConfig(sslRequired: "none"));

        var opts = ResolveJwtBearerOptions(services);

        opts.RequireHttpsMetadata.Should().BeFalse();
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_SslRequiredExternal_EnablesHttpsMetadata()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebApiAuthentication(BuildConfig(sslRequired: "external"));

        var opts = ResolveJwtBearerOptions(services);

        opts.RequireHttpsMetadata.Should().BeTrue();
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_DefaultSslRequired_EnablesHttpsMetadata()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        // Empty ssl-required defaults to requiring HTTPS
        services.AddKeycloakWebApiAuthentication(BuildConfig(sslRequired: ""));

        var opts = ResolveJwtBearerOptions(services);

        opts.RequireHttpsMetadata.Should().BeTrue();
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_SavesToken()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebApiAuthentication(BuildConfig());

        var opts = ResolveJwtBearerOptions(services);

        opts.SaveToken.Should().BeTrue();
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_WithCustomJwtBearerOptions_CallbackIsApplied()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebApiAuthentication(
            BuildConfig(),
            configureJwtBearerOptions: opts => opts.RequireHttpsMetadata = false
        );

        var opts = ResolveJwtBearerOptions(services);

        // Callback runs last, so it overrides the value derived from ssl-required
        opts.RequireHttpsMetadata.Should().BeFalse();
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_WithCallbacks_ConfiguresKeycloakAndJwtBearerOptions()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddKeycloakWebApiAuthentication(
            configureKeycloakOptions: opts =>
            {
                opts.AuthServerUrl = "https://keycloak.example.com/";
                opts.Realm = "test-realm";
                opts.Resource = "test-client";
            },
            configureJwtBearerOptions: opts => opts.SaveToken = false
        );

        var jwtOpts = ResolveJwtBearerOptions(services);

        jwtOpts.Authority.Should().Be("https://keycloak.example.com/realms/test-realm/");
        jwtOpts.Audience.Should().Be("test-client");
        jwtOpts.SaveToken.Should().BeFalse();
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_WithCustomScheme_OptionsRegisteredUnderScheme()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        const string customScheme = "MyCustomScheme";

        services.AddKeycloakWebApiAuthentication(
            configureKeycloakOptions: opts =>
            {
                opts.AuthServerUrl = "https://keycloak.example.com/";
                opts.Realm = "test";
                opts.Resource = "client";
            },
            jwtBearerScheme: customScheme
        );

        var opts = ResolveJwtBearerOptions(services, customScheme);

        opts.Authority.Should().Be("https://keycloak.example.com/realms/test/");
    }

    [Fact]
    public void AddKeycloakWebApiAuthentication_WithMetadataAddress_AppendedToAuthority()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        const string oauthMetadataPath = ".well-known/oauth-authorization-server";

        services.AddKeycloakWebApiAuthentication(
            BuildConfig(
                realm: "test",
                authServerUrl: "https://keycloak.example.com/",
                metadataAddress: oauthMetadataPath
            )
        );

        var opts = ResolveJwtBearerOptions(services);

        opts.MetadataAddress.Should()
            .Be($"https://keycloak.example.com/realms/test/{oauthMetadataPath}");
    }
}
