namespace Api.Application.Commands;

using System.Threading;
using System.Threading.Tasks;
using Api.Data;
using MediatR;

public record CreateWorkspaceCommand(string Name, IList<Project>? Projects = default) : IRequest;

public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand>
{
    private readonly IApplicationDbContext db;

    public CreateWorkspaceCommandHandler(IApplicationDbContext db) => this.db = db;

    public async Task<Unit> Handle(
        CreateWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        this.db.Workspaces.Add(new Workspace
        {
            Name = request.Name,
            Projects = request.Projects ?? new List<Project>(),
        });
        await this.db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
