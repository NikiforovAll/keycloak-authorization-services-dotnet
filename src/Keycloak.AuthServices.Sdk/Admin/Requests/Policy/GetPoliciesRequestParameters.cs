namespace Keycloak.AuthServices.Sdk.Admin.Requests.Policy;

using AuthZ;
using Refit;

/// <summary>
/// This is used to send requests to the Protection API via the <see cref="IKeycloakPolicyClient"/>
/// </summary>
public class GetPoliciesRequestParameters
{
    /// <summary>
    /// Query based on the permission name
    /// </summary>
    [AliasAs("name")]
    public string? PermissionName { get; init; }

    /// <summary>
    /// Query based on the resource Id
    /// </summary>
    [AliasAs("resource")]
    public string? ResourceId { get; init; }
    
    /// <summary>
    /// Query based on the scope
    /// </summary>
    [AliasAs("scope")]
    public string? Scope { get; init; }

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
}
