namespace Keycloak.AuthServices.Sdk.Admin;

/// <summary>
/// Group management
/// </summary>
public interface IKeycloakGroupClient
{
    // /// <summary>
    // /// Get a stream of groups on the realm.
    // /// </summary>
    // /// <param name="realm">Realm name.</param>
    // /// <param name="parameters">Optional query parameters.</param>
    // /// <returns>A stream of groups, filtered according to query parameters.</returns>
    // Task<IEnumerable<Group>> GetGroups(
    //     string realm,
    //     GetGroupRequestParameters? parameters = default
    // );

    // /// <summary>
    // /// Get representation of a group.
    // /// </summary>
    // /// <param name="realm">Realm name.</param>
    // /// <param name="groupId">group ID.</param>
    // /// <returns>The group representation.</returns>
    // Task<Group> GetGroup(string realm, string groupId);

    // /// <summary>
    // /// Create a new group.
    // /// </summary>
    // /// <remarks>
    // /// Group name must be unique.
    // /// </remarks>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="group">Group representation.</param>
    // /// <returns></returns>
    // Task<HttpResponseMessage> CreateGroup(string realm, Group group);

    // /// <summary>
    // /// Update the group.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="groupId">group ID.</param>
    // /// <param name="group">Group representation.</param>
    // /// <returns></returns>
    // Task<HttpResponseMessage> UpdateGroup(string realm, string groupId, Group group);

    //    /// <summary>
    // /// Get a stream of groups on the realm.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="parameters">Optional query parameters.</param>
    // /// <returns>A stream of groups, filtered according to query parameters.</returns>
    // [Get(KeycloakClientApiConstants.GetGroups)]
    // [Headers("Accept: application/json")]
    // Task<IEnumerable<Group>> GetGroups(string realm, [Query] GetGroupsRequestParameters? parameters = default);

    // /// <summary>
    // /// Get representation of a Group.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="groupId">Group ID.</param>
    // /// <returns>The group representation.</returns>
    // [Get(KeycloakClientApiConstants.GetGroup)]
    // [Headers("Accept: application/json")]
    // Task<Group> GetGroup(string realm, [AliasAs("id")] string groupId);

    // /// <summary>
    // /// Create a new Group.
    // /// </summary>
    // /// <remarks>
    // /// Name must be unique.
    // /// </remarks>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="group">Group representation.</param>
    // /// <returns></returns>
    // [Post(KeycloakClientApiConstants.CreateGroup)]
    // [Headers("Content-Type: application/json")]
    // Task<HttpResponseMessage> CreateGroup(string realm, [Body] Group group);

    // /// <summary>
    // /// Update the Group.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="groupId">Group ID.</param>
    // /// <param name="group">Group representation.</param>
    // /// <returns></returns>
    // [Put(KeycloakClientApiConstants.UpdateGroup)]
    // [Headers("Content-Type: application/json")]
    // Task UpdateGroup(string realm, [AliasAs("id")] string groupId, [Body] Group group);

    // /// <summary>
    // /// Create a new child group.
    // /// </summary>
    // /// <remarks>
    // /// Name must be unique.
    // /// </remarks>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="groupId">Group ID.</param>
    // /// <param name="group">Group representation.</param>
    // /// <returns></returns>
    // [Post(KeycloakClientApiConstants.CreateChildGroup)]
    // [Headers("Content-Type: application/json")]
    // Task<HttpResponseMessage> CreateChildGroup(string realm, [AliasAs("id")] string groupId, [Body] Group group);

    // /// <summary>
    // /// Delete a group. Note: the Keycloak documentation does not specify if this deletes subgroups.
    // /// </summary>
    // /// <param name="realm">Realm name (not ID).</param>
    // /// <param name="groupId">Group ID.</param>
    // /// <returns></returns>
    // [Delete(KeycloakClientApiConstants.DeleteGroup)]
    // Task DeleteGroup(string realm, [AliasAs("id")] string groupId);
}
