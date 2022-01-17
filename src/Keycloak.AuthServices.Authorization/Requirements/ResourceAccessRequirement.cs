namespace Keycloak.AuthServices.Authorization.Requirements;

using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

public class ResourceAccessRequirement : IAuthorizationRequirement
{
    public string? Resource { get; set; }
    public IReadOnlyCollection<string> Roles { get; }

    public ResourceAccessRequirement(string? resource, params string[] roles)
    {
        this.Resource = resource;
        this.Roles = roles;
    }

    public override string ToString() =>
        $"{nameof(ResourceAccessRequirement)}: Roles are one of the following values: ({string.Join("|", this.Roles)}) for client '{this.Resource}'";
}

public class ResourceAccessRequirementHandler : AuthorizationHandler<ResourceAccessRequirement>
{
    private readonly KeycloakInstallationOptions keycloakOptions;
    private readonly ILogger<ResourceAccessRequirementHandler> logger;

    public ResourceAccessRequirementHandler(
        KeycloakInstallationOptions keycloakOptions,
        ILogger<ResourceAccessRequirementHandler> logger)
    {
        this.keycloakOptions = keycloakOptions;
        this.logger = logger;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceAccessRequirement requirement)
    {
        var clientId = requirement.Resource ?? this.keycloakOptions.Resource;
        requirement.Resource = clientId;

        var success = false;

        if (context.User.Claims.TryGetResourceCollection(out var resourcesAccess) &&
            resourcesAccess.TryGetValue(clientId, out var resourceAccess))
        {
            success = resourceAccess.Roles.Intersect(requirement.Roles).Any();

            if (success)
            {
                context.Succeed(requirement);
            }
        }

        this.logger.LogDebug(
            "[{Requirement}] Access outcome {Outcome} for user {UserName}",
            requirement.ToString(), success, context.User.Identity?.Name);

        return Task.CompletedTask;
    }
}
