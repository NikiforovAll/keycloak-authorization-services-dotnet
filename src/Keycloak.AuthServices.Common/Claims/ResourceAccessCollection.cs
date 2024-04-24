namespace Keycloak.AuthServices.Common.Claims;

using System.Text.Json.Serialization;

/// <summary>
/// </summary>
public class ResourceAccessCollection : Dictionary<string, ResourceAccess>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceAccessCollection"/> class.
    /// </summary>
    public ResourceAccessCollection()
        : base(StringComparer.OrdinalIgnoreCase) { }
}

/// <summary>
/// </summary>
public class ResourceAccess
{
    /// <summary>
    /// Represents a collection of resource access roles.
    /// </summary>
    [JsonPropertyName("roles")]
    public List<string> Roles { get; init; } = new();
}
