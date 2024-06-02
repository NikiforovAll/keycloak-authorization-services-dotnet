var builder = DistributedApplication.CreateBuilder(args);

#if EnableKeycloakImport
var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithDataVolume()
    .WithImport("./KeycloakConfiguration/Test-realm.json")
    .WithImport("./KeycloakConfiguration/Test-users-0.json");
#else
var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithDataVolume();
#endif

var realm = keycloak.AddRealm("Test");

builder.AddProject<Projects.Api>("api").WithReference(keycloak).WithReference(realm);

builder.Build().Run();
