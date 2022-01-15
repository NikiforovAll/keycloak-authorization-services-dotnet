namespace Keycloak.AuthServices.Authorization.Handlers;

using Microsoft.AspNetCore.Authorization;

public class DecisionRequirement : IAuthorizationRequirement
{
    public string Resource { get; }
    public string Scope { get; }

    public DecisionRequirement(string resource, string scope)
    {
        this.Resource = resource;
        this.Scope = scope;
    }
}
