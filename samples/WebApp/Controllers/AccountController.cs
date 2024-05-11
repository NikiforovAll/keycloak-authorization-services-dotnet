namespace WebApp_OpenIDConnect_DotNet.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult SignIn()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return this.Challenge(OpenIdConnectDefaults.AuthenticationScheme);
        }

        return this.RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task<IActionResult> SignOutAsync()
    {
        var idToken = await this.HttpContext.GetTokenAsync("id_token");

        return this.SignOut(
            new AuthenticationProperties
            {
                RedirectUri = "/",
                Items = { { "id_token_hint", idToken } }
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }
}
