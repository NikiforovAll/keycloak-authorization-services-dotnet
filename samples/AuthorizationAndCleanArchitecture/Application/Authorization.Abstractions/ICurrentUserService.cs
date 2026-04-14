namespace Api.Application.Authorization.Abstractions;

using System.Security.Claims;

public interface ICurrentUserService
{
    public string? UserId { get; }

    public string? UserName { get; }

    public ClaimsPrincipal? Principal { get; }
}
