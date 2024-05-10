namespace Keycloak.AuthServices.Authorization;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.Extensions.Logging;

internal static partial class LoggerExtensions
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
        103,
        LogLevel.Information,
        "[{Requirement}] Authorization failed for user '{UserName}'"
    )]
    public static partial void LogAuthorizationFailed(
        this ILogger logger,
        string requirement,
        string? userName
    );

    [LoggerMessage(101, LogLevel.Warning, "[{Requirement}] Has been skipped because of '{Reason}'")]
    public static partial void LogRequirementSkipped(
        this ILogger logger,
        string requirement,
        string reason = "User is not Authenticated"
    );

    [LoggerMessage(
        102,
        LogLevel.Debug,
        "User - '{UserName}' has verification table: {Verification}"
    )]
    public static partial void LogVerificationTable(
        this ILogger logger,
        string verification,
        string? userName
    );

    [LoggerMessage(
        112,
        LogLevel.Debug,
        "User - '{UserName}' has verification plan: {Verification}"
    )]
    public static partial void LogVerificationPlan(
        this ILogger logger,
        string verification,
        string? userName
    );

    [LoggerMessage(
        104,
        LogLevel.Error,
        "Exception occurred during resource[{Resource}#{Scopes}] verification"
    )]
    public static partial void LogAuthorizationError(
        this ILogger logger,
        string resource,
        string? scopes
    );

    [LoggerMessage(105, LogLevel.Debug, "HttpContext is null, continuing without token")]
    public static partial void LogHttpContextIsNull(this ILogger logger);

    [LoggerMessage(106, LogLevel.Information, "Token is null or empty, continuing without token")]
    public static partial void LogTokenIsEmpty(this ILogger logger);

    [LoggerMessage(
        107,
        LogLevel.Debug,
        "Verifying access to resource '{Resource}' with scope '{Scope}'"
    )]
    public static partial void LogVerifyingAccess(
        this ILogger logger,
        string resource,
        string scope
    );

    [LoggerMessage(
        108,
        LogLevel.Debug,
        "Validating scopes for resource '{Resource}' in {ValidationMode}"
    )]
    public static partial void LogValidatingScopes(
        this ILogger logger,
        string resource,
        ScopesValidationMode validationMode
    );

    [LoggerMessage(
        109,
        LogLevel.Error,
        "An unexpected error occurred while verifying access to resource '{Resource}' with scope '{Scope}'"
    )]
    public static partial void LogVerifyAccessToResourceFailed(
        this ILogger logger,
        Exception exception,
        string resource,
        string scope
    );

    [LoggerMessage(
        110,
        LogLevel.Warning,
        "Verification on resource '{Resource}' was not able to recognize response. Verification terminated due to '{Reason}'"
    )]
    public static partial void LogUnableToRecognizeResponse(
        this ILogger logger,
        string resource,
        string reason
    );

    [LoggerMessage(111, LogLevel.Debug, "Resource '{Resource}' resolved as '{ResourceValue}'")]
    public static partial void LogResourceResolved(
        this ILogger logger,
        string resource,
        string resourceValue
    );
}
