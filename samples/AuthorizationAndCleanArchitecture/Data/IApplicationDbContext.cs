namespace Api.Data;

using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    public DbSet<Workspace> Workspaces { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
