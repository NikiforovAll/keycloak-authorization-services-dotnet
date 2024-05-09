# Resource Authorization

## Use Cases

### Only users with roles - Admin or Reader can access workspace functionality

* Implement based on Protected Resource "workspaces"
* Implement based on RealRoles from Claims

### Admin can manage workspaces

* Showcase how to apply resource-type policies.

### Users can list names of all workspaces

* Showcase policies for particular resource "Workspaces".

### A member of workspace can see workspace details

### A member of workspace can see other members

### Unauthorized users can access to public workspace

* We want to show that `AllowAnonymous` impacts `ProtectedResource` and takes precedence.

## Sync Realm Config

Inside docker container run:

```bash
/opt/keycloak/bin/kc.sh export --dir /opt/keycloak/data/import --realm Test
```
