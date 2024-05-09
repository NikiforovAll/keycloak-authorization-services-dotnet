namespace ResourceAuthorization.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ResourceAuthorization.Models;

#region WorkspaceAPI
[ApiController]
[Route("workspaces")]
[OpenApiTag("Workspaces", Description = "Manage workspaces.")]
[ProtectedResource("workspaces")]
public class WorkspacesController(WorkspaceService workspaceService) : ControllerBase
{
    [HttpGet(Name = nameof(GetWorkspacesAsync))]
    [OpenApiOperation("[workspace:list]", "")]
    [ProtectedResource("workspaces", "workspace:list")]
    public async Task<ActionResult<IEnumerable<string>>> GetWorkspacesAsync()
    {
        var workspaces = await workspaceService.ListWorkspacesAsync();

        return this.Ok(workspaces.Select(w => w.Name));
    }

    [HttpGet("public")]
    [OpenApiIgnore]
    [AllowAnonymous]
    public async Task<IActionResult> GetPublicWorkspaceAsync() =>
        await this.GetWorkspaceAsync("public");

    [HttpGet("{id}", Name = nameof(GetWorkspaceAsync))]
    [OpenApiOperation("[workspace:read]", "")]
    [ProtectedResource("workspaces__{id}", "workspace:read")]
    public async Task<IActionResult> GetWorkspaceAsync(string id)
    {
        var workspace = await workspaceService.GetWorkspaceAsync(id);

        return this.Ok(workspace);
    }

    [HttpPost("", Name = nameof(CreateWorkspaceAsync))]
    [OpenApiOperation("[workspace:create]", "")]
    [ProtectedResource("workspaces", "workspace:create")]
    public async Task<IActionResult> CreateWorkspaceAsync(Workspace workspace)
    {
        await workspaceService.CreateWorkspaceAsync(workspace);

        return this.Created();
    }

    [HttpDelete("{id}", Name = nameof(DeleteWorkspaceAsync))]
    [OpenApiOperation("[workspace:delete]", "")]
    [ProtectedResource("workspaces__{id}", "workspace:delete")]
    public async Task<IActionResult> DeleteWorkspaceAsync(string id)
    {
        await workspaceService.DeleteWorkspaceAsync(id);

        return this.NoContent();
    }
}
#endregion WorkspaceAPI
