namespace Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Builds an <see cref="AuthorizationPolicy"/> from a protected resource policy name.
/// </summary>
/// <remarks>
/// Implement this interface to customize how policies are built from protected resource names,
/// for example to add caching, logging, or additional requirements.
/// </remarks>
public interface IProtectedResourcePolicyBuilder
{
    /// <summary>
    /// Builds an authorization policy from the given policy name.
    /// </summary>
    /// <param name="policyName">The policy name in the format <c>resource#scope</c>.</param>
    /// <returns>The built <see cref="AuthorizationPolicy"/>, or <c>null</c> if the name is not valid.</returns>
    public AuthorizationPolicy? Build(string policyName);
}
