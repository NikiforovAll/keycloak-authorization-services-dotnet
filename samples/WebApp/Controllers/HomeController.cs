namespace WebApp_OpenIDConnect_DotNet.Controllers;

using System.Diagnostics;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp_OpenIDConnect_DotNet.Models;

[Authorize]
public class HomeController : Controller
{
    private readonly IKeycloakAccessTokenProvider tokenProvider;

    public HomeController(IKeycloakAccessTokenProvider tokenProvider) =>
        this.tokenProvider = tokenProvider;

    public async Task<IActionResult> Index()
    {
        // Resolving the token here triggers the auto-detection log on the first request.
        // In a real app, the token would be used to call a downstream API.
        var token = await this.tokenProvider.GetAccessTokenAsync();
        this.ViewData["HasToken"] = !string.IsNullOrEmpty(token);
        return this.View();
    }

    [Authorize(Policy = "PrivacyAccess")]
    public IActionResult Privacy() => this.View();

    [AllowAnonymous]
    public IActionResult Public() => this.View();

    [AllowAnonymous]
    public IActionResult AccessDenied() => this.View();

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        this.View(
            new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            }
        );
}
