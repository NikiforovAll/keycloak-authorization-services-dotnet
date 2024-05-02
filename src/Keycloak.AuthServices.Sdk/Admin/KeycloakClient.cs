namespace Keycloak.AuthServices.Sdk.Admin;

using Keycloak.AuthServices.Sdk.Admin.Constants;

/// <summary>
/// TBD:
/// </summary>
public partial class KeycloakClient : IKeycloakClient
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="httpClient"></param>
    public KeycloakClient(HttpClient httpClient) => this.httpClient = httpClient;

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetRealmWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default
    )
    {
        var path = KeycloakClientApiConstants.GetRealm.Replace("{realm}", realm);

        var responseMessage = await this.httpClient.GetAsync(
            path,
            cancellationToken: cancellationToken
        );

        return responseMessage!;
    }
}
