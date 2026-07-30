namespace Keycloak.AuthServices.Sdk.Admin.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Representation of a user's membership in a Keycloak organization.
/// </summary>
public class OrganizationMemberRepresentation
{
    /// <summary>User id (matches the id on <c>UserRepresentation</c>).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>Username.</summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>Email address.</summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>Given name.</summary>
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    /// <summary>Family name.</summary>
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    /// <summary>Whether the user account is enabled.</summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// Type of membership. Keycloak defines <c>MANAGED</c> and <c>UNMANAGED</c> —
    /// managed users are auto-created and are deleted when they leave the organization.
    /// </summary>
    [JsonPropertyName("membershipType")]
    public string? MembershipType { get; set; }
}
