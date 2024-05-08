namespace ResourceAuthorization.Controllers;

using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ResourceAuthorization.Models;

[ApiController]
[Route("workspaces/{id}/users")]
[OpenApiTag("Users", Description = "Manage Users.")]
public class WorkspaceUsersController : ControllerBase
{
    [HttpGet("", Name = nameof(ListUsersAsync))]
    [OpenApiOperation("[workspace:list-users]", "")]
    [ProtectedResource("workspaces/{id}", "workspace:list-users")]
    public IActionResult ListUsersAsync(string id) =>
        string.IsNullOrWhiteSpace(id) ? this.BadRequest() : this.Ok();

    [HttpPost("", Name = nameof(AddUserAsync))]
    [OpenApiOperation("[workspace:add-user]", "")]
    [ProtectedResource("workspaces/{id}", "workspace:add-user")]
    public IActionResult AddUserAsync(string id, User user) =>
        string.IsNullOrWhiteSpace(id) ? this.BadRequest() : this.Ok(user);

    [HttpDelete("", Name = nameof(RemoveUserAsync))]
    [OpenApiOperation("[workspace:remove-user]", "")]
    [ProtectedResource("workspaces/{id}", "workspace:remove-user")]
    public IActionResult RemoveUserAsync(string id, string email) =>
        string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(id)
            ? this.BadRequest()
            : this.NoContent();
}
