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
endpoints
    .MapGet("DynamicProtectedResourcePolicy", () => new { Success = true })
    .RequireProtectedResource("my-workspace", "workspace:delete");

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

public partial class Program { }
