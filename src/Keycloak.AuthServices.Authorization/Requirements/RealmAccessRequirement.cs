namespace Keycloak.AuthServices.Authorization.Requirements;

using Common;
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
public partial class RealmAccessRequirementHandler : AuthorizationHandler<RealmAccessRequirement>
{
    private readonly ILogger<RealmAccessRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="logger"></param>
    public RealmAccessRequirementHandler(ILogger<RealmAccessRequirementHandler> logger)
    {
        this.logger = logger;
    }

    [LoggerMessage(100, LogLevel.Debug,
        "[{Requirement}] Access outcome {Outcome} for user {UserName}")]
    partial void RealmAuthorizationResult(string requirement, bool outcome, string? userName);

    /// <inheritdoc />
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RealmAccessRequirement requirement)
    {
        var success = false;
        if (context.User.Claims.TryGetRealmResource(out var resourceAccess))
        {
            success = resourceAccess.Roles.Intersect(requirement.Roles).Any();

            if (success)
            {
                context.Succeed(requirement);
            }
        }

        this.RealmAuthorizationResult(
            requirement.ToString(), success, context.User.Identity?.Name);

        return Task.CompletedTask;
    }
}
