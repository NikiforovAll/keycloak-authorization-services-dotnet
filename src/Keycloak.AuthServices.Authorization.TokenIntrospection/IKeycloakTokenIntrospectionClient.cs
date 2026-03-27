namespace Keycloak.AuthServices.Authorization.TokenIntrospection;

using System.Text.Json;

/// <summary>
/// Client for Keycloak token introspection endpoint.
/// Resolves the full claim set for lightweight access tokens.
/// </summary>
public interface IKeycloakTokenIntrospectionClient
{
    /// <summary>
    /// Introspects the given access token and returns the full claim set.
    /// </summary>
    /// <param name="token">The access token to introspect.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The introspection response, or null if the request failed.</returns>
    Task<TokenIntrospectionResponse?> IntrospectTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
/// Represents the response from the Keycloak token introspection endpoint.
/// </summary>
public sealed class TokenIntrospectionResponse
{
    /// <summary>
    /// Whether the token is active (valid).
    /// </summary>
    public bool Active { get; init; }

    /// <summary>
    /// The full set of claims from the introspection response.
    /// Keys are claim names, values are the raw JSON elements.
    /// </summary>
    public Dictionary<string, JsonElement> Claims { get; init; } = new();
}
