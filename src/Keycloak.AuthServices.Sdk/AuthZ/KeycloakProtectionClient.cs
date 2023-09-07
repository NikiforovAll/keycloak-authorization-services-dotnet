namespace Keycloak.AuthServices.Sdk.AuthZ;

using Admin.Models.Tokens;
using Common;
using System.Net.Http.Json;

/// <inheritdoc />
public class KeycloakProtectionClient : IKeycloakProtectionClient
{
    private readonly HttpClient httpClient;
    private readonly KeycloakProtectionClientOptions clientOptions;

    // Does this exist as a const elsewhere / in a library? 
    private const string UmaTicketGrantType = "urn:ietf:params:oauth:grant-type:uma-ticket";

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
            {"grant_type", UmaTicketGrantType},
            {"response_mode", "decision"},
            {"audience", audience ?? string.Empty},
            {"permission", $"{resource}#{scope}"}
        };

        var response = await this.httpClient.PostAsync(
            KeycloakConstants.TokenEndpointPath, new FormUrlEncodedContent(data), cancellationToken);

        return response.IsSuccessStatusCode;
    }

    /// <inheritdoc />
    public async Task<TokenResponse?> GetTokenForResource(string resource, CancellationToken cancellationToken)
    {
        var audience = this.clientOptions.Resource;

        var data = new Dictionary<string, string>
        {
            {"grant_type", UmaTicketGrantType},
            {"response_include_resource_name", "true"},
            {"audience", audience},
            {"permission", resource}
        };

        var response = await this.httpClient.PostAsync(
            KeycloakConstants.TokenEndpointPath, new FormUrlEncodedContent(data), cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ResourcePermission>?> GetResourcePermissions(string scope, CancellationToken cancellationToken)
    {
        var audience = this.clientOptions.Resource;

        var data = new Dictionary<string, string>
        {
            {"grant_type", UmaTicketGrantType},
            {"response_include_resource_name", "true"},
            {"audience", audience},
            {"permission", $"#{scope}"},       // Use an empty resource for the query
            {"response_mode", "permissions"}   // Ensure the response is providing a list of permissions
        };

        var response = await this.httpClient.PostAsync(
            KeycloakConstants.TokenEndpointPath, new FormUrlEncodedContent(data), cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IEnumerable<ResourcePermission>>(cancellationToken: cancellationToken);
    }
}
