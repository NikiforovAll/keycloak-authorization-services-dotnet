namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

/// <summary>
/// Provides access tokens for Keycloak authorization server requests.
/// </summary>
/// <remarks>
/// The default implementation uses <see cref="Microsoft.AspNetCore.Http.IHttpContextAccessor"/> to extract the token
/// from the current HTTP context. For SignalR or other long-lived connection scenarios,
/// implement this interface to provide fresh tokens from an alternative source.
/// </remarks>
public interface IKeycloakAccessTokenProvider
{
    /// <summary>
    /// Gets the current access token.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The access token, or <c>null</c> if no token is available.</returns>
    public Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
