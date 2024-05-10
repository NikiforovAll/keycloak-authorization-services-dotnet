namespace Keycloak.AuthServices.Authorization.Requirements;

using System.Diagnostics;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Keycloak.AuthServices.Authorization.ActivityConstants;

/// <summary>
/// Decision requirement
/// </summary>
public class ParameterizedProtectedResourceRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Internal name for global policy
    /// </summary>
    public const string DynamicProtectedResourcePolicy = "$DynamicProtectedResourcePolicy";
}

/// <summary>
/// </summary>
public class ParameterizedProtectedResourceRequirementHandler
    : AuthorizationHandler<ParameterizedProtectedResourceRequirement>
{
    private readonly IAuthorizationServerClient client;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger<ParameterizedProtectedResourceRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="httpContextAccessor"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ParameterizedProtectedResourceRequirementHandler(
        IAuthorizationServerClient client,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ParameterizedProtectedResourceRequirementHandler> logger
    )
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
        this.httpContextAccessor = httpContextAccessor;
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ParameterizedProtectedResourceRequirement requirement
    )
    {
        using var activity = AuthServicesActivitySource.Default.StartActivity(
            Activities.ProtectedResourceRequirement
        );
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(requirement);

        var userName = context.User.Identity?.Name;

        if (!context.User.IsAuthenticated())
        {
            this.logger.LogRequirementSkipped(
                nameof(ParameterizedProtectedResourceRequirementHandler),
                "User is not Authenticated",
                userName
            );

            return;
        }

        var endpoint = this.httpContextAccessor.HttpContext?.GetEndpoint();

        var requirementData =
            endpoint?.Metadata?.GetOrderedMetadata<IProtectedResourceData>()
            ?? Array.Empty<IProtectedResourceData>();

        var verificationPlan = new VerificationPlan(requirementData);

        foreach (var entry in verificationPlan)
        {
            using var resourceActivity = AuthServicesActivitySource.Default.StartActivity(
                Activities.ProtectedResourceVerification
            );

            var scopes = entry.GetScopesExpression();
            resourceActivity?.AddEvent(new(Events.VerificationStarted));

            var resource = Utils.ResolveResource(
                entry.Resource,
                this.httpContextAccessor.HttpContext
            );

            resourceActivity?.AddTag(Tags.Resource, resource);
            resourceActivity?.AddTag(Tags.Scopes, scopes);

            var success = false;
            try
            {
                success = await this.client.VerifyAccessToResource(
                    resource,
                    scopes,
                    CancellationToken.None
                );
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

            verificationPlan.Complete(entry.Resource, success);

            resourceActivity?.AddEvent(new(Events.VerificationCompleted));
            resourceActivity?.AddTag(Tags.Outcome, success);

            if (!success)
            {
                this.logger.LogAuthorizationResult(
                    nameof(ParameterizedProtectedResourceRequirementHandler),
                    false,
                    userName
                );
                this.logger.LogAuthorizationFailed(requirement.ToString()!, userName);
                this.logger.LogVerification(verificationPlan.ToString(), userName);
                activity?.AddTag(Tags.Outcome, false);

                context.Fail();

                return;
            }
        }

        this.logger.LogAuthorizationResult(
            nameof(ParameterizedProtectedResourceRequirementHandler),
            true,
            userName
        );
        this.logger.LogVerification(verificationPlan.ToString(), userName);
        activity?.AddTag(Tags.Outcome, true);

        context.Succeed(requirement);
    }
}
