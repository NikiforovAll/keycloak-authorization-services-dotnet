namespace Keycloak.AuthServices.Common;

using System.Text.Json.Serialization;

public class ResourceAccessCollection : Dictionary<string, ResourceAccess>
{
    public ResourceAccessCollection()
        : base(StringComparer.OrdinalIgnoreCase)
    {
    }
}

public class ResourceAccess
{
    [JsonPropertyName("roles")]
    public List<string> Roles { get; init; } = new();
}

