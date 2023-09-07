namespace Keycloak.AuthServices.Authorization.Requirements;

using Sdk.AuthZ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

/// <summary>
/// Decision requirement
/// </summary>
public class DecisionRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Resource name
    /// </summary>
    public string Resource { get; }


    /// <summary>
    /// Resource scope
    /// </summary>
    public string Scope { get; }

    /// <summary>
    /// Constructs requirement
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    public DecisionRequirement(string resource, string scope)
    {
        this.Resource = resource;
        this.Scope = scope;
    }

    /// <summary>
    /// Constructs requirement based on resource id
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="id"></param>
    /// <param name="scope"></param>
    public DecisionRequirement(string resource, string id, string scope)
        : this($"{resource}/{id}", scope)
    {
    }

    /// <inheritdoc />
    public override string ToString() => $"{nameof(DecisionRequirement)}: {this.Resource}#{this.Scope}";
}

/// <summary>
/// </summary>
public partial class DecisionRequirementHandler : AuthorizationHandler<DecisionRequirement>
{
    private readonly IKeycloakProtectionClient client;
    private readonly ILogger<DecisionRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public DecisionRequirementHandler(IKeycloakProtectionClient client,
        ILogger<DecisionRequirementHandler> logger)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [LoggerMessage(103, LogLevel.Debug,
        "[{Requirement}] Access outcome {Outcome} for user {UserName}")]
    partial void DecisionAuthorizationResult(string requirement, bool outcome, string? userName);

    /// <inheritdoc/>
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DecisionRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var success = await this.client.VerifyAccessToResource(
            requirement.Resource, requirement.Scope, CancellationToken.None);

            this.DecisionAuthorizationResult(
                requirement.ToString(), success, context.User.Identity?.Name);

            if (success)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
        else
        {
            this.DecisionAuthorizationResult(
                requirement.ToString(), false, context.User.Identity?.Name);
        }
    }
}
