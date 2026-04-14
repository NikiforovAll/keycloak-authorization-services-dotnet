namespace RazorPagesApp.Pages.Permissions;

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
    public IList<PermissionTicket> Tickets { get; set; } = [];
    public string? Message { get; set; }
    public bool IsError { get; set; }

    public async Task OnGetAsync()
    {
        await this.LoadTicketsAsync();
    }

    public async Task<IActionResult> OnPostApproveAsync(string ticketId)
    {
        var realm = authzOptions.Value.Realm;
        var ticket = new PermissionTicket { Id = ticketId, Granted = true };

        try
        {
            await protectionClient.UpdatePermissionTicketAsync(realm, ticket);
            this.Message = "Permission approved.";
        }
        catch
        {
            this.Message = "Failed to approve permission.";
            this.IsError = true;
        }

        await this.LoadTicketsAsync();
        return this.Page();
    }

    public async Task<IActionResult> OnPostDenyAsync(string ticketId)
    {
        var realm = authzOptions.Value.Realm;

        try
        {
            await protectionClient.DeletePermissionTicketAsync(realm, ticketId);
            this.Message = "Permission request denied.";
        }
        catch
        {
            this.Message = "Failed to deny permission.";
            this.IsError = true;
        }

        await this.LoadTicketsAsync();
        return this.Page();
    }

    private async Task LoadTicketsAsync()
    {
        var realm = authzOptions.Value.Realm;
        this.Tickets = await protectionClient.GetPermissionTicketsAsync(
            realm,
            new GetPermissionTicketsRequestParameters { Granted = false, ReturnNames = true }
        );
    }
}
