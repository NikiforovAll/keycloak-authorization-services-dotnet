namespace Api.Application.Authorization.Abstractions.Impl;

using System.Security.Claims;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
        this.httpContextAccessor = httpContextAccessor;

    public string? UserId => this.httpContextAccessor
        .HttpContext
        ?.User
        ?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName => this.httpContextAccessor
        .HttpContext
        ?.User
        ?.FindFirstValue("preferred_username");

    public ClaimsPrincipal? Principal => this.httpContextAccessor
        .HttpContext
        ?.User;
}
