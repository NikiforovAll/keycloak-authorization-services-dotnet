namespace Microsoft.Extensions.DependencyInjection;

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationSwagger(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var options = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Auth",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(
                            $"{options!.KeycloakUrlRealm}/protocol/openid-connect/auth"
                        ),
                        TokenUrl = new Uri(
                            $"{options.KeycloakUrlRealm}/protocol/openid-connect/token"
                        ),
                        Scopes = new Dictionary<string, string>(),
                    },
                },
            };
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
            c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [
                    new OpenApiSecuritySchemeReference(
                        JwtBearerDefaults.AuthenticationScheme,
                        document
                    )
                ] = [],
            });
        });
        return services;
    }

    public static IApplicationBuilder UseApplicationSwagger(
        this IApplicationBuilder app,
        IConfiguration configuration
    )
    {
        KeycloakAuthenticationOptions options = new();

        configuration.BindKeycloakOptions(options);

        app.UseSwagger();
        app.UseSwaggerUI(s => s.OAuthClientId(options.Resource));

        return app;
    }
}
