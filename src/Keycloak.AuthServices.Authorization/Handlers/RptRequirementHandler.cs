namespace Keycloak.AuthServices.Authorization.Handlers;

using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

public class RptRequirementHandler : AuthorizationHandler<RptRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RptRequirement requirement)
    {
        // the client application responsible for acquiring of the token
        // should request special RPT access_token
        var authorizationClaim = context.User.FindFirstValue("authorization");
        if (String.IsNullOrWhiteSpace(authorizationClaim))
        {
            return Task.CompletedTask;
        }

        /* Sample value for authorizationClaim
        {
            "permissions":[
                {
                    "scopes":["read"],
                    "rsid":"deb61104-5008-4001-8792-ac5734b1235b",
                    "rsname":"customers"
                },
                {
                    "scopes":["read","create","update","archive"],
                    "rsid":"cca3a1af-b8c5-478f-acee-01044961db50",
                    "rsname":"projects"
                }
            ]
        }
        */
        var json = JsonDocument.Parse(authorizationClaim);
        var permissions = json.RootElement.GetProperty("permissions");
        foreach (var permission in permissions.EnumerateArray())
        {
            if (permission.GetProperty("rsname").GetString() != requirement.Resource)
            {
                continue;
            }

            if (permission.GetProperty("scopes")
                .EnumerateArray()
                .All(scope => scope.GetString() != requirement.Scope))
            {
                continue;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
