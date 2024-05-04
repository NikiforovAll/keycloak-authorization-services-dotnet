namespace Keycloak.AuthServices.Sdk;

using System.Text.Json.Serialization;

/// <summary>
/// TBD:
/// </summary>
public sealed record ErrorResponse
{
    /// <summary>
    /// TBD:
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; init; } = default!;

    /// <summary>
    /// TBD:
    /// </summary>
    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; init; } = default!;
}
