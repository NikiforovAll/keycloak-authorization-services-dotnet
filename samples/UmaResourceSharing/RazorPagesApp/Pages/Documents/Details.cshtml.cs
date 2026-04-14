namespace RazorPagesApp.Pages.Documents;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize]
public class DetailsModel(IAuthorizationService authorizationService) : PageModel
{
    public string DocumentName { get; set; } = default!;
    public string Scope { get; set; } = default!;
    public string? Content { get; set; }
    public bool AccessGranted { get; set; }

    public async Task OnGetAsync(string name, string scope = "read")
    {
        this.DocumentName = name;
        this.Scope = scope;

        var policyName = scope == "write" ? "UmaWrite" : "UmaRead";
        var result = await authorizationService.AuthorizeAsync(this.User, policyName);

        if (result.Succeeded)
        {
            this.AccessGranted = true;
            this.Content = scope == "write" ? $"Detailed content of {name}" : $"Content of {name}";
        }
    }
}
