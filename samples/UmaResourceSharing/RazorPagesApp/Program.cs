using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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
            options.MapInboundClaims = false;
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("email");
        }
    );

var resourceServerClientId = "uma-resource-server";
var resourceServerSecret = "uma-resource-server-secret";

services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationBuilder()
    .AddPolicy("UmaRead", policy => policy.RequireProtectedResource("shared-document", "read"))
    .AddPolicy("UmaWrite", policy => policy.RequireProtectedResource("shared-document", "write"));

services
    .AddAuthorizationServer(options =>
    {
        configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .BindKeycloakOptions(options);
        options.Resource = resourceServerClientId;
        options.Credentials = new() { Secret = resourceServerSecret };
        options.VerifyTokenAudience = true;
    })
    .AddStandardResilienceHandler();

var tokenClientName = ClientCredentialsClientName.Parse("uma_protection");

services.AddDistributedMemoryCache();
services
    .AddClientCredentialsTokenManagement()
    .AddClient(
        tokenClientName,
        client =>
        {
            var keycloakOptions =
                configuration.GetKeycloakOptions<KeycloakAuthenticationOptions>()!;
            client.ClientId = ClientId.Parse(resourceServerClientId);
            client.ClientSecret = ClientSecret.Parse(resourceServerSecret);
            client.TokenEndpoint = new Uri(keycloakOptions.KeycloakTokenEndpoint);
        }
    );

services
    .AddKeycloakProtectionHttpClient(options =>
    {
        configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .BindKeycloakOptions(options);
        options.Resource = resourceServerClientId;
        options.Credentials = new() { Secret = resourceServerSecret };
    })
    .AddClientCredentialsTokenHandler(tokenClientName);

services.AddHttpContextAccessor();
services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGroup("/authentication").MapLoginAndLogout();

app.MapDefaultEndpoints();

app.Run();
