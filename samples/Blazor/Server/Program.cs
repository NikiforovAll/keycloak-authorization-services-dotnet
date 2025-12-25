using System.Globalization;
using Keycloak.AuthServices.Authentication;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllersWithViews();
services.AddRazorPages();

services.AddEndpointsApiExplorer();
var openIdConnectUrl =
    $"{configuration["Keycloak:auth-server-url"]}realms/{configuration["Keycloak:realm"]}/.well-known/openid-configuration";

services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Auth",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OpenIdConnect,
        OpenIdConnectUrl = new Uri(openIdConnectUrl),
        Scheme = "bearer",
        BearerFormat = "JWT",
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = [],
    });
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console(
        outputTemplate: "[{Level:u4}] | {Message:lj}{NewLine}{Exception}",
        restrictedToMinimumLevel: LogEventLevel.Information,
        formatProvider: CultureInfo.InvariantCulture
    )
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

services.AddKeycloakWebApiAuthentication(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger().UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
