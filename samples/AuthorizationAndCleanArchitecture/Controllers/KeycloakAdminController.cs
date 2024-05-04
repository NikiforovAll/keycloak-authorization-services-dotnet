namespace Api.Controllers;

using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Protection;
using Microsoft.AspNetCore.Mvc;

[Route("api/keycloak-api")]
public class KeycloakAdminController : ApiControllerBase
{
    private readonly IKeycloakRealmClient keycloakRealmClient;
    private readonly IKeycloakProtectionClient protectedResourceClient;
    private const string DefaultRealm = "authz";

    public KeycloakAdminController(
        IKeycloakRealmClient keycloakRealmClient,
        IKeycloakProtectionClient protectedResourceClient)
    {
        this.keycloakRealmClient = keycloakRealmClient;
        this.protectedResourceClient = protectedResourceClient;
    }

    [HttpGet("realms")]
    public async Task<IActionResult> GetRealms()
    {
        return this.Ok(await this.keycloakRealmClient.GetRealmAsync(DefaultRealm));
    }

    [HttpGet("resources")]
    public async Task<IActionResult> GetResources()
    {
        return this.Ok(await this.protectedResourceClient.GetResources(DefaultRealm));
    }

    [HttpGet("resources/{id}")]
    public async Task<IActionResult> GetResource(string id)
    {
        return this.Ok(await this.protectedResourceClient.GetResource(DefaultRealm, id));
    }

    [HttpPost("resources")]
    public async Task<IActionResult> CreateResource()
    {
        var resource = new Resource($"workspaces/{Guid.NewGuid()}", new[] { "workspaces:read", "workspaces:delete" })
        {
            Attributes = { ["test"] = "Owner, Operations" },
            Type = "urn:workspace-authz:resource:workspaces",
        };
        return this.Ok(await this.protectedResourceClient.CreateResource(DefaultRealm, resource));
    }
}
