namespace Keycloak.AuthServices.Sdk.Admin;

using System.Collections;
using System.Threading.Tasks;
using Constants;
using Models;
using Refit;
using Requests.Users;

/// <summary>
/// Group management
/// </summary>
[Headers("Accept: application/json")]
public interface IKeycloakGroupClient
{

    /// <summary>
    /// Get a stream of groups on the realm.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <returns>A stream of groups, filtered according to query parameters.</returns>
    [Get(KeycloakClientApiConstants.GetGroups)]
    Task<IEnumerable<Group>> GetGroups(string realm, [Query] GetUsersRequestParameters? parameters = default);

    /// <summary>
    /// Get representation of a group.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="groupId">group ID.</param>
    /// <returns>The group representation.</returns>
    [Get(KeycloakClientApiConstants.GetGroups)]
    Task<Group> GetGroups(string realm, [AliasAs("id")] string groupId);
}
