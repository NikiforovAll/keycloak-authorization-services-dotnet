namespace Keycloak.AuthServices.Sdk.Protection.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a single permission request entry for creating a UMA permission ticket.
/// </summary>
public class PermissionTicketRequest
{
    /// <summary>
    /// The resource ID or name to request permission for.
    /// </summary>
    [JsonPropertyName("resource_id")]
    public string ResourceId { get; set; } = default!;

    /// <summary>
    /// The resource scopes requested.
    /// </summary>
    [JsonPropertyName("resource_scopes")]
    public IList<string>? ResourceScopes { get; set; }
}
