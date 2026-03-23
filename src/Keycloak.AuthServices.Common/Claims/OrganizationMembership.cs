namespace Keycloak.AuthServices.Common.Claims;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a user's membership in a Keycloak organization.
/// </summary>
public class OrganizationMembership
{
    /// <summary>
    /// Gets the organization alias (the key in the organization claim map).
    /// </summary>
    public required string Alias { get; init; }

    /// <summary>
    /// Gets the organization identifier, if included in the token.
    /// </summary>
    /// <remarks>
    /// Only present when the "Add organization id" option is enabled in the Organization Membership mapper.
    /// </remarks>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Gets the organization attributes, if included in the token.
    /// </summary>
    /// <remarks>
    /// Only present when the "Add organization attributes" option is enabled in the Organization Membership mapper.
    /// </remarks>
    [JsonIgnore]
    public IReadOnlyDictionary<string, string[]>? Attributes { get; init; }
}
