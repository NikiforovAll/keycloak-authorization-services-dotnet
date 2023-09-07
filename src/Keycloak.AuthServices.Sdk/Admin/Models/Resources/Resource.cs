namespace Keycloak.AuthServices.Sdk.Admin.Models.Resources;

using System.Text.Json.Serialization;

/// <summary>
/// </summary>
public class Resource
{
    /// <summary>
    /// Constructs resource
    /// </summary>
    /// <param name="name">Name of the resource</param>
    /// <param name="scopes">Scope of the resource. Usually, it is in a form of verb </param>
    public Resource(string name, string[] scopes)
    {
        this.Name = name;
        this.Scopes = scopes;
    }

    /// <summary>
    /// Resource name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Display name
    /// </summary>
    public string? DisplayName { get; init; }

    /// <summary>
    /// Resource type
    /// </summary>
    /// <example>urn:workspace-authz:resource:workspaces</example>
    public string? Type { get; init; }

    /// <summary>
    /// Resource scopes
    /// </summary>
    [JsonPropertyName("resource_scopes")] public string[] Scopes { get; }

    /// <summary>
    /// Resource attributes
    /// </summary>
    public Dictionary<string, string> Attributes { get; init; } = new Dictionary<string, string>();
     
    /// <summary> 
    /// When set, will be used to enforce ownership at the time of the request 
    /// </summary> 
    public string? Owner { get; set; } 
 
    /// <summary> 
    /// When set, will enforce owner-managed-access. 
    /// </summary> 
    public bool? OwnerManagedAccess { get; set; } 
}
