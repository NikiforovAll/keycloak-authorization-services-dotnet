namespace Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Http;

/// <summary>
/// Resolves parameters from HTTP request headers.
/// </summary>
public sealed class HeaderParameterResolver : IParameterResolver
{
    /// <inheritdoc/>
    public string? Resolve(
        string parameter,
        HttpContext httpContext,
        IServiceProvider serviceProvider
    ) => httpContext.Request.Headers[parameter].FirstOrDefault();
}
