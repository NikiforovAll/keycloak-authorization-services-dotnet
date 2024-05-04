namespace Keycloak.AuthServices.Sdk.Protection;

using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;

/// <summary>
/// Access to protected resource API.
/// </summary>
/// <remarks>
/// See: https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_api
/// </remarks>
public interface IKeycloakProtectionClient
{
    #region ResourcesRegion
    /// <summary>
    /// Searches for resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> GetResourcesIdsWithResponseAsync(
        string realm,
        GetResourcesRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Searches for resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task<IList<string>> GetResourcesIdsAsync(
        string realm,
        GetResourcesRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetResourcesIdsWithResponseAsync(
            realm,
            parameters,
            cancellationToken
        );

        return await response.GetResponseAsync<IList<string>>(cancellationToken)
            ?? Array.Empty<string>();
    }

    /// <summary>
    /// Searches for resources
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> GetResourcesWithResponseAsync(
        string realm,
        GetResourcesRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Searches for resources
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task<IList<ResourceResponse>> GetResourcesAsync(
        string realm,
        GetResourcesRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetResourcesWithResponseAsync(
            realm,
            parameters,
            cancellationToken
        );

        return await response.GetResponseAsync<IList<ResourceResponse>>(cancellationToken)
            ?? Array.Empty<ResourceResponse>();
    }

    /// <summary>
    /// Gets resource by Id
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> GetResourceWithResponseAsync(
        string realm,
        string resourceId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets resource by Id
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task<ResourceResponse> GetResourceAsync(
        string realm,
        string resourceId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetResourceWithResponseAsync(
            realm,
            resourceId,
            cancellationToken
        );

        return await response.GetResponseAsync<ResourceResponse>(cancellationToken) ?? new();
    }

    /// <summary>
    /// Creates resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> CreateResourceWithResponseAsync(
        string realm,
        Resource resource,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task<ResourceResponse> CreateResourceAsync(
        string realm,
        Resource resource,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.CreateResourceWithResponseAsync(
            realm,
            resource,
            cancellationToken
        );

        return await response.GetResponseAsync<ResourceResponse>(cancellationToken) ?? new();
    }

    /// <summary>
    /// Updates resource
    /// </summary>
    /// <remarks>
    ///     Docs: https://github.com/keycloak/keycloak-documentation/blob/main/authorization_services/topics/service-protection-resources-api-papi.adoc#updating-resources
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <param name="resource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> UpdateResourceWithResponseAsync(
        string realm,
        string resourceId,
        Resource resource,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates resource
    /// </summary>
    /// <remarks>
    ///     Docs: https://github.com/keycloak/keycloak-documentation/blob/main/authorization_services/topics/service-protection-resources-api-papi.adoc#updating-resources
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <param name="resource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task UpdateResourceAsync(
        string realm,
        string resourceId,
        Resource resource,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.UpdateResourceWithResponseAsync(
            realm,
            resourceId,
            resource,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> DeleteResourceWithResponseAsync(
        string realm,
        string resourceId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a resource
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">Resource ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task DeleteResourceAsync(
        string realm,
        string resourceId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.DeleteResourceWithResponseAsync(
            realm,
            resourceId,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    #endregion
}
