using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Sdk;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.AddServiceDefaults();

services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddKeycloakWebApp(
        configuration.GetSection(KeycloakAuthenticationOptions.Section),
        configureOpenIdConnectOptions: options =>
        {
            options.SaveTokens = true;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.RequireHttpsMetadata = false;
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("email");
        }
    );

services.AddAuthorization();
services.AddCascadingAuthenticationState();

services.AddMudServices();

services.AddHttpContextAccessor();
services.AddKeycloakUmaTicketExchangeHttpClient(configuration);
services
    .AddHttpClient(
        "ResourceServer",
        (sp, client) =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var baseUrl =
                config["services:resource-server:http:0"]
                ?? config["ResourceServer:BaseUrl"]
                ?? "http://localhost:5180";
            client.BaseAddress = new Uri(baseUrl);
        }
    )
    .AddUmaTokenHandler();

services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<ClientApp.Components.App>().AddInteractiveServerRenderMode();

app.MapGroup("/authentication").MapLoginAndLogout();

app.MapDefaultEndpoints();

app.Run();
