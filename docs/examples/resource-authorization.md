# Resource Authorization

Resource authorization is a concept in software development that involves controlling access to specific resources or functionalities within an application. It ensures that only authorized users can perform certain actions or access certain data.

For example, assume we have project management software. A user can be an *Owner* for one project and don't have access at all to another project. The authorization logic is centered around resources and permissions and not about user roles necessarily.

You can create a **hierarchical structure** for authorization that allows for efficient management and fine-grained control over access to resources. This is particularly useful in complex applications where resources and users need to be organized in a meaningful way.

> [!TIP]
> 💡 Keycloak supports User-Managed Access ([**UMA**](https://en.wikipedia.org/wiki/User-Managed_Access)).

> [!TIP]
> 💡The other benefit of using Keycloak as *Authorization Server* - you can change authorization rules at runtime without the need of redeploying your code.

## Workspaces API Overview

The Workspaces API includes endpoints for creating, listing, reading, and deleting workspaces, as well as managing users within those workspaces.

> [!INFO]
> Workspaces: This term is used in the context of collaborative applications. A workspace is a shared environment where a group of users can access and manipulate a set of resources. For example, in a project management application, each project could be considered a workspace. Access to the project (and its associated tasks, files, etc.) can be controlled at the workspace level.

## Authorization Use Cases

Here are use cases implemented in this example:

1. Global access to "Workspaces" functionality. Only users with roles - "Admin" or "Reader" can access API.
2. Admins can manage workspaces - get, create, delete, add/remove users
3. Only workspace members get see a workspace details
4. Workspace members get see each other.
5. Everyone, including anonymous users, can get a details about "public" workspace, but anonymous users can't see workspace's members.

### Endpoints

#### `/workspaces`

- **GET**: Lists all workspaces. Returns an array of strings.
- **POST**: Creates a new workspace. Requires a JSON body specifying workspace details.

#### `/workspaces/{id}`

- **GET**: Retrieves details of a specific workspace identified by `{id}`.
- **DELETE**: Deletes a workspace identified by `{id}`.

#### `/my/workspaces`

- **GET**: Lists all workspaces associated with the authenticated user.

#### `/workspaces/{id}/users`

- **GET**: Lists all users within a specific workspace identified by `{id}`.
- **POST**: Adds a user to a workspace. Requires a JSON body with user details.
- **DELETE**: Removes a user from a workspace identified by `{id}` and a user `email` specified in the query.

### Data Models

#### Workspace

- **Type**: Object
- **Properties**:
  - `name`: String
  - `membersCount`: Integer (nullable)

#### User

- **Type**: Object
- **Properties**:
  - `email`: String

## Code

Setup DI:

<<< @/../samples/ResourceAuthorization/Program.cs

Protected Resource are configured based on `ProtectedResourceAttribute`, attributes are applied based on hierarchy.

<<< @/../samples/ResourceAuthorization/Controllers/WorkspacesController.cs

The idea is to define Keycloak Protected Resources during the creation of a domain object (i.e.: workspace). Protected Resources are the base for enforcement of the authorization rules.

<<< @/../samples/ResourceAuthorization/WorkspaceService.cs

See sample source code: [keycloak-authorization-services-dotnet/tree/main/samples/ResourceAuthorization](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/ResourceAuthorization)

:::details Configuration of Authorization Server
<<< @/../samples/ResourceAuthorization/KeycloakConfiguration/clients/test-client-auth-rules.json
:::
