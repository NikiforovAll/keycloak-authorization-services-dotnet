namespace Keycloak.AuthServices.Authorization.Requirements;

using System.Diagnostics;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.Extensions.Logging;
using static Keycloak.AuthServices.Authorization.ActivityConstants;

internal sealed class ProtectedResourceVerifier
{
    private readonly IAuthorizationServerClient client;
    private readonly ILogger logger;

    public ProtectedResourceVerifier(IAuthorizationServerClient client, ILogger logger)
    {
        this.client = client;
        this.logger = logger;
    }

    public async Task<bool> Verify(
        string resource,
        string scopes,
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
            success = await this.client.VerifyAccessToResource(resource, scopes, cancellationToken);
        }
        catch (Exception exception)
        {
            this.logger.LogAuthorizationError(resource, scopes);

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
