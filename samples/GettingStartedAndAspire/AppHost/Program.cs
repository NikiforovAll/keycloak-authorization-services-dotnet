var builder = DistributedApplication.CreateBuilder(args);

var pgUser = builder.AddParameter("pg-username", "postgres");
var pgPassword = builder.AddParameter("pg-password", "postgres", secret: true);

var postgres = builder
    .AddPostgres("postgres", pgUser, pgPassword)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("keycloak-db", databaseName: "keycloak");

var keycloak = builder
    .AddKeycloakContainer("keycloak")
    .WithDataVolume()
    .WithHostname("http://localhost:8080/")
    .WithPostgresDatabase(postgres)
    .WithOtlpExporter()
    .WithImport("./KeycloakConfiguration/Test-realm.json")
    .WithImport("./KeycloakConfiguration/Test-users-0.json");

var realm = keycloak.AddRealm("Test");

builder.AddProject<Projects.Api>("api").WithReference(keycloak).WithReference(realm);

builder.Build().Run();
