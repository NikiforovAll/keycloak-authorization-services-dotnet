namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

/// <summary>
/// Keycloak AuthorizationServer API
/// </summary>
public interface IAuthorizationServerClient
{
    /// <summary>
    /// Verifies access to the protected resource. Sends decision request to token endpoint {resource}#{scope}
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        CancellationToken cancellationToken = default
    ) =>
        this.VerifyAccessToResource(resource, scope, ScopesValidationMode.AllOf, audience: null, cancellationToken);

    /// <summary>
    /// Verifies access to the protected resource. Sends decision request to token endpoint {resource}#{scope}
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <param name="scopesValidationMode"></param>
    /// <param name="audience">
    /// The Keycloak client ID of the target resource server to use as the UMA <c>audience</c> parameter.
    /// When <c>null</c>, the global <c>KeycloakAuthorizationServerOptions.Resource</c> value is used.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        ScopesValidationMode? scopesValidationMode = default,
        string? audience = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Verifies access to the protected resource using an explicit access token.
    /// Use this overload in SignalR or other long-lived connection scenarios
    /// where the token from <see cref="Microsoft.AspNetCore.Http.HttpContext"/> may be stale.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <param name="accessToken">The access token to use for the authorization request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        string accessToken,
        CancellationToken cancellationToken = default
    ) =>
        this.VerifyAccessToResource(
            resource,
            scope,
            accessToken,
            ScopesValidationMode.AllOf,
            cancellationToken
        );

    /// <summary>
    /// Verifies access to the protected resource using an explicit access token.
    /// Use this overload in SignalR or other long-lived connection scenarios
    /// where the token from <see cref="Microsoft.AspNetCore.Http.HttpContext"/> may be stale.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <param name="accessToken">The access token to use for the authorization request.</param>
    /// <param name="scopesValidationMode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        string accessToken,
        ScopesValidationMode? scopesValidationMode = default,
        CancellationToken cancellationToken = default
    );
}
