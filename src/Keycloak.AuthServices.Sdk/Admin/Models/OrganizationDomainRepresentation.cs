namespace Keycloak.AuthServices.Sdk.Admin.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Representation of a domain associated with a Keycloak organization.
/// </summary>
public class OrganizationDomainRepresentation
{
    /// <summary>The domain name (e.g. <c>example.com</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>Whether the domain has been verified.</summary>
    [JsonPropertyName("verified")]
    public bool Verified { get; set; }
}