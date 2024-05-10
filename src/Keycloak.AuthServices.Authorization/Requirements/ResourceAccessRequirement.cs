namespace Keycloak.AuthServices.Authorization.Requirements;

using System;
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
    private readonly KeycloakMetrics metrics;
    private readonly ILogger<ResourceAccessRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="keycloakOptions"></param>
    /// <param name="metrics"></param>
    /// <param name="logger"></param>
    public ResourceAccessRequirementHandler(
        IOptions<KeycloakAuthorizationOptions> keycloakOptions,
        KeycloakMetrics metrics,
        ILogger<ResourceAccessRequirementHandler> logger
    )
    {
        this.keycloakOptions = keycloakOptions;
        this.metrics = metrics;
        this.logger = logger;
    }

    /// <inheritdoc />
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceAccessRequirement requirement
    )
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(requirement);

        var userName = context.User.Identity?.Name;

        if (!context.User.IsAuthenticated())
        {
            this.metrics.SkipRequirement(nameof(RealmAccessRequirement));
            this.logger.LogRequirementSkipped(
                nameof(ParameterizedProtectedResourceRequirementHandler)
            );

            return Task.CompletedTask;
        }

        var clientId =
            requirement.Resource
            ?? this.keycloakOptions.Value.RolesResource
            ?? this.keycloakOptions.Value.Resource;
        requirement.Resource = clientId;

        var success = false;

        if (string.IsNullOrWhiteSpace(clientId))
        {
            this.metrics.ErrorRequirement(nameof(ResourceAccessRequirement));
            throw new KeycloakException(
                $"Unable to resolve Resource for Role Validation - please make sure {nameof(KeycloakAuthorizationOptions)} are configured. \n\n See documentation for more details - https://nikiforovall.github.io/keycloak-authorization-services-dotnet/configuration/configuration-authorization.html#require-resource-roles"
            );
        }

        if (
            context.User.Claims.TryGetResourceCollection(out var resourcesAccess)
            && resourcesAccess.TryGetValue(clientId, out var resourceAccess)
        )
        {
            success = resourceAccess.Roles.Intersect(requirement.Roles).Any();
        }

        this.logger.LogAuthorizationResult(requirement.ToString()!, success, userName);

        if (success)
        {
            this.metrics.SucceedRequirement(nameof(ResourceAccessRequirement));

            context.Succeed(requirement);
        }
        else
        {
            this.metrics.FailRequirement(nameof(ResourceAccessRequirement));
            this.logger.LogAuthorizationFailed(requirement.ToString()!, userName);
        }

        return Task.CompletedTask;
    }
}
