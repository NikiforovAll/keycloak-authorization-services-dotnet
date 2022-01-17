namespace Keycloak.AuthServices.Authorization.Requirements;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Sdk.AuthZ;

public class DecisionRequirement : IAuthorizationRequirement
{
    public string Resource { get; }
    public string Scope { get; }

    public DecisionRequirement(string resource, string scope)
    {
        this.Resource = resource;
        this.Scope = scope;
    }

    public DecisionRequirement(string resource, string id, string scope)
        : this($"{resource}/{id}", scope)
    {
    }

    public override string ToString() => $"{nameof(DecisionRequirement)}: {this.Resource}#{this.Scope}";
}

public class DecisionRequirementHandler : AuthorizationHandler<DecisionRequirement>
{
    private readonly IKeycloakProtectionClient client;
    private readonly ILogger<DecisionRequirementHandler> logger;

    public DecisionRequirementHandler(IKeycloakProtectionClient client,
        ILogger<DecisionRequirementHandler> logger)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DecisionRequirement requirement)
    {
        var success = await this.client.VerifyAccessToResource(
            requirement.Resource, requirement.Scope, CancellationToken.None);

        this.logger.LogDebug(
            "[{Requirement}] Access outcome {Outcome} for user {UserName}",
            requirement.ToString(), success, context.User.Identity.Name);
        if (success)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
