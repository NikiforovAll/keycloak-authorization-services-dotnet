namespace Keycloak.AuthServices.Authorization.Uma;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Keycloak.AuthServices.Authorization.Requirements;
using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
/// Handles authorization failures for UMA-protected resources by issuing permission tickets
/// via the <c>WWW-Authenticate: UMA</c> challenge header.
/// </summary>
/// <remarks>
/// When a <see cref="DecisionRequirement"/> or <see cref="ParameterizedProtectedResourceRequirement"/> fails,
/// this handler:
/// <list type="number">
///   <item>Creates a permission ticket via the Keycloak Protection Permission API</item>
///   <item>Returns HTTP 401 with <c>WWW-Authenticate: UMA as_uri="...", ticket="..."</c></item>
/// </list>
/// For non-UMA authorization failures, it delegates to the default handler.
/// </remarks>
/// <remarks>
/// Initializes a new instance of the <see cref="UmaAuthorizationMiddlewareResultHandler"/> class.
/// </remarks>
public class UmaAuthorizationMiddlewareResultHandler(
    IKeycloakProtectionClient protectionClient,
    IOptions<KeycloakAuthorizationServerOptions> options,
    ILogger<UmaAuthorizationMiddlewareResultHandler> logger
) : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

    /// <inheritdoc />
    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult
    )
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(policy);
        ArgumentNullException.ThrowIfNull(authorizeResult);

        if (authorizeResult.Challenged || !authorizeResult.Forbidden)
        {
            await this.defaultHandler.HandleAsync(next, context, policy, authorizeResult);
            return;
        }

        var resourceData = ResolveProtectedResourceData(context, policy, authorizeResult);

        if (resourceData is null)
        {
            await this.defaultHandler.HandleAsync(next, context, policy, authorizeResult);
            return;
        }

#pragma warning disable CA1031 // Do not catch general exception types

        try
        {
            var ticket = await this.CreatePermissionTicketAsync(resourceData);

            if (ticket is not null)
            {
                var asUri = options.Value.KeycloakUrlRealm;

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers.Append(
                    "WWW-Authenticate",
                    $"UMA as_uri=\"{asUri}\", ticket=\"{ticket}\""
                );

                logger.LogUmaChallenge(resourceData.Resource, ticket);
                return;
            }
        }
        catch (Exception ex)
        {
            logger.LogUmaChallengeError(resourceData.Resource, ex);
        }
#pragma warning restore CA1031 // Do not catch general exception types

        await this.defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }

    private static IProtectedResourceData? ResolveProtectedResourceData(
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult
    )
    {
        // Check for DecisionRequirement in failed requirements
        var decisionRequirement = authorizeResult
            .AuthorizationFailure?.FailedRequirements.OfType<DecisionRequirement>()
            .FirstOrDefault();

        if (decisionRequirement is not null)
        {
            return decisionRequirement;
        }

        // Check for ParameterizedProtectedResourceRequirement in failed requirements
        var parameterizedRequirement = authorizeResult
            .AuthorizationFailure?.FailedRequirements.OfType<ParameterizedProtectedResourceRequirement>()
            .FirstOrDefault();

        // Fall back to policy requirements when FailCalled is true (explicit Fail()
        // creates ExplicitFail with empty FailedRequirements).
        if (
            parameterizedRequirement is null
            && authorizeResult.AuthorizationFailure?.FailCalled == true
        )
        {
            if (policy.Requirements.OfType<DecisionRequirement>().FirstOrDefault() is { } dr)
            {
                return dr;
            }

            parameterizedRequirement = policy
                .Requirements.OfType<ParameterizedProtectedResourceRequirement>()
                .FirstOrDefault();
        }

        if (parameterizedRequirement is null)
        {
            return null;
        }

        // For ParameterizedProtectedResourceRequirement, resource data is on the endpoint metadata
        var endpoint = context.GetEndpoint();
        return endpoint?.Metadata.GetOrderedMetadata<IProtectedResourceData>().FirstOrDefault();
    }

    private async Task<string?> CreatePermissionTicketAsync(IProtectedResourceData resourceData)
    {
        var realm = options.Value.Realm;

        var resourceIds = await protectionClient.GetResourcesIdsAsync(
            realm,
            new GetResourcesRequestParameters { Name = resourceData.Resource, ExactName = true }
        );

        if (resourceIds.Count == 0)
        {
            logger.LogPermissionTicketCreationFailed(
                resourceData.Resource,
                System.Net.HttpStatusCode.NotFound
            );
            return null;
        }

        var scopes = resourceData.Scopes?.Where(s => !string.IsNullOrEmpty(s)).ToArray();

        var permissions = new List<PermissionTicketRequest>
        {
            new()
            {
                ResourceId = resourceIds[0],
                ResourceScopes = scopes is { Length: > 0 } ? scopes : null,
            },
        };

        try
        {
            var result = await protectionClient.CreatePermissionTicketAsync(realm, permissions);
            return result.Ticket;
        }
        catch (KeycloakHttpClientException ex)
        {
            logger.LogPermissionTicketCreationFailed(
                resourceData.Resource,
                (System.Net.HttpStatusCode)ex.StatusCode
            );
            return null;
        }
    }
}
