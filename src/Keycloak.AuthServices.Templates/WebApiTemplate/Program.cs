using Api;
using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddApplicationOpenApi(configuration);

services.AddKeycloakWebApiAuthentication(
    configuration,
    options =>
    {
        options.Audience = "workspaces-client";
        options.RequireHttpsMetadata = false;
    }
);
services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseApplicationOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/hello", () => "Hello World!").RequireAuthorization();

app.Run();
