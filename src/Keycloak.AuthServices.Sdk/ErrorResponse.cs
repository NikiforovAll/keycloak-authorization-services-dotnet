namespace Keycloak.AuthServices.Sdk;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an error response from Keycloak.
/// </summary>
public sealed record ErrorResponse
{
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; init; } = default!;

    /// <summary>
    /// Gets or sets the error description.
    /// </summary>
    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; init; } = default!;
}
