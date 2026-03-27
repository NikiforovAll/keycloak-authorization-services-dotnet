namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
/// Delegating handler to propagate headers
/// </summary>
public class AccessTokenPropagationHandler : DelegatingHandler
{
    private readonly IKeycloakAccessTokenProvider tokenProvider;
    private readonly ILogger<AccessTokenPropagationHandler> logger;
    private readonly KeycloakAuthorizationServerOptions options;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessTokenPropagationHandler"/> class.
    /// </summary>
    /// <param name="tokenProvider">The access token provider.</param>
    /// <param name="options">The Keycloak client options.</param>
    /// <param name="logger"></param>
    public AccessTokenPropagationHandler(
        IKeycloakAccessTokenProvider tokenProvider,
        IOptions<KeycloakAuthorizationServerOptions> options,
        ILogger<AccessTokenPropagationHandler> logger
    )
    {
        this.tokenProvider = tokenProvider;
        this.logger = logger;
        ArgumentNullException.ThrowIfNull(options);
        this.options = options.Value;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Headers.Authorization is not null)
        {
            return await Continue();
        }

        var token = await this.tokenProvider.GetAccessTokenAsync(cancellationToken);

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                this.options.SourceAuthenticationScheme,
                token
            );
        }

        return await Continue();

        Task<HttpResponseMessage> Continue() => base.SendAsync(request, cancellationToken);
    }
}
