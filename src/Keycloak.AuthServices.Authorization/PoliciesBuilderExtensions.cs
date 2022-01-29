namespace Keycloak.AuthServices.Authorization;

using Common;
using Microsoft.AspNetCore.Authorization;
using Requirements;

/// <summary>
/// </summary>
public static class PoliciesBuilderExtensions
{
    /// <summary>
    /// Adds resource role requirement to builder. Ensures that at least one resource role is present in resource claims.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public static AuthorizationPolicyBuilder RequireResourceRoles(
        this AuthorizationPolicyBuilder builder, params string[] roles) =>
        builder
            .RequireClaim(KeycloakConstants.ResourceAccessClaimType)
            .AddRequirements(new ResourceAccessRequirement(default, roles));

    /// <summary>
    /// Adds realm role requirement to builder. Ensures that at least one realm role is present in realm claims.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public static AuthorizationPolicyBuilder RequireRealmRoles(
        this AuthorizationPolicyBuilder builder, params string[] roles) =>
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
        this AuthorizationPolicyBuilder builder, string resource, string scope) =>
        builder
            .AddRequirements(new DecisionRequirement(resource, scope));
}
