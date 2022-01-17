namespace Api.Controllers;

using Application.Queries;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Manager")]
[Route("api/manager/workspaces")]
public class WorkspacesController2 : ApiControllerBase
{
    // OR construct same authorization with policy builder
    [Authorize(Policy = PolicyConstants.AccessManagement)]
    [HttpGet]
    public async Task<IEnumerable<Workspace>> Get() =>
        await this.Mediator.Send(new GetWorkspacesQuery());

    [Authorize(Policy = PolicyConstants.CanDeleteAllWorkspaces)]
    [HttpDelete("")]
    public async Task DeleteAll([FromServices] ApplicationDbContext db) =>
        await db.Database.ExecuteSqlRawAsync("TRUNCATE public.\"Workspaces\" CASCADE");
}
