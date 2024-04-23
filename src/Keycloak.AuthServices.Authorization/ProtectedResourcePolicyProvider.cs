namespace Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Requirements;

/// <summary>
/// </summary>
public class ProtectedResourcePolicyProvider
    : DefaultAuthorizationPolicyProvider
{
    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    public ProtectedResourcePolicyProvider(
        IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    /// <inheritdoc />
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var registeredPolicy = await base.GetPolicyAsync(policyName);
        if (!ProtectedResourcePolicy.Match(policyName) || registeredPolicy is not null)
        {
            return registeredPolicy;
        }

        // TODO: policy should be cached and managed properly, not production ready
        // https://0xnf.github.io/posts/oauthserver/15/#dynamically-handling-policies
        var builder = new AuthorizationPolicyBuilder();
        var tokens = policyName.Split('#');

        if (tokens is not {Length: 2})
        {
            return default;
        }

        var authorizationRequirement = new DecisionRequirement(tokens[0], tokens[1]);
        builder.AddRequirements(authorizationRequirement);

        return builder.Build();
    }
}
