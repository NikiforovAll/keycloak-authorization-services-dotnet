namespace Keycloak.AuthServices.Authorization.Requirements;

using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Common.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
public partial class ResourceAccessRequirementHandler
    : AuthorizationHandler<ResourceAccessRequirement>
{
    private readonly IOptions<KeycloakAuthorizationOptions> keycloakOptions;
    private readonly ILogger<ResourceAccessRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="keycloakOptions"></param>
    /// <param name="logger"></param>
    public ResourceAccessRequirementHandler(
        IOptions<KeycloakAuthorizationOptions> keycloakOptions,
        ILogger<ResourceAccessRequirementHandler> logger
    )
    {
        this.keycloakOptions = keycloakOptions;
        this.logger = logger;
    }

    [LoggerMessage(
        101,
        LogLevel.Debug,
        "[{Requirement}] Access outcome {Outcome} for user {UserName}"
    )]
    partial void ResourceAuthorizationResult(string requirement, bool outcome, string? userName);

    /// <inheritdoc />
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceAccessRequirement requirement
    )
    {
        var clientId =
            requirement.Resource
            ?? this.keycloakOptions.Value.RolesResource
            ?? this.keycloakOptions.Value.Resource;
        requirement.Resource = clientId;

        var success = false;

        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new KeycloakException(
                $"Unable to resolve Resource for Role Validation - please make sure {nameof(KeycloakAuthorizationOptions)} are configured."
            );
        }

        if (
            context.User.Claims.TryGetResourceCollection(out var resourcesAccess)
            && resourcesAccess.TryGetValue(clientId, out var resourceAccess)
        )
        {
            success = resourceAccess.Roles.Intersect(requirement.Roles).Any();

            if (success)
            {
                context.Succeed(requirement);
            }
        }

        this.ResourceAuthorizationResult(
            requirement.ToString(),
            success,
            context.User.Identity?.Name
        );

        return Task.CompletedTask;
    }
}
