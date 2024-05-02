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
}
