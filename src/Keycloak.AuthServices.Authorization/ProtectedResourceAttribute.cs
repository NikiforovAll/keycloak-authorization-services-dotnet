namespace Keycloak.AuthServices.Authorization;

/// <summary>
/// Specifies that the class or method that this attribute is applied to requires the specified authorization.
/// </summary>
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Method,
    AllowMultiple = true,
    Inherited = true
)]
public sealed class ProtectedResourceAttribute : Attribute, IProtectedResourceData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProtectedResourceAttribute"/> class with the specified policy.
    /// </summary>
    public ProtectedResourceAttribute(string resource, string? scope = default)
        : this(resource, string.IsNullOrWhiteSpace(scope) ? Array.Empty<string>() : new[] { scope })
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProtectedResourceAttribute"/> class with the specified policy.
    /// </summary>
    public ProtectedResourceAttribute(string resource, string[] scopes)
    {
        this.Resource = resource;
        this.Scopes = scopes;
    }

    /// <inheritdoc/>
    public string Resource { get; set; }

    /// <inheritdoc/>
    public string[]? Scopes { get; set; }
}
