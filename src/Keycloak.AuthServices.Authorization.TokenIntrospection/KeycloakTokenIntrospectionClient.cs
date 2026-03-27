namespace Keycloak.AuthServices.Authorization.TokenIntrospection;

using System.Text.Json;
using Keycloak.AuthServices.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <inheritdoc />
public class KeycloakTokenIntrospectionClient : IKeycloakTokenIntrospectionClient
{
    private readonly HttpClient httpClient;
    private readonly IOptions<KeycloakTokenIntrospectionOptions> options;
    private readonly ILogger<KeycloakTokenIntrospectionClient> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakTokenIntrospectionClient"/> class.
    /// </summary>
    public KeycloakTokenIntrospectionClient(
        HttpClient httpClient,
        IOptions<KeycloakTokenIntrospectionOptions> options,
        ILogger<KeycloakTokenIntrospectionClient> logger
    )
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.options = options;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<TokenIntrospectionResponse?> IntrospectTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(token);

        var opts = this.options.Value;

        using var content = new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                ["token"] = token,
                ["client_id"] = opts.Resource,
                ["client_secret"] = opts.Credentials.Secret,
            }
        );

        using var response = await this.httpClient.PostAsync(
            KeycloakConstants.TokenIntrospectionEndpointPath,
            content,
            cancellationToken
        );

        if (!response.IsSuccessStatusCode)
        {
            this.logger.LogTokenIntrospectionRequestFailed(response.StatusCode);
            return null;
        }

        using var document = await JsonDocument.ParseAsync(
            await response.Content.ReadAsStreamAsync(cancellationToken),
            cancellationToken: cancellationToken
        );

        var root = document.RootElement;
        var active = root.TryGetProperty("active", out var activeProp) && activeProp.GetBoolean();

        var claims = new Dictionary<string, JsonElement>();
        foreach (var property in root.EnumerateObject())
        {
            // Clone so values survive document disposal
            claims[property.Name] = property.Value.Clone();
        }

        return new TokenIntrospectionResponse { Active = active, Claims = claims };
    }
}
