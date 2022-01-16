namespace Api.Application.Authorization.Abstractions;

using System.Security.Claims;

public interface ICurrentUserService
{
    string? UserId { get; }

    string? UserName { get; }

    ClaimsPrincipal? Principal { get; }
}
