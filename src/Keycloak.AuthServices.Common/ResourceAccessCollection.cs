namespace Keycloak.AuthServices.Common;

using System.Text.Json.Serialization;

/// <summary>
/// </summary>
public class ResourceAccessCollection : Dictionary<string, ResourceAccess>
{
    /// <summary>
    /// </summary>
    public ResourceAccessCollection()
        : base(StringComparer.OrdinalIgnoreCase)
    {
    }
}

/// <summary>
/// </summary>
public class ResourceAccess
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("roles")]
    public List<string> Roles { get; init; } = new();
}

