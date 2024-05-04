namespace Api.Application.Commands;

using System.Threading;
using System.Threading.Tasks;
using Authorization;
using Authorization.Abstractions;
using Data;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using MediatR;

[AuthorizeProtectedResource("workspaces", "workspaces:create")]
public record CreateWorkspaceCommand(string Name, IList<Project>? Projects = default) : IRequest;

public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand>
{
    private readonly IApplicationDbContext db;
    private readonly IKeycloakProtectionClient resourceClient;
    private readonly IIdentityService identityService;

    public CreateWorkspaceCommandHandler(
        IApplicationDbContext db,
        IKeycloakProtectionClient resourceClient,
        IIdentityService identityService
    )
    {
        this.db = db ?? throw new ArgumentNullException(nameof(db));
        this.resourceClient =
            resourceClient ?? throw new ArgumentNullException(nameof(resourceClient));
        this.identityService =
            identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    public async Task Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var (name, projects) = request;

        var workspace = new Workspace { Name = name, Projects = projects ?? new List<Project>() };
        this.db.Workspaces.Add(workspace);
        await this.db.SaveChangesAsync(cancellationToken);

        var userName = this.identityService?.UserName ?? throw new InvalidOperationException();
        await this.resourceClient.CreateResourceAsync(
            "authz",
            new Resource(
                $"workspaces/{workspace.Id}",
                new[] { "workspaces:read", "workspaces:delete" }
            )
            {
                Attributes = { [userName] = "Owner" },
                Type = "urn:workspace-authz:resource:workspaces",
            },
            cancellationToken
        );
    }
}
