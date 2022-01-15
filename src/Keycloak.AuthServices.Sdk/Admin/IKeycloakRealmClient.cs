namespace Keycloak.AuthServices.Sdk.Admin;

using Constants;
using Refit;

public interface IKeycloakRealmClient
{
    [Get(KeycloakClientApiConstants.GetRealm)]
    [Headers("Accept: application/json")]
    Task<string> GetRealm(string realm);
}
