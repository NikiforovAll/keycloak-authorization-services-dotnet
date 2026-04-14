namespace Keycloak.AuthServices.Sdk.Protection;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
/// Implements UMA ticket exchange using the Keycloak token endpoint.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UmaTicketExchangeClient"/> class.
/// </remarks>
public class UmaTicketExchangeClient(
    HttpClient httpClient,
    IOptions<KeycloakProtectionClientOptions> options,
    ILogger<UmaTicketExchangeClient> logger
) : IUmaTicketExchangeClient
{
    private readonly HttpClient httpClient = httpClient;
    private readonly KeycloakProtectionClientOptions options = options.Value;
    private readonly ILogger<UmaTicketExchangeClient> logger = logger;

    /// <inheritdoc/>
    public async Task<string?> ExchangeTicketForRptAsync(
        string accessToken,
        string ticket,
        CancellationToken cancellationToken = default
    )
    {
        var tokenEndpoint = this.options.KeycloakTokenEndpoint;

        var formData = new Dictionary<string, string>
        {
            ["grant_type"] = "urn:ietf:params:oauth:grant-type:uma-ticket",
            ["ticket"] = ticket,
            ["audience"] = this.options.Resource ?? string.Empty,
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
        request.Content = new FormUrlEncodedContent(formData);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await this.httpClient.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
            this.logger.LogRptObtained();
            return result?.AccessToken;
        }

        this.logger.LogRptExchangeFailed(response.StatusCode);
        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> SubmitPermissionRequestAsync(
        string accessToken,
        string ticket,
        CancellationToken cancellationToken = default
    )
    {
        var tokenEndpoint = this.options.KeycloakTokenEndpoint;

        var formData = new Dictionary<string, string>
        {
            ["grant_type"] = "urn:ietf:params:oauth:grant-type:uma-ticket",
            ["ticket"] = ticket,
            ["audience"] = this.options.Resource ?? string.Empty,
            ["submit_request"] = "true",
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
        request.Content = new FormUrlEncodedContent(formData);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await this.httpClient.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return false;
        }

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(
                cancellationToken
            );
            if (errorBody?.ErrorDescription?.Contains("request_submitted") == true)
            {
                this.logger.LogPermissionRequestSubmitted();
                return true;
            }
        }

        this.logger.LogPermissionRequestFailed(response.StatusCode);
        return false;
    }

    private sealed class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = default!;
    }

    private sealed class ErrorResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("error_description")]
        public string? ErrorDescription { get; set; }
    }
}

internal static partial class UmaTicketExchangeLoggerExtensions
{
    [LoggerMessage(200, LogLevel.Information, "RPT obtained via UMA ticket exchange")]
    public static partial void LogRptObtained(this ILogger logger);

    [LoggerMessage(201, LogLevel.Warning, "RPT exchange failed with status {StatusCode}")]
    public static partial void LogRptExchangeFailed(
        this ILogger logger,
        System.Net.HttpStatusCode statusCode
    );

    [LoggerMessage(202, LogLevel.Information, "Permission request submitted for owner approval")]
    public static partial void LogPermissionRequestSubmitted(this ILogger logger);

    [LoggerMessage(
        203,
        LogLevel.Warning,
        "Permission request submission failed with status {StatusCode}"
    )]
    public static partial void LogPermissionRequestFailed(
        this ILogger logger,
        System.Net.HttpStatusCode statusCode
    );
}
