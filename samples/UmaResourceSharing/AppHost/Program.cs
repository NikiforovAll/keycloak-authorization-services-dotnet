var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithDataVolume()
    .WithImport("./KeycloakConfiguration/Test-realm.json")
    .WithImport("./KeycloakConfiguration/Test-users-0.json");

var realm = keycloak.AddRealm("Test");

var resourceServer = builder
    .AddProject<Projects.ResourceServer>("resource-server")
    .WithReference(keycloak)
    .WithReference(realm);

builder
    .AddProject<Projects.ClientApp>("client-app")
    .WithReference(keycloak)
    .WithReference(realm)
    .WithReference(resourceServer);

builder.Build().Run();
