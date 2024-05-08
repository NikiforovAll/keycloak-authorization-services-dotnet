namespace Keycloak.AuthServices.Authorization.Requirements;

using System.Collections;
using System.Globalization;
using System.Text;

/// <summary>
/// Represents a verification plan for protected resources.
/// </summary>
internal sealed class VerificationPlan : IEnumerable<IProtectedResourceData>
{
    public VerificationPlan() { }

    public VerificationPlan(IEnumerable<IProtectedResourceData> protectedResources) =>
        this.AddRange(protectedResources);

    /// <summary>
    /// Gets the list of resources in the verification plan.
    /// </summary>
    public List<string> Resources { get; } = new();
    private Dictionary<string, List<string>> resourceToScopes = new();
    private Dictionary<string, bool> resourceToOutcomes = new();

    /// <summary>
    /// Adds a range of protected resources to the verification plan.
    /// </summary>
    /// <param name="protectedResources">The protected resources to add.</param>
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

    /// <summary>
    /// Adds a protected resource with its associated scopes to the verification plan.
    /// </summary>
    /// <param name="resource">The resource to add.</param>
    /// <param name="scopes">The scopes associated with the resource.</param>
    public void Add(string resource, string scopes)
    {
        if (
            this.resourceToScopes.ContainsKey(resource)
            && !this.resourceToScopes[resource].Contains(scopes)
        )
        {
            this.resourceToScopes[resource].Add(scopes);
        }
        else
        {
            this.Resources.Add(resource);

            this.resourceToScopes[resource] = new List<string>() { scopes };
        }
    }

    /// <summary>
    /// Removes a protected resource from the verification plan.
    /// </summary>
    /// <param name="resource">The resource to remove. Pass an empty string to remove all resources.</param>
    /// <returns>True if the resource was successfully removed, false otherwise.</returns>
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

    /// <summary>
    /// Marks a protected resource as completed with the specified result.
    /// </summary>
    /// <param name="resource">The resource to mark as completed.</param>
    /// <param name="result">The result of the verification.</param>
    public void Complete(string resource, bool result) =>
        this.resourceToOutcomes[resource] = result;

    /// <summary>
    /// Returns a string representation of the verification plan.
    /// </summary>
    /// <returns>A string representation of the verification plan.</returns>
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

    /// <inheritdoc/>
    public IEnumerator<IProtectedResourceData> GetEnumerator()
    {
        var resources = new List<ProtectedResourceAttribute>();

        foreach (var resource in this.Resources)
        {
            resources.Add(new(resource, this.resourceToScopes[resource].ToArray()));
        }

        return resources.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
