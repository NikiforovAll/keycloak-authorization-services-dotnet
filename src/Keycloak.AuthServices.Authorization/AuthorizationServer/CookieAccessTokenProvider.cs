namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

/// <summary>
/// <see cref="IKeycloakAccessTokenProvider"/> implementation for OIDC+Cookie Web Apps.
/// Retrieves the access token stored in the cookie authentication scheme when
/// <c>SaveTokens = true</c> is configured on the OpenIdConnect options.
/// </summary>
/// <remarks>
/// Register this provider instead of (or before) calling <c>AddAuthorizationServer</c>
/// when using <c>AddKeycloakWebAppAuthentication</c>:
/// <code>
/// services.AddCookieAccessTokenProvider();
/// services.AddKeycloakAuthorization().AddAuthorizationServer(...);
/// </code>
/// Ensure <c>SaveTokens = true</c> is set in your OIDC options so the token
/// is persisted in the cookie:
/// <code>
/// .AddKeycloakWebApp(config, configureOpenIdConnectOptions: o =>
/// {
///     o.SaveTokens = true;
///     o.ResponseType = OpenIdConnectResponseType.Code;
/// });
/// </code>
/// </remarks>
public class CookieAccessTokenProvider : IKeycloakAccessTokenProvider
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly ILogger<CookieAccessTokenProvider> logger;
    private readonly string cookieScheme;
    private readonly string tokenName;

    /// <summary>
    /// Initializes a new instance of the <see cref="CookieAccessTokenProvider"/> class.
    /// </summary>
    /// <param name="contextAccessor">The HTTP context accessor.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="cookieScheme">
    /// The cookie authentication scheme where tokens are stored.
    /// Defaults to <see cref="CookieAuthenticationDefaults.AuthenticationScheme"/> ("Cookies").
    /// </param>
    /// <param name="tokenName">
    /// The name of the token to retrieve. Defaults to <c>"access_token"</c>.
    /// </param>
    public CookieAccessTokenProvider(
        IHttpContextAccessor contextAccessor,
        ILogger<CookieAccessTokenProvider> logger,
        string cookieScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        string tokenName = "access_token"
    )
    {
        ArgumentNullException.ThrowIfNull(contextAccessor);
        ArgumentNullException.ThrowIfNull(logger);
        this.contextAccessor = contextAccessor;
        this.logger = logger;
        this.cookieScheme = cookieScheme;
        this.tokenName = tokenName;
    }

    /// <summary>
    /// Gets the cookie authentication scheme name used for token retrieval.
    /// </summary>
    public string CookieScheme => this.cookieScheme;

    /// <inheritdoc/>
    public async Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (this.contextAccessor.HttpContext is null)
        {
            this.logger.LogHttpContextIsNull();
            return null;
        }

        var token = await this.contextAccessor.HttpContext.GetTokenAsync(
            this.cookieScheme,
            this.tokenName
        );

        if (string.IsNullOrEmpty(token))
        {
            this.logger.LogTokenIsEmpty();
        }

        return token;
    }
}
