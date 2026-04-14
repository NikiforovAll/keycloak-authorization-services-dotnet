namespace Keycloak.AuthServices.Sdk.Protection;

using Keycloak.AuthServices.Sdk.Protection.Models;

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
    /// Gets all permission tickets.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request.</returns>
    public Task<HttpResponseMessage> GetPermissionTicketsWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all permission tickets.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of permission tickets.</returns>
    public async Task<IList<PermissionTicket>> GetPermissionTicketsAsync(
        string realm,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetPermissionTicketsWithResponseAsync(realm, cancellationToken);

        return await response.GetResponseAsync<IList<PermissionTicket>>(cancellationToken)
            ?? Array.Empty<PermissionTicket>();
    }
}
