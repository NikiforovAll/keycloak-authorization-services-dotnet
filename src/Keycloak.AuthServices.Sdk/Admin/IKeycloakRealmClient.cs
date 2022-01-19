namespace Keycloak.AuthServices.Sdk.Admin;

using Constants;
using Refit;

/// <summary>
/// Realm management
/// </summary>
public interface IKeycloakRealmClient
{
    /// <summary>
    /// Get realm
    /// </summary>
    /// <param name="realm"></param>
    /// <returns></returns>
    [Get(KeycloakClientApiConstants.GetRealm)]
    [Headers("Accept: application/json")]
    Task<string> GetRealm(string realm);
}
