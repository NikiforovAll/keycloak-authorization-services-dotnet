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
/// </summary>
public class DecisionRequirementHandler : AuthorizationHandler<DecisionRequirement>
{
    private readonly IAuthorizationServerClient client;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger<DecisionRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="httpContextAccessor"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public DecisionRequirementHandler(
        IAuthorizationServerClient client,
        IHttpContextAccessor httpContextAccessor,
        ILogger<DecisionRequirementHandler> logger
    )
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
        this.httpContextAccessor =
            httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

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
            this.logger.LogRequirementSkipped(
                nameof(ParameterizedProtectedResourceRequirementHandler)
            );

            return;
        }

        var resource = Utils.ResolveResource(
            requirement.Resource,
            this.httpContextAccessor.HttpContext
        );
        this.logger.LogResourceResolved(requirement.Resource, resource);

        var scopes = (requirement as IProtectedResourceData).GetScopesExpression();

        var verifier = new ProtectedResourceVerifier(this.client, this.logger);

        var success = await verifier.Verify(
            resource,
            scopes,
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
            context.Succeed(requirement);
        }
        else
        {
            this.logger.LogAuthorizationFailed(requirement.ToString()!, userName);

            context.Fail();
        }
    }
}
