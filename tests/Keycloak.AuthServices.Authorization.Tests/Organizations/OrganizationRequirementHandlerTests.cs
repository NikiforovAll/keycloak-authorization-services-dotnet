namespace Keycloak.AuthServices.Authorization.Tests.Organizations;

using System.Security.Claims;
using FluentAssertions;
using Keycloak.AuthServices.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

public class OrganizationRequirementHandlerTests
{
    private const string JsonValueType = "JSON";
    private const string Issuer = "https://keycloak.example.com/realms/test";

    [Fact]
    public async Task AnyOrg_UserHasOrgs_Succeeds()
    {
        var requirement = new OrganizationRequirement();
        var principal = CreatePrincipal( /*lang=json,strict*/
            """{"acme-corp": {}}"""
        );
        var handler = CreateHandler();

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task AnyOrg_UserHasNoOrgs_Fails()
    {
        var requirement = new OrganizationRequirement();
        var principal = CreatePrincipal("{}");
        var handler = CreateHandler();

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task SpecificAlias_UserIsMember_Succeeds()
    {
        var requirement = new OrganizationRequirement("acme-corp");
        var principal = CreatePrincipal( /*lang=json,strict*/
            """{"acme-corp": {"id": "uuid-1"}}"""
        );
        var handler = CreateHandler();

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task SpecificAlias_UserIsNotMember_Fails()
    {
        var requirement = new OrganizationRequirement("other-org");
        var principal = CreatePrincipal( /*lang=json,strict*/
            """{"acme-corp": {}}"""
        );
        var handler = CreateHandler();

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task SpecificId_UserIsMember_Succeeds()
    {
        var requirement = new OrganizationRequirement("a56bea03-5904-470a-b21c-92b7f1069d44");
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """{"acme-corp": {"id": "a56bea03-5904-470a-b21c-92b7f1069d44"}}"""
        );
        var handler = CreateHandler();

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task RouteParam_ResolvesAndChecks()
    {
        var requirement = new OrganizationRequirement("{orgId}");

        var httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["orgId"] = "acme-corp";

        var services = new ServiceCollection();
        services.AddSingleton<RouteParameterResolver>();
        httpContext.RequestServices = services.BuildServiceProvider();

        var principal = CreatePrincipal( /*lang=json,strict*/
            """{"acme-corp": {}}"""
        );
        var handler = CreateHandler(httpContext);

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task RouteParam_NotMember_Fails()
    {
        var requirement = new OrganizationRequirement("{orgId}");

        var httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["orgId"] = "other-org";

        var services = new ServiceCollection();
        services.AddSingleton<RouteParameterResolver>();
        httpContext.RequestServices = services.BuildServiceProvider();

        var principal = CreatePrincipal( /*lang=json,strict*/
            """{"acme-corp": {}}"""
        );
        var handler = CreateHandler(httpContext);

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task Unauthenticated_Skips()
    {
        var requirement = new OrganizationRequirement();
        var principal = new ClaimsPrincipal(new ClaimsIdentity());
        var handler = CreateHandler();

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task NoClaim_Fails()
    {
        var requirement = new OrganizationRequirement();
        var principal = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim("sub", "user1")], "Bearer")
        );
        var handler = CreateHandler();

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task CustomClaimType_UserHasOrgs_Succeeds()
    {
        var requirement = new OrganizationRequirement();
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """{"acme-corp": {}}""",
            claimType: "tenant"
        );
        var handler = CreateHandler(
            authorizationOptions: new KeycloakAuthorizationOptions
            {
                OrganizationClaimType = "tenant",
            }
        );

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task CustomClaimType_DefaultClaimType_Fails()
    {
        var requirement = new OrganizationRequirement();
        var principal = CreatePrincipal(
            /*lang=json,strict*/
            """{"acme-corp": {}}""",
            claimType: "tenant"
        );
        var handler = CreateHandler();

        var context = new AuthorizationHandlerContext([requirement], principal, null);
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    private static OrganizationRequirementHandler CreateHandler(
        HttpContext? httpContext = null,
        KeycloakAuthorizationOptions? authorizationOptions = null
    )
    {
        var httpContextAccessor = new HttpContextAccessor { HttpContext = httpContext };
        var options = Options.Create(authorizationOptions ?? new KeycloakAuthorizationOptions());
        var metrics = new KeycloakMetrics(new TestMeterFactory());
        var logger = NullLogger<OrganizationRequirementHandler>.Instance;
        return new OrganizationRequirementHandler(httpContextAccessor, options, metrics, logger);
    }

    private static ClaimsPrincipal CreatePrincipal(
        string organizationClaimValue,
        string claimType = "organization"
    ) =>
        new(
            new ClaimsIdentity(
                [new Claim(claimType, organizationClaimValue, JsonValueType, Issuer, Issuer)],
                "Bearer"
            )
        );

    private sealed class TestMeterFactory : System.Diagnostics.Metrics.IMeterFactory
    {
        public System.Diagnostics.Metrics.Meter Create(
            System.Diagnostics.Metrics.MeterOptions options
        ) => new(options);

        public void Dispose() { }
    }
}
