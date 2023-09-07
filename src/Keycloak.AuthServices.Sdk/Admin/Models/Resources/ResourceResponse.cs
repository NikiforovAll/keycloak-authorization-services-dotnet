#pragma warning disable CS1591, CS8618
namespace Keycloak.AuthServices.Sdk.Admin.Models.Resources;

using System.Text.Json.Serialization;

/// <summary>
/// </summary>
public class ResourceResponse
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Owner Owner { get; set; }
    public bool OwnerManagedAccess { get; set; }
    public string DisplayName { get; set; }
    public Dictionary<string, List<string>> Attributes { get; set; }
    public List<string> Uris { get; set; }
    [JsonPropertyName("resource_scopes")]
    public List<ResourceScope> ResourceScopes { get; set; }
    public List<Scope> Scopes { get; set; }
}
