namespace Keycloak.AuthServices.Sdk.HttpMidleware;

using HttpMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
