namespace Keycloak.AuthServices.Common;

using System.Text.Json.Serialization;

/// <summary>
/// A collection of <see cref="ResourcePermission"/> objects. These are normally found in the "authorization"
/// claim of any user who has been issued a UMA ticket.
/// </summary>
/// <remarks>This and the <see cref="ResourcePermission"/> class are used when dealing with access tokens.
/// Related documentation:
/// <list type="bullet">
/// <item><see href="https://www.keycloak.org/docs-api/21.1.1/rest-api/index.html#_accesstoken"/></item>
/// <item><see href="https://www.keycloak.org/docs-api/21.1.1/rest-api/index.html#_accesstoken-authorization"/></item>
/// <item><see href="https://www.keycloak.org/docs-api/21.1.1/rest-api/index.html#_permission" /></item>
/// </list>
/// </remarks>
public class AuthorizationCollection
{
    /// <summary>
    /// The list of resource permissions within the authorization data
    /// </summary>
    [JsonPropertyName("permissions")]
    public List<ResourcePermission> Permissions { get; init; } = new();
}

/// <summary>
/// See the Keycloak documentation for details: https://www.keycloak.org/docs-api/21.1.1/rest-api/index.html#_permission
/// </summary>
public class ResourcePermission
{
    /// <summary>
    /// The list of scopes for the resource
    /// </summary>
    [JsonPropertyName("scopes")]
    public List<string> Scopes { get; init; } = new();

    /// <summary>
    /// The resource ID to which the permission is associated
    /// </summary>
    [JsonPropertyName("rsid")]
    public string? ResourceId { get; init; }

    /// <summary>
    /// The resource name to which the permission is associated. 
    /// </summary>
    [JsonPropertyName("rsname")]
    public string? ResourceName { get; init; }
}
