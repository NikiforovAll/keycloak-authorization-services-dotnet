namespace Api.Data;
public class Workspace
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public IList<Project> Projects { get; set; } = new List<Project>();
}

public class Project
{
    public Guid Id { get; set; }

    public Guid? WorkspaceId { get; set; }

    public string Alias { get; init; } = default!;
}
