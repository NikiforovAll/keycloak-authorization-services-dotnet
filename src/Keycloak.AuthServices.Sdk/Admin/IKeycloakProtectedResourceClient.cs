namespace Keycloak.AuthServices.Sdk.Admin;

using Constants;
using Models;
using Refit;

/// <summary>
/// Access to protected resource API
/// </summary>
public interface IKeycloakProtectedResourceClient
{
    /// <summary>
    /// Searches for resource
    /// </summary>
    /// <param name="realm"></param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetResources)]
    [Headers("Accept: application/json")]
    Task<List<string>> GetResources(string realm);

    /// <summary>
    /// Get resource by Id
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetResource)]
    [Headers("Accept: application/json")]
    Task<ResourceResponse> GetResource(string realm, string id);

    /// <summary>
    /// Create resource
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [Post(KeycloakClientApiConstants.CreateResource)]
    [Headers("Accept: application/json", "Content-Type: application/json")]
    Task<ResourceResponse> CreateResource(string realm, [Body] Resource resource);
}
