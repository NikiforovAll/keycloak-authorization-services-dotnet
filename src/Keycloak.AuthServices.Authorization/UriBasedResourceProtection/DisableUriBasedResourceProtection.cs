namespace Keycloak.AuthServices.Authorization;

/// <summary>
/// Use this attribute on a method when the <see cref="UriBasedResourceProtectionMiddleware"/>
/// to exclude the method from uri based resource protection.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class DisableUriBasedResourceProtectionAttribute : Attribute
{
}