﻿namespace Keycloak.AuthServices.Sdk.Admin;

using Constants;
using Models;
using Refit;
using Requests.Users;

/// <summary>
/// User management
/// </summary>
[Headers("Accept: application/json")]
public interface IKeycloakUserClient
{
    /// <summary>
    /// Get a stream of users on the realm.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <returns>A stream of users, filtered according to query parameters.</returns>
    [Get(KeycloakClientApiConstants.GetUsers)]
    Task<IEnumerable<User>> GetUsers(string realm, [Query] GetUsersRequestParameters? parameters = default);

    /// <summary>
    /// Get representation of a user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <returns>The user representation.</returns>
    [Get(KeycloakClientApiConstants.GetUser)]
    Task<User> GetUser(string realm, [AliasAs("id")] string userId);

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
    [Headers("Content-Type: application/json")]
    Task<HttpResponseMessage> CreateUser(string realm, [Body] User user);

    /// <summary>
    /// Update the user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="user">User representation.</param>
    /// <returns></returns>
    [Put(KeycloakClientApiConstants.UpdateUser)]
    [Headers("Content-Type: application/json")]
    Task UpdateUser(string realm, [AliasAs("id")] string userId, [Body] User user);

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
    [Put(KeycloakClientApiConstants.SendVerifyEmail)]
    Task SendVerifyEmail(string realm, [AliasAs("id")] string userId,
        [Query][AliasAs("client_id")] string? clientId = default,
        [Query][AliasAs("redirect_uri")] string? redirectUri = default);

    /// <summary>
    /// Execute actions email for the user.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="userId">User ID.</param>
    /// <param name="clientId">Client ID.</param>
    /// <param name="lifespan">Number of seconds after which the generated token expires</param>
    /// <param name="redirectUri">Redirect URI. The default for the redirect is the account client.</param>
    /// <param name="actions">Actions</param>
    [Put(KeycloakClientApiConstants.ExecuteActionsEmail)]
    [Headers("Content-Type: application/json")]
    Task ExecuteActionsEmail(string realm, [AliasAs("id")] string userId,
        [Query][AliasAs("client_id")] string? clientId = default,
        [Query] int? lifespan = default,
        [Query][AliasAs("redirect_uri")] string? redirectUri = default,
        [Body] List<string>? actions = default);
}
