namespace Keycloak.AuthServices.Authorization.Requirements;

using System.Diagnostics;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.Extensions.Logging;
using static Keycloak.AuthServices.Authorization.ActivityConstants;

internal sealed class ProtectedResourceVerifier(
    IAuthorizationServerClient client,
    KeycloakMetrics metrics,
    ILogger logger
)
{
    private readonly IAuthorizationServerClient client = client;
    private readonly KeycloakMetrics metrics = metrics;
    private readonly ILogger logger = logger;

    public async Task<bool> Verify(
        string resource,
        string scopes,
        string requirement,
        ScopesValidationMode? scopesValidationMode = default,
        string? audience = null,
        CancellationToken cancellationToken = default
    )
    {
        using var resourceActivity = KeycloakActivitySource.Default.StartActivity(
            Activities.ProtectedResourceVerification
        );

        resourceActivity?.AddEvent(new(Events.VerificationStarted));
        resourceActivity?.AddTag(Tags.Resource, resource);
        resourceActivity?.AddTag(Tags.Scopes, scopes);

        var success = false;

        try
        {
            success = await this.client.VerifyAccessToResource(
                resource,
                scopes,
                scopesValidationMode,
                audience,
                cancellationToken
            );
        }
        catch (Exception exception)
        {
            this.logger.LogAuthorizationError(resource, scopes);
            this.metrics.ErrorRequirement(requirement);

            resourceActivity?.SetStatus(
                ActivityStatusCode.Error,
                $"Unable to complete verification - {exception.Message}"
            );

            throw;
        }

        resourceActivity?.AddEvent(new(Events.VerificationCompleted));
        resourceActivity?.AddTag(Tags.Outcome, success);

        return success;
    }
}
