namespace Keycloak.AuthServices.Authorization.Tests;

using System.Security.Claims;
using FluentAssertions;
using Keycloak.AuthServices.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging.Abstractions;

public class RptRequirementHandlerTests
{
    private static RptRequirementHandler CreateHandler() =>
        new(NullLogger<RptRequirementHandler>.Instance);

    private static ClaimsPrincipal CreatePrincipalWithAuthorization(string authorizationJson) =>
        new(new ClaimsIdentity([new Claim("authorization", authorizationJson, "JSON")], "Bearer"));

    private static ClaimsPrincipal CreateUnauthenticatedPrincipal() => new(new ClaimsIdentity());

    [Fact]
    public async Task HandleRequirement_UserNotAuthenticated_Skips()
    {
        var requirement = new RptRequirement("customers", "read");
        var principal = CreateUnauthenticatedPrincipal();
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirement_NoAuthorizationClaim_DoesNotSucceed()
    {
        var requirement = new RptRequirement("customers", "read");
        var principal = new ClaimsPrincipal(new ClaimsIdentity([], "Bearer"));
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirement_MatchingResourceAndScope_Succeeds()
    {
        var requirement = new RptRequirement("customers", "read");
        var authJson = """
            {
                "permissions": [
                    {
                        "scopes": ["read"],
                        "rsid": "deb61104-5008-4001-8792-ac5734b1235b",
                        "rsname": "customers"
                    }
                ]
            }
            """;
        var principal = CreatePrincipalWithAuthorization(authJson);
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task HandleRequirement_WrongResource_DoesNotSucceed()
    {
        var requirement = new RptRequirement("projects", "read");
        var authJson = """
            {
                "permissions": [
                    {
                        "scopes": ["read"],
                        "rsname": "customers"
                    }
                ]
            }
            """;
        var principal = CreatePrincipalWithAuthorization(authJson);
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirement_WrongScope_DoesNotSucceed()
    {
        var requirement = new RptRequirement("customers", "write");
        var authJson = """
            {
                "permissions": [
                    {
                        "scopes": ["read"],
                        "rsname": "customers"
                    }
                ]
            }
            """;
        var principal = CreatePrincipalWithAuthorization(authJson);
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirement_MultiplePermissions_MatchesCorrectOne()
    {
        var requirement = new RptRequirement("projects", "create");
        var authJson = """
            {
                "permissions": [
                    {
                        "scopes": ["read"],
                        "rsname": "customers"
                    },
                    {
                        "scopes": ["read", "create", "update", "archive"],
                        "rsid": "cca3a1af-b8c5-478f-acee-01044961db50",
                        "rsname": "projects"
                    }
                ]
            }
            """;
        var principal = CreatePrincipalWithAuthorization(authJson);
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task HandleRequirement_MissingPermissionsProperty_DoesNotSucceed()
    {
        var requirement = new RptRequirement("customers", "read");
        var authJson = """{"other": "data"}""";
        var principal = CreatePrincipalWithAuthorization(authJson);
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        // Should not throw even though "permissions" property is absent
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirement_PermissionMissingRsname_SkipsEntry()
    {
        var requirement = new RptRequirement("customers", "read");
        var authJson = """
            {
                "permissions": [
                    {
                        "scopes": ["read"],
                        "rsid": "deb61104-5008-4001-8792-ac5734b1235b"
                    }
                ]
            }
            """;
        var principal = CreatePrincipalWithAuthorization(authJson);
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        // Should not throw even though "rsname" is absent
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirement_PermissionMissingScopes_SkipsEntry()
    {
        var requirement = new RptRequirement("customers", "read");
        var authJson = """
            {
                "permissions": [
                    {
                        "rsname": "customers"
                    }
                ]
            }
            """;
        var principal = CreatePrincipalWithAuthorization(authJson);
        var handler = CreateHandler();
        var context = new AuthorizationHandlerContext([requirement], principal, null);

        // Should not throw even though "scopes" is absent
        await handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }
}
