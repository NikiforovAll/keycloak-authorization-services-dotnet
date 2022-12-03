namespace Api.Application.Authorization.Abstractions;

using System.Security.Claims;

public class NullUserService : ICurrentUserService
{
    public string UserId => default!;

    public string UserName => default!;

    public ClaimsPrincipal Principal => default!;
}
