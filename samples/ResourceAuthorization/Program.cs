using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Kiota;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ResourceAuthorization;
using KeycloakAdminClientOptions = Keycloak.AuthServices.Sdk.Kiota.KeycloakAdminClientOptions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddProblemDetails();
services.AddApplicationSwagger();

services.AddControllers(options => options.AddProtectedResources());

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddKeycloakWebApi(builder.Configuration);

services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationServer(builder.Configuration);

var adminSection = "KeycloakAdmin";

var adminClient = "admin";
var protectionClient = "protection";

AddAccessTokenManagement(builder, services);

services
    .AddKiotaKeycloakAdminHttpClient(builder.Configuration, keycloakClientSectionName: adminSection)
    .AddClientCredentialsTokenHandler(adminClient);

services
    .AddKeycloakProtectionHttpClient(
        builder.Configuration,
        keycloakClientSectionName: KeycloakProtectionClientOptions.Section
    )
    .AddClientCredentialsTokenHandler(protectionClient);

services.AddScoped<WorkspaceService>();

var app = builder.Build();

app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseOpenApi();
app.UseSwaggerUi(ui => ui.UseApplicationSwaggerSettings(builder.Configuration));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();

void AddAccessTokenManagement(WebApplicationBuilder builder, IServiceCollection services)
{
    services.AddDistributedMemoryCache();
    services
        .AddClientCredentialsTokenManagement()
        .AddClient(
            adminClient,
            client =>
            {
                var options = builder.Configuration.GetKeycloakOptions<KeycloakAdminClientOptions>(
                    adminSection
                )!;

                client.ClientId = options.Resource;
                client.ClientSecret = options.Credentials.Secret;
                client.TokenEndpoint = options.KeycloakTokenEndpoint;
            }
        )
        .AddClient(
            protectionClient,
            client =>
            {
                var options =
                    builder.Configuration.GetKeycloakOptions<KeycloakProtectionClientOptions>()!;

                client.ClientId = options.Resource;
                client.ClientSecret = options.Credentials.Secret;
                client.TokenEndpoint = options.KeycloakTokenEndpoint;
            }
        );
}
