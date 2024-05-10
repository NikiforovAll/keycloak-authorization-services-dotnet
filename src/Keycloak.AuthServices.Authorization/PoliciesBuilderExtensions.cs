namespace Keycloak.AuthServices.Authorization;

using System;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Keycloak.AuthServices.Authorization.Requirements;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// </summary>
public static class PoliciesBuilderExtensions
{
    /// <summary>
    /// Adds resource role requirement to builder. Ensures that at least one resource role is present in resource claims.
    /// Note, make sure role source is configure. See documentation for more details.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
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
    /// <param name="builder"></param>
    /// <param name="client"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
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
    /// <param name="builder"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
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
    /// <param name="builder"></param>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
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
    /// <param name="builder"></param>
    /// <param name="resource"></param>
    /// <param name="scopes"></param>
    /// <param name="scopesValidationMode"></param>
    /// <returns></returns>
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
                ScopesValidationMode = scopesValidationMode
            }
        );
    }
    #endregion RequireProtectedResourceScopes
}
