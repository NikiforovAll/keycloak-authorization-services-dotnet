namespace Keycloak.AuthServices.Sdk.Admin;

using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Users;

/// <summary>
/// User management
/// </summary>
public interface IKeycloakUserClient
{
    /// <summary>
    /// Get a stream of users on the realm.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A stream of users, filtered according to query parameters.</returns>
    async Task<IEnumerable<UserRepresentation>> GetUsersAsync(
        string realm,
        GetUsersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetUsersWithResponseAsync(realm, parameters, cancellationToken);

        return (
            await response.GetResponseAsync<IEnumerable<UserRepresentation>>(cancellationToken)
        )!;
    }

    /// <summary>
    /// Get a stream of users on the realm.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A stream of users, filtered according to query parameters.</returns>
    Task<HttpResponseMessage> GetUsersWithResponseAsync(
        string realm,
        GetUsersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get representation of a user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="includeUserProfileMetadata"> Indicates if the user profile metadata should be added to the response.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The user representation.</returns>
    async Task<UserRepresentation> GetUserAsync(
        string realm,
        string userId,
        bool includeUserProfileMetadata = false,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetUserWithResponseAsync(
            realm,
            userId,
            includeUserProfileMetadata,
            cancellationToken
        );

        return (await response.GetResponseAsync<UserRepresentation>(cancellationToken))!;
    }

    // <summary>
    /// Get representation of a user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="includeUserProfileMetadata"> Indicates if the user profile metadata should be added to the response.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The user representation.</returns>
    Task<HttpResponseMessage> GetUserWithResponseAsync(
        string realm,
        string userId,
        bool includeUserProfileMetadata = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <remarks>
    /// Username must be unique.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="user">User representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task CreateUserAsync(
        string realm,
        UserRepresentation user,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.CreateUserWithResponseAsync(realm, user, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <remarks>
    /// Username must be unique.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="user">User representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> CreateUserWithResponseAsync(
        string realm,
        UserRepresentation user,
        CancellationToken cancellationToken = default
    );

    // /// <summary>
    // /// Update the user.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="userId">User ID.</param>
    // /// <param name="user">User representation.</param>
    // /// <returns></returns>
    // Task UpdateUser(string realm, string userId, User user);

    // /// <summary>
    // /// Delete the given user.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="userId">User ID.</param>
    // /// <returns></returns>
    // Task DeleteUser(string realm, string userId);

    // /// <summary>
    // /// Send an email-verification email to the user.
    // /// </summary>
    // /// <remarks>
    // /// An email contains a link the user can click to verify their email address.
    // /// </remarks>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="userId">User ID.</param>
    // /// <param name="clientId">Client ID.</param>
    // /// <param name="redirectUri">Redirect URI. The default for the redirect is the account client.</param>
    // Task SendVerifyEmail(
    //     string realm,
    //     string userId,
    //     string? clientId = default,
    //     string? redirectUri = default
    // );

    // /// <summary>
    // /// Execute actions email for the user.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="userId">User ID.</param>
    // /// <param name="clientId">Client ID.</param>
    // /// <param name="lifespan">Number of seconds after which the generated token expires</param>
    // /// <param name="redirectUri">Redirect URI. The default for the redirect is the account client.</param>
    // /// <param name="actions">Actions</param>
    // Task ExecuteActionsEmail(
    //     string realm,
    //     string userId,
    //     string? clientId = default,
    //     int? lifespan = default,
    //     string? redirectUri = default,
    //     List<string>? actions = default
    // );

    // /// <summary>
    // /// Get a users's groups.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="userId">User ID.</param>
    // /// <param name="parameters">Optional query parameters.</param>
    // /// <returns>A stream of users, filtered according to query parameters.</returns>
    // Task<IEnumerable<Group>> GetUserGroups(
    //     string realm,
    //     string userId,
    //     GetGroupRequestParameters? parameters = default
    // );
}
