namespace Api.Application.Queries;

using System.Threading;
using System.Threading.Tasks;
using Authorization;
using Authorization.Abstractions;
using Data;
using Keycloak.AuthServices.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record GetWorkspaceByIdQuery(Guid Id) : IRequest<Workspace>;

public class GetWorkspaceByIdQueryHandler : IRequestHandler<GetWorkspaceByIdQuery, Workspace>
{
    private readonly IApplicationDbContext db;
    private readonly IIdentityService identityService;

    public GetWorkspaceByIdQueryHandler(
        IApplicationDbContext db, IIdentityService identityService)
    {
        this.db = db;
        this.identityService = identityService;
    }

    public async Task<Workspace> Handle(
        GetWorkspaceByIdQuery request,
        CancellationToken cancellationToken)
    {
        var authorized = await this.identityService.AuthorizeAsync(
            ProtectedResourcePolicy.From("workspaces", request.Id.ToString(), "workspaces:read"));

        if (!authorized)
        {
            throw new ForbiddenAccessException();
        }

        var workspace = await this.db
            .Workspaces
            .Include(w => w.Projects)
            .FirstAsync(x => x.Id == request.Id, cancellationToken);

        return workspace;
    }
}
