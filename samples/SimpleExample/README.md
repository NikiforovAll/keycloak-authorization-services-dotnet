# Simple Example

Demonstrates how to add authentication and authorization.

## Custom Policies

Ownership by attribute:

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
