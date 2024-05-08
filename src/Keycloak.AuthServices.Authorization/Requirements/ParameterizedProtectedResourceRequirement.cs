namespace Keycloak.AuthServices.Authorization.Requirements;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

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
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(requirement);

        if (!(context.User.Identity?.IsAuthenticated ?? false))
        {
            this.logger.LogRequirementSkipped(
                nameof(ParameterizedProtectedResourceRequirementHandler),
                "User is not Authenticated",
                context.User.Identity?.Name
            );

            return;
        }

        var endpoint = this.httpContextAccessor.HttpContext?.GetEndpoint();
        var userName = context.User.Identity?.Name;

        var requirementData =
            endpoint?.Metadata?.GetOrderedMetadata<IProtectedResourceData>()
            ?? Array.Empty<IProtectedResourceData>();

        var verificationPlan = new VerificationPlan(requirementData);

        foreach (var entry in verificationPlan)
        {
            var scopes = entry.GetScopesExpression();

            var resource = ResolveResource(entry.Resource, this.httpContextAccessor.HttpContext);

            var success = await this.client.VerifyAccessToResource(
                resource,
                scopes,
                CancellationToken.None
            );

            verificationPlan.Complete(entry.Resource, success);

            if (!success)
            {
                this.logger.LogVerification(verificationPlan.ToString(), userName);
                this.logger.LogAuthorizationResult(
                    nameof(ParameterizedProtectedResourceRequirementHandler),
                    false,
                    userName
                );
                this.logger.LogAuthorizationFailed(requirement.ToString()!, userName);

                context.Fail();

                return;
            }
        }

        this.logger.LogVerification(verificationPlan.ToString(), userName);
        this.logger.LogAuthorizationResult(
            nameof(ParameterizedProtectedResourceRequirementHandler),
            true,
            userName
        );

        context.Succeed(requirement);
    }

    private static string ResolveResource(string resource, HttpContext? httpContext)
    {
        if (httpContext is null)
        {
            return resource;
        }

        var pathParameters = httpContext.GetRouteData()?.Values;

        if (pathParameters != null && resource.Contains('}') && resource.Contains('{'))
        {
            foreach (var parameter in pathParameters)
            {
                var parameterName = parameter.Key;

                if (resource.Contains($"{{{parameterName}}}"))
                {
                    var parameterValue = parameter.Value?.ToString();
                    resource = resource.Replace($"{{{parameterName}}}", parameterValue);
                }
            }
        }

        return resource;
    }
}
