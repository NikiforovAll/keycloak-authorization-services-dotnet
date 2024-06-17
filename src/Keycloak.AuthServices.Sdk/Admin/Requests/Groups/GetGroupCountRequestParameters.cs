namespace Keycloak.AuthServices.Sdk.Admin.Requests.Groups;

/// <summary>
/// Optional request parameters for the <see cref="IKeycloakClient.GetGroupCountAsync"/> endpoint.
/// </summary>
public class GetGroupCountRequestParameters
{
    /// <summary>
    /// A search query for the groups that should be counted
    /// </summary>
    public string? Search { get; init; }

    /// <summary>
    /// Whether the top groups should be returned
    /// </summary>
    public bool? Top { get; init; }
}
