namespace AutoAuthorization.Controllers;

using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ApiController : ControllerBase
{
    [HttpGet]
    [Route("/api/auto-auth")]
    [ProducesResponseType(statusCode: 200, type: typeof(string))]
    public IActionResult GetData() => this.Ok("Auto Authorization works.");

    [HttpGet]
    [Route("/api/no-auth")]
    [ProducesResponseType(statusCode: 200, type: typeof(string))]
    [ExplicitResourceProtection(true)]
    public IActionResult GetDataWithoutAuth() => this.Ok("Auto Authorization works, but is disabled by attribute.");

    [HttpGet]
    [Route("/api/explicit-auth")]
    [ProducesResponseType(statusCode: 200, type: typeof(string))]
    [ExplicitResourceProtection("/api/auto-auth", "GET")]
    public IActionResult GetDataWithExplicitAuth() => this.Ok("Auto Authorization works with explicit choosing resource name and scope.");
}
