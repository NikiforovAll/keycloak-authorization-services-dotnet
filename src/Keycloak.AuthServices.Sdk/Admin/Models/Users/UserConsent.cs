#pragma warning disable CS1591, CS8618
namespace Keycloak.AuthServices.Sdk.Admin.Models.Users;

/// <summary>
/// User consent representation.
/// </summary>
public class UserConsent
{
    public string? ClientId { get; init; }
    public long? CreatedDate { get; init; }
    public long? LastUpdatedDate { get; init; }
    public string[]? GrantedClientScopes { get; init; }
}
