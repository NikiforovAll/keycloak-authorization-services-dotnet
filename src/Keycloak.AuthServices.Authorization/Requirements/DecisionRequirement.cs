namespace Keycloak.AuthServices.Authorization.Requirements;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Keycloak.AuthServices.Authorization.ActivityConstants;

/// <summary>
/// Decision requirement
/// </summary>
public class DecisionRequirement : IAuthorizationRequirement, IProtectedResourceData
{
    /// <summary>
    /// Resource name
    /// </summary>
    public string Resource { get; }

    /// <summary>
    /// Resource scopes
    /// </summary>
    public string[] Scopes { get; }

    /// <summary>
    /// Validation Mode
    /// </summary>
    public ScopesValidationMode? ScopesValidationMode { get; set; }

    /// <summary>
    /// Constructs requirement
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    public DecisionRequirement(string resource, string scope)
    {
        this.Resource = resource;
        this.Scopes = new[] { scope };
    }

    /// <summary>
    /// Constructs requirement
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scopes"></param>
    public DecisionRequirement(string resource, string[] scopes)
    {
        this.Resource = resource;
        this.Scopes = scopes;
    }

    /// <summary>
    /// Constructs requirement based on resource id
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="id"></param>
    /// <param name="scope"></param>
    public DecisionRequirement(string resource, string id, string scope)
        : this($"{resource}/{id}", scope) { }

    /// <inheritdoc />
    public override string ToString() =>
        $"{nameof(DecisionRequirement)}: {this.Resource}#{(this as IProtectedResourceData).GetScopesExpression()}";
}

/// <summary>
/// Authorization handler for <see cref="DecisionRequirement"/>
/// </summary>
/// <param name="client"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="serviceProvider"></param>
/// <param name="metrics"></param>
/// <param name="logger"></param>
/// <exception cref="ArgumentNullException"></exception>
public class DecisionRequirementHandler(
    IAuthorizationServerClient client,
    IHttpContextAccessor httpContextAccessor,
    IServiceProvider serviceProvider,
    KeycloakMetrics metrics,
    ILogger<DecisionRequirementHandler> logger
) : AuthorizationHandler<DecisionRequirement>
{
    private readonly IAuthorizationServerClient client =
        client ?? throw new ArgumentNullException(nameof(client));
    private readonly IHttpContextAccessor httpContextAccessor =
        httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    private readonly IServiceProvider serviceProvider =
        serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly KeycloakMetrics metrics = metrics;
    private readonly ILogger<DecisionRequirementHandler> logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    /// <inheritdoc/>
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DecisionRequirement requirement
    )
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(requirement);

        using var activity = KeycloakActivitySource.Default.StartActivity(
            Activities.DecisionRequirement
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

        var resource = Utils.ResolveResource(
            requirement,
            this.httpContextAccessor,
            this.serviceProvider
        );
        this.logger.LogResourceResolved(requirement.Resource, resource);

        var scopes = (requirement as IProtectedResourceData).GetScopesExpression();

        var verifier = new ProtectedResourceVerifier(this.client, this.metrics, this.logger);

        var success = await verifier.Verify(
            resource,
            scopes,
            nameof(DecisionRequirement),
            requirement.ScopesValidationMode,
            CancellationToken.None
        );

        activity?.AddTag(Tags.Outcome, success);

        this.logger.LogAuthorizationResult(
            nameof(ParameterizedProtectedResourceRequirementHandler),
            success,
            userName
        );

        if (success)
        {
            this.metrics.SucceedRequirement(nameof(DecisionRequirement));
            context.Succeed(requirement);
        }
        else
        {
            this.metrics.FailRequirement(nameof(DecisionRequirement));
            this.logger.LogAuthorizationFailed(requirement.ToString()!, userName);

            context.Fail();
        }
    }
}
