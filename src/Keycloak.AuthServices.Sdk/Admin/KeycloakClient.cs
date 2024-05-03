namespace Keycloak.AuthServices.Sdk.Admin;

using System.Net.Http.Json;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Users;
using Keycloak.AuthServices.Sdk.Utils;

/// <summary>
/// TBD:
/// </summary>
public partial class KeycloakClient : IKeycloakClient
{
    private const string RealmParam = "{realm}";
    private readonly HttpClient httpClient;

    /// <summary>
    /// TBD:
    /// </summary>
    /// <param name="httpClient"></param>
    public KeycloakClient(HttpClient httpClient) => this.httpClient = httpClient;

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> CreateUserWithResponseAsync(
        string realm,
        UserRepresentation user,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.CreateUser.Replace(RealmParam, realm);

        var responseMessage = await this.httpClient.PostAsJsonAsync(path, user, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetRealmWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetRealm.Replace(RealmParam, realm);

        var responseMessage = await this.httpClient.GetAsync(
            path,
            cancellationToken: cancellationToken
        );

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetUsersWithResponseAsync(
        string realm,
        GetUsersRequestParameters? parameters = null,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetUsers.Replace(RealmParam, realm);

        var query = string.Empty;

        if (parameters is not null)
        {
#pragma warning disable CA1305 // Specify IFormatProvider
            var queryParameters = new List<KeyValuePair<string, string?>>()
            {
                new("briefRepresentation", parameters.BriefRepresentation?.ToString()),
                new("email", parameters.Email),
                new("emailVerified", parameters.EmailVerified?.ToString()),
                new("enabled", parameters.Enabled?.ToString()),
                new("exact", parameters.Exact?.ToString()),
                new("first", parameters.First?.ToString()),
                new("firstName", parameters.FirstName),
                new("idpAlias", parameters.IdpAlias),
                new("idpUserId", parameters.IdpUserId),
                new("lastName", parameters.LastName),
                new("max", parameters.Max?.ToString()),
                new("q", parameters.Query),
                new("search", parameters.Search),
                new("username", parameters.Username)
            };
#pragma warning restore CA1305 // Specify IFormatProvider

            query = new QueryBuilder(queryParameters.Where(q => q.Value is not null)!)
                .ToQueryString()
                .ToString();
        }

        var responseMessage = await this.httpClient.GetAsync(path + query, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetUserWithResponseAsync(
        string realm,
        string userId,
        bool includeUserProfileMetadata = false,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetUser.Replace(RealmParam, realm).Replace("{id}", userId);

        var query = includeUserProfileMetadata
            ? new QueryBuilder()
                .Add("includeUserProfileMetadata", includeUserProfileMetadata.ToString())
                .ToQueryString()
                .ToString()
            : string.Empty;

        var responseMessage = await this.httpClient.GetAsync(path + query, cancellationToken);

        return responseMessage!;
    }
}
