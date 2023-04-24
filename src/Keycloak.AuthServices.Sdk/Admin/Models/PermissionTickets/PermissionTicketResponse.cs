namespace Keycloak.AuthServices.Sdk.Admin.Models.PermissionTickets;

using System.Text.Json.Serialization;

/// <summary>
/// The permission ticket response contains token values
/// </summary>
public class PermissionTicketResponse
{
    // TODO: fill in with proper values--this is just a regular token and not the permission ticket response

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
    [JsonPropertyName("expires_in")]
    public long? ExpiresIn { get; set; }
    [JsonPropertyName("refresh_expires_in")]
    public long? RefreshExpiresIn { get; set; }
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
    [JsonPropertyName("not-before-policy")]
    public long? NotBeforePolicy { get; set; }
    public string? Scope { get; set; }
}
