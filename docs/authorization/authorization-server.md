# Authorization Server

Keycloak is an open-source Identity and Access Management solution that provides an Authorization Server. The Authorization Server is responsible for access to clients after successfully authenticating and authorizing the users.

In addition to the Authorization Server, Keycloak also supports a *Policy Enforcement Point* (PEP). The PEP is responsible for enforcing access control policies and protecting resources. It intercepts requests from clients and verifies the access token before allowing or denying access to the requested resource.

By integrating Keycloak's Authorization Server and PEP into your application, you can implement fine-grained access control and secure your resources based on user roles, permissions, and other attributes.

## Evaluate Permissions

Assume we have a default resource with Name *"urn:test-client:resources:default"*.

We want to check if a given user has access to it. It is accomplished based on permissions. In our case default permission is applied to default resource type. *"Default Permission"* is based on policy named - *"Require Admin Role"*. This policy checks if a user has *"Admin"* realm role.

Here is how to do it from code:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/AuthorizationServerPolicyTests.cs#RequireProtectedResource_DefaultResource_Verified

> [!Note]
> The calls to Authorization Servers are made on behalf of a user based on header propagation. We are taking user's *access_token* (JWT Bearer Token) from `IHttpContextAccessor`. `AddHeaderPropagation` adds `AccessTokenPropagationHandler` delegating handler to `IKeycloakProtectionClient` responsible for header propagation.
