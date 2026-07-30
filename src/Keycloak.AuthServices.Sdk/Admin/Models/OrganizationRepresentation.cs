namespace Keycloak.AuthServices.Sdk.Admin.Models;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Representation of a Keycloak organization.
/// </summary>
public class OrganizationRepresentation
{
    /// <summary>Unique identifier of the organization.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>Display name of the organization.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>URL-friendly alias for the organization.</summary>
    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    /// <summary>Whether the organization is enabled.</summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>Optional description.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>Optional redirect URL used during organization onboarding flows.</summary>
    [JsonPropertyName("redirectUrl")]
    public string? RedirectUrl { get; set; }

    /// <summary>Free-form attributes.</summary>
    [JsonPropertyName("attributes")]
    public IDictionary<string, IEnumerable<string>>? Attributes { get; set; }

    /// <summary>Domains associated with the organization.</summary>
    [JsonPropertyName("domains")]
    public IEnumerable<OrganizationDomainRepresentation>? Domains { get; set; }
}