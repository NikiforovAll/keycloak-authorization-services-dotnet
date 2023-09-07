namespace Keycloak.AuthServices.Authorization.Requirements;

using Sdk.AuthZ;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

/// <summary>
/// Resource requirement
/// </summary>
public class ResourceAccessRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Client/Resource name
    /// </summary>
    public string? Resource { get; set; }

    /// <summary>
    /// Roles
    /// </summary>
    public IReadOnlyCollection<string> Roles { get; }

    /// <summary>
    /// Constructs
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="roles"></param>
    public ResourceAccessRequirement(string? resource, params string[] roles)
    {
        this.Resource = resource;
        this.Roles = roles;
    }

    /// <inheritdoc />
    public override string ToString() =>
        $"{nameof(ResourceAccessRequirement)}: Roles are one of the following values: ({string.Join("|", this.Roles)}) for client '{this.Resource}'";
}

/// <summary>
/// </summary>
public partial class ResourceAccessRequirementHandler : AuthorizationHandler<ResourceAccessRequirement>
{
    private readonly KeycloakProtectionClientOptions keycloakOptions;
    private readonly ILogger<ResourceAccessRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="keycloakOptions"></param>
    /// <param name="logger"></param>
    public ResourceAccessRequirementHandler(
        KeycloakProtectionClientOptions keycloakOptions,
        ILogger<ResourceAccessRequirementHandler> logger)
    {
        this.keycloakOptions = keycloakOptions;
        this.logger = logger;
    }

    [LoggerMessage(101, LogLevel.Debug,
        "[{Requirement}] Access outcome {Outcome} for user {UserName}")]
    partial void ResourceAuthorizationResult(string requirement, bool outcome, string? userName);

    /// <inheritdoc />
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

        this.ResourceAuthorizationResult(
            requirement.ToString(), success, context.User.Identity?.Name);

        return Task.CompletedTask;
    }
}
