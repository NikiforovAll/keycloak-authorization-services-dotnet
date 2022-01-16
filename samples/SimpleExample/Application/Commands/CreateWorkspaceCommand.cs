namespace Api.Application.Commands;

using System.Threading;
using System.Threading.Tasks;
using Data;
using Authorization;
using Authorization.Abstractions;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using MediatR;

[AuthorizeProtectedResource("workspaces", "workspaces:create")]
public record CreateWorkspaceCommand(string Name, IList<Project>? Projects = default) : IRequest;

public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand>
{
    private readonly IApplicationDbContext db;
    private readonly IKeycloakProtectedResourceClient resourceClient;
    private readonly IIdentityService identityService;

    public CreateWorkspaceCommandHandler(
        IApplicationDbContext db,
        IKeycloakProtectedResourceClient resourceClient,
        IIdentityService identityService)
    {
        this.db = db;
        this.resourceClient = resourceClient;
        this.identityService = identityService;
    }

    public async Task<Unit> Handle(
        CreateWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        var (name, projects) = request;

        var workspace = new Workspace {Name = name, Projects = projects ?? new List<Project>()};
        this.db.Workspaces.Add(workspace);
        await this.db.SaveChangesAsync(cancellationToken);

        await resourceClient.CreateResource("authz",
            new Resource($"workspaces/{workspace.Id}", new[] {"workspaces:read", "workspaces:delete"})
            {
                Attributes = {[identityService.UserName] = "Owner, Operations"},
                Type = "urn:workspace-authz:resource:workspaces",
            });
        return Unit.Value;
    }
}
