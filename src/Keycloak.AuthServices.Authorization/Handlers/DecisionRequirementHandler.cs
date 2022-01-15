namespace Keycloak.AuthServices.Authorization.Handlers;

using Microsoft.AspNetCore.Authorization;
using Sdk.Admin;

public class DecisionRequirementHandler : AuthorizationHandler<DecisionRequirement>
{
    private readonly IKeycloakProtectionClient client;

    public DecisionRequirementHandler(IKeycloakProtectionClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DecisionRequirement requirement)
    {
        var success = await this.client.VerifyAccessToResource(
            requirement.Resource, requirement.Scope, CancellationToken.None);

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
