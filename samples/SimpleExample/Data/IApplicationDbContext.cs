namespace Api.Data;

using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Workspace> Workspaces { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
