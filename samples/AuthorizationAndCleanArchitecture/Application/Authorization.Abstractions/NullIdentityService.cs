namespace Api.Application.Authorization.Abstractions;

using System.Security.Claims;
using System.Threading.Tasks;

public class NullIdentityService : IIdentityService
{
    public string UserId => default!;

    public string UserName => default!;

    public ClaimsPrincipal Principal => default!;

    public Task<bool> AuthorizeAsync(string policyName) => throw new NotImplementedException();
    public Task<bool> AuthorizeAsync(object resource, string policyName) => throw new NotImplementedException();
    public bool IsInRoleAsync(string role) => throw new NotImplementedException();
}
