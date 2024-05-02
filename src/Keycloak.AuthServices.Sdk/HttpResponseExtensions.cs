namespace Keycloak.AuthServices.Sdk;

using System.Net.Http.Json;

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
    public static async Task<T?> GetAsync<T>(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default
    )
    {
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<T>(
            cancellationToken: cancellationToken
        );

        return result;
    }
}
