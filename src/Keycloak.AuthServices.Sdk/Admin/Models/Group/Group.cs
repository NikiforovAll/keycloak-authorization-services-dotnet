#pragma warning disable CS1591, CS8618
namespace Keycloak.AuthServices.Sdk.Admin.Models;

using System.Collections.Generic;

/// <summary>
/// Group representation.
/// </summary>
public class Group
{
    public string Id { get; init; } = default!;
    public string? Name { get; init; }
    public string? Path { get; init; }
    public Dictionary<string, string>? ClientRoles { get; init; }
    public string[]? RealmRoles { get; init; }
    public Group[]? SubGroups { get; init; }
    public Dictionary<string, string[]>? Attributes { get; init; }
}
