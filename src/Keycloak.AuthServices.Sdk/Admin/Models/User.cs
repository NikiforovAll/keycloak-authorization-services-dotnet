#pragma warning disable CS1591
#pragma warning disable CS8618
namespace Keycloak.AuthServices.Sdk.Admin.Models;

/// <summary>
/// User representation.
/// </summary>
public class User
{
    public Dictionary<string, string>? Access { get; init; }
    public Dictionary<string, string>? Attributes { get; init; }
    public UserConsent[]? ClientConsents { get; init; }
    public Dictionary<string, string>? ClientRoles { get; init; }
    public long? CreatedTimestamp { get; init; }
    public Credential[]? Credentials { get; init; }
    public string[]? DisableableCredentialTypes { get; init; }
    public string? Email { get; init; }
    public bool? EmailVerified { get; init; }
    public bool? Enabled { get; init; }
    public FederatedIdentity? FederatedIdentities { get; init; }
    public string? FederationLink { get; init; }
    public string? FirstName { get; init; }
    public string[]? Groups { get; init; }
    public string? Id { get; init; }
    public string? LastName { get; init; }
    public int? NotBefore { get; init; }
    public string? Origin { get; init; }
    public Dictionary<string, string>? RealmRoles { get; init; }
    public Dictionary<string, string>? RequiredActions { get; init; }
    public string? Self { get; init; }
    public string? ServiceAccountClientId { get; init; }
    public string? Username { get; init; }
}

/// <summary>
/// Credential representation.
/// </summary>
public class Credential
{
    public long? CreatedDate { get; init; }
    public string? CredentialData { get; init; }
    public string? Id { get; init; }
    public int? Priority { get; init; }
    public string? SecretData { get; init; }
    public bool? Temporary { get; init; }
    public string? Type { get; init; }
    public string? UserLabel { get; init; }
    public string? Value { get; init; }
}

/// <summary>
/// User consent representation.
/// </summary>
public class UserConsent
{
    public string? ClientId { get; init; }
    public long? CreatedDate { get; init; }
    public string? LastUpdatedDate { get; init; }
    public string[]? GrantedClientScopes { get; init; }
}

/// <summary>
/// Federated identity representation.
/// </summary>
public class FederatedIdentity
{
    public string? IdentityProvider { get; init; }
    public string? UserId { get; init; }
    public string? UserName { get; init; }
}
