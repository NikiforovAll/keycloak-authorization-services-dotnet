namespace Keycloak.AuthServices.Sdk.Protection.Models;

using System.Text.Json.Serialization;
using Keycloak.AuthServices.Sdk.Admin.Models;

/// <summary>
/// Represents a response containing information about a resource.
/// </summary>
public class ResourceResponse
{
    /// <summary>
    /// Gets or sets the ID of the resource.
    /// </summary>
    [JsonPropertyName("_id")]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name of the resource.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the resource.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the owner of the resource.
    /// </summary>
    public Owner? Owner { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the access to the resource is managed by the owner.
    /// </summary>
    public bool? OwnerManagedAccess { get; set; }

    /// <summary>
    /// Gets or sets the display name of the resource.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the attributes associated with the resource.
    /// </summary>
    public Dictionary<string, List<string>?>? Attributes { get; set; }

    /// <summary>
    /// Gets or sets the URIs associated with the resource.
    /// </summary>
    public List<string>? Uris { get; set; }

    /// <summary>
    /// Gets or sets the resource scopes associated with the resource.
    /// </summary>
    [JsonPropertyName("resource_scopes")]
    public List<ResourceScope>? ResourceScopes { get; set; }

    /// <summary>
    /// Gets or sets the scopes associated with the resource.
    /// </summary>
    public List<Scope>? Scopes { get; set; }
}

/// <summary>
/// Represents a resource scope.
/// </summary>
public class ResourceScope
{
    /// <summary>
    /// Gets or sets the name of the resource scope.
    /// </summary>
    public string Name { get; set; } = default!;
}

/// <summary>
/// Represents a scope.
/// </summary>
public class Scope
{
    /// <summary>
    /// Gets or sets the name of the scope.
    /// </summary>
    public string Name { get; set; } = default!;
}
