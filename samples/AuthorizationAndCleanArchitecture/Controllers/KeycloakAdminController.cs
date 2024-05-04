namespace Api.Controllers;

using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/keycloak-api")]
public class KeycloakAdminController : ApiControllerBase
{
    private readonly IKeycloakRealmClient keycloakRealmClient;
    private readonly IKeycloakProtectionClient protectionClient;
    private const string DefaultRealm = "authz";

    public KeycloakAdminController(
        IKeycloakRealmClient keycloakRealmClient,
        IKeycloakProtectionClient protectionClient
    )
    {
        this.keycloakRealmClient = keycloakRealmClient;
        this.protectionClient = protectionClient;
    }

    [HttpGet("realms")]
    public async Task<IActionResult> GetRealms() =>
        this.Ok(await this.keycloakRealmClient.GetRealmAsync(DefaultRealm));

    [HttpGet("resources")]
    public async Task<IActionResult> GetResources() =>
        this.Ok(await this.protectionClient.GetResourcesAsync(DefaultRealm));

    [HttpGet("resources/{id}")]
    public async Task<IActionResult> GetResource(string id) =>
        this.Ok(await this.protectionClient.GetResourceAsync(DefaultRealm, id));

    [HttpPost("resources")]
    public async Task<IActionResult> CreateResource()
    {
        var resource = new Resource(
            $"workspaces/{Guid.NewGuid()}",
            new[] { "workspaces:read", "workspaces:delete" }
        )
        {
            Attributes = { ["test"] = "Owner, Operations" },
            Type = "urn:workspace-authz:resource:workspaces",
        };
        return this.Ok(await this.protectionClient.CreateResourceAsync(DefaultRealm, resource));
    }
}
