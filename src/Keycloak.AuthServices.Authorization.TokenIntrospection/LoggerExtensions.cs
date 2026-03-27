namespace Keycloak.AuthServices.Authorization.TokenIntrospection;

using System.Net;
using Microsoft.Extensions.Logging;

internal static partial class LoggerExtensions
{
    [LoggerMessage(113, LogLevel.Debug, "Introspecting token for user '{UserName}'")]
    public static partial void LogIntrospectingToken(this ILogger logger, string? userName);

    [LoggerMessage(114, LogLevel.Debug, "Token introspection cache hit for '{CacheKey}'")]
    public static partial void LogTokenIntrospectionCacheHit(this ILogger logger, string cacheKey);

    [LoggerMessage(115, LogLevel.Error, "Token introspection failed")]
    public static partial void LogTokenIntrospectionFailed(
        this ILogger logger,
        Exception exception
    );

    [LoggerMessage(116, LogLevel.Warning, "Introspected token is not active for user '{UserName}'")]
    public static partial void LogTokenNotActive(this ILogger logger, string? userName);

    [LoggerMessage(
        117,
        LogLevel.Debug,
        "Enriched ClaimsPrincipal with {ClaimCount} claims from token introspection"
    )]
    public static partial void LogTokenIntrospectionEnrichedClaims(
        this ILogger logger,
        int claimCount
    );

    [LoggerMessage(
        119,
        LogLevel.Warning,
        "Token introspection request failed with status code {StatusCode}"
    )]
    public static partial void LogTokenIntrospectionRequestFailed(
        this ILogger logger,
        HttpStatusCode statusCode
    );
}
