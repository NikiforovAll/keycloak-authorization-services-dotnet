namespace Keycloak.AuthServices.Sdk.HttpMiddleware;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
        builder.AddHttpMessageHandler(serviceProvider =>
        {
            var contextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

            return new AccessTokenPropagationHandler(contextAccessor);
        });

        return builder;
    }
}
