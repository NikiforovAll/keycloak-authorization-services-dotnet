namespace Api.Application.Queries;

using System.Threading;
using System.Threading.Tasks;
using Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record GetWorkspaceByIdQuery(Guid Id) : IRequest<Workspace>;

public class GetWorkspaceByIdQueryHandler : IRequestHandler<GetWorkspaceByIdQuery, Workspace>
{
    private readonly IApplicationDbContext db;

    public GetWorkspaceByIdQueryHandler(IApplicationDbContext db) => this.db = db;

    public async Task<Workspace> Handle(
        GetWorkspaceByIdQuery request,
        CancellationToken cancellationToken) =>
            await this.db
                .Workspaces
                .Include(w => w.Projects)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
}
