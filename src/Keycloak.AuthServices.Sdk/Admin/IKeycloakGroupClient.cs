namespace Keycloak.AuthServices.Sdk.Admin;

using System.Text.Json;
using System.Text.Json.Nodes;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Groups;

/// <summary>
/// Group management
/// </summary>
public interface IKeycloakGroupClient
{
    /// <summary>
    /// Gets the integer amount of groups in the realm matching the given <see cref="GetGroupCountRequestParameters"/>.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The amount of groups in the realm</returns>
    Task<HttpResponseMessage> GetGroupCountWithResponseAsync(
        string realm,
        GetGroupCountRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the integer amount of groups in the realm matching the given <see cref="GetGroupCountRequestParameters"/>.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The amount of groups in the realm</returns>
    async Task<int> GetGroupCountAsync(
        string realm,
        GetGroupCountRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetGroupCountWithResponseAsync(realm, parameters, cancellationToken);

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var jsonNode = JsonSerializer.Deserialize<JsonNode>(json);

        return jsonNode!.AsObject()["count"]!.GetValue<int>();
    }

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
    /// Gets all members of a given group.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The stream of users (<see cref="UserRepresentation"/>s)</returns>
    Task<HttpResponseMessage> GetGroupMembersWithResponseAsync(
        string realm,
        string groupId,
        GetGroupMembersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all members of a given group.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">Group ID.</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The stream of users (<see cref="UserRepresentation"/>s)</returns>
    async Task<IEnumerable<UserRepresentation>> GetGroupMembersAsync(
        string realm,
        string groupId,
        GetGroupMembersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetGroupMembersWithResponseAsync(realm, groupId, parameters, cancellationToken);

        return await response.GetResponseAsync<IEnumerable<UserRepresentation>>(cancellationToken)
               ?? Enumerable.Empty<UserRepresentation>();
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
