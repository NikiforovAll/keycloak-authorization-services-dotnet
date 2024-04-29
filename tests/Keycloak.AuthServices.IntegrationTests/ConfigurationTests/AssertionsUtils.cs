namespace Keycloak.AuthServices.IntegrationTests.ConfigurationTests;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public static class AssertionsUtils
{
    public static void EnsureConfiguredJwtOptions(
        this IServiceProvider serviceProvider,
        JwtBearerOptions? expected
    )
    {
        var bearerOptionsMonitor = serviceProvider.GetService<IOptionsMonitor<JwtBearerOptions>>();
        EnsureMatchingJwtOptions(
            bearerOptionsMonitor?.Get(JwtBearerDefaults.AuthenticationScheme),
            expected
        );
    }

    public static void EnsureMatchingJwtOptions(
        JwtBearerOptions? source,
        JwtBearerOptions? expected
    )
    {
        source
            .Should()
            .BeEquivalentTo(
                expected,
                cfg =>
                    cfg.Including(f => f!.Audience)
                        .Including(f => f!.Authority)
                        .Including(f => f!.RequireHttpsMetadata)
                        .Including(f => f!.TokenValidationParameters.ValidateAudience)
            );
    }
}
