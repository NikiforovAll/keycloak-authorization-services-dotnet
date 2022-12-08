# Simple Example

Demonstrates how to add keycloak authentication and authorization to ASP.NET Core project.

## Authorization is based on ASP.NET Core Authorization services

`Keycloak.AuthServices.Authorization` provide three authorization extensions for ASP.NET Core:

Realm roles:

```json
authorizationOptions.AddPolicy("CanDeleteAllWorkspaces", policyBuilder =>
{
    // User should have realm role - "SuperManager"
    policyBuilder.RequireRealmRoles("SuperManager");
});
```

Resource (clients) roles:

```csharp
authorizationOptions.AddPolicy("AccessManagement", policyBuilder =>
{
    // User should have client role - "Manager"
    policyBuilder.RequireResourceRoles("Manager");
});
```

Protected Resource Authorization - <https://www.keycloak.org/docs/latest/authorization_services/index.html> and <https://docs.kantarainitiative.org/uma/wg/oauth-uma-grant-2.0-09.html>

Basically, keycloak can be used as authorization server, so you can verify permissions for (resource+scope) by calling keycloak. Permissions are evaluated over policies and can be configured at runtime.

```csharp
authorizationOptions.AddPolicy("ProtectWorkspacesOnRead", policyBuilder =>
{
    // Can user read all workspaces?
    policyBuilder.RequireProtectedResource("workspaces", "workspaces:read");
});
```

For more details see [Program.cs](Program.cs).

JWT token example:

```json
{
  "exp": 1642433984,
  "iat": 1642430384,
  "auth_time": 1642428609,
  "jti": "b89e599b-8c69-4707-b95d-af40b475de9c",
  "iss": "http://localhost:8088/auth/realms/authz",
  "aud": [
    "workspace-authz",
    "account"
  ],
  "sub": "eefae679-99a3-44d1-b746-67e357dca78a",
  "typ": "Bearer",
  "azp": "frontend",
  "session_state": "167dcc27-30d0-43d5-a80e-b291d4ffa824",
  "acr": "0",
  "realm_access": {
    "roles": [
      "SuperManager",
      "offline_access",
      "uma_authorization"
    ]
  },
  "resource_access": {
    "workspace-authz": {
      "roles": [
        "Manager"
      ]
    },
    "account": {
      "roles": [
        "manage-account",
        "manage-account-links",
        "view-profile"
      ]
    }
  },
  "scope": "openid email profile",
  "email_verified": false,
  "preferred_username": "test"
}
```

## Clean Architecture and Authorization

You can add authorization as part of Clean Architecture setup by using [IIdentityService.cs](Application/Authorization.Abstractions/IIdentityService.cs) abstraction inside the application layer.

The authorization is added as aspect (AOP).

```csharp
[AuthorizeProtectedResource("workspaces", "workspaces:create")]
public record CreateWorkspaceCommand(string Name, IList<Project>? Projects = default) : IRequest;
```

Authorization services are wired based on pipeline behavior provided by *MediatR*:

```csharp
public class AuthorizationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
        public AuthorizationBehavior(IIdentityService identityService)
    {
        this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        // impl, see source code for more details
    }
}
```

## Custom Policies

See keycloak realm and client export files. [realm-authz.json](keycloak/realm-authz.json).

Keycloak authorization policies are quire powerful, you can write custom policies in Java and JavaScript.

Here is how you might want to implement resource ownership:

The idea is to use custom attributes defined as part of protected resource:

```js
var resource = $evaluation.getPermission().getResource();

if (resource.type == "urn:workspace-authz:resource:workspaces"
    && resource.name != "workspaces") {
    var identity = $evaluation.context.getIdentity();
    var username = identity.getAttributes()
        .getValue("preferred_username").asString(0);

    var accessGroup = resource
        .getAttribute(username);

    print(accessGroup);
    if (accessGroup && accessGroup.contains("Owner")) {
        $evaluation.grant();
    }
    // var accessGroup = resource.getAttribute(username);

    // if (accessGroup && accessGroup[0] == "Owner") {
    //     $evaluation.grant();
    // }
}
```

Every time you create resource, you need to protect in keycloak:

```csharp
public async Task<Unit> Handle(
    CreateWorkspaceCommand request,
    CancellationToken cancellationToken)
{
    var (name, projects) = request;

    var workspace = new Workspace {Name = name, Projects = projects ?? new List<Project>()};
    this.db.Workspaces.Add(workspace);
    await this.db.SaveChangesAsync(cancellationToken);

    // IKeycloakProtectedResourceClient
    await resourceClient.CreateResource("authz",
        new Resource($"workspaces/{workspace.Id}", new[] {"workspaces:read", "workspaces:delete"})
        {
            Attributes = {[identityService.UserName] = "Owner"},
            Type = "urn:workspace-authz:resource:workspaces",
        });
    return Unit.Value;
}
```
