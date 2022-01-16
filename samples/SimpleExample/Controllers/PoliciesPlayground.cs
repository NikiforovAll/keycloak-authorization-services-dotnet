namespace Api.Controllers;

using Data;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("/policies-playground")]
public class PoliciesPlayground : ApiControllerBase
{
    /// <summary>
    /// Roles are mapped from keycloak claims.
    /// Use keycloak mappers to implement RBAC.
    /// </summary>
    [Authorize(Roles = "Manager, Guest")]
    [HttpGet("role-based")]
    public void RoleCheck() => this.Ok();

    /// <summary>
    /// Named policy registered via policyBuilder.
    /// Default policy builder should be registered in DI.
    /// </summary>
    [Authorize(Policy = PolicyConstants.MyCustomPolicy)]
    [HttpGet("custom-policy-from-builder")]
    public void CustomPolicy() => this.Ok();

    /// <summary>
    /// Policies used by custom authorize attributes should be resolved.
    /// Create attribute derived from <see cref="AuthorizeAttribute"/>
    /// </summary>
    [HttpGet("custom-policy-attribute")]
    public void CustomPolicyFromAttribute() => this.Ok();

    /// <summary>
    /// Automatically registers policies based on <see cref="ProtectedResourcePolicyProvider"/>
    /// </summary>
    [Authorize(Policy = "workspaces#workspaces:read")]
    [HttpGet("auto-registered-policy")]
    public void AutoPolicy() => this.Ok();

    [HttpGet("authorization-services")]
    public async Task<IActionResult> AspNetCoreAuthorizationServices(
        [FromQuery] string? resource,
        [FromQuery] string? scope,
        [FromServices] IAuthorizationService authorizationService)
    {
        // var workspace = new Workspace() {Id = id ?? Guid.Parse("1d654851-ff72-42b6-bd4a-468f95f61c7a")};
        // var requirement = new DecisionRequirement($"workspaces/{workspace.Id}", "read");
        var requirement = new DecisionRequirement(resource, scope);

        var accessed = await authorizationService
            .AuthorizeAsync(this.User, null, requirement);

        return !accessed.Succeeded ? this.Forbid() : this.Ok();
    }
}
