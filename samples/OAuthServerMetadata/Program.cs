using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

// Uses RFC 8414 OAuth 2.0 Authorization Server Metadata discovery
// instead of the default OIDC discovery (/.well-known/openid-configuration)
services.AddKeycloakWebApiAuthentication(options =>
{
    configuration.BindKeycloakOptions(options);
    options.MetadataAddress = KeycloakConstants.OAuthAuthorizationServerMetadataPath;
});

services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication().UseAuthorization();

app.MapGet("/", () => "Hello from RFC 8414!").RequireAuthorization();

app.MapGet(
    "/metadata-info",
    (IConfiguration config) =>
    {
        var keycloakOptions = new KeycloakAuthenticationOptions();
        config.BindKeycloakOptions(keycloakOptions);

        return Results.Ok(
            new
            {
                Authority = keycloakOptions.KeycloakUrlRealm,
                OidcDiscovery = $"{keycloakOptions.KeycloakUrlRealm}{KeycloakConstants.OpenIdConnectConfigurationPath}",
                OAuthMetadata = $"{keycloakOptions.KeycloakUrlRealm}{KeycloakConstants.OAuthAuthorizationServerMetadataPath}",
                ActiveMetadataPath = KeycloakConstants.OAuthAuthorizationServerMetadataPath,
            }
        );
    }
);

await app.RunAsync();
