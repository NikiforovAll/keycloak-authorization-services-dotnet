namespace Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Http;

/// <summary>
/// Resolves parameter values from a request context for protected resource templates.
/// </summary>
public interface IParameterResolver
{
    /// <summary>
    /// Resolves the value of a named parameter from the current request context.
    /// </summary>
    /// <param name="parameter">The parameter name extracted from the resource template (e.g., "workspaceId" from "{workspaceId}").</param>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="serviceProvider">The request-scoped service provider.</param>
    /// <returns>The resolved parameter value, or <c>null</c> if the parameter could not be resolved.</returns>
    public string? Resolve(
        string parameter,
        HttpContext httpContext,
        IServiceProvider serviceProvider
    );
}
