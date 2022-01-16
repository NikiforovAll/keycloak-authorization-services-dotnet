namespace Api.Application.Authorization;

using Abstractions;
using MediatR;

public class AuthorizationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IIdentityService identityService;
    private readonly ILogger<TRequest> logger;

    public AuthorizationBehavior(IIdentityService identityService, ILogger<TRequest> logger)
    {
        this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        // TODO: consider reflection performance impact
        var authorizeAttributes = request
            .GetType()
            .GetCustomAttributes(typeof(AuthorizeAttribute), true)
            .Cast<AuthorizeAttribute>()
            .ToList();

        if (!authorizeAttributes.Any())
        {
            return await next();
        }

        this.EnsureAuthorizedRoles(authorizeAttributes);

        await this.EnsureAuthorizedPolicies(authorizeAttributes);

        // User is authorized / authorization not required
        return await next();
    }

    private async Task EnsureAuthorizedPolicies(IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        // Policy-based authorization
        var authorizeAttributesWithPolicies = authorizeAttributes
            .Where(a => !string.IsNullOrWhiteSpace(a.Policy));

        if (!authorizeAttributesWithPolicies.Any())
        {
            return;
        }

        var requiredPolicies = authorizeAttributesWithPolicies.Select(a => a.Policy);

        foreach (var policy in requiredPolicies)
        {
            var authorized = await this.identityService
                .AuthorizeAsync(this.identityService.Principal, policy);

            if (authorized)
            {
                continue;
            }

            this.logger.LogDebug("Failed policy authorization {Policy}", policy);
            throw new ForbiddenAccessException();
        }
    }

    private void EnsureAuthorizedRoles(IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        // Role-based authorization
        var authorizeAttributesWithRoles = authorizeAttributes
            .Where(a => !string.IsNullOrWhiteSpace(a.Roles));

        if (!authorizeAttributesWithRoles.Any())
        {
            return;
        }

        var requiredRoles = authorizeAttributesWithRoles
            .Select(a => a.Roles.Split(','));

        if (requiredRoles
            .Select(roles => roles.Any(role => this.identityService.IsInRoleAsync(role.Trim())))
            .Any(authorized => !authorized))
        {
            this.logger.LogDebug("Failed role authorization");
            throw new ForbiddenAccessException();
        }
    }
}
