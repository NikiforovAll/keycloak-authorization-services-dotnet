namespace Keycloak.AuthServices.Authorization.Requirements;

using Common;
using Microsoft.AspNetCore.Authorization;

public class ResourceAccessRequirement : IAuthorizationRequirement
{
    public string? Resource { get; }
    public IReadOnlyCollection<string> Roles { get; }

    public ResourceAccessRequirement(string? resource, params string[] roles)
    {
        this.Resource = resource;
        this.Roles = roles;
    }

    public override string ToString() =>
        $"{nameof(ResourceAccessRequirement)}: Roles are one of the following values: ({string.Join("|", this.Roles)}) for client '{this.Resource}'.";
}

public class ResourceAccessRequirementHandler : AuthorizationHandler<ResourceAccessRequirement>
{
    private readonly KeycloakInstallationOptions keycloakOptions;

    public ResourceAccessRequirementHandler(KeycloakInstallationOptions keycloakOptions) =>
        this.keycloakOptions = keycloakOptions;

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceAccessRequirement requirement)
    {
        var clientId = requirement.Resource ?? this.keycloakOptions.Resource;

        if (!context.User.Claims.TryGetResourceCollection(out var resourcesAccess))
        {
            return Task.CompletedTask;
        }

        if (!resourcesAccess.ContainsKey(clientId))
        {
            return Task.CompletedTask;
        }

        var resourceAccess = resourcesAccess[clientId];
        if (resourceAccess.Roles.Intersect(requirement.Roles).Any())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
