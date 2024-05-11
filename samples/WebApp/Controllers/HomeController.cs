namespace WebApp_OpenIDConnect_DotNet.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp_OpenIDConnect_DotNet.Models;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index() => this.View();

    public IActionResult Privacy() => this.View();

    [AllowAnonymous]
    public IActionResult Public() => this.View();

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
