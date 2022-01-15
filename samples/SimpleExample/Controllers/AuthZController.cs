namespace Api.Controllers;

using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Mvc;

[Route("api/authz")]
public class KeycloakAuthZController : ApiControllerBase
{
    private readonly IKeycloakProtectionClient protectionClient;

    public KeycloakAuthZController(IKeycloakProtectionClient protectionClient)
    {
        this.protectionClient = protectionClient;
    }

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
