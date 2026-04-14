namespace Microsoft.Extensions.DependencyInjection;

using Keycloak.AuthServices.Authorization.Uma;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Extension methods for adding UMA token handler to an <see cref="IHttpClientBuilder"/>.
/// </summary>
public static class UmaHttpClientBuilderExtensions
{
    /// <summary>
    /// Adds the <see cref="UmaTokenHandler"/> to the HTTP client pipeline.
    /// When the downstream service returns a 401 with a <c>WWW-Authenticate: UMA</c> challenge,
    /// the handler automatically exchanges the permission ticket for an RPT and retries the request.
    /// </summary>
    /// <remarks>
    /// Requires <c>AddKeycloakUmaTicketExchangeHttpClient</c> to be called first to register
    /// the <see cref="Keycloak.AuthServices.Sdk.Protection.IUmaTicketExchangeClient"/>.
    /// Also requires <c>AddHttpContextAccessor</c> to be called.
    /// </remarks>
    /// <param name="builder">The HTTP client builder.</param>
    /// <returns>The <see cref="IHttpClientBuilder"/> for further configuration.</returns>
    public static IHttpClientBuilder AddUmaTokenHandler(this IHttpClientBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.TryAddScoped<UmaTokenHandler>();
        builder.AddHttpMessageHandler<UmaTokenHandler>();
        return builder;
    }
}
