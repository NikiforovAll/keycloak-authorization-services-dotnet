namespace Keycloak.AuthServices.Authorization;

using Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

public class ProtectedResourcePolicyProvider
    : DefaultAuthorizationPolicyProvider
{
    public ProtectedResourcePolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var registeredPolicy = await base.GetPolicyAsync(policyName);
        if (!policyName.Contains('#') || registeredPolicy is not null)
        {
            return registeredPolicy;
        }

        // TODO: policy should be cached and managed properly not production ready
        // https://0xnf.github.io/posts/oauthserver/15/#dynamically-handling-policies
        var builder = new AuthorizationPolicyBuilder();
        var tokens = policyName.Split('#');

        if (tokens is not {Length: 2})
        {
            return default; //exit
        }
        builder.AddRequirements(new DecisionRequirement(tokens[0], tokens[1]));

        return builder.Build();
    }
}
