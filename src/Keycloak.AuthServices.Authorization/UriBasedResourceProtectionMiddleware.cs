namespace Keycloak.AuthServices.Authorization;

using Keycloak.AuthServices.Sdk.AuthZ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

/// <summary>
/// Policy Enforcment Point, which automatically protects request URLs by requesting permission at the keycloak authorization API.
/// Using this, there is no need to:
/// a) register policies in code (as the policies provided in keycloak will be used for authentication)
/// b) annotate controller methods with the "[Authorize]" attribute
/// It is possible to exclude methods from the policy enforcement by annotate them with the "[AllowAnonymous]" attribute, however.
/// </summary>
public class UriBasedResourceProtectionMiddleware
{
    private readonly RequestDelegate next;
    private readonly IKeycloakProtectionClient client;

    /// <summary>
    /// <see cref="UriBasedResourceProtectionMiddleware"/> 
    /// </summary>
    /// <param name="next">The <see cref="RequestDelegate"/> to proceed with in case of authorization success.</param>
    /// <param name="client">The <see cref="IKeycloakProtectionClient"/> is used for the authorization request.</param>
    /// <exception cref="ArgumentNullException">Thrown, if <see cref="IKeycloakProtectionClient"/> is null.</exception>
    public UriBasedResourceProtectionMiddleware(RequestDelegate next, IKeycloakProtectionClient client)
    {
        this.next = next;
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <inheritdoc/>
    public async Task InvokeAsync(HttpContext context)
    {
        var targetAllowsAnonymous = context.Features?
            .Get<IEndpointFeature>()?
            .Endpoint?
            .Metadata
            .Any(attribute => attribute is AllowAnonymousAttribute) ?? false;

        if (!targetAllowsAnonymous)
        {
            var isAuthorized = await this.client.VerifyAccessToResource(
            context.Request.Path, context.Request.Method, CancellationToken.None);

            if (!isAuthorized)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        await this.next(context);
    }
}

/// <summary/>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Extension method to enable automatic resource protection.
    /// </summary>
    /// <param name="builder">The <see cref="IApplicationBuilder"/> isntance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> isntance with <see cref="UriBasedResourceProtectionMiddleware"/> usage.</returns>
    public static IApplicationBuilder UseUriBasedKeycloakEndpointProtection(
        this IApplicationBuilder builder) => builder.UseMiddleware<UriBasedResourceProtectionMiddleware>();
}
