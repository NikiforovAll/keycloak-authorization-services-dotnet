using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddProblemDetails();
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

services.AddControllers(options => options.AddProtectedResources());

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(builder.Configuration);

services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(builder.Configuration);

var app = builder.Build();

app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseOpenApi();
app.UseSwaggerUi(ui =>
{
    var keycloakOptions =
        builder.Configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;

    ui.OAuth2Client = new OAuth2ClientSettings
    {
        ClientId = keycloakOptions.Resource,
        ClientSecret = keycloakOptions?.Credentials?.Secret,
    };
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
