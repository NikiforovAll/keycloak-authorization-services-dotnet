namespace Keycloak.AuthServices.Sdk;

using System.Net.Http.Json;
using System.Text.Json;
using Keycloak.AuthServices.Sdk.Admin.Models;

/// <summary>
/// TBD:
/// </summary>
public static class HttpResponseExtensions
{
    /// <summary>
    /// TBD:
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<T?> GetResponseAsync<T>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default
    )
    {
        await response.EnsureResponseAsync(cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<T>(
            cancellationToken: cancellationToken
        );

        return result;
    }

    /// <summary>
    /// TBD:
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task EnsureResponseAsync(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            var error = JsonSerializer.Deserialize<ErrorResponse>(body);

            throw new KeycloakHttpClientException(
                message: $"Unable to submit the request - '{error?.Error}'",
                statusCode: (int)response.StatusCode,
                httpResponse: body,
                response: error!,
                innerException: exception
            );
        }
    }
}
