namespace Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Authorization;
using Requirements;

/// <summary>
/// Default implementation of <see cref="IProtectedResourcePolicyBuilder"/> that creates
/// a <see cref="DecisionRequirement"/> from the policy name.
/// </summary>
public class DefaultProtectedResourcePolicyBuilder : IProtectedResourcePolicyBuilder
{
    /// <inheritdoc />
    public AuthorizationPolicy? Build(string policyName)
    {
        var tokens = policyName.Split('#');

        if (tokens is not { Length: 2 })
        {
            return null;
        }

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new DecisionRequirement(tokens[0], tokens[1]))
            .Build();
    }
}
