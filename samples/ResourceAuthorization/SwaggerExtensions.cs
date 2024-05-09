namespace ResourceAuthorization;

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

public static class SwaggerExtensions
{
    public static void AddApplicationSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApiDocument(
            (document, sp) =>
            {
                var keycloakOptions = sp.GetRequiredService<
                        IOptionsMonitor<KeycloakAuthenticationOptions>
                    >()
                    ?.Get(JwtBearerDefaults.AuthenticationScheme)!;

                document.Title = "Workspaces API";

                document.AddSecurity(
                    OpenIdConnectDefaults.AuthenticationScheme,
                    [],
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.OpenIdConnect,
                        OpenIdConnectUrl = keycloakOptions.OpenIdConnectUrl,
                    }
                );

                document.OperationProcessors.Add(
                    new OperationSecurityScopeProcessor(OpenIdConnectDefaults.AuthenticationScheme)
                );
            }
        );
    }

    public static void UseApplicationSwaggerSettings(
        this SwaggerUiSettings ui,
        IConfiguration configuration
    )
    {
        var keycloakOptions = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;

        ui.OAuth2Client = new OAuth2ClientSettings
        {
            ClientId = keycloakOptions.Resource,
            ClientSecret = keycloakOptions?.Credentials?.Secret,
        };
    }
}
