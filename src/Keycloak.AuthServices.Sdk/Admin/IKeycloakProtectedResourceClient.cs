namespace Keycloak.AuthServices.Sdk.Admin;

using Constants;
using Requests.Resources;
using Models.Resources;
using Refit;

/// <summary>
/// Access to protected resource API
/// </summary>
public interface IKeycloakProtectedResourceClient
{
    /// <summary>
    /// Searches for resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="getResourcesRequestParameters">Optional query parameters</param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetResources)]
    [Headers("Accept: application/json")]
    Task<List<string>> GetResources(string realm, [Query] GetResourcesRequestParameters? getResourcesRequestParameters = default);

    /// <summary>
    /// Searches for resources. NOTE: you must set the <see cref="GetResourcesRequestParameters.Deep"/> property to true
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="getResourcesDeepRequestParameters">Query parameters</param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetResources)]
    [Headers("Accept: application/json")]
    Task<List<ResourceResponse>> GetResourcesDeep(string realm, [Query] GetResourcesDeepRequestParameters getResourcesDeepRequestParameters);

    /// <summary>
    /// Gets resource by Id
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetResource)]
    [Headers("Accept: application/json")]
    Task<ResourceResponse> GetResource(string realm, [AliasAs("id")] string resourceId);

    /// <summary>
    /// Gets resource by Name
    /// </summary>
    /// <remarks>
    ///     https://github.com/keycloak/keycloak/blob/main/docs/documentation/authorization_services/topics/service-protection-resources-api-papi.adoc#querying-resources
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="name"></param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetResourceByExactName)]
    [Headers("Accept: application/json")]
    Task<string[]> SearchResourcesByName(string realm, [Query] string name);

    /// <summary>
    /// Creates resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
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
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <param name="resource"></param>
    /// <returns></returns>
    [Put(KeycloakClientApiConstants.PutResource)]
    [Headers("Accept: application/json", "Content-Type: application/json")]
    Task UpdateResource(string realm, [AliasAs("id")] string resourceId, [Body] Resource resource);

    /// <summary>
    /// Delete a resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <returns></returns>
    [Delete(KeycloakClientApiConstants.DeleteResource)]
    [Headers("Accept: application/json")]
    Task DeleteResource(string realm, [AliasAs("id")] string resourceId);
}
