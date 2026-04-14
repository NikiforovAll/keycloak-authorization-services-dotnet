namespace Keycloak.AuthServices.Authorization.Tests;

using System.Net;
using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Keycloak.AuthServices.Authorization.Requirements;
using Keycloak.AuthServices.Authorization.Uma;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;

public class UmaAuthorizationMiddlewareResultHandlerTests
{
    private const string Realm = "TestRealm";
    private const string AuthServerUrl = "http://localhost:8080/";

    private readonly MockHttpMessageHandler mockHttp = new();

    private UmaAuthorizationMiddlewareResultHandler CreateHandler(
        MockHttpMessageHandler? handler = null
    )
    {
        handler ??= this.mockHttp;

        var httpClientFactory = new TestHttpClientFactory(handler, AuthServerUrl);
        var options = Options.Create(
            new KeycloakAuthorizationServerOptions { Realm = Realm, AuthServerUrl = AuthServerUrl }
        );
        var logger = NullLogger<UmaAuthorizationMiddlewareResultHandler>.Instance;

        return new UmaAuthorizationMiddlewareResultHandler(httpClientFactory, options, logger);
    }

    [Fact]
    public async Task HandleAsync_WhenForbiddenWithDecisionRequirement_ReturnsUmaChallenge()
    {
        var ticketValue = "test-ticket-123";
        this.mockHttp.Expect(
                HttpMethod.Post,
                $"{AuthServerUrl}realms/{Realm}/authz/protection/permission"
            )
            .Respond(HttpStatusCode.OK, "application/json", $$"""{"ticket":"{{ticketValue}}"}""");

        var handler = this.CreateHandler();
        var httpContext = new DefaultHttpContext();
        var requirement = new DecisionRequirement("workspaces", "read");
        var failure = AuthorizationFailure.Failed(new[] { requirement });
        var authorizeResult = PolicyAuthorizationResult.Forbid(failure);
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        await handler.HandleAsync(_ => Task.CompletedTask, httpContext, policy, authorizeResult);

        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        httpContext
            .Response.Headers["WWW-Authenticate"]
            .ToString()
            .Should()
            .Contain("UMA")
            .And.Contain($"ticket=\"{ticketValue}\"")
            .And.Contain($"as_uri=\"{AuthServerUrl}realms/{Realm}/\"");

        this.mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task HandleAsync_WhenExplicitFailWithDecisionInPolicy_ReturnsUmaChallenge()
    {
        var ticketValue = "test-ticket-456";
        this.mockHttp.Expect(
                HttpMethod.Post,
                $"{AuthServerUrl}realms/{Realm}/authz/protection/permission"
            )
            .Respond(HttpStatusCode.OK, "application/json", $$"""{"ticket":"{{ticketValue}}"}""");

        var handler = this.CreateHandler();
        var httpContext = new DefaultHttpContext();
        var requirement = new DecisionRequirement("workspaces", "read");
        // ExplicitFail has empty FailedRequirements but FailCalled = true
        var failure = AuthorizationFailure.ExplicitFail();
        var authorizeResult = PolicyAuthorizationResult.Forbid(failure);
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddRequirements(requirement)
            .Build();

        await handler.HandleAsync(_ => Task.CompletedTask, httpContext, policy, authorizeResult);

        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        httpContext
            .Response.Headers["WWW-Authenticate"]
            .ToString()
            .Should()
            .Contain("UMA")
            .And.Contain($"ticket=\"{ticketValue}\"");

        this.mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task HandleAsync_WhenForbiddenWithoutDecisionRequirement_DelegatesToDefault()
    {
        var handler = this.CreateHandler();
        var httpContext = CreateHttpContextWithServices();
        var requirement = new RptRequirement("workspaces", "read");
        var failure = AuthorizationFailure.Failed(new IAuthorizationRequirement[] { requirement });
        var authorizeResult = PolicyAuthorizationResult.Forbid(failure);
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        await handler.HandleAsync(_ => Task.CompletedTask, httpContext, policy, authorizeResult);

        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }

    [Fact]
    public async Task HandleAsync_WhenChallenged_DelegatesToDefault()
    {
        var handler = this.CreateHandler();
        var httpContext = CreateHttpContextWithServices();
        var authorizeResult = PolicyAuthorizationResult.Challenge();
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        await handler.HandleAsync(_ => Task.CompletedTask, httpContext, policy, authorizeResult);

        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

    [Fact]
    public async Task HandleAsync_WhenPermissionTicketCreationFails_DelegatesToDefault()
    {
        this.mockHttp.Expect(
                HttpMethod.Post,
                $"{AuthServerUrl}realms/{Realm}/authz/protection/permission"
            )
            .Respond(HttpStatusCode.Forbidden);

        var handler = this.CreateHandler();
        var httpContext = CreateHttpContextWithServices();
        var requirement = new DecisionRequirement("workspaces", "read");
        var failure = AuthorizationFailure.Failed(new[] { requirement });
        var authorizeResult = PolicyAuthorizationResult.Forbid(failure);
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        await handler.HandleAsync(_ => Task.CompletedTask, httpContext, policy, authorizeResult);

        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        this.mockHttp.VerifyNoOutstandingExpectation();
    }

    private static DefaultHttpContext CreateHttpContextWithServices()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services
            .AddAuthentication("Test")
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
        var httpContext = new DefaultHttpContext
        {
            RequestServices = services.BuildServiceProvider(),
        };
        return httpContext;
    }

    private sealed class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            Microsoft.Extensions.Logging.ILoggerFactory logger,
            System.Text.Encodings.Web.UrlEncoder encoder
        )
            : base(options, logger, encoder) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync() =>
            Task.FromResult(AuthenticateResult.NoResult());
    }

    private sealed class TestHttpClientFactory : IHttpClientFactory
    {
        private readonly MockHttpMessageHandler handler;
        private readonly string baseAddress;

        public TestHttpClientFactory(MockHttpMessageHandler handler, string baseAddress)
        {
            this.handler = handler;
            this.baseAddress = baseAddress;
        }

        public HttpClient CreateClient(string name)
        {
            var client = this.handler.ToHttpClient();
            client.BaseAddress = new Uri(this.baseAddress);
            return client;
        }
    }
}
