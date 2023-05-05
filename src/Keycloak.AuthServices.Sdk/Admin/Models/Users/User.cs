#pragma warning disable CS1591, CS8618
namespace Keycloak.AuthServices.Sdk.Admin.Models.Users;

/// <summary>
/// User representation.
/// </summary>
public class User
{
    public Dictionary<string, bool>? Access { get; init; }
    public Dictionary<string, string[]>? Attributes { get; init; }
    public UserConsent[]? ClientConsents { get; init; }
    public Dictionary<string, string>? ClientRoles { get; init; }
    public long? CreatedTimestamp { get; init; }
    public Credential[]? Credentials { get; init; }
    public string[]? DisableableCredentialTypes { get; init; }
    public string? Email { get; init; }
    public bool? EmailVerified { get; init; }
    public bool? Enabled { get; init; }
    public FederatedIdentity[]? FederatedIdentities { get; init; }
    public string? FederationLink { get; init; }
    public string? FirstName { get; init; }
    public string[]? Groups { get; set; }
    public string? Id { get; init; }
    public string? LastName { get; init; }
    public int? NotBefore { get; init; }
    public string? Origin { get; init; }
    public string[]? RealmRoles { get; init; }
    public string[]? RequiredActions { get; init; }
    public string? Self { get; init; }
    public string? ServiceAccountClientId { get; init; }
    public string? Username { get; init; }
    public bool? Totp { get; init; }
}
