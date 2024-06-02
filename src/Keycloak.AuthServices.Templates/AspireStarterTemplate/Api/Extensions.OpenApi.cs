namespace Api;

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Microsoft.OpenApi.Models;

public static class ExtensionsOpenApi
{
    public static IServiceCollection AddApplicationOpenApi(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var keycloakOptions =
                configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;

            c.AddSecurityDefinition(
                "oidc",
                new OpenApiSecurityScheme
                {
                    Name = "OIDC",
                    Type = SecuritySchemeType.OpenIdConnect,
                    OpenIdConnectUrl = new Uri(keycloakOptions.OpenIdConnectUrl!)
                }
            );

            c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oidc"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );

            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"API (v1)", Version = "v1" });
        });

        return services;
    }

    public static IApplicationBuilder UseApplicationOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}
