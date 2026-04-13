# Client Secret JWT (client_secret_jwt)

This sample demonstrates how to use Keycloak's **"Signed JWT with Client Secret"** client authentication method (`client_secret_jwt`).

Instead of sending the raw client secret over the wire, the app builds a short-lived HS256-signed JWT assertion and sends that to Keycloak's token endpoint. Keycloak verifies the signature using the same shared secret — the secret itself never leaves the application.

This satisfies compliance policies that prohibit transmitting client secrets over the network.

<<< @/../samples/ClientSecretJwt/ClientSecretJwtAssertionService.cs

See sample source code: [keycloak-authorization-services-dotnet/tree/main/samples/ClientSecretJwt](https://github.com/NikiforovAll/keycloak-authorization-services-dotnet/tree/main/samples/ClientSecretJwt)
