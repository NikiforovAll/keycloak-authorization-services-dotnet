namespace ResourceAuthorization.Controllers;

using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ResourceAuthorization.Models;

#region WorkspaceAPI
[ApiController]
[Route("workspaces")]
[OpenApiTag("Workspaces", Description = "Manage workspaces.")]
public class WorkspacesController : ControllerBase
{
    [HttpGet(Name = nameof(GetWorkspacesAsync))]
    [OpenApiOperation("[workspace:list]", "")]
    [ProtectedResource("workspaces", "workspace:list")]
    public ActionResult<IEnumerable<string>> GetWorkspacesAsync() => this.Ok(Array.Empty<string>());

    [HttpGet("public")]
    [OpenApiIgnore]
    [IgnoreProtectedResource]
    public IActionResult GetPublicWorkspaceAsync() => this.Ok(new { Id = "public" });

    [HttpGet("{id}", Name = nameof(GetWorkspaceAsync))]
    [OpenApiOperation("[workspace:read]", "")]
    [ProtectedResource("workspaces/{id}", "workspace:read")]
    public IActionResult GetWorkspaceAsync(string id) => this.Ok(new { id });

    [HttpPost("", Name = nameof(CreateWorkspaceAsync))]
    [OpenApiOperation("[workspace:create]", "")]
    [ProtectedResource("workspaces", "workspace:create")]
    public IActionResult CreateWorkspaceAsync(Workspace workspace) => this.Ok(workspace);

    [HttpDelete("{id}", Name = nameof(DeleteWorkspaceAsync))]
    [OpenApiOperation("[workspace:delete]", "")]
    [ProtectedResource("workspaces/{id}", "workspace:delete")]
    public IActionResult DeleteWorkspaceAsync(string id) =>
        string.IsNullOrWhiteSpace(id) ? this.BadRequest() : this.NoContent();
}
#endregion WorkspaceAPI
