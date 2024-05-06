namespace Keycloak.AuthServices.Authorization.Requirements;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

/// <summary>
/// Decision requirement
/// </summary>
public class DynamicProtectedResourceRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Internal name for global policy
    /// </summary>
    public const string DynamicProtectedResourcePolicy = "$DynamicProtectedResourcePolicy";
}

/// <summary>
/// </summary>
public partial class DynamicProtectedResourceRequirementHandler
    : AuthorizationHandler<DynamicProtectedResourceRequirement>
{
    private readonly IAuthorizationServerClient client;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger<DynamicProtectedResourceRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="httpContextAccessor"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public DynamicProtectedResourceRequirementHandler(
        IAuthorizationServerClient client,
        IHttpContextAccessor httpContextAccessor,
        ILogger<DynamicProtectedResourceRequirementHandler> logger
    )
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
        this.httpContextAccessor = httpContextAccessor;
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [LoggerMessage(
        103,
        LogLevel.Debug,
        "[{Requirement}] Access outcome {Outcome} for user {UserName}"
    )]
    partial void DecisionAuthorizationResult(string requirement, bool outcome, string? userName);

    /// <inheritdoc/>
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DynamicProtectedResourceRequirement requirement
    )
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var endpoint = this.httpContextAccessor.HttpContext?.GetEndpoint();

            var requirementData =
                endpoint?.Metadata?.GetOrderedMetadata<IProtectedResourceData>()
                ?? Array.Empty<IProtectedResourceData>();

            if (requirementData.Count > 0)
            {
                var success = false;
                foreach (var r in requirementData)
                {
                    var scopes = r.Scopes is not null ? string.Join(',', r.Scopes) : string.Empty;
                    success = await this.client.VerifyAccessToResource(
                        r.Resource,
                        scopes,
                        CancellationToken.None
                    );

                    if (!success)
                    {
                        this.DecisionAuthorizationResult(
                            nameof(DynamicProtectedResourceRequirementHandler),
                            success,
                            context.User.Identity?.Name
                        );
                    }
                }

                if (success)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
        }
        else
        {
            this.DecisionAuthorizationResult(
                nameof(DynamicProtectedResourceRequirementHandler),
                false,
                context.User.Identity?.Name
            );
        }
    }
}
