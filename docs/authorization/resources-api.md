# Protect ASP.NET Core

ASP.NET Core allows you to built policies based on [Microsoft.AspNetCore.Authorization.AuthorizationBuilder](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authorization.authorizationbuilder). `Keycloak.AuthServices.Authorization` adds [Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authorization.authorizationpolicybuilder) extension methods to work with protected resources and configure your polices.

<<< @/../src/Keycloak.AuthServices.Authorization/PoliciesBuilderExtensions.cs#RequireProtectedResource

<!-- <<< @/../src/Keycloak.AuthServices.Authorization/PoliciesBuilderExtensions.cs#RequireProtectedResourceScopes -->

## Add to your code

Here is how to use to use protected resource authorization.

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/AuthorizationServerPolicyTests.cs#RequireProtectedResource_Scopes_Verified{7,12-15,18 cs:line-numbers}

Here are the assertions from the integration test for this scenario:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/AuthorizationServerPolicyTests.cs#RequireProtectedResource_Scopes_Verified_Assertion

Source code of integration test: [tests/Keycloak.AuthServices.IntegrationTests/AuthorizationServerPolicyTests.cs](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/blob/main/tests/Keycloak.AuthServices.IntegrationTests/AuthorizationServerPolicyTests.cs)

## Validate Multiple Scopes

You can specify multiple scopes to validate against and control comparison by using `ScopesValidationMode`.

Here is an example for `ScopesValidationMode.AllOf`:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/AuthorizationServerPolicyTests.cs#RequireProtectedResource_MultipleScopesAllOf_Verified

Here is an example for `ScopesValidationMode.AnyOf`:

<<< @/../tests/Keycloak.AuthServices.IntegrationTests/AuthorizationServerPolicyTests.cs#RequireProtectedResource_MultipleScopesAnyOf_Verified
