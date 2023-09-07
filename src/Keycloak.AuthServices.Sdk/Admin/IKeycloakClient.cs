namespace Keycloak.AuthServices.Sdk.Admin;

/// <summary>
/// Keycloak Admin API Client
/// </summary>
/// <remarks>
/// Aggregates multiple clients. <see cref="IKeycloakRealmClient"/> and <see cref="IKeycloakProtectedResourceClient"/>
/// </remarks>
public interface IKeycloakClient
    : IKeycloakRealmClient, IKeycloakProtectedResourceClient, IKeycloakUserClient, IKeycloakGroupClient
{
}
