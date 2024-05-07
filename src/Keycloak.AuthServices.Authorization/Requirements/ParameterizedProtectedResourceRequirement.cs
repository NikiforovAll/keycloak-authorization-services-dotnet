namespace Keycloak.AuthServices.Authorization.Requirements;

using System.Collections;
using System.Globalization;
using System.Text;
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

        var verificationPlan = new VerificationPlan();
        verificationPlan.AddRange(requirementData);

        if (requirementData.Count > 0)
        {
            foreach (var entry in verificationPlan)
            {
                var scopes = entry.GetScopesExpression();

                var resource = ResolveResource(
                    entry.Resource,
                    this.httpContextAccessor.HttpContext
                );

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

    private sealed class VerificationPlan : IEnumerable<IProtectedResourceData>
    {
        public List<string> Resources { get; } = new();
        private Dictionary<string, List<string>> resourceToScopes = new();
        private Dictionary<string, bool> resourceToOutcomes = new();

        public void AddRange(IEnumerable<IProtectedResourceData> protectedResources)
        {
            foreach (var item in protectedResources)
            {
                if (item is IgnoreProtectedResourceAttribute)
                {
                    this.Remove(item.Resource);
                }
                else
                {
                    this.Add(item.Resource, item.GetScopesExpression());
                }
            }
        }

        public void Add(string resource, string scopes)
        {
            if (this.resourceToScopes.ContainsKey(resource))
            {
                this.resourceToScopes[resource].Add(scopes);
            }
            else
            {
                this.Resources.Add(resource);

                this.resourceToScopes[resource] = new List<string>() { scopes };
            }
        }

        public bool Remove(string resource)
        {
            if (resource == string.Empty)
            {
                this.resourceToScopes = new();
                this.resourceToOutcomes = new();
                this.Resources.RemoveAll(_ => true);

                return true;
            }
            else if (this.resourceToScopes.ContainsKey(resource))
            {
                this.resourceToScopes.Remove(resource);
                this.resourceToOutcomes.Remove(resource);
                this.Resources.Remove(resource);

                return true;
            }

            return false;
        }

        public void Complete(string resource, bool result) =>
            this.resourceToOutcomes[resource] = result;

        public override string ToString()
        {
            var sb = new StringBuilder(Environment.NewLine);

            sb.AppendLine(
                CultureInfo.InvariantCulture,
                $"Resource: {string.Empty, -5} Scopes: {string.Empty, -7}"
            );

            foreach (var data in this)
            {
                var executed = this.resourceToOutcomes.TryGetValue(data.Resource, out var outcome);

                sb.AppendLine(
                    CultureInfo.InvariantCulture,
                    $"{data.Resource, -15} {data.GetScopesExpression(), -20} {(executed ? outcome : string.Empty), -9}"
                );
            }

            return sb.ToString();
        }

        public IEnumerator<IProtectedResourceData> GetEnumerator()
        {
            var resources = new List<ProtectedResourceAttribute>();

            foreach (var resource in this.Resources)
            {
                resources.Add(new(resource, this.resourceToScopes[resource].ToArray()));
            }

            return resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
