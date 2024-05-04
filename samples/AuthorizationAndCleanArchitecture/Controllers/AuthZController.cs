namespace Api.Controllers;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Mvc;

[Route("api/authz")]
public class KeycloakAuthZController : ApiControllerBase
{
    private readonly IAuthorizationServerClient protectionClient;

    public KeycloakAuthZController(IAuthorizationServerClient protectionClient) =>
        this.protectionClient = protectionClient;

    [HttpGet("try-resource")]
    public async Task<IActionResult> VerifyAccess(
        [FromQuery] string? resource,
        [FromQuery] string? scope,
        CancellationToken cancellationToken)
    {
        var verified = await this.protectionClient
            .VerifyAccessToResource(resource ?? "workspaces", scope ?? "workspaces:read", cancellationToken);

        return this.Ok(verified);
    }
}
