namespace Keycloak.AuthServices.Sdk.AuthZ;

using Admin.Models.Tokens;

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

    /// <summary>
    /// Obtain a token response for a specific resource.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TokenResponse?> GetTokenForResource(string resource, CancellationToken cancellationToken);
}
