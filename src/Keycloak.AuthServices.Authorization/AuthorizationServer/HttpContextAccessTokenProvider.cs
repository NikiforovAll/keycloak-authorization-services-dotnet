namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
/// Default <see cref="IKeycloakAccessTokenProvider"/> implementation that retrieves the access token
/// from the current <see cref="HttpContext"/> using <see cref="AuthenticationHttpContextExtensions.GetTokenAsync"/>.
/// </summary>
public class HttpContextAccessTokenProvider : IKeycloakAccessTokenProvider
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly KeycloakAuthorizationServerOptions options;
    private readonly ILogger<HttpContextAccessTokenProvider> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpContextAccessTokenProvider"/> class.
    /// </summary>
    /// <param name="contextAccessor">The HTTP context accessor.</param>
    /// <param name="options">The Keycloak authorization server options.</param>
    /// <param name="logger">The logger.</param>
    public HttpContextAccessTokenProvider(
        IHttpContextAccessor contextAccessor,
        IOptions<KeycloakAuthorizationServerOptions> options,
        ILogger<HttpContextAccessTokenProvider> logger
    )
    {
        ArgumentNullException.ThrowIfNull(contextAccessor);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(logger);
        this.contextAccessor = contextAccessor;
        this.options = options.Value;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (this.contextAccessor.HttpContext is null)
        {
            this.logger.LogHttpContextIsNull();
            return null;
        }

        var retrievalScheme = this.options.SourceTokenRetrievalScheme
            ?? this.options.SourceAuthenticationScheme;

        var token = string.IsNullOrEmpty(retrievalScheme)
            ? await this.contextAccessor.HttpContext.GetTokenAsync(this.options.SourceTokenName)
            : await this.contextAccessor.HttpContext.GetTokenAsync(
                retrievalScheme,
                this.options.SourceTokenName
            );

        if (string.IsNullOrEmpty(token))
        {
            this.logger.LogTokenIsEmpty();
        }

        return token;
    }
}
