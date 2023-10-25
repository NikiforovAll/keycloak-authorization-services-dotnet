namespace Keycloak.AuthServices.Sdk.Admin;

using System.Threading.Tasks;
using Constants;
using Models;
using Refit;
using Requests.Groups;

/// <summary>
/// Group management
/// </summary>
[Headers("Accept: application/json")]
public interface IKeycloakGroupClient
{

    /// <summary>
    /// Get a stream of groups on the realm.
    /// </summary>
    /// <param name="realm">Realm name.</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <returns>A stream of groups, filtered according to query parameters.</returns>
    [Get(KeycloakClientApiConstants.GetGroups)]
    Task<IEnumerable<Group>> GetGroups(string realm, [Query] GetGroupRequestParameters? parameters = default);

    /// <summary>
    /// Get representation of a group.
    /// </summary>
    /// <param name="realm">Realm name.</param>
    /// <param name="groupId">group ID.</param>
    /// <returns>The group representation.</returns>
    [Get(KeycloakClientApiConstants.GetGroup)]
    Task<Group> GetGroup(string realm, [AliasAs("id")] string groupId);
}
