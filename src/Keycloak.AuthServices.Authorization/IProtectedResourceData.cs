namespace Keycloak.AuthServices.Authorization;

/// <summary>
/// Defines the set of data required to apply authorization rules to a resource.
/// </summary>
public interface IProtectedResourceData
{
    /// <summary>
    /// Gets or sets resource name
    /// </summary>
    public string Resource { get; }

    /// <summary>
    /// Get or sets scopes
    /// </summary>
    public string[]? Scopes { get; }

    /// <summary>
    /// Gets the type of <see cref="IParameterResolver"/> used to resolve resource template parameters.
    /// When <c>null</c>, the default <c>RouteParameterResolver</c> is used.
    /// </summary>
    public Type? ResolverType => null;

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public string GetScopesExpression() =>
        this.Scopes is not null
            ? string.Join(',', this.Scopes.Where(s => !string.IsNullOrWhiteSpace(s)))
            : string.Empty;
}
