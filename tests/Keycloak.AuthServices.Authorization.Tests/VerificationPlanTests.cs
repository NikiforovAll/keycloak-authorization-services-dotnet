namespace Keycloak.AuthServices.Authorization.Tests;

using Keycloak.AuthServices.Authorization.Requirements;

public class VerificationPlanTests
{
    [Fact]
    public void Add_SingleResource_CanBeEnumerated()
    {
        var plan = new VerificationPlan();
        plan.Add("my-resource", "read");

        var items = plan.ToList();

        items.Should().ContainSingle();
        items[0].Resource.Should().Be("my-resource");
        items[0].GetScopesExpression().Should().Be("read");
    }

    [Fact]
    public void Add_DuplicateResource_DifferentScopes_MergesScopes()
    {
        var plan = new VerificationPlan();
        plan.Add("my-resource", "read");
        plan.Add("my-resource", "write");

        var items = plan.ToList();

        items.Should().ContainSingle();
        items[0].GetScopesExpression().Should().Be("read,write");
    }

    [Fact]
    public void Add_DuplicateResource_SameScopes_DoesNotDuplicate()
    {
        var plan = new VerificationPlan();
        plan.Add("my-resource", "read");
        plan.Add("my-resource", "read");

        var items = plan.ToList();

        items.Should().ContainSingle();
        items[0].GetScopesExpression().Should().Be("read");
    }

    [Fact]
    public void Add_MultipleResources_EnumeratesAll()
    {
        var plan = new VerificationPlan();
        plan.Add("res-a", "read");
        plan.Add("res-b", "write");

        var items = plan.ToList();

        items.Should().HaveCount(2);
        items.Select(x => x.Resource).Should().ContainInOrder("res-a", "res-b");
    }

    [Fact]
    public void Remove_SpecificResource_RemovesOnlyThatResource()
    {
        var plan = new VerificationPlan();
        plan.Add("res-a", "read");
        plan.Add("res-b", "write");

        plan.Remove("res-a");

        var items = plan.ToList();
        items.Should().ContainSingle();
        items[0].Resource.Should().Be("res-b");
    }

    [Fact]
    public void Remove_EmptyString_ClearsAll()
    {
        var plan = new VerificationPlan();
        plan.Add("res-a", "read");
        plan.Add("res-b", "write");

        plan.Remove(string.Empty);

        plan.ToList().Should().BeEmpty();
        plan.Resources.Should().BeEmpty();
    }

    [Fact]
    public void AddRange_WithIgnoreProtectedResource_RemovesMatchingResource()
    {
        var plan = new VerificationPlan();
        plan.Add("res-a", "read");
        plan.Add("res-b", "write");

        plan.AddRange([new IgnoreProtectedResourceAttribute("res-a")]);

        var items = plan.ToList();
        items.Should().ContainSingle();
        items[0].Resource.Should().Be("res-b");
    }

    [Fact]
    public void AddRange_WithIgnoreAll_ClearsAllResources()
    {
        var plan = new VerificationPlan();
        plan.Add("res-a", "read");
        plan.Add("res-b", "write");

        plan.AddRange([new IgnoreProtectedResourceAttribute()]);

        plan.ToList().Should().BeEmpty();
    }

    [Fact]
    public void Constructor_WithProtectedResources_AddsAll()
    {
        var resources = new IProtectedResourceData[]
        {
            new ProtectedResourceAttribute("res-a", ["read"]),
            new ProtectedResourceAttribute("res-b", ["write"]),
        };

        var plan = new VerificationPlan(resources);

        plan.ToList().Should().HaveCount(2);
    }

    [Fact]
    public void ToString_EmptyPlan_ReturnsEmpty()
    {
        var plan = new VerificationPlan();

        plan.ToString().Should().Be("<empty>");
    }

    [Fact]
    public void ToString_WithResources_ContainsResourceInfo()
    {
        var plan = new VerificationPlan();
        plan.Add("my-resource", "read");

        var result = plan.ToString();

        result.Should().Contain("my-resource");
        result.Should().Contain("read");
    }

    [Fact]
    public void ToString_WithCompletedResource_ContainsOutcome()
    {
        var plan = new VerificationPlan();
        plan.Add("my-resource", "read");
        plan.Complete("my-resource", true);

        var result = plan.ToString();

        result.Should().Contain("True");
    }

    [Fact]
    public void GetEnumerator_CanBeEnumeratedMultipleTimes()
    {
        var plan = new VerificationPlan();
        plan.Add("res-a", "read");
        plan.Add("res-b", "write");

        var first = plan.ToList();
        var second = plan.ToList();

        first.Should().HaveCount(2);
        second.Should().HaveCount(2);
    }
}
