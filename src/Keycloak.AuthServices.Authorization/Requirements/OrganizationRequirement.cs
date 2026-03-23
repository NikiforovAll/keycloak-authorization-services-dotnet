namespace Keycloak.AuthServices.Authorization.Requirements;

using Keycloak.AuthServices.Common.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
/// Authorization requirement that validates the user's membership in a Keycloak organization.
/// </summary>
public class OrganizationRequirement : IAuthorizationRequirement, IAuthorizationRequirementData
{
    /// <inheritdoc/>
    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }

    /// <summary>
    /// Internal name for the organization policy.
    /// </summary>
    public const string OrganizationPolicy = "$OrganizationPolicy";

    /// <summary>
    /// Gets the organization alias or route template to check membership against.
    /// When <c>null</c>, any organization membership satisfies the requirement.
    /// Supports <c>{param}</c> route templates that are resolved at evaluation time.
    /// </summary>
    public string? Organization { get; }

    /// <summary>
    /// Gets the type of <see cref="IParameterResolver"/> used to resolve organization template parameters.
    /// When <c>null</c>, the default <c>RouteParameterResolver</c> is used.
    /// </summary>
    public Type? ResolverType { get; init; }

    /// <summary>
    /// Constructs requirement that checks for any organization membership.
    /// </summary>
    public OrganizationRequirement() { }

    /// <summary>
    /// Constructs requirement that checks for membership in a specific organization.
    /// </summary>
    /// <param name="organization">The organization alias, id, or route template (e.g., "{orgId}").</param>
    public OrganizationRequirement(string organization) => this.Organization = organization;

    /// <inheritdoc/>
    public override string ToString() =>
        this.Organization is null
            ? $"{nameof(OrganizationRequirement)}: Any organization membership"
            : $"{nameof(OrganizationRequirement)}: Organization '{this.Organization}'";
}

/// <summary>
/// Handles <see cref="OrganizationRequirement"/> by checking the user's organization claim.
/// </summary>
public class OrganizationRequirementHandler : AuthorizationHandler<OrganizationRequirement>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly KeycloakMetrics metrics;
    private readonly ILogger<OrganizationRequirementHandler> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationRequirementHandler"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    /// <param name="metrics">The Keycloak metrics tracker.</param>
    /// <param name="logger">The logger.</param>
    public OrganizationRequirementHandler(
        IHttpContextAccessor httpContextAccessor,
        KeycloakMetrics metrics,
        ILogger<OrganizationRequirementHandler> logger
    )
    {
        this.httpContextAccessor = httpContextAccessor;
        this.metrics = metrics;
        this.logger = logger;
    }

    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OrganizationRequirement requirement
    )
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(requirement);

        using var activity = KeycloakActivitySource.Default.StartActivity(
            ActivityConstants.Activities.OrganizationRequirement
        );

        var userName = context.User.Identity?.Name;

        if (!context.User.IsAuthenticated())
        {
            this.metrics.SkipRequirement(nameof(OrganizationRequirement));
            this.logger.LogRequirementSkipped(nameof(OrganizationRequirementHandler));
            return Task.CompletedTask;
        }

        var organizations = context.User.GetOrganizations();

        bool success;

        if (requirement.Organization is null)
        {
            success = organizations.Count > 0;
        }
        else
        {
            var organization = ResolveOrganization(requirement);
            success = organizations.Any(o =>
                o.Alias.Equals(organization, StringComparison.OrdinalIgnoreCase)
                || (o.Id != null && o.Id.Equals(organization, StringComparison.OrdinalIgnoreCase))
            );
        }

        activity?.AddTag(ActivityConstants.Tags.Outcome, success);
        this.logger.LogAuthorizationResult(requirement.ToString()!, success, userName);

        if (success)
        {
            this.metrics.SucceedRequirement(nameof(OrganizationRequirement));
            context.Succeed(requirement);
        }
        else
        {
            this.metrics.FailRequirement(nameof(OrganizationRequirement));
            this.logger.LogAuthorizationFailed(requirement.ToString()!, userName);
        }

        return Task.CompletedTask;
    }

    private string ResolveOrganization(OrganizationRequirement requirement)
    {
        var organization = requirement.Organization!;
        var httpContext = this.httpContextAccessor.HttpContext;

        if (httpContext is null || !organization.Contains('{'))
        {
            return organization;
        }

        var resolverType = requirement.ResolverType ?? typeof(RouteParameterResolver);
        var resolver = (IParameterResolver)
            httpContext.RequestServices.GetRequiredService(resolverType);

        return Utils.ResolveResource(
            organization,
            httpContext,
            resolver,
            httpContext.RequestServices
        );
    }
}
