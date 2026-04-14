namespace Keycloak.AuthServices.Authorization.Uma;

using Microsoft.Extensions.Logging;

internal static partial class LoggerExtensions
{
    [LoggerMessage(
        116,
        LogLevel.Information,
        "UMA challenge issued for resource '{Resource}' with ticket '{RedactedTicket}'"
    )]
    public static partial void LogUmaChallengeInternal(
        this ILogger logger,
        string resource,
        string redactedTicket
    );

    public static void LogUmaChallenge(this ILogger logger, string resource, string ticket)
    {
        var redacted = ticket.Length > 8 ? $"{ticket[..4]}...{ticket[^4..]}" : "****";
        LogUmaChallengeInternal(logger, resource, redacted);
    }

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
