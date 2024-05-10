namespace Keycloak.AuthServices.Authorization.Requirements;

using System;
using Keycloak.AuthServices.Common.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

/// <summary>
/// Realm requirement
/// </summary>
public class RealmAccessRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Required roles
    /// </summary>
    public IReadOnlyCollection<string> Roles { get; }

    /// <summary>
    /// Constructs requirement
    /// </summary>
    /// <param name="roles"></param>
    public RealmAccessRequirement(params string[] roles) => this.Roles = roles;

    /// <inheritdoc/>
    public override string ToString() =>
        $"{nameof(RealmAccessRequirement)}: Roles are one of the following values: ({string.Join("|", this.Roles)})";
}

/// <summary>
/// </summary>
public class RealmAccessRequirementHandler : AuthorizationHandler<RealmAccessRequirement>
{
    private readonly KeycloakMetrics metrics;
    private readonly ILogger<RealmAccessRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="metrics"></param>
    /// <param name="logger"></param>
    public RealmAccessRequirementHandler(
        KeycloakMetrics metrics,
        ILogger<RealmAccessRequirementHandler> logger
    )
    {
        this.metrics = metrics;
        this.logger = logger;
    }

    /// <inheritdoc />
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RealmAccessRequirement requirement
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

        var success = false;

        if (context.User.Claims.TryGetRealmResource(out var resourceAccess))
        {
            success = resourceAccess.Roles.Intersect(requirement.Roles).Any();
        }

        this.logger.LogAuthorizationResult(requirement.ToString()!, success, userName);

        if (success)
        {
            this.metrics.SucceedRequirement(nameof(RealmAccessRequirement));

            context.Succeed(requirement);
        }
        else
        {
            this.metrics.FailRequirement(nameof(RealmAccessRequirement));
            this.logger.LogAuthorizationFailed(requirement.ToString()!, userName);
        }

        return Task.CompletedTask;
    }
}
