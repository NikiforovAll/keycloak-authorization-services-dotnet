namespace Keycloak.AuthServices.Authorization;

/// <summary>
/// Specifies that the class or method that this attribute is applied to requires the specified authorization.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ProtectedResourceAttribute"/> class with the specified policy.
/// </remarks>
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Method,
    AllowMultiple = true,
    Inherited = true
)]
public sealed class ProtectedResourceAttribute(string resource, string[] scopes)
    : Attribute,
        IProtectedResourceData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProtectedResourceAttribute"/> class with the specified policy.
    /// </summary>
    public ProtectedResourceAttribute(string resource, string? scope = default)
        : this(resource, string.IsNullOrWhiteSpace(scope) ? [] : [scope]) { }

    /// <inheritdoc/>
    public string Resource { get; } = resource;

    /// <inheritdoc/>
    public string[]? Scopes { get; } = scopes;

    /// <inheritdoc/>
    public string? Audience { get; set; }

    /// <summary>
    /// Gets or sets the type of <see cref="IParameterResolver"/> used to resolve resource template parameters.
    /// When <c>null</c>, the default <c>RouteParameterResolver</c> is used.
    /// </summary>
    public Type? ResolverType { get; set; }
}
