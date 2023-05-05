namespace Keycloak.AuthServices.Sdk.Admin.Models.Groups;

/// <summary>
/// Group representation
/// </summary>
public class Group
{
    public Dictionary<string, bool>? Access { get; init; }
    public Dictionary<string, string[]>? Attributes { get; init; }
    public Dictionary<string, string>? ClientRoles { get; init; }
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Path { get; init; }
    public string[]? RealmRoles { get; init; }
    public Group[]? SubGroups { get; set; }
}
