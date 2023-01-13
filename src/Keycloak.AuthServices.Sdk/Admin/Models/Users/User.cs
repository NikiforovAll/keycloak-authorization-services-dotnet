#pragma warning disable CS1591, CS8618
namespace Keycloak.AuthServices.Sdk.Admin.Models;

/// <summary>
/// User representation.
/// </summary>
public class User
{
    public Dictionary<string, bool>? Access { get; set; }
    public Dictionary<string, string[]>? Attributes { get; set; }
    public UserConsent[]? ClientConsents { get; set; }
    public Dictionary<string, string>? ClientRoles { get; set; }
    public long? CreatedTimestamp { get; set; }
    public Credential[]? Credentials { get; set; }
    public string[]? DisableableCredentialTypes { get; set; }
    public string? Email { get; set; }
    public bool? EmailVerified { get; set; }
    public bool? Enabled { get; set; }
    public FederatedIdentity[]? FederatedIdentities { get; set; }
    public string? FederationLink { get; set; }
    public string? FirstName { get; set; }
    public string[]? Groups { get; set; }
    public string? Id { get; set; }
    public string? LastName { get; set; }
    public int? NotBefore { get; set; }
    public string? Origin { get; set; }
    public string[]? RealmRoles { get; set; }
    public string[]? RequiredActions { get; set; }
    public string? Self { get; set; }
    public string? ServiceAccountClientId { get; set; }
    public string? Username { get; set; }
    public bool? Totp { get; set; }
}
