namespace Keycloak.AuthServices.Sdk.Protection;

/// <summary>
/// Access to protected resource API.
/// </summary>
/// <remarks>
/// See: https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_api
/// </remarks>
public interface IKeycloakProtectionClient
    : IKeycloakProtectedResourceClient,
        IKeycloakPolicyClient { }
