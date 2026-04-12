namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
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
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.AddHttpContextAccessor();
        builder.Services.TryAddScoped<
            IKeycloakAccessTokenProvider,
            HttpContextAccessTokenProvider
        >();

        return builder.AddHttpMessageHandler(
            (sp) =>
            {
                var tokenProvider = sp.GetRequiredService<IKeycloakAccessTokenProvider>();
                var options = sp.GetRequiredService<IOptions<KeycloakAuthorizationServerOptions>>();
                var logger = sp.GetRequiredService<ILogger<AccessTokenPropagationHandler>>();

                return new AccessTokenPropagationHandler(tokenProvider, options, logger);
            }
        );
    }

    /// <summary>
    /// Registers <see cref="CookieAccessTokenProvider"/> as the <see cref="IKeycloakAccessTokenProvider"/>,
    /// replacing the default <see cref="HttpContextAccessTokenProvider"/>.
    /// </summary>
    /// <remarks>
    /// Use this for OIDC+Cookie Web Apps (<c>AddKeycloakWebAppAuthentication</c>) where access tokens
    /// are stored in the cookie scheme via <c>SaveTokens = true</c>.
    /// Call this before <c>AddAuthorizationServer</c>, or it will override the default registration.
    /// </remarks>
    /// <param name="services">The service collection.</param>
    /// <param name="cookieScheme">
    /// The cookie authentication scheme where tokens are stored.
    /// Defaults to <see cref="CookieAuthenticationDefaults.AuthenticationScheme"/> ("Cookies").
    /// </param>
    /// <param name="tokenName">
    /// The name of the token to retrieve. Defaults to <c>"access_token"</c>.
    /// </param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddCookieAccessTokenProvider(
        this IServiceCollection services,
        string cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        string tokenName = "access_token"
    )
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddHttpContextAccessor();
        services.AddScoped<IKeycloakAccessTokenProvider>(sp =>
            new CookieAccessTokenProvider(
                sp.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
                sp.GetRequiredService<ILogger<CookieAccessTokenProvider>>(),
                cookieScheme,
                tokenName
            )
        );

        return services;
    }
}
