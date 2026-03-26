namespace Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

/// <summary>
/// Dynamically provides authorization policies for protected resources based on policy name conventions.
/// </summary>
public class ProtectedResourcePolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly IProtectedResourcePolicyBuilder builder;

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    /// <param name="builder"></param>
    public ProtectedResourcePolicyProvider(
        IOptions<AuthorizationOptions> options,
        IProtectedResourcePolicyBuilder? builder = null
    )
        : base(options)
    {
        this.builder = builder ?? new DefaultProtectedResourcePolicyBuilder();
    }

    /// <inheritdoc />
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var registeredPolicy = await base.GetPolicyAsync(policyName);
        if (!ProtectedResourcePolicy.Match(policyName) || registeredPolicy is not null)
        {
            return registeredPolicy;
        }

        return this.builder.Build(policyName);
    }
}
