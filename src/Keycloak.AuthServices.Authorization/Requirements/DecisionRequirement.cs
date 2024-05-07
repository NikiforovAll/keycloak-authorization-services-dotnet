namespace Keycloak.AuthServices.Authorization.Requirements;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

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
public partial class DecisionRequirementHandler : AuthorizationHandler<DecisionRequirement>
{
    private readonly IAuthorizationServerClient client;
    private readonly ILogger<DecisionRequirementHandler> logger;

    /// <summary>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public DecisionRequirementHandler(
        IAuthorizationServerClient client,
        ILogger<DecisionRequirementHandler> logger
    )
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DecisionRequirement requirement
    )
    {
        if (!(context.User.Identity?.IsAuthenticated ?? false))
        {
            this.logger.LogRequirementSkipped(
                nameof(ParameterizedProtectedResourceRequirementHandler),
                "User is not Authenticated",
                context.User.Identity?.Name
            );

            return;
        }

        var success = await this.client.VerifyAccessToResource(
            requirement.Resource,
            (requirement as IProtectedResourceData).GetScopesExpression(),
            requirement.ScopesValidationMode,
            CancellationToken.None
        );

        this.logger.LogAuthorizationResult(
            requirement.ToString(),
            success,
            context.User.Identity?.Name
        );

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
