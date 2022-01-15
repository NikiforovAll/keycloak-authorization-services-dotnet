namespace Keycloak.AuthServices.Authorization.Handlers;

using Microsoft.AspNetCore.Authorization;

public class RptRequirement : IAuthorizationRequirement
{
    public string Resource { get; }
    public string Scope { get; }

    public RptRequirement(string resource, string scope)
    {
        this.Resource = resource;
        this.Scope = scope;
    }
}
