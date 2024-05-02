namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

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
    Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        CancellationToken cancellationToken = default
    ) =>
        this.VerifyAccessToResource(resource, scope, ScopesValidationMode.AllOf, cancellationToken);

    /// <summary>
    /// Verifies access to the protected resource. Sends decision request to token endpoint {resource}#{scope}
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <param name="scopesValidationMode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        ScopesValidationMode? scopesValidationMode = default,
        CancellationToken cancellationToken = default
    );
}
