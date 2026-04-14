namespace Keycloak.AuthServices.Sdk.Protection;

using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;

/// <summary>
/// UMA Permission API — creates permission tickets and queries existing ones.
/// </summary>
/// <remarks>
/// See: https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_permission_api_papi
/// </remarks>
public interface IKeycloakPermissionClient
{
    /// <summary>
    /// Creates a permission ticket for the specified resources and scopes.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="permissions">The permission requests.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request.</returns>
    public Task<HttpResponseMessage> CreatePermissionTicketWithResponseAsync(
        string realm,
        IList<PermissionTicketRequest> permissions,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates a permission ticket for the specified resources and scopes.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="permissions">The permission requests.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The permission ticket response containing the ticket string.</returns>
    public async Task<PermissionTicketResponse> CreatePermissionTicketAsync(
        string realm,
        IList<PermissionTicketRequest> permissions,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.CreatePermissionTicketWithResponseAsync(
            realm,
            permissions,
            cancellationToken
        );

        return await response.GetResponseAsync<PermissionTicketResponse>(cancellationToken)
            ?? new();
    }

    /// <summary>
    /// Gets permission tickets with optional filtering.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters for filtering.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request.</returns>
    public Task<HttpResponseMessage> GetPermissionTicketsWithResponseAsync(
        string realm,
        GetPermissionTicketsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets permission tickets with optional filtering.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters for filtering.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of permission tickets.</returns>
    public async Task<IList<PermissionTicket>> GetPermissionTicketsAsync(
        string realm,
        GetPermissionTicketsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetPermissionTicketsWithResponseAsync(
            realm,
            parameters,
            cancellationToken
        );

        return await response.GetResponseAsync<IList<PermissionTicket>>(cancellationToken)
            ?? Array.Empty<PermissionTicket>();
    }

    /// <summary>
    /// Updates a permission ticket (e.g., to approve or revoke).
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="ticket">The permission ticket with updated fields.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request.</returns>
    public Task<HttpResponseMessage> UpdatePermissionTicketWithResponseAsync(
        string realm,
        PermissionTicket ticket,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates a permission ticket (e.g., to approve or revoke).
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="ticket">The permission ticket with updated fields.</param>
    /// <param name="cancellationToken"></param>
    public async Task UpdatePermissionTicketAsync(
        string realm,
        PermissionTicket ticket,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.UpdatePermissionTicketWithResponseAsync(
            realm,
            ticket,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a permission ticket.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="ticketId">The permission ticket ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request.</returns>
    public Task<HttpResponseMessage> DeletePermissionTicketWithResponseAsync(
        string realm,
        string ticketId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a permission ticket.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="ticketId">The permission ticket ID.</param>
    /// <param name="cancellationToken"></param>
    public async Task DeletePermissionTicketAsync(
        string realm,
        string ticketId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.DeletePermissionTicketWithResponseAsync(
            realm,
            ticketId,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Creates a stored permission ticket (e.g., to request access to a resource).
    /// This uses the <c>/permission/ticket</c> endpoint, not the UMA permission endpoint.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="ticket">The permission ticket to create.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request.</returns>
    public Task<HttpResponseMessage> CreateStoredPermissionTicketWithResponseAsync(
        string realm,
        PermissionTicket ticket,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates a stored permission ticket (e.g., to request access to a resource).
    /// This uses the <c>/permission/ticket</c> endpoint, not the UMA permission endpoint.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="ticket">The permission ticket to create.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The created permission ticket.</returns>
    public async Task<PermissionTicket> CreateStoredPermissionTicketAsync(
        string realm,
        PermissionTicket ticket,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.CreateStoredPermissionTicketWithResponseAsync(
            realm,
            ticket,
            cancellationToken
        );

        return await response.GetResponseAsync<PermissionTicket>(cancellationToken) ?? new();
    }
}
