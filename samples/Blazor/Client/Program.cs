using Blazor.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;

RegisterHttpClient(builder, services);

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.MetadataUrl = "http://localhost:8080/realms/Test/.well-known/openid-configuration";
    options.ProviderOptions.Authority = "http://localhost:8080/realms/Test";
    options.ProviderOptions.ClientId = "test-client";
    options.ProviderOptions.ResponseType = "id_token token";
    //options.ProviderOptions.DefaultScopes.Add("Audience");

    options.UserOptions.NameClaim = "preferred_username";
    options.UserOptions.RoleClaim = "roles";
    options.UserOptions.ScopeClaim = "scope";
});

var app = builder.Build();

await app.RunAsync();

static void RegisterHttpClient(
    WebAssemblyHostBuilder builder,
    IServiceCollection services)
{
    var httpClientName = "Default";

    services.AddHttpClient(httpClientName,
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

    services.AddScoped(
        sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient(httpClientName));
}
