namespace Keycloak.AuthServices.Authorization;

using Common;
using Microsoft.AspNetCore.Authorization;
using Requirements;

/// <summary>
/// </summary>
public static class PoliciesBuilderExtensions
{
    /// <summary>
    /// Add resource role requirement to builder
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
    /// Add realm role requirement to builder
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public static AuthorizationPolicyBuilder RequireRealmRoles(
        this AuthorizationPolicyBuilder builder, params string[] roles) =>
        builder
            .RequireClaim(KeycloakConstants.ResourceAccessClaimType)
            .AddRequirements(new RealmAccessRequirement(roles));

    /// <summary>
    /// Add protected resource requirement to builder
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
