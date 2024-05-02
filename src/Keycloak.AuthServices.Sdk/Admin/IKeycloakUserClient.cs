namespace Keycloak.AuthServices.Sdk.Admin;

/// <summary>
/// User management
/// </summary>
public interface IKeycloakUserClient
{
    // /// <summary>
    // /// Get a stream of users on the realm.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="parameters">Optional query parameters.</param>
    // /// <returns>A stream of users, filtered according to query parameters.</returns>
    // Task<IEnumerable<User>> GetUsers(string realm, GetUsersRequestParameters? parameters = default);

    // /// <summary>
    // /// Get representation of a user.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="userId">User ID.</param>
    // /// <returns>The user representation.</returns>
    // Task<User> GetUser(string realm, string userId);

    // /// <summary>
    // /// Create a new user.
    // /// </summary>
    // /// <remarks>
    // /// Username must be unique.
    // /// </remarks>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="user">User representation.</param>
    // /// <returns></returns>
    // Task<HttpResponseMessage> CreateUser(string realm, User user);

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
