var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume()
    .AddDatabase("keycloak-db", databaseName: "keycloak");

var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithDataVolume()
    .WithHostname("http://localhost:8080/")
    .WithPostgresDatabase(postgres)
    .WithImport("./KeycloakConfiguration/Test-realm.json")
    .WithImport("./KeycloakConfiguration/Test-users-0.json");

var realm = keycloak.AddRealm("Test");

builder.AddProject<Projects.Api>("api").WithReference(keycloak).WithReference(realm);

builder.Build().Run();
