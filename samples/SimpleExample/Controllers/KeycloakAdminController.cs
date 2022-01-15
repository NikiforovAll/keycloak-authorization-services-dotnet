namespace Api.Controllers;

using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Mvc;

[Route("api/keycloak")]
public class KeycloakAdminController: ApiControllerBase
{
    private readonly IKeycloakRealmClient keycloakRealmClient;

    public KeycloakAdminController(IKeycloakRealmClient keycloakRealmClient)
    {
        this.keycloakRealmClient = keycloakRealmClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetRealms()
    {
        return this.Ok(await this.keycloakRealmClient.GetRealm("authz"));
    }
}
