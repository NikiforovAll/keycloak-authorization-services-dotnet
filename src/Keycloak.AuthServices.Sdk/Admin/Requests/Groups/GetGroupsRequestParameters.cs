namespace Keycloak.AuthServices.Sdk.Admin.Requests.Groups;

using Refit;

/// <summary>
/// Optional parameters for the <see cref="IKeycloakGroupClient.GetGroups"/> endpoint.
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
    /// Whether to match the search exactly or not
    /// </summary>
    [AliasAs("exact")]
    public bool? Exact { get; init; }
    
    /// <summary>
    /// Maximum results size. Default is 100.
    /// </summary>
    [AliasAs("max")]
    public int? Max { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="Models.Groups.Group.Name"/> or <see cref="Models.Groups.Group.Path"/>.
    /// </summary>
    [AliasAs("search")]
    public string? Search { get; init; }
}
