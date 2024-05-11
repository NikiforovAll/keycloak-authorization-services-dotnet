namespace Keycloak.AuthServices.Sdk.Protection.Requests;

/// <summary>
/// This is used to send requests to the Protection API via the <see cref="IKeycloakPolicyClient"/>
/// </summary>
public class GetPoliciesRequestParameters
{
    /// <summary>
    /// Query based on the permission name
    /// </summary>
    public string? PermissionName { get; init; }

    /// <summary>
    /// Query based on the resource Id
    /// </summary>
    public string? ResourceId { get; init; }

    /// <summary>
    /// Query based on the scope
    /// </summary>
    public string? Scope { get; init; }

    /// <summary>
    /// Pagination offset.
    /// </summary>
    public int? First { get; init; }

    /// <summary>
    /// Maximum results size. Default is 100.
    /// </summary>
    public int? Max { get; init; }
}
