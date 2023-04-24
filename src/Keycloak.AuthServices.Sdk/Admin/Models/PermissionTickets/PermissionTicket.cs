namespace Keycloak.AuthServices.Sdk.Admin.Models.PermissionTickets;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// The permission ticket is used to grant access to a specific resource for a specific purpose to a specific user, group, role, etc.
/// Other claims may be provided in the request which will ensure they are available in the response.
/// </summary>
public class PermissionTicket
{
    /// <summary>
    /// Create a permission ticket for the given requester against the given resource.
    /// </summary>
    /// <param name="resourceId">The Id of the <see cref="Resource"/> to grant permissions to</param>
    /// <param name="requester">The user Id to grant the permissions to</param>
    public PermissionTicket(string resourceId, string requester)
    {
        this.ResourceId = resourceId;
        this.Requester = requester;
    }

    [JsonPropertyName("resource_id")] public string? ResourceId { get; }
    [JsonPropertyName("resource_scopes")] public List<ResourceScope> ResourceScopes { get; init; } = new();
    public string? Requester { get; init; } // user ID
    public string? ScopeName { get; init; }
    public bool? Granted { get; init; }
    public Dictionary<string, List<string>> Claims { get; init; } = new();
    public string[]? Scopes { get; init; }
    public string[]? Groups { get; init; }
    public string[]? Roles { get; init; }
}
