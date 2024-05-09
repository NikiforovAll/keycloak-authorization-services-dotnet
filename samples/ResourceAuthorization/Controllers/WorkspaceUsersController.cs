namespace ResourceAuthorization.Controllers;

using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ResourceAuthorization.Models;

[ApiController]
[Route("workspaces/{id}/users")]
[OpenApiTag("Users", Description = "Manage Users.")]
public class WorkspaceUsersController(WorkspaceService workspaceService) : ControllerBase
{
    [HttpGet("", Name = nameof(ListUsersAsync))]
    [OpenApiOperation("[workspace:list-users]", "")]
    [ProtectedResource("workspaces__{id}", "workspace:list-users")]
    public async Task<IActionResult> ListUsersAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return this.BadRequest();
        }

        var users = await workspaceService.ListMembersAsync(id);

        return this.Ok(users);
    }

    [HttpPost("", Name = nameof(AddUserAsync))]
    [OpenApiOperation("[workspace:add-user]", "")]
    [ProtectedResource("workspaces__{id}", "workspace:add-user")]
    public async Task<IActionResult> AddUserAsync(string id, User user)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return this.BadRequest();
        }

        await workspaceService.AddMember(id, user);

        return this.Created();
    }

    [HttpDelete("", Name = nameof(RemoveUserAsync))]
    [OpenApiOperation("[workspace:remove-user]", "")]
    [ProtectedResource("workspaces__{id}", "workspace:remove-user")]
    public async Task<IActionResult> RemoveUserAsync(string id, string email)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(id))
        {
            return this.BadRequest();
        }

        var user = new User(email);
        await workspaceService.RemoveMember(id, user);

        return this.NoContent();
    }
}
