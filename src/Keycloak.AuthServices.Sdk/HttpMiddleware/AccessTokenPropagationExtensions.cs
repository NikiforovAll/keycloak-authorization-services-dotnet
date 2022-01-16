namespace Keycloak.AuthServices.Sdk.HttpMiddleware;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class AccessTokenPropagationExtensions
{
    public static IHttpClientBuilder AddHeaderPropagation(this IHttpClientBuilder builder)
    {
        builder.AddHttpMessageHandler((sp) =>
        {
            var contextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

            return new AccessTokenPropagationHandler(contextAccessor);
        });

        return builder;
    }
}
