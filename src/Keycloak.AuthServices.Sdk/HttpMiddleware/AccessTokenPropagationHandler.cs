namespace Keycloak.AuthServices.Sdk.HttpMiddleware;

using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

/// <summary>
/// Delegating handler to propagate headers
/// </summary>
public class AccessTokenPropagationHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor contextAccessor;

    /// <summary>
    /// Constructs
    /// </summary>
    /// <param name="contextAccessor"></param>
    public AccessTokenPropagationHandler(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (this.contextAccessor.HttpContext == null)
        {
            return await Continue();
        }

        var httpContext = this.contextAccessor.HttpContext;
        var token = await httpContext
            .GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");

        if (!StringValues.IsNullOrEmpty(token))
        {
            request.SetToken(JwtBearerDefaults.AuthenticationScheme, token);
        }

        return await Continue();

        Task<HttpResponseMessage> Continue() =>
            base.SendAsync(request, cancellationToken);
    }
}
