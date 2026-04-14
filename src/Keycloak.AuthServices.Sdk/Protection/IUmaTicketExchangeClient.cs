namespace Keycloak.AuthServices.Sdk.Protection;

/// <summary>
/// Client for exchanging UMA permission tickets at the Keycloak token endpoint.
/// </summary>
/// <remarks>
/// Implements the UMA grant type (<c>urn:ietf:params:oauth:grant-type:uma-ticket</c>)
/// to exchange permission tickets for RPTs (Requesting Party Tokens) or to submit
/// permission requests for resource owner approval.
/// </remarks>
public interface IUmaTicketExchangeClient
{
    /// <summary>
    /// Exchanges a UMA permission ticket for a Requesting Party Token (RPT).
    /// </summary>
    /// <param name="accessToken">The bearer access token of the requesting party.</param>
    /// <param name="ticket">The permission ticket received from the resource server's UMA challenge.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The RPT access token string, or <c>null</c> if the exchange failed.</returns>
    public Task<string?> ExchangeTicketForRptAsync(
        string accessToken,
        string ticket,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Submits a permission request for resource owner approval.
    /// </summary>
    /// <remarks>
    /// Uses the <c>submit_request=true</c> parameter in the UMA ticket exchange.
    /// When access is denied, Keycloak persists a pending permission request that
    /// the resource owner can approve.
    /// </remarks>
    /// <param name="accessToken">The bearer access token of the requesting party.</param>
    /// <param name="ticket">The permission ticket received from the resource server's UMA challenge.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><c>true</c> if the permission request was submitted for owner approval; otherwise <c>false</c>.</returns>
    public Task<bool> SubmitPermissionRequestAsync(
        string accessToken,
        string ticket,
        CancellationToken cancellationToken = default
    );
}
