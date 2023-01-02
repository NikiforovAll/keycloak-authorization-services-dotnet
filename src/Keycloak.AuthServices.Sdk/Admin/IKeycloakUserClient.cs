namespace Keycloak.AuthServices.Sdk.Admin;

using Constants;
using Models;
using Refit;

/// <summary>
/// User management
/// </summary>
public interface IKeycloakUserClient
{
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [Post(KeycloakClientApiConstants.CreateUser)]
    [Headers("Accept: application/json", "Content-Type: application/json")]
    Task CreateUser(string realm, [Body] User user);
}
