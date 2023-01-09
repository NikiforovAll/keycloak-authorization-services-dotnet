namespace Keycloak.AuthServices.Sdk.Admin;

using Constants;
using Models;
using Refit;

/// <summary>
/// User management
/// </summary>
public interface IKeycloakUserClient
{
    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <remarks>
    /// Username must be unique.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="user">User representation.</param>
    /// <returns></returns>
    [Post(KeycloakClientApiConstants.CreateUser)]
    [Headers("Accept: application/json", "Content-Type: application/json")]
    Task CreateUser(string realm, [Body] User user);

    /// <summary>
    /// Send an email-verification email to the user.
    /// </summary>
    /// <remarks>
    /// An email contains a link the user can click to verify their email address.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="clientId">Client ID.</param>
    /// <param name="redirectUri">Redirect URI. The default for the redirect is the account client.</param>
    /// <returns></returns>
    [Put(KeycloakClientApiConstants.SendVerifyEmail)]
    [Headers("Accept: application/json", "Content-Type: application/json")]
    Task SendVerifyEmail(string realm, string userId,
        [Query][AliasAs("client_id")] string? clientId = default,
        [Query][AliasAs("redirect_uri")] string? redirectUri = default);
}
