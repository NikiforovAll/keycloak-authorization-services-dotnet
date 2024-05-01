namespace Keycloak.AuthServices.Authorization.AuthorizationServer;

using Keycloak.AuthServices.Common;
using Microsoft.Extensions.Options;

/// <inheritdoc />
public class KeycloakProtectionClient : IKeycloakProtectionClient
{
    private readonly HttpClient httpClient;
    private readonly IOptions<KeycloakProtectionClientOptions> clientOptions;

    /// <summary>
    /// Constructs KeycloakProtectionClient
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="clientOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public KeycloakProtectionClient(
        HttpClient httpClient,
        IOptions<KeycloakProtectionClientOptions> clientOptions
    )
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.clientOptions = clientOptions;
    }

    /// <inheritdoc />
    public async Task<bool> VerifyAccessToResource(
        string resource,
        string scope,
        CancellationToken cancellationToken
    )
    {
        var audience = this.clientOptions.Value.Resource;

        var permission = string.IsNullOrWhiteSpace(scope) ? resource : $"{resource}#{scope}";

        var data = new Dictionary<string, string>
        {
            { "grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket" },
            { "response_mode", "decision" },
            { "audience", audience ?? string.Empty },
            { "permission", permission }
        };

        var response = await this.httpClient.PostAsync(
            KeycloakConstants.TokenEndpointPath,
            new FormUrlEncodedContent(data),
            cancellationToken
        );

        return response.IsSuccessStatusCode;
    }
}
