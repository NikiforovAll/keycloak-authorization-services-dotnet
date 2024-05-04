namespace Keycloak.AuthServices.Sdk.Admin;

/// <summary>
/// Keycloak Admin API Client
/// </summary>
/// <remarks>
/// Aggregates multiple clients
/// </remarks>
public interface IKeycloakClient
    : IKeycloakRealmClient,
        IKeycloakUserClient,
        IKeycloakGroupClient { }
