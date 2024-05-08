namespace TestWebApiWithControllers.Controllers;

using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Mvc;

#region WorkspaceAPI
[ApiController]
[Route("workspaces")]
[ProtectedResource("workspaces")]
public class WorkspacesController : ControllerBase
{
    [HttpGet]
    [ProtectedResource("workspaces", "workspace:list")]
    public ActionResult<IEnumerable<string>> GetWorkspacesAsync() => this.Ok(Array.Empty<string>());

    [HttpGet("public")]
    [IgnoreProtectedResource]
    public IActionResult GetPublicWorkspaceAsync() => this.Ok(new { Id = "public" });

    [HttpGet("{id}")]
    [ProtectedResource("{id}", "workspace:read")]
    public IActionResult GetWorkspaceAsync(string id) => this.Ok(new { id });

    [HttpDelete("{id}")]
    [ProtectedResource("{id}", "workspace:delete")]
    public IActionResult DeleteWorkspaceAsync(string id) =>
        string.IsNullOrWhiteSpace(id) ? this.BadRequest() : this.NoContent();
}
#endregion WorkspaceAPI
