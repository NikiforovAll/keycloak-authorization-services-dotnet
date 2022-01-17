# Keycloak.AuthServices.Authorization

Authorization services:

* `DecisionRequirement` - protected resource authorization. Keycloak is Authorization Server (Policy Server). Access to protected resources is checked by `IKeycloakProtectionClient`. The client is registered by `AddKeycloakAuthorization` and depends on `KeycloakInstallationOptions` configuration.
* `RealmAccessRequirement` - require realm role
* `ResourceAccessRequirement` - require resource/client role
* `RptRequirement` - RPT request, grant_type="urn:ietf:params:oauth:grant-type:uma-ticket"

```csharp
services.AddAuthorization(o =>
{
    o.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    o.AddPolicy("ProtectedResource", b =>
    {
        // b.AddRequirements(new DecisionRequirement("workspaces", "workspaces:read"));
        b.RequireProtectedResource("workspaces", "workspaces:read");
    });

    o.AddPolicy("RealmRole", b =>
    {
        // b..AddRequirements(new RealmAccessRequirement("SuperManager"));
        b.RequireRealmRoles("SuperManager");
    });

    o.AddPolicy("ClientRole", b =>
    {
        // b.AddRequirements(new ResourceAccessRequirement(default, "Manager"));
        b.RequireResourceRoles("Manager");
    });
}).AddKeycloakAuthorization(configuration);
```

Automatic policy registration is based on `ProtectedResourcePolicyProvider`. The expected policy format is: `<resource>#<scope>`, e.g: `workspaces:read`.

```csharp
services.AddSingleton<IAuthorizationPolicyProvider, ProtectedResourcePolicyProvider>();
```
