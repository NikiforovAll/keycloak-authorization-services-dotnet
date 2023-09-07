namespace Keycloak.AuthServices.Sdk.Admin.Models.Tokens;

using System.Text.Json.Serialization;

/// <summary>
/// A token response 
/// </summary>
public class TokenResponse
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("upgraded")]
    public bool Upgraded { get; init; }

    /// <summary>
    /// The access token for immediate use
    /// </summary>
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; init; }

    /// <summary>
    /// The refresh token
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; init; }

    /// <summary>
    /// The expiry of the access token
    /// </summary>
    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; init; }

    /// <summary>
    /// The expiry of the refresh token
    /// </summary>
    [JsonPropertyName("refresh_expires_in")]
    public long RefreshExpiresIn { get; init; }

    /// <summary>
    /// The token type which should typically be 'Bearer'
    /// </summary>
    [JsonPropertyName("token_type")]
    public string? TokenType { get; init; }

    /// <summary>
    /// The not-before-policy of the token response
    /// </summary>
    [JsonPropertyName("not-before-policy")]
    public long NotBeforePolicy { get; init; }
}