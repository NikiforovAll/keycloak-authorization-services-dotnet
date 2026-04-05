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
    public void AdditionalAudiences_WhenNotSet_ValidAudiencesIsNull()
    {
        var opts = BuildOptions(o => o.Resource = "primary-api");

        opts.TokenValidationParameters.ValidAudiences.Should().BeNull();
    }

    [Fact]
    public void AdditionalAudiences_WhenEmpty_ValidAudiencesIsNull()
    {
        var opts = BuildOptions(o =>
        {
            o.Resource = "primary-api";
            o.AdditionalAudiences = [];
        });

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
}
