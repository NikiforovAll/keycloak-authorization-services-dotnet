namespace Keycloak.AuthServices.Sdk.Admin;

using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Organizations;

/// <summary>
/// Keycloak Organizations management.
/// </summary>
public interface IKeycloakOrganizationClient
{
    /// <summary>
    /// Returns a paginated list of organizations filtered by the given parameters.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A paginated list of organizations.</returns>
    public Task<HttpResponseMessage> GetOrganizationsWithResponseAsync(
        string realm,
        GetOrganizationsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a paginated list of organizations filtered by the given parameters.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A paginated list of organizations.</returns>
    public async Task<IEnumerable<OrganizationRepresentation>> GetOrganizationsAsync(
        string realm,
        GetOrganizationsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetOrganizationsWithResponseAsync(realm, parameters, cancellationToken);

        return await response.GetResponseAsync<IEnumerable<OrganizationRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<OrganizationRepresentation>();
    }

    /// <summary>
    /// Returns the total organization count for the realm.
    ///
    /// Note that the response is not JSON, but simply the integer value as a string.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An integer amount of organizations.</returns>
    public Task<HttpResponseMessage> GetOrganizationCountWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns the total organization count for the realm.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An integer amount of organizations.</returns>
    public async Task<int> GetOrganizationCountAsync(
        string realm,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetOrganizationCountWithResponseAsync(realm, cancellationToken);
        var stringContent = await response.Content.ReadAsStringAsync(cancellationToken);
#pragma warning disable CA1305 // Specify IFormatProvider
        return Convert.ToInt32(stringContent);
#pragma warning restore CA1305 // Specify IFormatProvider
    }

    /// <summary>
    /// Returns the representation of the organization with the given id.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The organization representation.</returns>
    public Task<HttpResponseMessage> GetOrganizationWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns the representation of the organization with the given id.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The organization representation.</returns>
    public async Task<OrganizationRepresentation> GetOrganizationAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetOrganizationWithResponseAsync(realm, orgId, cancellationToken);

        return await response.GetResponseAsync<OrganizationRepresentation>(cancellationToken) ?? new();
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="organization">Organization representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HttpResponseMessage> CreateOrganizationWithResponseAsync(
        string realm,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="organization">Organization representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task CreateOrganizationAsync(
        string realm,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.CreateOrganizationWithResponseAsync(realm, organization, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Updates the organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="organization">Organization representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HttpResponseMessage> UpdateOrganizationWithResponseAsync(
        string realm,
        string orgId,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="organization">Organization representation.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task UpdateOrganizationAsync(
        string realm,
        string orgId,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.UpdateOrganizationWithResponseAsync(realm, orgId, organization, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes the organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HttpResponseMessage> DeleteOrganizationWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes the organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteOrganizationAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.DeleteOrganizationWithResponseAsync(realm, orgId, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Returns a paginated list of members of the organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A paginated list of organization members.</returns>
    public Task<HttpResponseMessage> GetOrganizationMembersWithResponseAsync(
        string realm,
        string orgId,
        GetOrganizationMembersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a paginated list of members of the organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="parameters">Optional query parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A paginated list of organization members.</returns>
    public async Task<IEnumerable<OrganizationMemberRepresentation>> GetOrganizationMembersAsync(
        string realm,
        string orgId,
        GetOrganizationMembersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetOrganizationMembersWithResponseAsync(realm, orgId, parameters, cancellationToken);

        return await response.GetResponseAsync<IEnumerable<OrganizationMemberRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<OrganizationMemberRepresentation>();
    }

    /// <summary>
    /// Returns the total member count for the organization.
    ///
    /// Note that the response is not JSON, but simply the integer value as a string.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An integer amount of members.</returns>
    public Task<HttpResponseMessage> GetOrganizationMemberCountWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns the total member count for the organization.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An integer amount of members.</returns>
    public async Task<int> GetOrganizationMemberCountAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetOrganizationMemberCountWithResponseAsync(realm, orgId, cancellationToken);
        var stringContent = await response.Content.ReadAsStringAsync(cancellationToken);
#pragma warning disable CA1305 // Specify IFormatProvider
        return Convert.ToInt32(stringContent);
#pragma warning restore CA1305 // Specify IFormatProvider
    }

    /// <summary>
    /// Get representation of the given member.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="memberId">Member ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The member representation.</returns>
    public Task<HttpResponseMessage> GetOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get representation of the given member.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="memberId">Member ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The member representation.</returns>
    public async Task<OrganizationMemberRepresentation> GetOrganizationMemberAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetOrganizationMemberWithResponseAsync(realm, orgId, memberId, cancellationToken);

        return await response.GetResponseAsync<OrganizationMemberRepresentation>(cancellationToken) ?? new();
    }

    /// <summary>
    /// Adds (associates) an existing realm user to the organization.
    /// </summary>
    /// <remarks>
    /// If no user is found, or if the user is already a member, Keycloak returns an error response.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HttpResponseMessage> AddOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string userId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Adds (associates) an existing realm user to the organization.
    /// </summary>
    /// <remarks>
    /// If no user is found, or if the user is already a member, Keycloak returns an error response.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task AddOrganizationMemberAsync(
        string realm,
        string orgId,
        string userId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.AddOrganizationMemberWithResponseAsync(realm, orgId, userId, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Removes the user from the organization.
    /// </summary>
    /// <remarks>
    /// For managed members the user is deleted; for unmanaged members only the association is removed.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="memberId">Member ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HttpResponseMessage> RemoveOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Removes the user from the organization.
    /// </summary>
    /// <remarks>
    /// For managed members the user is deleted; for unmanaged members only the association is removed.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="memberId">Member ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task RemoveOrganizationMemberAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.RemoveOrganizationMemberWithResponseAsync(realm, orgId, memberId, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Returns the groups within this organization that the given member belongs to.
    /// </summary>
    /// <remarks>
    /// Returns 404 if the user is not a member of the organization.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="memberId">Member ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of group representations.</returns>
    public Task<HttpResponseMessage> GetOrganizationMemberGroupsWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns the groups within this organization that the given member belongs to.
    /// </summary>
    /// <remarks>
    /// Returns 404 if the user is not a member of the organization.
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="orgId">Organization ID.</param>
    /// <param name="memberId">Member ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of group representations.</returns>
    public async Task<IEnumerable<GroupRepresentation>> GetOrganizationMemberGroupsAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetOrganizationMemberGroupsWithResponseAsync(realm, orgId, memberId, cancellationToken);

        return await response.GetResponseAsync<IEnumerable<GroupRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<GroupRepresentation>();
    }

    /// <summary>
    /// Returns the organizations that the user with the specified id belongs to.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="memberId">Member ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of organizations.</returns>
    public Task<HttpResponseMessage> GetUserOrganizationsWithResponseAsync(
        string realm,
        string memberId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns the organizations that the user with the specified id belongs to.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="memberId">Member ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of organizations.</returns>
    public async Task<IEnumerable<OrganizationRepresentation>> GetUserOrganizationsAsync(
        string realm,
        string memberId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetUserOrganizationsWithResponseAsync(realm, memberId, cancellationToken);

        return await response.GetResponseAsync<IEnumerable<OrganizationRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<OrganizationRepresentation>();
    }
}
