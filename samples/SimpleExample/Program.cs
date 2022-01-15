using Api;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

host.ConfigureLogger();
host.ConfigureKeycloakConfigurationSource("keycloak.json");

services.AddInfrastructure(configuration);

DatabaseUtils.MigrateDatabase(services.BuildServiceProvider());

services
    .AddApplication()
    .AddSwagger();

services.AddKeycloakAuthentication(configuration, o =>
{
    o.RequireHttpsMetadata = false;
});

services.AddAuthorization(o =>
{
    o.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
}).AddKeycloakAuthorization(configuration);

services.AddControllers();

services.AddKeycloakAdminHttpClient(configuration);

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();

