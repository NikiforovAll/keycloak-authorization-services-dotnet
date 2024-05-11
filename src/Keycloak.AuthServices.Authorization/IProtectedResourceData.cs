namespace Keycloak.AuthServices.Authorization;

/// <summary>
/// Defines the set of data required to apply authorization rules to a resource.
/// </summary>
public interface IProtectedResourceData
{
    /// <summary>
    /// Gets or sets resource name
    /// </summary>
    string Resource { get; }

    /// <summary>
    /// Get or sets scopes
    /// </summary>
    string[]? Scopes { get; }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public string GetScopesExpression() =>
        this.Scopes is not null
            ? string.Join(',', this.Scopes.Where(s => !string.IsNullOrWhiteSpace(s)))
            : string.Empty;
}
