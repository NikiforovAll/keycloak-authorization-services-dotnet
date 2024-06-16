namespace Keycloak.AuthServices.Sdk.Admin.Requests.Groups;

/// <summary>
/// Optional request parameters for the <see cref="IKeycloakClient.GetGroupMembersAsync"/> endpoint.
/// </summary>
public class GetGroupMembersRequestParameters
{
    /// <summary>
    /// Defines whether brief representations are returned. Default is false.
    /// </summary>
    public bool? BriefRepresentation { get; init; }

    /// <summary>
    /// The pagination offset. Default is 0.
    /// </summary>
    public int? First { get; init; }

    /// <summary>
    /// The maximum results size. Default is 100.
    /// </summary>
    public int? Max { get; init; }
}
