namespace Keycloak.AuthServices.Authorization.Requirements;

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
/// Authorization handler for <see cref="ParameterizedProtectedResourceRequirement"/>
/// </summary>
/// <param name="client"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="serviceProvider"></param>
/// <param name="metrics"></param>
/// <param name="logger"></param>
/// <exception cref="ArgumentNullException"></exception>
public class ParameterizedProtectedResourceRequirementHandler(
    IAuthorizationServerClient client,
    IHttpContextAccessor httpContextAccessor,
    IServiceProvider serviceProvider,
    KeycloakMetrics metrics,
    ILogger<ParameterizedProtectedResourceRequirementHandler> logger
) : AuthorizationHandler<ParameterizedProtectedResourceRequirement>
{
    private readonly IAuthorizationServerClient client =
        client ?? throw new ArgumentNullException(nameof(client));
    private readonly IHttpContextAccessor httpContextAccessor =
        httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    private readonly IServiceProvider serviceProvider =
        serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly KeycloakMetrics metrics =
        metrics ?? throw new ArgumentNullException(nameof(metrics));
    private readonly ILogger<ParameterizedProtectedResourceRequirementHandler> logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    /// <inheritdoc/>
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ParameterizedProtectedResourceRequirement requirement
    )
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(requirement);

        using var activity = KeycloakActivitySource.Default.StartActivity(
            Activities.ProtectedResourceRequirement
        );
        var userName = context.User.Identity?.Name;

        if (!context.User.IsAuthenticated())
        {
            this.metrics.SkipRequirement(nameof(ParameterizedProtectedResourceRequirement));
            this.logger.LogRequirementSkipped(
                nameof(ParameterizedProtectedResourceRequirementHandler)
            );

            return;
        }

        var endpoint = this.httpContextAccessor.HttpContext?.GetEndpoint();

        var requirementData =
            endpoint?.Metadata?.GetOrderedMetadata<IProtectedResourceData>() ?? [];

        var verificationPlan = new VerificationPlan(requirementData);
        this.logger.LogVerificationPlan(verificationPlan.ToString(), userName);

        var success = true;

        foreach (var entry in verificationPlan)
        {
            var scopes = entry.GetScopesExpression();
            var resource = Utils.ResolveResource(
                entry,
                this.httpContextAccessor,
                this.serviceProvider
            );
            this.logger.LogResourceResolved(entry.Resource, resource);

            var verifier = new ProtectedResourceVerifier(this.client, this.metrics, this.logger);

            success = await verifier.Verify(
                resource,
                scopes,
                nameof(ParameterizedProtectedResourceRequirement),
                cancellationToken: CancellationToken.None
            );
            verificationPlan.Complete(entry.Resource, success);

            if (!success)
            {
                break;
            }
        }

        activity?.AddTag(Tags.Outcome, success);

        this.logger.LogVerificationTable(verificationPlan.ToString(), userName);
        this.logger.LogAuthorizationResult(
            nameof(ParameterizedProtectedResourceRequirementHandler),
            success,
            userName
        );

        if (success)
        {
            this.metrics.SucceedRequirement(nameof(ParameterizedProtectedResourceRequirement));
            context.Succeed(requirement);
        }
        else
        {
            this.metrics.FailRequirement(nameof(ParameterizedProtectedResourceRequirement));
            this.logger.LogAuthorizationFailed(requirement.ToString()!, userName);

            context.Fail();
        }
    }
}
