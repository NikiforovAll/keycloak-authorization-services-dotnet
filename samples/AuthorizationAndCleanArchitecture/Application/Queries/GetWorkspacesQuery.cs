namespace Api.Application.Queries;

using System.Threading;
using System.Threading.Tasks;
using Data;
using Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

[AuthorizeProtectedResource("workspaces", "workspaces:read")]
public record GetWorkspacesQuery : IRequest<IEnumerable<Workspace>>;

public class GetWorkspacesQueryHandler : IRequestHandler<GetWorkspacesQuery, IEnumerable<Workspace>>
{
    private readonly IApplicationDbContext db;

    public GetWorkspacesQueryHandler(IApplicationDbContext db) => this.db = db;

    public async Task<IEnumerable<Workspace>> Handle(
        GetWorkspacesQuery request,
        CancellationToken cancellationToken) =>
            await this.db
                .Workspaces
                .Include(w => w.Projects)
                .AsNoTracking().ToListAsync(cancellationToken);
}
