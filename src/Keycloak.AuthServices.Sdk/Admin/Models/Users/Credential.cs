#pragma warning disable CS1591, CS8618
namespace Keycloak.AuthServices.Sdk.Admin.Models.Users;

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
