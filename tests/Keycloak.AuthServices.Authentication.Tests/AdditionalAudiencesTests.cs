namespace Keycloak.AuthServices.Authentication.Tests;

using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public class AdditionalAudiencesTests
{
    private static JwtBearerOptions BuildOptions(Action<KeycloakAuthenticationOptions> configure)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddKeycloakWebApiAuthentication(configure);
        using var sp = services.BuildServiceProvider();
        return sp.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>()
            .Get(JwtBearerDefaults.AuthenticationScheme);
    }

    [Fact]
    public void AdditionalAudiences_WhenSet_SetsValidAudiences()
    {
        var opts = BuildOptions(o =>
        {
            o.Resource = "primary-api";
            o.AdditionalAudiences = ["billing-service", "reporting-service"];
        });

        opts.TokenValidationParameters.ValidAudiences.Should()
            .BeEquivalentTo(["primary-api", "billing-service", "reporting-service"]);
    }

    [Fact]
    public void AdditionalAudiences_WhenNotSet_SetsValidAudienceOnTvp()
    {
        var opts = BuildOptions(o => o.Resource = "primary-api");

        opts.TokenValidationParameters.ValidAudience.Should().Be("primary-api");
        opts.TokenValidationParameters.ValidAudiences.Should().BeNull();
    }

    [Fact]
    public void AdditionalAudiences_WhenEmpty_SetsValidAudienceOnTvp()
    {
        var opts = BuildOptions(o =>
        {
            o.Resource = "primary-api";
            o.AdditionalAudiences = [];
        });

        opts.TokenValidationParameters.ValidAudience.Should().Be("primary-api");
        opts.TokenValidationParameters.ValidAudiences.Should().BeNull();
    }

    [Fact]
    public void AdditionalAudiences_PrimaryAudienceIsIncluded()
    {
        var opts = BuildOptions(o =>
        {
            o.Audience = "explicit-audience";
            o.Resource = "client-id";
            o.AdditionalAudiences = ["secondary"];
        });

        opts.TokenValidationParameters.ValidAudiences.Should()
            .Contain("explicit-audience")
            .And.Contain("secondary");
    }

    [Fact]
    public void AdditionalAudiences_NullPrimaryAudience_UsesOnlyAdditional()
    {
        var opts = BuildOptions(o =>
        {
            o.Resource = null;
            o.Audience = null;
            o.AdditionalAudiences = ["extra-service"];
        });

        opts.TokenValidationParameters.ValidAudiences.Should().BeEquivalentTo(["extra-service"]);
    }

    // Regression tests: previously options.Audience was set before TokenValidationParameters was
    // replaced with `new TokenValidationParameters { ... }`, causing ValidAudience to be silently
    // lost on the new TVP instance. The fix sets ValidAudience directly in the TVP initializer
    // and also sets options.Audience after TVP replacement for API consistency.

    [Fact]
    public void Regression_AudienceFromResource_NotLostAfterTvpReplacement()
    {
        var opts = BuildOptions(o => o.Resource = "my-client");

        // Would be null under the old buggy ordering
        opts.TokenValidationParameters.ValidAudience.Should().Be("my-client");
        opts.Audience.Should().Be("my-client");
    }

    [Fact]
    public void Regression_AudienceFromAudienceProperty_NotLostAfterTvpReplacement()
    {
        var opts = BuildOptions(o =>
        {
            o.Audience = "my-audience";
            o.Resource = "my-client";
        });

        // Audience takes precedence over Resource; would be null under old buggy ordering
        opts.TokenValidationParameters.ValidAudience.Should().Be("my-audience");
        opts.Audience.Should().Be("my-audience");
    }

    [Fact]
    public void Regression_AudienceValidationEnabled_WhenVerifyTokenAudienceTrue()
    {
        var opts = BuildOptions(o =>
        {
            o.Resource = "my-client";
            o.VerifyTokenAudience = true;
        });

        opts.TokenValidationParameters.ValidateAudience.Should().BeTrue();
        opts.TokenValidationParameters.ValidAudience.Should().Be("my-client");
        opts.Audience.Should().Be("my-client");
    }
}
