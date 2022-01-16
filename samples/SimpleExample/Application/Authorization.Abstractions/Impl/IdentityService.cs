namespace Api.Application.Authorization.Abstractions.Impl;

using System.Security.Authentication;
using System.Security.Claims;
using Abstractions;
using Microsoft.AspNetCore.Authorization;

public class IdentityService : IIdentityService
{
    private readonly IAuthorizationService authorizationService;
    private readonly ICurrentUserService userService;

    public IdentityService(IAuthorizationService authorizationService, ICurrentUserService userService)
    {
        this.authorizationService =
            authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    #region ICurrentUserService

    public string? UserId => this.userService.UserId;

    public string? UserName => this.userService.UserName;

    public ClaimsPrincipal? Principal => this.userService.Principal;

    #endregion

    public Task<bool> AuthorizeAsync(string policyName)
    {
        var principal = this.GetPrincipal();
        return this.AuthorizeAsync(principal, policyName);
    }


    public async Task<bool> AuthorizeAsync(object resource, string policyName)
    {
        var principal = this.GetPrincipal();
        var result = await this.authorizationService
            .AuthorizeAsync(principal, resource, policyName);

        return result.Succeeded;
    }

    public bool IsInRoleAsync(string role) => this.Principal?.IsInRole(role) ?? false;

    private async Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string policyName)
    {
        var result = await this.authorizationService
            .AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    private ClaimsPrincipal GetPrincipal()
    {
        var principal = this.userService?.Principal
                        ?? throw new AuthenticationException("Couldn't find principal. Please authenticate");
        return principal;
    }
}
