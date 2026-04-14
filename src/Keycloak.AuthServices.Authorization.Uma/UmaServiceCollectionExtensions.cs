namespace Microsoft.Extensions.DependencyInjection;

using Keycloak.AuthServices.Authorization.Uma;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Extension methods for registering UMA permission ticket challenge support.
/// </summary>
public static class UmaServiceCollectionExtensions
{
    /// <summary>
    /// Adds UMA permission ticket challenge support. When authorization fails for a
    /// protected resource requirement, the server returns HTTP 401 with a
    /// <c>WWW-Authenticate: UMA</c> header containing a permission ticket.
    /// </summary>
    /// <remarks>
    /// Requires <c>AddAuthorizationServer</c> and <c>AddKeycloakProtectionHttpClient</c>
    /// to be called first. The protection client must be configured with client credentials
    /// to authenticate against the Keycloak Protection API.
    /// </remarks>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddUmaPermissionTicketChallenge(
        this IServiceCollection services
    )
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<
            IAuthorizationMiddlewareResultHandler,
            UmaAuthorizationMiddlewareResultHandler
        >();

        return services;
    }
}
