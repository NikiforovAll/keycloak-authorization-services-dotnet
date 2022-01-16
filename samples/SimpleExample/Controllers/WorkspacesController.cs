namespace Api.Controllers;

using Application.Commands;
using Application.Queries;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class WorkspacesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Workspace>> Get() =>
        await this.Mediator.Send(new GetWorkspacesQuery());

    [HttpGet("{id:guid}")]
    public async Task<Workspace> GetById(Guid id) =>
        await this.Mediator.Send(new GetWorkspaceByIdQuery(id));

    /// <summary>
    /// Creates workspace with related projects
    /// </summary>
    /// <remarks>
    /// {
    ///     "name": "New Project",
    ///     "projects": [
    ///         {
    ///             "alias": "awesome-project"
    ///         }
    ///     ]
    /// }
    /// </remarks>
    [HttpPost]
    public async Task Create(CreateWorkspaceCommand command) =>
        await this.Mediator.Send(command);

    [HttpDelete("{id:guid}")]
    public async Task Delete(Guid id) =>
        await this.Mediator.Send(new DeleteWorkspaceCommand(id));

    [HttpDelete("")]
    public async Task DeleteAll([FromServices] ApplicationDbContext db) =>
        await db.Database.ExecuteSqlRawAsync("TRUNCATE public.\"Workspaces\" CASCADE");
}
