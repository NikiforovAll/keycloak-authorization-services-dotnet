namespace Keycloak.AuthServices.Sdk.AuthZ;

using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;

/// <inheritdoc />
public class KeycloakProtectionClient : IKeycloakProtectionClient
{
    private readonly HttpClient httpClient;
    private readonly KeycloakProtectionClientOptions clientOptions;

    /// <summary>
    /// Constructs KeycloakProtectionClient
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="clientOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public KeycloakProtectionClient(
        HttpClient httpClient, KeycloakProtectionClientOptions clientOptions)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.clientOptions = clientOptions;
    }

    /// <inheritdoc />
    public async Task<bool> VerifyAccessToResource(
        string resource, string scope, CancellationToken cancellationToken)
    {
        var audience = this.clientOptions.Resource;

        var data = new Dictionary<string, string>
        {
            {"grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket"},
            {"response_mode", "decision"},
            {"audience", audience ?? string.Empty},
            {"permission", $"{resource}#{scope}"}
        };

        // client.DefaultRequestHeaders.Authorization = new("Bearer", token);
        // var configuration = await options.ConfigurationManager.GetConfigurationAsync(CancellationToken.None);
        var response = await this.httpClient.PostAsync(
            KeycloakConstants.TokenEndpointPath, new FormUrlEncodedContent(data), cancellationToken);

        return response.IsSuccessStatusCode;
    }
}
