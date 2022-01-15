namespace Api;

using Api.Data;

public static class SeedDatabase
{
    public static void Run(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Workspaces.Any())
        {
            return;
        }
        context.Workspaces.AddRange(new()
        {
            Id = Guid.NewGuid(),
            Name = "Default Workspace",
            Projects = new List<Project>()
            {
                new() { Id = Guid.NewGuid(), Alias = "Project One" },
                new() { Id = Guid.NewGuid(), Alias = "Project Two" },
            }
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Extra Workspace",
            Projects = new List<Project>()
            {
                new() { Id = Guid.NewGuid(), Alias = "Project Three" },
            }
        });

        context.SaveChanges();
    }
}
