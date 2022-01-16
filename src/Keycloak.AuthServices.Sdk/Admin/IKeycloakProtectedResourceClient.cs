namespace Keycloak.AuthServices.Sdk.Admin;

using Constants;
using Models;
using Refit;

public interface IKeycloakProtectedResourceClient
{
    // TODO: add search params
    [Get(KeycloakClientApiConstants.GetResources)]
    [Headers("Accept: application/json")]
    Task<List<string>> GetResources(string realm);

    [Get(KeycloakClientApiConstants.GetResource)]
    [Headers("Accept: application/json")]
    Task<ResourceResponse> GetResource(string realm, string id);

    [Post(KeycloakClientApiConstants.CreateResource)]
    [Headers("Accept: application/json", "Content-Type: application/json")]
    Task<ResourceResponse> CreateResource(string realm, [Body] Resource resource);
}
