using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
});

builder
    .Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddKeycloakWebApp(
        builder.Configuration.GetSection(KeycloakAuthenticationOptions.Section),
        configureOpenIdConnectOptions: options =>
        {
            // we need this for front-channel sign-out
            options.SaveTokens = true;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.Events = new OpenIdConnectEvents
            {
                OnSignedOutCallbackRedirect = context =>
                {
                    context.Response.Redirect("/Home/Public");
                    context.HandleResponse();

                    return Task.CompletedTask;
                },
            };
        }
    );

builder
    .Services.AddKeycloakAuthorization(builder.Configuration)
    .AddAuthorizationBuilder()
    .AddPolicy("PrivacyAccess", policy => policy.RequireRealmRoles("Admin"));

// AddAuthorizationServer registers IKeycloakAccessTokenProvider with auto-detection:
// - If cookie auth is registered → CookieAccessTokenProvider (for Web Apps with SaveTokens = true)
// - Otherwise → HttpContextAccessTokenProvider (for Web APIs with Bearer tokens)
// UseProtectedResourcePolicyProvider enables dynamic [ProtectedResource] policy resolution.
builder.Services.AddAuthorizationServer(builder.Configuration);
builder.Services.Configure<KeycloakAuthorizationServerOptions>(options =>
    options.UseProtectedResourcePolicyProvider = true
);

builder.Services.AddControllersWithViews(options => options.AddProtectedResources());

builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization();
app.MapRazorPages();

app.Run();
