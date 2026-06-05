namespace Keycloak.AuthServices.Sdk.Admin;

using Sdk;
using Models;

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
    public async Task<RealmRepresentation> GetRealmAsync(
        string realm,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetRealmWithResponseAsync(realm, cancellationToken);

        return (await response.GetResponseAsync<RealmRepresentation>(cancellationToken))!;
    }

    /// <summary>
    /// Get realm
    /// </summary>
    /// <param name="realm"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HttpResponseMessage> GetRealmWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default
    );
}
