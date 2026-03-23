namespace Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

/// <summary>
/// Resolves parameters from route values. This is the default resolver.
/// </summary>
internal sealed class RouteParameterResolver : IParameterResolver
{
    /// <inheritdoc/>
    public string? Resolve(
        string parameter,
        HttpContext httpContext,
        IServiceProvider serviceProvider
    ) => httpContext.GetRouteValue(parameter)?.ToString();
}
