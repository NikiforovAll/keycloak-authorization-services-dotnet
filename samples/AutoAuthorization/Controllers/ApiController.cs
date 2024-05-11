namespace AutoAuthorization.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ApiController : ControllerBase
{
    [HttpGet]
    [Route("/api/auto-auth")]
    [ProducesResponseType(statusCode: 200, type: typeof(string))]
    public IActionResult GetData() => this.Ok("Auto Authorization works.");
}
