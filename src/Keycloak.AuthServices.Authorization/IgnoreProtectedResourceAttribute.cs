namespace Keycloak.AuthServices.Authorization;

/// <summary>
/// Specifies that the class or method that this attribute is applied to requires the specified authorization.
/// </summary>
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Method,
    AllowMultiple = true,
    Inherited = true
)]
public sealed class IgnoreProtectedResourceAttribute : Attribute, IProtectedResourceData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProtectedResourceAttribute"/> class with the specified policy.
    /// </summary>
    public IgnoreProtectedResourceAttribute(string resource) => this.Resource = resource;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProtectedResourceAttribute"/> class with the specified policy.
    /// </summary>
    public IgnoreProtectedResourceAttribute() => this.Resource = string.Empty;

    /// <inheritdoc/>
    public string Resource { get; }

    /// <inheritdoc/>
    public string[]? Scopes { get; }
}
