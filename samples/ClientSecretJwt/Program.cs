using ClientSecretJwt;
using Duende.AccessTokenManagement;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

// Register the JWT assertion service.
// When Duende requests a token, this service builds a short-lived HS256-signed JWT
// (client_assertion) using the shared secret. Only the signed JWT travels over the wire —
// the raw secret never does.
services.AddSingleton<IClientAssertionService, ClientSecretJwtAssertionService>();

services.AddDistributedMemoryCache();

// Configure Duende client credentials token management.
// Note: ClientSecret is intentionally omitted — IClientAssertionService handles auth.
services
    .AddClientCredentialsTokenManagement()
    .AddClient(
        "keycloak",
        client =>
        {
            var keycloakOptions = configuration.GetKeycloakOptions<KeycloakAdminClientOptions>()!;
            client.ClientId = ClientId.Parse(keycloakOptions.Resource);
            client.TokenEndpoint = new Uri(keycloakOptions.KeycloakTokenEndpoint);
        }
    );

// Register the Keycloak Admin HTTP client and attach the Duende token handler.
services
    .AddKeycloakAdminHttpClient(configuration)
    .AddClientCredentialsTokenHandler(ClientCredentialsClientName.Parse("keycloak"));

var app = builder.Build();

// Returns the Test realm info from Keycloak Admin API.
// Requires: view-realm role on the service account.
app.MapGet(
    "/realm",
    async (IKeycloakRealmClient client) =>
    {
        var realm = await client.GetRealmAsync("Test");
        return Results.Ok(realm);
    }
);

// Lists users in the Test realm.
// Requires: view-users role on the service account.
app.MapGet(
    "/users",
    async (IKeycloakUserClient client) =>
    {
        var users = await client.GetUsersAsync("Test");
        return Results.Ok(users);
    }
);

await app.RunAsync();
