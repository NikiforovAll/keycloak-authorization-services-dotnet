using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
});

builder
    .Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddKeycloakWebApp(
        builder.Configuration.GetSection(KeycloakAuthorizationServerOptions.Section),
        configureOpenIdConnectOptions: options =>
        {
            // used for front-channel logout
            options.SaveTokens = true;

            options.Events = new OpenIdConnectEvents
            {
                OnSignedOutCallbackRedirect = context =>
                {
                    context.Response.Redirect("/Home/Public");
                    context.HandleResponse();

                    return Task.CompletedTask;
                }
            };

            // NOTE, the source for claims is id_token and not access_token.
            // By default, id_token doesn't contain realm_roles claim
            // and you will need to create a mapper for that
            options.ClaimActions.MapUniqueJsonKey("realm_access", "realm_access");
        }
    );

builder.Services.PostConfigure<OpenIdConnectOptions>(options => { });

builder.Services.AddControllersWithViews();

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
