﻿namespace Keycloak.AuthServices.Authorization;

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
    ) =>
        builder
            .RequireClaim(KeycloakConstants.ResourceAccessClaimType)
            .AddRequirements(new ResourceAccessRequirement(default, roles));

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
    ) =>
        builder
            .RequireClaim(KeycloakConstants.ResourceAccessClaimType)
            .AddRequirements(new ResourceAccessRequirement(client, roles));

    /// <summary>
    /// Adds realm role requirement to builder. Ensures that at least one realm role is present in realm claims.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public static AuthorizationPolicyBuilder RequireRealmRoles(
        this AuthorizationPolicyBuilder builder,
        params string[] roles
    ) =>
        builder
            .RequireClaim(KeycloakConstants.RealmAccessClaimType)
            .AddRequirements(new RealmAccessRequirement(roles));

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
    ) => builder.AddRequirements(new DecisionRequirement(resource, scope));

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
    ) =>
        builder.AddRequirements(
            new DecisionRequirement(resource, scopes)
            {
                ScopesValidationMode = scopesValidationMode
            }
        );
}
