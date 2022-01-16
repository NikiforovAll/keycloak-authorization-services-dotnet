namespace Api.Application.Authorization.Abstractions.Impl;

using System.Security.Claims;
using Abstractions;
using Microsoft.AspNetCore.Authorization;

public class IdentityService : IIdentityService
{
    private readonly IAuthorizationService authorizationService;
    private readonly ICurrentUserService userService;

    public IdentityService(IAuthorizationService authorizationService, ICurrentUserService userService)
    {
        this.authorizationService = authorizationService
                                    ?? throw new ArgumentNullException(nameof(authorizationService));
        this.userService = userService
                           ?? throw new ArgumentNullException(nameof(userService));
    }

    #region ICurrentUserService

    public string? UserId => this.userService.UserId;

    public string? UserName => this.userService.UserName;

    public ClaimsPrincipal? Principal => this.userService.Principal;

    #endregion

    public Task<bool> AuthorizeAsync(string policyName) =>
        this.AuthorizeAsync(this.userService?.Principal, policyName);

    public async Task<bool> AuthorizeAsync(object resource, string policyName)
    {
        var result = await this.authorizationService
            .AuthorizeAsync(this.userService?.Principal, resource, policyName);

        return result.Succeeded;
    }

    public bool IsInRoleAsync(string role) => this.Principal?.IsInRole(role) ?? false;

    private async Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string policyName)
    {
        var result = await this.authorizationService
            .AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }
}
