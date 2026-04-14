namespace Keycloak.AuthServices.Authorization.Uma;

using Microsoft.Extensions.Logging;

internal static partial class LoggerExtensions
{
    [LoggerMessage(
        116,
        LogLevel.Information,
        "UMA challenge issued for resource '{Resource}' with ticket '{Ticket}'"
    )]
    public static partial void LogUmaChallenge(this ILogger logger, string resource, string ticket);

    [LoggerMessage(
        117,
        LogLevel.Error,
        "Failed to create UMA permission ticket for resource '{Resource}'"
    )]
    public static partial void LogUmaChallengeError(
        this ILogger logger,
        string resource,
        Exception exception
    );

    [LoggerMessage(
        119,
        LogLevel.Warning,
        "Permission ticket creation failed for resource '{Resource}' with status {StatusCode}"
    )]
    public static partial void LogPermissionTicketCreationFailed(
        this ILogger logger,
        string resource,
        System.Net.HttpStatusCode statusCode
    );
}
