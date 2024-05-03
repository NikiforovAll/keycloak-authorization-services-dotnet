# Configure Authorization

*RBAC* (Role-Based Access Control) is a widely used authorization model in software applications. It provides a way to control access to resources based on the roles assigned to users. Keycloak, an open-source identity and access management solution, offers robust support for RBAC.

With [Keycloak.AuthServices.Authorization](https://www.nuget.org/packages/Keycloak.AuthServices.Authorization), you can configure roles by defining realm roles and resource roles. Realm roles are global roles that apply to the entire realm, while resource roles are specific to a particular client or resource.

*Table of Contents*:
[[toc]]

## Require Realm Roles

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/PolicyTests.cs#RequireRealmRoles_AdminRole_Verified

## Require Resource Roles

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/PolicyTests.cs#RequireClientRoles_TestClientRole_Verified

Configure default roles source. The client name is taken from the `KeycloakAuthorizationOptions.Resource`:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/PolicyTests.cs#RequireClientRoles_TestClientRoleWithConfiguration_Verified

```json
{
  "Keycloak": {
    "resource": "test-client"
  }
}
```

> [!IMPORTANT]
> If you don't configure the default roles source `KeycloakException` exception will be thrown.

Override default source with `KeycloakAuthorizationOptions.RolesResource`:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/PolicyTests.cs#RequireClientRoles_TestClientRoleWithInlineConfiguration_Verified

Note it has more priority over the `KeycloakAuthorizationOptions.Resource`

## Keycloak Role Claims Transformation

Keycloak roles can be automatically transformed to [AspNetCore Roles](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles). This feature is **disabled by default** and is based on `KeycloakRolesClaimsTransformation`.

Specify `KeycloakAuthorizationOptions.EnableRolesMapping` to enable it. E.g.:

```json
{
  "Keycloak": {
    "EnableRolesMapping": "Realm"
  }
}
```

Here an example of how to configure realm role:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/PolicyTests.cs#RequireRealmRoles_AdminRoleWithMapping_Verified

Here an example of how to configure client role:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/PolicyTests.cs#RequireClientRoles_TestClientRoleWithMapping_Verified

There are three options to determine a source for the roles:

```csharp
public enum RolesClaimTransformationSource
{
    /// <summary>
    /// No Transformation. Default
    /// </summary>
    None,

    /// <summary>
    /// Use realm roles as source
    /// </summary>
    Realm,

    /// <summary>
    /// Use client roles as source
    /// </summary>
    ResourceAccess
}
```

Here is an example of decoded JWT token:

```json
{
  "exp": 1714057504,
  "iat": 1714057204,
  "jti": "7250d2a9-e5a1-442f-9e76-5e6b78bb2760",
  "iss": "http://localhost:8080/realms/Test",
  "aud": [
    "test-client",
    "account"
  ],
  "sub": "bf0b3371-ccdc-44f6-8861-ce25cbfcac39",
  "typ": "Bearer",
  "azp": "test-client",
  "session_state": "563332d2-111a-4ef2-b6a0-ebc1d3ae9a1e",
  "acr": "1",
  "allowed-origins": [
    "/*"
  ],
  "realm_access": {
    "roles": [
      "default-roles-test",
      "offline_access",
      "uma_authorization"
    ]
  },
  "resource_access": {
    "test-client": {
      "roles": [
        "manage-account",
        "manage-account-links",
        "view-profile"
      ]
    }
  },
  "scope": "profile email",
  "sid": "563332d2-111a-4ef2-b6a0-ebc1d3ae9a1e",
  "email_verified": false,
  "name": "Test Test",
  "preferred_username": "test",
  "given_name": "Test",
  "family_name": "Test",
  "email": "test@test.com"
}
```

If we specify `KeycloakAuthorizationOptions.EnableRolesMapping = RolesClaimTransformationSource.Realm` the roles are taken from $token.realm_access.roles.

Result = ["default-roles-test","offline_access","uma_authorization"]

If we specify `KeycloakAuthorizationOptions.EnableRolesMapping = RolesClaimTransformationSource.ResourceAccess` and `KeycloakAuthorizationOptions.RolesResource="test-client"` the roles are taken from $token.realm_access.test-client.roles.

Result = ["manage-account","manage-account-links","view-profile"]

The target claim can be configured `KeycloakAuthorizationOptions.RoleClaimType`, the default value is "role".
