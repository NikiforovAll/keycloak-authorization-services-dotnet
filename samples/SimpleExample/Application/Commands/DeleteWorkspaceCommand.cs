namespace Api.Application.Commands;

using System.Threading;
using System.Threading.Tasks;
using Api.Data;
using MediatR;

public record DeleteWorkspaceCommand(Guid Id) : IRequest;

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand>
{
    private readonly IApplicationDbContext db;

    public DeleteWorkspaceCommandHandler(IApplicationDbContext db) => this.db = db;

    public async Task<Unit> Handle(
        DeleteWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        var workspace = new Workspace() { Id = request.Id };
        this.db.Workspaces.Remove(workspace);
        await this.db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
