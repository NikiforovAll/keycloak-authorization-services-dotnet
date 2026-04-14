namespace Api.Application.Authorization.Abstractions;

public interface IIdentityService : ICurrentUserService
{
    public Task<bool> AuthorizeAsync(string policyName);

    public Task<bool> AuthorizeAsync(object resource, string policyName);

    public bool IsInRoleAsync(string role);
}
