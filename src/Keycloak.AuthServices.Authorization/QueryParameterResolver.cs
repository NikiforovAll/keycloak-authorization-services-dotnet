namespace Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Http;

/// <summary>
/// Resolves parameters from query string values.
/// </summary>
public sealed class QueryParameterResolver : IParameterResolver
{
    /// <inheritdoc/>
    public string? Resolve(
        string parameter,
        HttpContext httpContext,
        IServiceProvider serviceProvider
    ) => httpContext.Request.Query[parameter].FirstOrDefault();
}
