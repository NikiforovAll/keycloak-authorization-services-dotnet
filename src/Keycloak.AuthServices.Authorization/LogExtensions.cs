namespace Keycloak.AuthServices.Authorization;

using Microsoft.Extensions.Logging;

internal static partial class LogExtensions
{
    [LoggerMessage(
        100,
        LogLevel.Debug,
        "[{Requirement}] Access outcome '{Outcome}' for user '{UserName}'"
    )]
    public static partial void LogAuthorizationResult(
        this ILogger logger,
        string requirement,
        bool outcome,
        string? userName
    );

    [LoggerMessage(
        101,
        LogLevel.Warning,
        "[{Requirement}] Has been skipped because of '{Reason}' for user '{UserName}'"
    )]
    public static partial void LogRequirementSkipped(
        this ILogger logger,
        string requirement,
        string reason,
        string? userName
    );

    [LoggerMessage(102, LogLevel.Debug, "User - '{UserName}' has verification table: {Verification}")]
    public static partial void LogVerification(
        this ILogger logger,
        string verification,
        string? userName
    );
}
