namespace Keycloak.AuthServices.Sdk.Admin;

using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Groups;

/// <summary>
/// Group management
/// </summary>
public interface IKeycloakGroupClient
{
    /// <summary>
    /// Get a collection of groups on the realm.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A stream of groups, filtered according to query parameters.</returns>
    // [Get(KeycloakClientApiConstants.GetGroups)]
    Task<HttpResponseMessage> GetGroupsWithResponseAsync(
        string realm,
        GetGroupsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get group hierarchy. Only name and ids are returned.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A stream of groups, filtered according to query parameters.</returns>
    async Task<IEnumerable<GroupRepresentation>> GetGroupsAsync(
        string realm,
        GetGroupsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetGroupsWithResponseAsync(realm, parameters, cancellationToken);

        return await response.GetResponseAsync<IEnumerable<GroupRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<GroupRepresentation>();
    }


    /// <summary>
    /// Get a collection of sub groups by parent's id on the realm.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parentGroupId">Parent Group ID.</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> GetSubGroupsWithResponseAsync(
        string realm,
        string parentGroupId,
        GetGroupsRequestParameters? parameters = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get group hierarchy. Only name and ids are returned.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A stream of groups, filtered according to query parameters.</returns>
    async Task<IEnumerable<GroupRepresentation>> GetSubGroupsAsync(
        string realm,
        string parentGroupId,
        GetGroupsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetSubGroupsWithResponseAsync(realm, parentGroupId, parameters, cancellationToken);

        return await response.GetResponseAsync<IEnumerable<GroupRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<GroupRepresentation>();
    }


    /// <summary>
    /// Get representation of a Group.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The group representation.</returns>
    Task<HttpResponseMessage> GetGroupWithResponseAsync(
        string realm,
        string groupId,
        CancellationToken cancellationToken = default
    );

    


    /// <summary>
    /// Get representation of a Group.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The group representation.</returns>
    async Task<GroupRepresentation> GetGroupAsync(
        string realm,
        string groupId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetGroupWithResponseAsync(realm, groupId, cancellationToken);

        return await response.GetResponseAsync<GroupRepresentation>(cancellationToken) ?? new();
    }

    /// <summary>
    /// Create or add a top level realm groupSet or create child.
    /// </summary>
    /// <remarks>
    /// This will update the group and set the parent if it exists. Create it and set the parent if the group doesn’t exist.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="group">Group representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> CreateGroupWithResponseAsync(
        string realm,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Create or add a top level realm groupSet or create child.
    /// </summary>
    /// <remarks>
    /// This will update the group and set the parent if it exists. Create it and set the parent if the group doesn’t exist.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="group">Group representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task CreateGroupAsync(
        string realm,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.CreateGroupWithResponseAsync(realm, group, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Update group, ignores subgroups.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="group">Group representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> UpdateGroupWithResponseAsync(
        string realm,
        string groupId,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Update group, ignores subgroups.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="group">Group representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task UpdateGroupAsync(
        string realm,
        string groupId,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.UpdateGroupWithResponseAsync(
            realm,
            groupId,
            group,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Set or create child.
    /// </summary>
    /// <remarks>
    /// This will just set the parent if it exists. Create it and set the parent if the group doesn’t exist.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="group">Group representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> CreateChildGroupWithResponseAsync(
        string realm,
        string groupId,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Set or create child.
    /// </summary>
    /// <remarks>
    /// This will just set the parent if it exists. Create it and set the parent if the group doesn’t exist.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="group">Group representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task CreateChildGroupAsync(
        string realm,
        string groupId,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.CreateChildGroupWithResponseAsync(
            realm,
            groupId,
            group,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Delete a group. Note: the Keycloak documentation does not specify if this deletes subgroups.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> DeleteGroupWithResponseAsync(
        string realm,
        string groupId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Delete a group. Note: the Keycloak documentation does not specify if this deletes subgroups.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task DeleteGroupAsync(
        string realm,
        string groupId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.DeleteGroupWithResponseAsync(realm, groupId, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

}
