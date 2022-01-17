namespace Keycloak.AuthServices.Sdk.Admin.Models;

using System.Text.Json.Serialization;

public class Resource
{
    public Resource(string name, string[] scopes)
    {
        this.Name = name;
        this.Scopes = scopes;
    }

    public string Name { get; }

    public string? DisplayName { get; init; }
    public string? Type { get; init; }

    [JsonPropertyName("resource_scopes")] public string[] Scopes { get; }

    public Dictionary<string, string> Attributes { get; init; } = new Dictionary<string, string>();
}
