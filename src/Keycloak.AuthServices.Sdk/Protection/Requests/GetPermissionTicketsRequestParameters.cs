namespace Keycloak.AuthServices.Sdk.Protection.Requests;

/// <summary>
/// This is used to send requests to the Protection API via the <see cref="IKeycloakPermissionClient"/>
/// </summary>
public class GetPermissionTicketsRequestParameters
{
    /// <summary>
    /// Filter by granted status.
    /// </summary>
    public bool? Granted { get; init; }

    /// <summary>
    /// Filter by requesting party user ID.
    /// </summary>
    public string? Requester { get; init; }

    /// <summary>
    /// Filter by resource ID.
    /// </summary>
    public string? ResourceId { get; init; }

    /// <summary>
    /// Filter by scope ID.
    /// </summary>
    public string? ScopeId { get; init; }

    /// <summary>
    /// Filter by resource owner user ID.
    /// </summary>
    public string? Owner { get; init; }

    /// <summary>
    /// Include human-readable names in the response.
    /// </summary>
    public bool? ReturnNames { get; init; }

    /// <summary>
    /// Pagination offset.
    /// </summary>
    public int? First { get; init; }

    /// <summary>
    /// Maximum results size. Default is 100.
    /// </summary>
    public int? Max { get; init; }
}
