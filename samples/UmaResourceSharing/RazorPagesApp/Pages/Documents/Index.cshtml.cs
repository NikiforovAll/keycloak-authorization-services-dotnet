namespace RazorPagesApp.Pages.Documents;

using System.Security.Claims;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

[Authorize]
public class IndexModel(
    IKeycloakProtectionClient protectionClient,
    IOptions<KeycloakAuthorizationServerOptions> authzOptions
) : PageModel
{
    public string? Message { get; set; }
    public bool IsError { get; set; }

    public async Task<IActionResult> OnPostRequestAccessAsync(string scope)
    {
        var realm = authzOptions.Value.Realm;
        var userId = this.User.FindFirstValue("sub");
        if (string.IsNullOrEmpty(userId))
        {
            this.Message = "Unable to determine user identity.";
            this.IsError = true;
            return this.Page();
        }

        var resourceIds = await protectionClient.GetResourcesIdsAsync(
            realm,
            new GetResourcesRequestParameters { Name = "shared-document", ExactName = true }
        );

        if (resourceIds.Count == 0)
        {
            this.Message = "Resource 'shared-document' not found.";
            this.IsError = true;
            return this.Page();
        }

        var resourceId = resourceIds[0];

        var existing = await protectionClient.GetPermissionTicketsAsync(
            realm,
            new GetPermissionTicketsRequestParameters
            {
                ResourceId = resourceId,
                Requester = userId,
            }
        );

        var existingScopes = existing.Select(t => t.ScopeName ?? t.Scope).ToHashSet();

        if (existingScopes.Contains(scope))
        {
            this.Message = $"A request for '{scope}' scope already exists.";
            return this.Page();
        }

        var ticket = new PermissionTicket
        {
            Resource = resourceId,
            Requester = userId,
            ScopeName = scope,
            Granted = false,
        };

        var response = await protectionClient.CreatePermissionTicketWithResponseAsync(
            realm,
            ticket
        );

        if (response.IsSuccessStatusCode)
        {
            this.Message =
                $"Access request for '{scope}' submitted. The resource owner will review your request.";
        }
        else
        {
            this.Message = $"Failed to submit access request ({(int)response.StatusCode}).";
            this.IsError = true;
        }

        return this.Page();
    }
}
