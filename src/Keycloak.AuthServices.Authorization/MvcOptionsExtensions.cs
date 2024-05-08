namespace Keycloak.AuthServices.Authorization;

using Keycloak.AuthServices.Authorization.Requirements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

/// <summary>
/// Provides extension methods for configuring MVC options related to protected resources.
/// </summary>
public static class MvcOptionsExtensions
{
    /// <summary>
    /// Adds filters and conventions for protecting resources using Keycloak authorization.
    /// </summary>
    /// <param name="options">The <see cref="MvcOptions"/> to configure.</param>
    /// <returns>The configured <see cref="MvcOptions"/>.</returns>
    public static MvcOptions AddProtectedResources(this MvcOptions options)
    {
        options.Filters.Add(
            new AuthorizeFilter(
                ParameterizedProtectedResourceRequirement.DynamicProtectedResourcePolicy
            )
        );
        options.Conventions.Add(new ProtectedResourceModelConvention());

        return options;
    }
}
