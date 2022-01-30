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
    /// Gets resource by Id
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetResource)]
    [Headers("Accept: application/json")]
    Task<ResourceResponse> GetResource(string realm, string id);

    /// <summary>
    /// Gets resource by Name
    /// </summary>
    /// <remarks>
    ///     https://github.com/keycloak/keycloak-documentation/blob/main/authorization_services/topics/service-protection-resources-api-papi.adoc#querying-resources
    /// </remarks>
    /// <param name="realm"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetResourceByExactName)]
    [Headers("Accept: application/json")]
    Task<string[]> SearchResourcesByName(string realm, [Query] string name);

    /// <summary>
    /// Creates resource
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [Post(KeycloakClientApiConstants.CreateResource)]
    [Headers("Accept: application/json", "Content-Type: application/json")]
    Task<ResourceResponse> CreateResource(string realm, [Body] Resource resource);

    /// <summary>
    /// Updates resource
    /// </summary>
    /// <remarks>
    ///     Docs: https://github.com/keycloak/keycloak-documentation/blob/main/authorization_services/topics/service-protection-resources-api-papi.adoc#updating-resources
    /// </remarks>
    /// <param name="realm"></param>
    /// <param name="id"></param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [Put(KeycloakClientApiConstants.PutResource)]
    [Headers("Accept: application/json", "Content-Type: application/json")]
    Task UpdateResource(
        string realm, string id, [Body] Resource resource);
}
