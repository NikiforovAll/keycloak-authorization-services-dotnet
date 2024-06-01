using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.AddServiceDefaults();

var clientName = "workspaces-client";

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    var keycloakOptions = configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;

    c.AddSecurityDefinition(
        "oidc",
        new OpenApiSecurityScheme
        {
            Name = "oauth2",
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

services.AddKeycloakWebApiAuthentication(
    configuration,
    options =>
    {
        options.Audience = clientName;
        options.RequireHttpsMetadata = false;
    }
);
services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/hello", () => "Hello World!").RequireAuthorization();

app.Run();
