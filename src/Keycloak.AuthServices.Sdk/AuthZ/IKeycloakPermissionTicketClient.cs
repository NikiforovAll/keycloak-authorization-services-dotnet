namespace Keycloak.AuthServices.Sdk.AuthZ;

using System.Threading.Tasks;
using Admin.Constants;
using Admin.Models.PermissionTickets;
using Admin.Requests.Permissions;
using Refit;

/// <summary>
/// Must be used by the owner of the resource for whom the ticket is being created.
/// </summary>
[Headers("Accept: application/json")] 
public interface IKeycloakPermissionTicketClient
{
    // Note that this is a representation of endpoints that are in a different documentation zone (i.e.: not the REST API docs directly):
    // https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_permission_api_papi

    /// <summary>
    /// Get all permission tickets
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters</param>
    /// <returns>A stream of permission tickets, filtered according to query parameters.</returns>
    [Get(KeycloakClientApiConstants.GetPermissionTickets)]
    [Headers("Accept: application/json")]
    Task<IEnumerable<PermissionTicketResponse>> GetPermissionTickets(string realm, [Query] GetPermissionTicketsRequestParameters? parameters = default);

    /// <summary>
    /// Create a new permission ticket
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="permissionTicket">Permission Ticket representation</param>
    /// <returns></returns>
    [Post(KeycloakClientApiConstants.CreatePermissionTicket)]
    [Headers("Content-Type: application/json")]
    Task<HttpResponseMessage> CreatePermissionTicket(string realm, [Body] PermissionTicket permissionTicket);

    /// <summary>
    /// Update the given permission ticket.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="permissionTicket">Permission Ticket representation</param>
    /// <returns></returns>
    [Put(KeycloakClientApiConstants.UpdatePermissionTicket)]
    [Headers("Content-Type: application/json")]
    Task UpdatePermissionTicket(string realm, [Body] PermissionTicket permissionTicket);

    /// <summary>
    /// Delete the given permission ticket.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="permissionTicketId">Permission Ticket Id</param>
    /// <returns></returns>
    [Delete(KeycloakClientApiConstants.DeletePermissionTicket)]
    [Headers("Content-Type: application/json")]
    Task DeletePermissionTicket(string realm, [AliasAs("id")] string permissionTicketId);
}
