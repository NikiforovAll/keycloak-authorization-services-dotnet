namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Token propagation middleware
/// </summary>
public static class AccessTokenPropagationExtensions
{
    /// <summary>
    /// Adds access token propagation middleware to HTTP pipeline
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddHeaderPropagation(this IHttpClientBuilder builder)
    {
        builder.AddHttpMessageHandler(
            (sp) =>
            {
                var contextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var options = sp.GetRequiredService<IOptions<KeycloakAuthorizationServerOptions>>();

                return new AccessTokenPropagationHandler(contextAccessor, options);
            }
        );

        return builder;
    }
}
