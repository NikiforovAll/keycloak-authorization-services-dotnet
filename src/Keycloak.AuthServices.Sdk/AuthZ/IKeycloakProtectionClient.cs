namespace Keycloak.AuthServices.Sdk.AuthZ;

/// <summary>
/// Keycloak Protection API
/// </summary>
public interface IKeycloakProtectionClient
{
    /// <summary>
    /// Verifies access to the protected resource. Sends decision request to token endpoint {resource}#{scope}
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> VerifyAccessToResource(string resource, string scope, CancellationToken cancellationToken);
}
