namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

/// <summary>
/// Delegating handler to propagate headers
/// </summary>

public class AccessTokenPropagationHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly KeycloakAuthorizationServerOptions options;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessTokenPropagationHandler"/> class.
    /// </summary>
    /// <param name="contextAccessor">The HTTP context accessor.</param>
    /// <param name="options">The Keycloak client options.</param>
    public AccessTokenPropagationHandler(
        IHttpContextAccessor contextAccessor,
        IOptions<KeycloakAuthorizationServerOptions> options
    )
    {
        this.contextAccessor = contextAccessor;
        this.options = options.Value;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        if (this.contextAccessor.HttpContext == null)
        {
            return await Continue();
        }

        var httpContext = this.contextAccessor.HttpContext;

        var token = await httpContext.GetTokenAsync(
            this.options.SourceAuthenticationScheme,
            "access_token"
        );

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
