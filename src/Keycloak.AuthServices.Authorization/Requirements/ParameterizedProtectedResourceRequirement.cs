namespace Keycloak.AuthServices.Authorization.Requirements;

using System.Globalization;
using System.Text;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
public partial class ParameterizedProtectedResourceRequirementHandler
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

        var verificationLog = new VerificationLog();

        if (requirementData.Count > 0)
        {
            foreach (var r in requirementData)
            {
                var scopes = r.GetScopesExpression();

                var success = await this.client.VerifyAccessToResource(
                    r.Resource,
                    scopes,
                    CancellationToken.None
                );

                verificationLog.Append(r.Resource, scopes, success);

                if (!success)
                {
                    this.logger.LogVerification(verificationLog.ToString(), userName);
                    this.logger.LogAuthorizationResult(
                        nameof(ParameterizedProtectedResourceRequirementHandler),
                        false,
                        userName
                    );
                    context.Fail();

                    return;
                }
            }

            this.logger.LogVerification(verificationLog.ToString(), userName);
            this.logger.LogAuthorizationResult(
                nameof(ParameterizedProtectedResourceRequirementHandler),
                true,
                userName
            );

            context.Succeed(requirement);
        }
    }

    private sealed class VerificationLog
    {
        public List<(string resource, string scopes, bool outcome)> Entries { get; } = new();

        public void Append(string resource, string scopes, bool outcome) =>
            this.Entries.Add((resource, scopes, outcome));

        public override string ToString()
        {
            var sb = new StringBuilder(Environment.NewLine);

            sb.AppendLine(
                CultureInfo.InvariantCulture,
                $"Resource: {string.Empty, -5} Scopes: {string.Empty, -7}"
            );
            foreach (var (resource, scopes, outcome) in this.Entries)
            {
                sb.AppendLine(
                    CultureInfo.InvariantCulture,
                    $"{resource, -15} {scopes, -20} {outcome, -9}"
                );
            }

            return sb.ToString();
        }
    }
}
