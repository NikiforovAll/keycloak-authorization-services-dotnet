using System.Security.Claims;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseAuthentication();
app.UseAuthorization();

var endpoints = app.MapGroup("/endpoints").RequireAuthorization();

endpoints.MapGet("1", () => new { Success = true });
endpoints.MapGet("RunPolicyBuyName", AuthorizeAsync);

var protectedResources = app.MapGroup("/pr");

SingleResourceSingleScopeSingleEndpoint(
    protectedResources,
    nameof(SingleResourceSingleScopeSingleEndpoint)
);

SingleResourceMultipleScopesSingleEndpoint(
    protectedResources,
    nameof(SingleResourceMultipleScopesSingleEndpoint)
);

SingleResourceMultipleScopesSingleEndpointV2(
    protectedResources,
    nameof(SingleResourceMultipleScopesSingleEndpointV2)
);

SingleResourceMultipleScopesEndpointHierarchy(
    protectedResources,
    nameof(SingleResourceMultipleScopesEndpointHierarchy)
);

MultipleResourcesMultipleScopesSingleEndpoint(
    protectedResources,
    nameof(MultipleResourcesMultipleScopesSingleEndpoint)
);

MultipleResourcesMultipleScopesEndpointHierarchy(
    protectedResources,
    nameof(MultipleResourcesMultipleScopesEndpointHierarchy)
);

app.Run();

static async Task<IResult> AuthorizeAsync(
    string policy,
    ClaimsPrincipal claimsPrincipal,
    IAuthorizationService authorizationService
)
{
    var result = await authorizationService.AuthorizeAsync(claimsPrincipal, policy);

    if (!result.Succeeded)
    {
        return TypedResults.Forbid();
    }

    return TypedResults.Ok(new { Success = result.Succeeded });
}

static Response Run(string? resource, string? scopes) => new(true, resource, scopes);

static void SingleResourceSingleScopeSingleEndpoint(RouteGroupBuilder app, string path) =>
    #region SingleResourceSingleScopeSingleEndpoint
    app.MapGet(path, Run).RequireProtectedResource("workspaces", "workspace:delete");
    #endregion SingleResourceSingleScopeSingleEndpoint

static void SingleResourceMultipleScopesSingleEndpoint(RouteGroupBuilder app, string path) =>
    #region SingleResourceMultipleScopesSingleEndpoint
    app.MapGet(path, Run)
        .RequireProtectedResource("workspaces", "workspace:read")
        .RequireProtectedResource("workspaces", "workspace:delete");
    #endregion SingleResourceMultipleScopesSingleEndpoint

static void SingleResourceMultipleScopesSingleEndpointV2(RouteGroupBuilder app, string path) =>
    #region SingleResourceMultipleScopesSingleEndpointV2
    app.MapGet(path, Run)
        .RequireProtectedResource("workspaces", ["workspace:read", "workspace:delete"]);
    #endregion SingleResourceMultipleScopesSingleEndpointV2

static void SingleResourceMultipleScopesEndpointHierarchy(RouteGroupBuilder app, string path)
{
    #region SingleResourceMultipleScopesEndpointHierarchy
    var endpoints = app.MapGroup(string.Empty)
        .RequireProtectedResource("workspaces", "workspace:read");

    // require workspaces#workspace:read,workspace:delete
    endpoints.MapGet(path, Run).RequireProtectedResource("workspaces", "workspace:delete");
    // require workspaces#workspace:read, inherited from parent group
    endpoints.MapGet("other-endpoint", Run);
    #endregion SingleResourceMultipleScopesEndpointHierarchy
}

static void MultipleResourcesMultipleScopesSingleEndpoint(RouteGroupBuilder app, string path) =>
    #region MultipleResourcesMultipleScopesSingleEndpoint
    app.MapGet(path, Run)
        .RequireProtectedResource("workspaces", "workspace:read")
        .RequireProtectedResource("my-workspace", "workspace:delete");
    #endregion MultipleResourcesMultipleScopesSingleEndpoint

static void MultipleResourcesMultipleScopesEndpointHierarchy(RouteGroupBuilder app, string path)
{
    #region MultipleResourcesMultipleScopesEndpointHierarchy

    var endpoints = app.MapGroup(string.Empty)
        .RequireProtectedResource("workspaces", "workspace:read");

    // require workspaces#workspace:read;my-workspace#workspace:delete
    endpoints.MapGet(path, Run).RequireProtectedResource("my-workspace", "workspace:delete");
    #endregion MultipleResourcesMultipleScopesEndpointHierarchy
}

#pragma warning disable CA1050 // Declare types in namespaces
public record Response(bool Success, string? Resource, string? Scopes);

public partial class Program { }
#pragma warning restore CA1050 // Declare types in namespaces
