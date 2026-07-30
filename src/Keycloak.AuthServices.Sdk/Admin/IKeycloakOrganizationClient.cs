namespace Keycloak.AuthServices.Sdk.Admin;

using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Organizations;


/// <summary>
/// Keycloak Organizations management.
/// </summary>
public interface IKeycloakOrganizationClient
{
    // ==============================================================
    // Organization CRUD
    // ==============================================================

    /// <summary>Returns a paginated list of organizations filtered by the given parameters.</summary>
    Task<HttpResponseMessage> GetOrganizationsWithResponseAsync(
        string realm,
        GetOrganizationsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetOrganizationsWithResponseAsync"/>
    public async Task<IEnumerable<OrganizationRepresentation>> GetOrganizationsAsync(
        string realm,
        GetOrganizationsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default)
    {
        var response = await this.GetOrganizationsWithResponseAsync(realm, parameters, cancellationToken);
        return await response.GetResponseAsync<IEnumerable<OrganizationRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<OrganizationRepresentation>();
    }

    /// <summary>Returns the total organization count for the realm.</summary>
    Task<HttpResponseMessage> GetOrganizationCountWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetOrganizationCountWithResponseAsync"/>
    public async Task<long> GetOrganizationCountAsync(
        string realm,
        CancellationToken cancellationToken = default)
    {
        var response = await this.GetOrganizationCountWithResponseAsync(realm, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
#pragma warning disable CA1305 // Specify IFormatProvider
        return Convert.ToInt64(content);
#pragma warning restore CA1305 // Specify IFormatProvider
    }

    /// <summary>Returns the representation of the organization with the given id.</summary>
    Task<HttpResponseMessage> GetOrganizationWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetOrganizationWithResponseAsync"/>
    public async Task<OrganizationRepresentation> GetOrganizationAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default)
    {
        var response = await this.GetOrganizationWithResponseAsync(realm, orgId, cancellationToken);
        return await response.GetResponseAsync<OrganizationRepresentation>(cancellationToken) ?? new();
    }

    /// <summary>Creates a new organization.</summary>
    Task<HttpResponseMessage> CreateOrganizationWithResponseAsync(
        string realm,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="CreateOrganizationWithResponseAsync"/>
    public async Task CreateOrganizationAsync(
        string realm,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default)
    {
        var response = await this.CreateOrganizationWithResponseAsync(realm, organization, cancellationToken);
        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>Updates the organization.</summary>
    Task<HttpResponseMessage> UpdateOrganizationWithResponseAsync(
        string realm,
        string orgId,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="UpdateOrganizationWithResponseAsync"/>
    public async Task UpdateOrganizationAsync(
        string realm,
        string orgId,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default)
    {
        var response = await this.UpdateOrganizationWithResponseAsync(realm, orgId, organization, cancellationToken);
        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>Deletes the organization.</summary>
    Task<HttpResponseMessage> DeleteOrganizationWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="DeleteOrganizationWithResponseAsync"/>
    public async Task DeleteOrganizationAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default)
    {
        var response = await this.DeleteOrganizationWithResponseAsync(realm, orgId, cancellationToken);
        await response.EnsureResponseAsync(cancellationToken);
    }

    // ==============================================================
    // Members
    // ==============================================================

    /// <summary>Returns a paginated list of members of the organization.</summary>
    Task<HttpResponseMessage> GetOrganizationMembersWithResponseAsync(
        string realm,
        string orgId,
        GetOrganizationMembersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetOrganizationMembersWithResponseAsync"/>
    public async Task<IEnumerable<OrganizationMemberRepresentation>> GetOrganizationMembersAsync(
        string realm,
        string orgId,
        GetOrganizationMembersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default)
    {
        var response = await this.GetOrganizationMembersWithResponseAsync(realm, orgId, parameters, cancellationToken);
        return await response.GetResponseAsync<IEnumerable<OrganizationMemberRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<OrganizationMemberRepresentation>();
    }

    /// <summary>Returns the total member count for the organization.</summary>
    Task<HttpResponseMessage> GetOrganizationMemberCountWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetOrganizationMemberCountWithResponseAsync"/>
    public async Task<long> GetOrganizationMemberCountAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default)
    {
        var response = await this.GetOrganizationMemberCountWithResponseAsync(realm, orgId, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
#pragma warning disable CA1305
        return Convert.ToInt64(content);
#pragma warning restore CA1305
    }

    /// <summary>Returns the representation of the given member.</summary>
    Task<HttpResponseMessage> GetOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetOrganizationMemberWithResponseAsync"/>
    public async Task<OrganizationMemberRepresentation> GetOrganizationMemberAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default)
    {
        var response = await this.GetOrganizationMemberWithResponseAsync(realm, orgId, memberId, cancellationToken);
        return await response.GetResponseAsync<OrganizationMemberRepresentation>(cancellationToken) ?? new();
    }

    /// <summary>
    /// Adds (associates) an existing realm user to the organization. If no user is found,
    /// or if the user is already a member, Keycloak returns an error response.
    /// </summary>
    Task<HttpResponseMessage> AddOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string userId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="AddOrganizationMemberWithResponseAsync"/>
    public async Task AddOrganizationMemberAsync(
        string realm,
        string orgId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var response = await this.AddOrganizationMemberWithResponseAsync(realm, orgId, userId, cancellationToken);
        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Removes the user from the organization. For managed members the user is deleted;
    /// for unmanaged members only the association is removed.
    /// </summary>
    Task<HttpResponseMessage> RemoveOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="RemoveOrganizationMemberWithResponseAsync"/>
    public async Task RemoveOrganizationMemberAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default)
    {
        var response = await this.RemoveOrganizationMemberWithResponseAsync(realm, orgId, memberId, cancellationToken);
        await response.EnsureResponseAsync(cancellationToken);
    }

    // ==============================================================
    // Member sub-resources
    // ==============================================================

    /// <summary>
    /// Returns the groups within this organization that the given member belongs to.
    /// Returns 404 if the user is not a member of the organization.
    /// </summary>
    Task<HttpResponseMessage> GetOrganizationMemberGroupsWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetOrganizationMemberGroupsWithResponseAsync"/>
    public async Task<IEnumerable<GroupRepresentation>> GetOrganizationMemberGroupsAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default)
    {
        var response = await this.GetOrganizationMemberGroupsWithResponseAsync(realm, orgId, memberId, cancellationToken);
        return await response.GetResponseAsync<IEnumerable<GroupRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<GroupRepresentation>();
    }

    /// <summary>Returns the organizations that the user with the specified id belongs to.</summary>
    Task<HttpResponseMessage> GetUserOrganizationsWithResponseAsync(
        string realm,
        string memberId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetUserOrganizationsWithResponseAsync"/>
    public async Task<IEnumerable<OrganizationRepresentation>> GetUserOrganizationsAsync(
        string realm,
        string memberId,
        CancellationToken cancellationToken = default)
    {
        var response = await this.GetUserOrganizationsWithResponseAsync(realm, memberId, cancellationToken);
        return await response.GetResponseAsync<IEnumerable<OrganizationRepresentation>>(cancellationToken)
            ?? Enumerable.Empty<OrganizationRepresentation>();
    }
}
