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
    Task<HttpResponseMessage> GetUsersWithResponseAsync(
        string realm,
        GetUsersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

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

        return await response.GetResponseAsync<IEnumerable<UserRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<UserRepresentation>();
    }

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

        return await response.GetResponseAsync<UserRepresentation>(cancellationToken) ?? new();
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
    Task<HttpResponseMessage> CreateUserWithResponseAsync(
        string realm,
        UserRepresentation user,
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
    /// Update the user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="user">User representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> UpdateUserWithResponseAsync(
        string realm,
        string userId,
        UserRepresentation user,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Update the user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="user">User representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task UpdateUserAsync(
        string realm,
        string userId,
        UserRepresentation user,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.UpdateUserWithResponseAsync(
            realm,
            userId,
            user,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Delete the given user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> DeleteUserWithResponseAsync(
        string realm,
        string userId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Delete the given user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task DeleteUserAsync(
        string realm,
        string userId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.DeleteUserWithResponseAsync(realm, userId, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

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
    /// <param name="cancellationToken"></param>
    Task<HttpResponseMessage> SendVerifyEmailWithResponseAsync(
        string realm,
        string userId,
        string? clientId = default,
        string? redirectUri = default,
        CancellationToken cancellationToken = default
    );

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
    /// <param name="cancellationToken"></param>
    async Task SendVerifyEmailAsync(
        string realm,
        string userId,
        string? clientId = default,
        string? redirectUri = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.SendVerifyEmailWithResponseAsync(
            realm,
            userId,
            clientId,
            redirectUri,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Execute actions email for the user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    Task<HttpResponseMessage> ExecuteActionsEmailWithResponseAsync(
        string realm,
        string userId,
        ExecuteActionsEmailRequest request,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Execute actions email for the user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    async Task ExecuteActionsEmailAsync(
        string realm,
        string userId,
        ExecuteActionsEmailRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.ExecuteActionsEmailWithResponseAsync(
            realm,
            userId,
            request,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Get a users's groups.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A stream of users, filtered according to query parameters.</returns>
    Task<HttpResponseMessage> GetUserGroupsWithResponseAsync(
        string realm,
        string userId,
        GetUserGroupsRequestParameters parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get a users's groups.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A stream of users, filtered according to query parameters.</returns>
    async Task<IEnumerable<GroupRepresentation>> GetUserGroupsAsync(
        string realm,
        string userId,
        GetUserGroupsRequestParameters parameters,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetUserGroupsWithResponseAsync(
            realm,
            userId,
            parameters,
            cancellationToken
        );

        return await response.GetResponseAsync<IEnumerable<GroupRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<GroupRepresentation>();
    }

    /// <summary>
    /// Join a group
    /// </summary>
    /// <param name="realm">Realm name(not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> JoinGroupWithResponseAsync(
        string realm,
        string userId,
        string groupId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Join a group
    /// </summary>
    /// <param name="realm">Realm name(not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task JoinGroupAsync(
        string realm,
        string userId,
        string groupId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.JoinGroupWithResponseAsync(
            realm,
            userId,
            groupId,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Leave a group
    /// </summary>
    /// <param name="realm">Realm name(not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> LeaveGroupWithResponseAsync(
        string realm,
        string userId,
        string groupId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Leave a group
    /// </summary>
    /// <param name="realm">Realm name(not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task LeaveGroupAsync(
        string realm,
        string userId,
        string groupId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.LeaveGroupWithResponseAsync(
            realm,
            userId,
            groupId,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }
}
