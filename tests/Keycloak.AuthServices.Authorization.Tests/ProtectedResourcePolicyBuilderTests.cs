namespace Keycloak.AuthServices.Authorization.Tests;

using Keycloak.AuthServices.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

public class DefaultProtectedResourcePolicyBuilderTests
{
    private readonly DefaultProtectedResourcePolicyBuilder sut = new();

    [Theory]
    [InlineData("workspaces#workspace:read", "workspaces", "workspace:read")]
    [InlineData("my-resource#scope1", "my-resource", "scope1")]
    [InlineData("resource/123#read", "resource/123", "read")]
    public void Build_ValidPolicyName_ReturnsPolicy(
        string policyName,
        string expectedResource,
        string expectedScope
    )
    {
        var policy = this.sut.Build(policyName);

        policy.Should().NotBeNull();
        policy!.Requirements.Should().ContainSingle();
        var requirement = policy.Requirements[0].Should().BeOfType<DecisionRequirement>().Subject;
        requirement.Resource.Should().Be(expectedResource);
        requirement.Scopes.Should().ContainSingle().Which.Should().Be(expectedScope);
    }

    [Theory]
    [InlineData("invalid-no-hash")]
    [InlineData("too#many#hashes")]
    [InlineData("")]
    public void Build_InvalidPolicyName_ReturnsNull(string policyName)
    {
        var policy = this.sut.Build(policyName);

        policy.Should().BeNull();
    }
}

public class ProtectedResourcePolicyProviderTests
{
    [Fact]
    public async Task GetPolicyAsync_ValidPolicyName_DelegatesToBuilder()
    {
        var expectedPolicy = new AuthorizationPolicyBuilder()
            .AddRequirements(new DecisionRequirement("test", "read"))
            .Build();
        var builder = new FakePolicyBuilder(expectedPolicy);
        var provider = new ProtectedResourcePolicyProvider(
            Options.Create(new AuthorizationOptions()),
            builder
        );

        var result = await provider.GetPolicyAsync("test#read");

        result.Should().BeSameAs(expectedPolicy);
        builder.LastPolicyName.Should().Be("test#read");
    }

    [Fact]
    public async Task GetPolicyAsync_NoBuilderInjected_UsesDefault()
    {
        var provider = new ProtectedResourcePolicyProvider(
            Options.Create(new AuthorizationOptions())
        );

        var result = await provider.GetPolicyAsync("workspaces#read");

        result.Should().NotBeNull();
        result!
            .Requirements.Should()
            .ContainSingle()
            .Which.Should()
            .BeOfType<DecisionRequirement>();
    }

    [Fact]
    public async Task GetPolicyAsync_NonProtectedResourcePolicy_ReturnsNull()
    {
        var builder = new FakePolicyBuilder(null);
        var provider = new ProtectedResourcePolicyProvider(
            Options.Create(new AuthorizationOptions()),
            builder
        );

        var result = await provider.GetPolicyAsync("some-normal-policy");

        result.Should().BeNull();
        builder.LastPolicyName.Should().BeNull();
    }

    [Fact]
    public async Task GetPolicyAsync_RegisteredPolicy_ReturnsRegisteredPolicy()
    {
        var registeredPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        var options = new AuthorizationOptions();
        options.AddPolicy("test#read", registeredPolicy);

        var builder = new FakePolicyBuilder(null);
        var provider = new ProtectedResourcePolicyProvider(Options.Create(options), builder);

        var result = await provider.GetPolicyAsync("test#read");

        result.Should().BeSameAs(registeredPolicy);
        builder.LastPolicyName.Should().BeNull();
    }

    private sealed class FakePolicyBuilder(AuthorizationPolicy? result)
        : IProtectedResourcePolicyBuilder
    {
        public string? LastPolicyName { get; private set; }

        public AuthorizationPolicy? Build(string policyName)
        {
            this.LastPolicyName = policyName;
            return result;
        }
    }
}
