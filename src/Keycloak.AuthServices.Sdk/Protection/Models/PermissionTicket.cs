namespace Keycloak.AuthServices.Sdk.Protection.Models;

/// <summary>
/// Represents a stored permission ticket (for querying existing tickets).
/// </summary>
public class PermissionTicket
{
    /// <summary>
    /// The permission ticket ID.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The resource owner.
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// The resource ID.
    /// </summary>
    public string? Resource { get; set; }

    /// <summary>
    /// The scope.
    /// </summary>
    public string? Scope { get; set; }

    /// <summary>
    /// Whether the permission has been granted.
    /// </summary>
    public bool? Granted { get; set; }

    /// <summary>
    /// The requesting party.
    /// </summary>
    public string? Requester { get; set; }

    /// <summary>
    /// The scope name (populated when <c>returnNames=true</c>).
    /// </summary>
    public string? ScopeName { get; set; }

    /// <summary>
    /// The resource name (populated when <c>returnNames=true</c>).
    /// </summary>
    public string? ResourceName { get; set; }

    /// <summary>
    /// The resource owner name (populated when <c>returnNames=true</c>).
    /// </summary>
    public string? OwnerName { get; set; }

    /// <summary>
    /// The requester name (populated when <c>returnNames=true</c>).
    /// </summary>
    public string? RequesterName { get; set; }
}
