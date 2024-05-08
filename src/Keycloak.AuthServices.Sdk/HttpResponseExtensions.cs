namespace Keycloak.AuthServices.Sdk;

using System.Net.Http.Json;
using System.Text.Json;

/// <summary>
/// Provides extension methods for handling HTTP responses.
/// </summary>
public static class HttpResponseExtensions
{
    /// <summary>
    /// Reads the HTTP response content as JSON and deserializes it into the specified type.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
    /// <param name="response">The HTTP response message.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized response content.</returns>
    public static async Task<T?> GetResponseAsync<T>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(response);

        await response.EnsureResponseAsync(cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<T>(
            cancellationToken: cancellationToken
        );

        return result;
    }

    /// <summary>
    /// Ensures that the HTTP response is successful, otherwise throws an exception.
    /// </summary>
    /// <param name="response">The HTTP response message.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="KeycloakHttpClientException">Thrown when the response is not successful.</exception>
    public static async Task EnsureResponseAsync(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(response);

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
