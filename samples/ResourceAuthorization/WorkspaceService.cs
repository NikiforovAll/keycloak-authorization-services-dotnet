namespace ResourceAuthorization;

using Keycloak.AuthServices.Sdk.Kiota.Admin;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;
using ResourceAuthorization.Models;

public class WorkspaceService(
    KeycloakAdminApiClient adminApiClient,
    IKeycloakProtectedResourceClient protectedResourceClient
)
{
    private const string DefaultRealm = "Test";
    private static readonly string[] Scopes =
    [
        "workspace:list",
        "workspace:create",
        "workspace:read",
        "workspace:delete",
        "workspace:list-users",
        "workspace:add-user",
        "workspace:remove-user"
    ];

    private const string WorkspaceType = "urn:workspaces";

    public async Task<IEnumerable<Workspace>> ListWorkspacesAsync()
    {
        var groups = await adminApiClient.Admin.Realms[DefaultRealm].Groups.GetAsync();

        return groups!.Select(Map);
    }

    public async Task<Workspace> GetWorkspaceAsync(string name)
    {
        var group = await this.GetGroupByExactName(name);

        return Map(group!);
    }

    public async Task DeleteWorkspaceAsync(string name)
    {
        var group = await this.GetGroupByExactName(name);

        if (group is not null)
        {
            await adminApiClient.Admin.Realms[DefaultRealm].Groups[group.Id].DeleteAsync();
        }

        var resource = await this.GetResourceByExactName(WorkspaceResourceId(name));

        if (resource is not null)
        {
            await protectedResourceClient.DeleteResourceAsync(DefaultRealm, resource.Id);
        }
    }

    private static Workspace Map(GroupRepresentation group) => new(group.Name!);

    public async Task CreateWorkspaceAsync(Workspace workspace)
    {
        await adminApiClient
            .Admin.Realms[DefaultRealm]
            .Groups.PostAsync(new GroupRepresentation() { Name = workspace.Name, });

        await protectedResourceClient.CreateResourceAsync(
            DefaultRealm,
            new Resource(WorkspaceResourceId(workspace.Name), Scopes) { Type = WorkspaceType }
        );
    }

    public async Task AddMember(string groupName, User member)
    {
        var group = await this.GetGroupByExactName(groupName);
        var user = await this.GetUserByExactName(member.Email);

        if (group is null || user is null)
        {
            return;
        }

        await adminApiClient.Admin.Realms[DefaultRealm].Users[user.Id].Groups[group.Id].PutAsync();
    }

    public async Task RemoveMember(string groupName, User member)
    {
        var group = await this.GetGroupByExactName(groupName);
        var user = await this.GetUserByExactName(member.Email);

        if (group is null || user is null)
        {
            return;
        }

        await adminApiClient
            .Admin.Realms[DefaultRealm]
            .Users[user.Id]
            .Groups[group.Id]
            .DeleteAsync();
    }

    public async Task<IEnumerable<User>> ListMembersAsync(string groupName)
    {
        var group = await this.GetGroupByExactName(groupName);

        if (group is null)
        {
            return [];
        }

        var members = await adminApiClient
            .Admin.Realms[DefaultRealm]
            .Groups[group.Id]
            .Members.GetAsync();

        return members!.Select(Map);
    }

    private static User Map(UserRepresentation user) => new(user.Email!);

    private static string WorkspaceResourceId(string name) => $"workspaces__{name}";

    private async Task<IEnumerable<GroupRepresentation>> GetGroupsByExactName(string name)
    {
        var groups = await adminApiClient
            .Admin.Realms[DefaultRealm]
            .Groups.GetAsync(q =>
            {
                q.QueryParameters.Search = name;
                q.QueryParameters.Exact = true;
            });

        return groups!;
    }

    private async Task<GroupRepresentation?> GetGroupByExactName(string name)
    {
        var groups = await this.GetGroupsByExactName(name);

        return groups!.FirstOrDefault();
    }

    private async Task<IEnumerable<UserRepresentation>> GetUsersByExactName(string name)
    {
        var users = await adminApiClient
            .Admin.Realms[DefaultRealm]
            .Users.GetAsync(q =>
            {
                q.QueryParameters.Search = name;
                q.QueryParameters.Exact = true;
            });

        return users!;
    }

    private async Task<UserRepresentation?> GetUserByExactName(string name)
    {
        var users = await this.GetUsersByExactName(name);

        return users!.FirstOrDefault();
    }

    private async Task<IEnumerable<ResourceResponse>> GetResourcesByExactName(string name)
    {
        var resources = await protectedResourceClient.GetResourcesAsync(
            DefaultRealm,
            new GetResourcesRequestParameters { ExactName = true, Name = name, }
        );

        return resources!;
    }

    private async Task<ResourceResponse?> GetResourceByExactName(string name)
    {
        var resources = await this.GetResourcesByExactName(name);

        return resources!.FirstOrDefault();
    }
}
