namespace Api.Application.Commands;

using System.Threading;
using System.Threading.Tasks;
using Data;
using Authorization;
using MediatR;

[AuthorizeProtectedResource(
    "workspaces", "workspaces:delete",
    ResourceAuthorizationMode.ResourceFromRequest)]
public record DeleteWorkspaceCommandAOP(Guid Id) : IRequest, IRequestWithResourceId
{
    public string ResourceId => this.Id.ToString();
}

public class DeleteWorkspaceCommandAOPHandler : IRequestHandler<DeleteWorkspaceCommandAOP>
{
    private readonly IApplicationDbContext db;

    public DeleteWorkspaceCommandAOPHandler(IApplicationDbContext db) =>
        this.db = db;

    public async Task<Unit> Handle(
        DeleteWorkspaceCommandAOP request,
        CancellationToken cancellationToken)
    {
        var workspace = new Workspace() {Id = request.Id};
        this.db.Workspaces.Remove(workspace);
        await this.db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
