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
    ui.DocumentTitle = "Workspaces";

    ui.OAuth2Client = new OAuth2ClientSettings
    {
        ClientId = keycloakOptions.Resource,
        ClientSecret = keycloakOptions?.Credentials?.Secret,
    };
});

app.UseAuthentication();
app.UseAuthorization();

var workspaces = app.MapGroup("/workspaces")
    .WithTags("Workspaces Management")
    .RequireProtectedResource("workspaces");

workspaces
    .MapGet("", () => "Hello World!")
    .RequireProtectedResource("workspaces", "workspace:list");

workspaces
    .MapPost("", () => "Hello World!")
    .RequireProtectedResource("workspaces", "workspace:create");

workspaces
    .MapGet("{id}", (string id) => "Hello World!")
    .RequireProtectedResource("workspaces/{id}", "workspace:read");

var users = workspaces.MapGroup("users").RequireProtectedResource("workspaces", "workspace:update");

users
    .MapGet("{id}/users", (string id) => "Hello World!")
    .RequireProtectedResource("workspaces/{id}", "workspace:list-users");

app.Run();
