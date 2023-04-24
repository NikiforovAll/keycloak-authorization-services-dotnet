namespace Keycloak.AuthServices.Sdk.Admin.Requests.Permissions;

using AuthZ;
using Refit;

/// <summary>
/// This is used to send requests to the Protection API via the <see cref="IKeycloakPermissionTicketClient"/>
/// </summary>
public class GetPermissionTicketsRequestParameters
{
    /// <summary>
    /// Search for permission tickets with a specific scope ID.
    /// </summary>
    [AliasAs("scopeId")]
    public string? ScopeId { get; init; }

    /// <summary>
    /// Search for permission tickets issued for a specific resource ID.
    /// </summary>
    [AliasAs("resourceId")]
    public string? ResourceId { get; init; }

    /// <summary>
    /// Search for permission tickets issued by a specific owner.
    /// </summary>
    [AliasAs("owner")]
    public string? Owner { get; init; }

    /// <summary>
    /// Search for permission tickets issued to a specific requester.
    /// </summary>
    [AliasAs("requester")]
    public string? Requester { get; init; }

    /// <summary>
    /// Search for permission tickets based on granted status.
    /// </summary>
    [AliasAs("granted")]
    public bool? Granted { get; init; }

    /// <summary>
    /// Defines whether names are returned. Default is false.
    /// </summary>
    [AliasAs("returnNames")]
    public bool? ReturnNames { get; init; }

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
