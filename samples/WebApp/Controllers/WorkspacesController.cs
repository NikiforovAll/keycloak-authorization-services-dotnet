namespace WebApp_OpenIDConnect_DotNet.Controllers;

using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class WorkspacesController : Controller
{
    private static readonly List<Workspace> Workspaces =
    [
        new(1, "Alpha", "First workspace"),
        new(2, "Beta", "Second workspace"),
        new(3, "Gamma", "Third workspace"),
    ];

    [ProtectedResource("workspace", "workspace:list")]
    public IActionResult Index() => this.View(Workspaces);

    [ProtectedResource("workspace", "workspace:read")]
    public IActionResult Details(int id)
    {
        var workspace = Workspaces.Find(w => w.Id == id);
        if (workspace is null)
        {
            return this.NotFound();
        }

        return this.View(workspace);
    }
}

public record Workspace(int Id, string Name, string Description);
