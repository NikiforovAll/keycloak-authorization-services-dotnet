namespace Keycloak.AuthServices.Authorization;

/// <summary>
/// Use this attribute on a method when the <see cref="UriBasedResourceProtectionMiddleware"/>
/// to exclude the method from uri based resource protection. In case a specific resource name and scope are passed,
/// it will use them for authorization instead.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class ExplicitResourceProtectionAttribute : Attribute
{
    /// <summary>
    /// <see cref="ExplicitResourceProtectionAttribute"/> 
    /// </summary>
    /// <param name="resourceName">The name of the resource a configured in Keycloak mandatory and unique "Name" field (also referred as RESOURCE_ID).</param>
    /// <param name="scope">A valid scope from the list of applied scopes to the resource.</param>
    public ExplicitResourceProtectionAttribute(string resourceName, string scope)
    {
        this.ResourceName = resourceName;
        this.Scope = scope;
    }

    /// <summary>
    /// <see cref="ExplicitResourceProtectionAttribute"/> 
    /// </summary>
    /// <param name="disable">Indicates if a method should be excluded from the uri nased resource protection.</param>
    public ExplicitResourceProtectionAttribute(bool disable) => this.Disable = disable;

    /// <summary>
    /// The resource name to authorize for.
    /// </summary>
    public string? ResourceName { get; }

    /// <summary>
    /// The scope to authorize the resource for.
    /// </summary>
    public string? Scope { get; }

    /// <summary>
    /// If authorization is disabled.
    /// </summary>
    public bool Disable { get; }
}