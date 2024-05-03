namespace Keycloak.AuthServices.Sdk.Admin.Requests.Groups;

using Keycloak.AuthServices.Sdk.Admin.Models;

/// <summary>
/// Optional parameters for the <see cref="IKeycloakGroupClient.GetGroupsAsync"/> endpoint.
/// </summary>
public class GetGroupsRequestParameters
{
    /// <summary>
    /// Defines whether brief representations are returned. Default is false.
    /// </summary>
    public bool? BriefRepresentation { get; init; }

    /// <summary>
    /// Pagination offset.
    /// </summary>
    public int? First { get; init; }

    /// <summary>
    /// Whether to match the search exactly or not
    /// </summary>
    public bool? Exact { get; init; }

    /// <summary>
    /// Maximum results size. Default is 100.
    /// </summary>
    public int? Max { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="GroupRepresentation.Name"/> or <see cref="GroupRepresentation.Path"/>.
    /// </summary>
    public string? Search { get; init; }
}
