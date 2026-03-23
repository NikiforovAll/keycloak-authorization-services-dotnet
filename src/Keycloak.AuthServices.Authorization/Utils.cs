namespace Keycloak.AuthServices.Authorization;

using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

internal static partial class Utils
{
    [GeneratedRegex(@"\{([^}]+)\}")]
    private static partial Regex ParameterPlaceholderRegex();

    public static string ResolveResource(
        string resource,
        HttpContext httpContext,
        IParameterResolver resolver,
        IServiceProvider serviceProvider
    )
    {
        if (!resource.Contains('{') || !resource.Contains('}'))
        {
            return resource;
        }

        return ParameterPlaceholderRegex()
            .Replace(
                resource,
                match =>
                {
                    var parameterName = match.Groups[1].Value;
                    return resolver.Resolve(parameterName, httpContext, serviceProvider)
                        ?? match.Value;
                }
            );
    }

    public static string ResolveResource(
        IProtectedResourceData resourceData,
        IHttpContextAccessor httpContextAccessor,
        IServiceProvider serviceProvider
    )
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return resourceData.Resource;
        }

        var resolverType = resourceData.ResolverType ?? typeof(RouteParameterResolver);
        var resolver = (IParameterResolver)serviceProvider.GetRequiredService(resolverType);

        return ResolveResource(resourceData.Resource, httpContext, resolver, serviceProvider);
    }

    public static bool IsAuthenticated(this ClaimsPrincipal? principal) =>
        principal?.Identity?.IsAuthenticated ?? false;
}
