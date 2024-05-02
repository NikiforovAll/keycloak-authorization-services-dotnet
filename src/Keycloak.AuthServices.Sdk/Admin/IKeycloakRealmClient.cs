namespace Keycloak.AuthServices.Sdk.Admin;

using Keycloak.AuthServices.Sdk;
using Keycloak.AuthServices.Sdk.Admin.Models;

/// <summary>
/// Realm management
/// </summary>
public interface IKeycloakRealmClient
{
    /// <summary>
    /// Get realm
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task<RealmRepresentation> GetRealmAsync(
        string realm,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetRealmWithResponseAsync(realm, cancellationToken);

        return (await response.GetAsync<RealmRepresentation>(cancellationToken))!;
    }

    /// <summary>
    /// Get realm
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> GetRealmWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default
    );
}
