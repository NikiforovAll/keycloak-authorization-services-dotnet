namespace Keycloak.AuthServices.Sdk.Admin.Requests.Groups;

using Models;
using Refit;

/// <summary>
/// Only name and ids are returned.
/// </summary>
public class GetGroupsRequestParameters
{
    /// <summary>
    /// Defines whether brief representations are returned. Default is false.
    /// </summary>
    [AliasAs("briefRepresentation")]
    public bool? BriefRepresentation { get; init; }

    /// <summary>
    /// Pagination offset.
    /// </summary>
    [AliasAs("first")]
    public int? First { get; init; }

    
    /// <summary>
    /// Maximum results size. Default is 100.
    /// </summary>
    [AliasAs("max")]
    public int? Max { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="Group.Name"/> or <see cref="Group.Path"/>.
    /// </summary>
    [AliasAs("search")]
    public string? Search { get; init; }
}
