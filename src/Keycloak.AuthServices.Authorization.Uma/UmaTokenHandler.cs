namespace Keycloak.AuthServices.Authorization.Uma;

using System.Net.Http.Headers;
using Keycloak.AuthServices.Sdk.Protection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

/// <summary>
/// DelegatingHandler that implements the UMA RPT exchange flow transparently.
/// When a 401 response with <c>WWW-Authenticate: UMA</c> is received, it extracts the
/// permission ticket, exchanges it for an RPT via <see cref="IUmaTicketExchangeClient"/>,
/// and retries the original request with the RPT.
/// </summary>
public class UmaTokenHandler(
    IHttpContextAccessor httpContextAccessor,
    IUmaTicketExchangeClient umaClient,
    ILogger<UmaTokenHandler> logger
) : DelegatingHandler
{
    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var accessToken = await httpContext.GetTokenAsync("access_token");
        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
        {
            return response;
        }

        if (string.IsNullOrEmpty(accessToken))
        {
            logger.LogRptExchangeFailedInHandler();
            return response;
        }

        var wwwAuthenticate = response.Headers.WwwAuthenticate.ToString();
        var ticket = UmaUtils.ExtractUmaTicket(wwwAuthenticate);

        if (string.IsNullOrEmpty(ticket))
        {
            return response;
        }

        logger.LogUmaChallengeReceived();

        var rpt = await umaClient.ExchangeTicketForRptAsync(accessToken, ticket, cancellationToken);

        if (rpt is null)
        {
            logger.LogRptExchangeFailedInHandler();
            return response;
        }

        logger.LogRetryingWithRpt();

        using var retryRequest = await CloneRequestAsync(request);
        retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", rpt);

        return await base.SendAsync(retryRequest, cancellationToken);
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        if (request.Content is not null)
        {
            var content = await request.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(content);
            if (request.Content.Headers.ContentType is not null)
            {
                clone.Content.Headers.ContentType = request.Content.Headers.ContentType;
            }
        }

        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}

internal static partial class UmaTokenHandlerLoggerExtensions
{
    [LoggerMessage(120, LogLevel.Information, "UMA challenge received, exchanging ticket for RPT")]
    public static partial void LogUmaChallengeReceived(this ILogger logger);

    [LoggerMessage(121, LogLevel.Warning, "RPT exchange failed, returning original 401 response")]
    public static partial void LogRptExchangeFailedInHandler(this ILogger logger);

    [LoggerMessage(122, LogLevel.Information, "RPT obtained, retrying request")]
    public static partial void LogRetryingWithRpt(this ILogger logger);
}
