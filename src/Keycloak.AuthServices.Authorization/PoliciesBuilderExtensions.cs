namespace Keycloak.AuthServices.Authorization;

using System;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Keycloak.AuthServices.Authorization.Requirements;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Extension methods for <see cref="AuthorizationPolicyBuilder"/> to add Keycloak-specific
/// authorization requirements such as realm roles, resource roles, protected resources,
/// and organization membership checks.
/// </summary>
public static class PoliciesBuilderExtensions
{
    /// <summary>
    /// Adds resource role requirement to builder. Ensures that at least one resource role is present in resource claims.
    /// Note, make sure role source is configure. See documentation for more details.
    /// </summary>
    /// <param name="builder">The authorization policy builder.</param>
    /// <param name="roles">The resource roles to require. At least one must match.</param>
    /// <returns>The authorization policy builder.</returns>
    public static AuthorizationPolicyBuilder RequireResourceRoles(
        this AuthorizationPolicyBuilder builder,
        params string[] roles
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(roles);

        return builder
            .RequireClaim(KeycloakConstants.ResourceAccessClaimType)
            .AddRequirements(new ResourceAccessRequirement(default, roles));
    }

    /// <summary>
    /// Adds resource role requirement to builder. Ensures that at least one resource role is present in resource claims.
    /// </summary>
    /// <param name="builder">The authorization policy builder.</param>
    /// <param name="client">The Keycloak client (resource) name to check roles against.</param>
    /// <param name="roles">The resource roles to require. At least one must match.</param>
    /// <returns>The authorization policy builder.</returns>
    public static AuthorizationPolicyBuilder RequireResourceRolesForClient(
        this AuthorizationPolicyBuilder builder,
        string client,
        string[] roles
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(roles);

        return builder
            .RequireAuthenticatedUser()
            .RequireClaim(KeycloakConstants.ResourceAccessClaimType)
            .AddRequirements(new ResourceAccessRequirement(client, roles));
    }

    /// <summary>
    /// Adds realm role requirement to builder. Ensures that at least one realm role is present in realm claims.
    /// </summary>
    /// <param name="builder">The authorization policy builder.</param>
    /// <param name="roles">The realm roles to require. At least one must match.</param>
    /// <returns>The authorization policy builder.</returns>
    public static AuthorizationPolicyBuilder RequireRealmRoles(
        this AuthorizationPolicyBuilder builder,
        params string[] roles
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(roles);

        return builder
            .RequireAuthenticatedUser()
            .RequireClaim(KeycloakConstants.RealmAccessClaimType)
            .AddRequirements(new RealmAccessRequirement(roles));
    }

    #region RequireProtectedResource
    /// <summary>
    /// Adds protected resource requirement to builder. Makes outgoing HTTP requests to Authorization Server.
    /// </summary>
    /// <param name="builder">The authorization policy builder.</param>
    /// <param name="resource">The protected resource name.</param>
    /// <param name="scope">The required scope for the resource.</param>
    /// <returns>The authorization policy builder.</returns>
    public static AuthorizationPolicyBuilder RequireProtectedResource(
        this AuthorizationPolicyBuilder builder,
        string resource,
        string scope
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(scope);

        return builder.AddRequirements(new DecisionRequirement(resource, scope));
    }
    #endregion RequireProtectedResource

    #region RequireProtectedResourceScopes
    /// <summary>
    /// Adds protected resource requirement to builder. Makes outgoing HTTP requests to Authorization Server.
    /// </summary>
    /// <param name="builder">The authorization policy builder.</param>
    /// <param name="resource">The protected resource name.</param>
    /// <param name="scopes">The required scopes for the resource.</param>
    /// <param name="scopesValidationMode">
    /// Controls whether all scopes (<see cref="ScopesValidationMode.AllOf"/>) or at least one scope
    /// (<see cref="ScopesValidationMode.AnyOf"/>) must be granted. Defaults to the server-level setting.
    /// </param>
    /// <returns>The authorization policy builder.</returns>
    public static AuthorizationPolicyBuilder RequireProtectedResource(
        this AuthorizationPolicyBuilder builder,
        string resource,
        string[] scopes,
        ScopesValidationMode? scopesValidationMode = default
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(resource);
        ArgumentNullException.ThrowIfNull(scopes);

        return builder.AddRequirements(
            new DecisionRequirement(resource, scopes)
            {
                ScopesValidationMode = scopesValidationMode,
            }
        );
    }
    #endregion RequireProtectedResourceScopes

    #region RequireOrganizationMembership
    /// <summary>
    /// Adds organization membership requirement to builder. Requires the user to be a member of any Keycloak organization.
    /// </summary>
    /// <param name="builder">The authorization policy builder.</param>
    /// <returns>The modified authorization policy builder.</returns>
    public static AuthorizationPolicyBuilder RequireOrganizationMembership(
        this AuthorizationPolicyBuilder builder
    )
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.RequireOrganizationMembership(new OrganizationRequirement());
    }

    /// <summary>
    /// Adds organization membership requirement to builder. Requires the user to be a member of the specified organization.
    /// </summary>
    /// <param name="builder">The authorization policy builder.</param>
    /// <param name="organization">The organization alias or identifier.</param>
    /// <returns>The modified authorization policy builder.</returns>
    public static AuthorizationPolicyBuilder RequireOrganizationMembership(
        this AuthorizationPolicyBuilder builder,
        string organization
    )
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(organization);

        return builder.RequireOrganizationMembership(new OrganizationRequirement(organization));
    }

    private static AuthorizationPolicyBuilder RequireOrganizationMembership(
        this AuthorizationPolicyBuilder builder,
        OrganizationRequirement requirement
    ) => builder.RequireAuthenticatedUser().AddRequirements(requirement);
    #endregion RequireOrganizationMembership
}
