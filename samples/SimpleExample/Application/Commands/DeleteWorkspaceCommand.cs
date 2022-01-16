namespace Api.Application.Commands;

using System.Threading;
using System.Threading.Tasks;
using Data;
using Authorization;
using Authorization.Abstractions;
using Keycloak.AuthServices.Authorization;
using MediatR;

public record DeleteWorkspaceCommand(Guid Id) : IRequest;

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand>
{
    private readonly IApplicationDbContext db;
    private readonly IIdentityService identityService;

    public DeleteWorkspaceCommandHandler(IApplicationDbContext db, IIdentityService identityService)
    {
        this.db = db;
        this.identityService = identityService;
    }

    public async Task<Unit> Handle(
        DeleteWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        var authorized = await this.identityService.AuthorizeAsync(
            ProtectedResourcePolicy.From("workspaces", request.Id.ToString(), "read"));

        if (!authorized)
        {
            throw new ForbiddenAccessException();
        }
        var workspace = new Workspace() { Id = request.Id };
        this.db.Workspaces.Remove(workspace);
        await this.db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
