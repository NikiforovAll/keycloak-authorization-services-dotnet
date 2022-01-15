using Api;
using Keycloak.AuthServices.Authentication;
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
});

services.AddControllers();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();

