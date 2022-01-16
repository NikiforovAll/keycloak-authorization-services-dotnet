namespace Keycloak.AuthServices.Authorization.Requirements;

using Common;
using Microsoft.AspNetCore.Authorization;

public class RealmAccessRequirement : IAuthorizationRequirement
{
    public IReadOnlyCollection<string> Roles { get; }

    public RealmAccessRequirement(params string[] roles) => this.Roles = roles;

    public override string ToString() =>
        $"{nameof(RealmAccessRequirement)}: Roles are one of the following values: ({string.Join("|", this.Roles)}).";
}

public class RealmAccessRequirementHandler : AuthorizationHandler<RealmAccessRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RealmAccessRequirement requirement)
    {
        if (!context.User.Claims.TryGetRealmResource(out var resourceAccess))
        {
            return Task.CompletedTask;
        }

        if (resourceAccess.Roles.Intersect(requirement.Roles).Any())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
