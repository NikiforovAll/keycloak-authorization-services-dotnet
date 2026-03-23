namespace Keycloak.AuthServices.Authorization.Tests;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public class ParameterResolverTests
{
    [Fact]
    public void RouteParameterResolver_ResolvesFromRouteValues()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues["workspaceId"] = "123";

        var resolver = new RouteParameterResolver();
        var result = resolver.Resolve(
            "workspaceId",
            httpContext,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().Be("123");
    }

    [Fact]
    public void RouteParameterResolver_ReturnsNull_WhenNotFound()
    {
        var httpContext = new DefaultHttpContext();

        var resolver = new RouteParameterResolver();
        var result = resolver.Resolve(
            "missing",
            httpContext,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().BeNull();
    }

    [Fact]
    public void HeaderParameterResolver_ResolvesFromHeaders()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["X-Tenant-Id"] = "acme";

        var resolver = new HeaderParameterResolver();
        var result = resolver.Resolve(
            "X-Tenant-Id",
            httpContext,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().Be("acme");
    }

    [Fact]
    public void HeaderParameterResolver_ReturnsNull_WhenNotFound()
    {
        var httpContext = new DefaultHttpContext();

        var resolver = new HeaderParameterResolver();
        var result = resolver.Resolve(
            "X-Tenant-Id",
            httpContext,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().BeNull();
    }

    [Fact]
    public void QueryParameterResolver_ResolvesFromQueryString()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.QueryString = new QueryString("?tenant=acme");

        var resolver = new QueryParameterResolver();
        var result = resolver.Resolve(
            "tenant",
            httpContext,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().Be("acme");
    }

    [Fact]
    public void QueryParameterResolver_ReturnsNull_WhenNotFound()
    {
        var httpContext = new DefaultHttpContext();

        var resolver = new QueryParameterResolver();
        var result = resolver.Resolve(
            "tenant",
            httpContext,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().BeNull();
    }

    [Fact]
    public void ResolveResource_WithResolver_ReplacesPlaceholders()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["X-Tenant-Id"] = "acme";

        var resolver = new HeaderParameterResolver();
        var result = Utils.ResolveResource(
            "workspaces/{X-Tenant-Id}",
            httpContext,
            resolver,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().Be("workspaces/acme");
    }

    [Fact]
    public void ResolveResource_WithResolver_PreservesUnresolvablePlaceholders()
    {
        var httpContext = new DefaultHttpContext();

        var resolver = new HeaderParameterResolver();
        var result = Utils.ResolveResource(
            "workspaces/{X-Tenant-Id}",
            httpContext,
            resolver,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().Be("workspaces/{X-Tenant-Id}");
    }

    [Fact]
    public void ResolveResource_WithResolver_ReturnsUnchanged_WhenNoPlaceholders()
    {
        var httpContext = new DefaultHttpContext();

        var resolver = new HeaderParameterResolver();
        var result = Utils.ResolveResource(
            "workspaces/static",
            httpContext,
            resolver,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().Be("workspaces/static");
    }

    [Fact]
    public void ResolveResource_WithResolver_HandlesMultiplePlaceholders()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["X-Org"] = "acme";
        httpContext.Request.Headers["X-Team"] = "engineering";

        var resolver = new HeaderParameterResolver();
        var result = Utils.ResolveResource(
            "orgs/{X-Org}/teams/{X-Team}",
            httpContext,
            resolver,
            new ServiceCollection().BuildServiceProvider()
        );

        result.Should().Be("orgs/acme/teams/engineering");
    }

    [Fact]
    public void ResolveResource_UsesRouteResolver_WhenResolverTypeIsNull()
    {
        var services = new ServiceCollection();
        services.AddSingleton<RouteParameterResolver>();
        var sp = services.BuildServiceProvider();

        var httpContextAccessor = new HttpContextAccessor
        {
            HttpContext = new DefaultHttpContext(),
        };
        httpContextAccessor.HttpContext.Request.RouteValues["id"] = "42";

        var resourceData = new ProtectedResourceAttribute("items/{id}", "read");

        var result = Utils.ResolveResource(resourceData, httpContextAccessor, sp);

        result.Should().Be("items/42");
    }

    [Fact]
    public void ResolveResource_UsesSpecifiedResolver()
    {
        var services = new ServiceCollection();
        services.AddSingleton<HeaderParameterResolver>();
        var sp = services.BuildServiceProvider();

        var httpContextAccessor = new HttpContextAccessor
        {
            HttpContext = new DefaultHttpContext(),
        };
        httpContextAccessor.HttpContext!.Request.Headers["X-Tenant-Id"] = "acme";

        var resourceData = new ProtectedResourceAttribute("workspaces/{X-Tenant-Id}", "read")
        {
            ResolverType = typeof(HeaderParameterResolver),
        };

        var result = Utils.ResolveResource(resourceData, httpContextAccessor, sp);

        result.Should().Be("workspaces/acme");
    }

    [Fact]
    public void ProtectedResourceAttribute_ResolverType_DefaultsToNull()
    {
        var attribute = new ProtectedResourceAttribute("resource", "scope");

        attribute.ResolverType.Should().BeNull();
    }

    [Fact]
    public void ProtectedResourceAttribute_ResolverType_CanBeSet()
    {
        var attribute = new ProtectedResourceAttribute("resource", "scope")
        {
            ResolverType = typeof(HeaderParameterResolver),
        };

        attribute.ResolverType.Should().Be<HeaderParameterResolver>();
    }

    [Fact]
    public void IProtectedResourceData_ResolverType_DefaultsToNull()
    {
        IProtectedResourceData data = new ProtectedResourceAttribute("resource", "scope");

        data.ResolverType.Should().BeNull();
    }

    [Fact]
    public void CustomResolver_CanAccessServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddSingleton(new TestTenantConfig("resolved-tenant"));
        var sp = services.BuildServiceProvider();

        var httpContext = new DefaultHttpContext();
        var resolver = new TestCustomResolver();

        var result = resolver.Resolve("tenant", httpContext, sp);

        result.Should().Be("resolved-tenant");
    }

    private sealed record TestTenantConfig(string TenantId);

    private sealed class TestCustomResolver : IParameterResolver
    {
        public string? Resolve(
            string parameter,
            HttpContext httpContext,
            IServiceProvider serviceProvider
        )
        {
            var config = serviceProvider.GetService<TestTenantConfig>();
            return config?.TenantId;
        }
    }
}
