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
        this.next = next ?? throw new ArgumentNullException(nameof(next));
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <inheritdoc/>
    public async Task InvokeAsync(HttpContext context)
    {
        if (this.EvaluateAuthorization(context).Result)
        {
            await this.next(context);
        }
    }

    private async Task<bool> EvaluateAuthorization(HttpContext context)
    {
        var attributes = context.Features?
            .Get<IEndpointFeature>()?
            .Endpoint?
            .Metadata;

        if (AllowAnonymous(attributes))
        {
            return true;
        }

        string? resourceName = null;
        string? scope = null;

        if (attributes != null && attributes
            .SingleOrDefault(attribute => attribute is ExplicitResourceProtectionAttribute)
                is ExplicitResourceProtectionAttribute explicitResourceProtectionAttribute)
        {
            if (explicitResourceProtectionAttribute.Disable)
            {
                return true;
            }
            resourceName = explicitResourceProtectionAttribute.ResourceName;
            scope = explicitResourceProtectionAttribute.Scope;
        }

        resourceName ??= context.Request.Path;
        scope ??= context.Request.Method;

        var isAuthorized = await this.client.VerifyAccessToResource(
        resourceName, scope, CancellationToken.None);

        if (isAuthorized)
        {
            return true;
        }

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return false;
    }

    private static bool AllowAnonymous(EndpointMetadataCollection? attributes) => attributes != null && attributes.Any(attribute => attribute is AllowAnonymousAttribute);
}

/// <summary/>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Extension method to enable automatic uri based resource protection.
    /// </summary>
    /// <param name="builder">The <see cref="IApplicationBuilder"/> isntance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> isntance with <see cref="UriBasedResourceProtectionMiddleware"/> usage.</returns>
    public static IApplicationBuilder UseUriBasedKeycloakEndpointProtection(
        this IApplicationBuilder builder) => builder.UseMiddleware<UriBasedResourceProtectionMiddleware>();
}