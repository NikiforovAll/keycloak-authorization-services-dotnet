using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using static Microsoft.Extensions.DependencyInjection.AuthorizationConstants.Policies;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

builder.AddSerilog();

services.AddApplicationSwagger(configuration).AddAuth(configuration);

services.Configure<JsonOptions>(opts =>
{
    opts.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    opts.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app.UseHttpsRedirection()
    .UseApplicationSwagger(configuration)
    .UseAuthentication()
    .UseAuthorization();

// login with required aspnet core identity role
app.MapGet("/endpoint1", (ClaimsPrincipal user) => user)
    .RequireAuthorization(RequireAspNetCoreRole);

// login with requireed realm role evaluated from corresponding claim
app.MapGet("/endpoint2", (ClaimsPrincipal user) => user).RequireAuthorization(RequireRealmRole);

// login with requireed client role evaluated from corresponding claim
app.MapGet("/endpoint3", (ClaimsPrincipal user) => user).RequireAuthorization(RequireClientRole);

// login based on remotely executed policy
// authorization is performed by Keycloak (Authorization Server)
app.MapGet("/endpoint4", (ClaimsPrincipal user) => user)
    .RequireAuthorization(RequireToBeInKeycloakGroupAsReader);

await app.RunAsync();
