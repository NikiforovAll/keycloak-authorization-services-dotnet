#pragma warning disable CS1591, CS8618
namespace Keycloak.AuthServices.Sdk.Admin.Models.Users;

/// <summary>
/// Federated identity representation.
/// </summary>
public class FederatedIdentity
{
    public string? IdentityProvider { get; init; }
    public string? UserId { get; init; }
    public string? UserName { get; init; }
}
