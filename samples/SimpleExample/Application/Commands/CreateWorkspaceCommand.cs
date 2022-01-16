namespace Api.Application.Commands;

using System.Threading;
using System.Threading.Tasks;
using Data;
using Authorization;
using Keycloak.AuthServices.Authorization;
using MediatR;

[Authorize(Roles = "Manager")]
[AuthorizeProtectedResource("workspaces", "workspaces:create")]
public record CreateWorkspaceCommand(string Name, IList<Project>? Projects = default) : IRequest;

public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand>
{
    private readonly IApplicationDbContext db;

    public CreateWorkspaceCommandHandler(IApplicationDbContext db) => this.db = db;

    public async Task<Unit> Handle(
        CreateWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        var (name, projects) = request;

        this.db.Workspaces.Add(
            new Workspace {Name = name, Projects = projects ?? new List<Project>(),});
        await this.db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
