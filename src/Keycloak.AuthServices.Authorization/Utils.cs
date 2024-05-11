namespace Keycloak.AuthServices.Authorization;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class Utils
{
    public static string ResolveResource(string resource, HttpContext? httpContext)
    {
        if (httpContext is null)
        {
            return resource;
        }

        var pathParameters = httpContext.GetRouteData()?.Values;

        if (pathParameters != null && resource.Contains('}') && resource.Contains('{'))
        {
            foreach (var parameter in pathParameters)
            {
                var parameterName = parameter.Key;

                if (resource.Contains($"{{{parameterName}}}"))
                {
                    var parameterValue = parameter.Value?.ToString();
                    resource = resource.Replace($"{{{parameterName}}}", parameterValue);
                }
            }
        }

        return resource;
    }

    public static bool IsAuthenticated(this ClaimsPrincipal? principal) =>
        principal?.Identity?.IsAuthenticated ?? false;
}
