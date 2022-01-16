namespace Keycloak.AuthServices.Authorization;

using Common;
using Microsoft.AspNetCore.Authorization;
using Requirements;

public static class PoliciesBuilderExtensions
{
    public static AuthorizationPolicyBuilder RequireResourceRoles(
        this AuthorizationPolicyBuilder builder, params string[] roles) =>
        builder
            .RequireClaim(KeycloakConstants.ResourceAccessClaimType)
            .AddRequirements(new ResourceAccessRequirement(default, roles));

    public static AuthorizationPolicyBuilder RequireRealmRoles(
        this AuthorizationPolicyBuilder builder, params string[] roles) =>
        builder
            .RequireClaim(KeycloakConstants.ResourceAccessClaimType)
            .AddRequirements(new RealmAccessRequirement(roles));

    public static AuthorizationPolicyBuilder RequireProtectedResource(
        this AuthorizationPolicyBuilder builder, string resource, string scope) =>
        builder
            .AddRequirements(new DecisionRequirement(resource, scope));
}
