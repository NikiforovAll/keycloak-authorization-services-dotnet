namespace Keycloak.AuthServices.Sdk.Protection;

using System.Globalization;
using System.Net.Http.Json;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;
using Keycloak.AuthServices.Sdk.Utils;

/// <summary>
/// Represents a client for interacting with Keycloak Protection API services.
/// </summary>
public partial class KeycloakProtectionClient : IKeycloakProtectionClient
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakProtectionClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used for making requests.</param>
    public KeycloakProtectionClient(HttpClient httpClient) => this.httpClient = httpClient;

    #region IKeycloakProtectedResourceClient
    ///<inheritdoc/>
    public async Task<HttpResponseMessage> CreateResourceWithResponseAsync(
        string realm,
        Resource resource,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.CreateResource.WithRealm(realm);

        var responseMessage = await this.httpClient.PostAsJsonAsync(
            path,
            resource,
            cancellationToken
        );

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> DeleteResourceWithResponseAsync(
        string realm,
        string resourceId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.DeleteResource.WithRealm(realm).Replace("{id}", resourceId);

        var responseMessage = await this.httpClient.DeleteAsync(path, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public Task<HttpResponseMessage> GetResourcesIdsWithResponseAsync(
        string realm,
        GetResourcesRequestParameters? parameters = null,
        CancellationToken cancellationToken = default
    ) => this.GetResourcesWithResponseCoreAsync(realm, false, parameters, cancellationToken);

    ///<inheritdoc/>
    public Task<HttpResponseMessage> GetResourcesWithResponseAsync(
        string realm,
        GetResourcesRequestParameters? parameters = null,
        CancellationToken cancellationToken = default
    ) => this.GetResourcesWithResponseCoreAsync(realm, true, parameters, cancellationToken);

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> GetResourceWithResponseAsync(
        string realm,
        string resourceId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetResource.WithRealm(realm).Replace("{id}", resourceId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> UpdateResourceWithResponseAsync(
        string realm,
        string resourceId,
        Resource resource,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.UpdateResource.WithRealm(realm).Replace("{id}", resourceId);

        var responseMessage = await this.httpClient.PutAsJsonAsync(
            path,
            resource,
            cancellationToken
        );

        return responseMessage!;
    }

    private async Task<HttpResponseMessage> GetResourcesWithResponseCoreAsync(
        string realm,
        bool deep,
        GetResourcesRequestParameters? parameters = null,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetResources.WithRealm(realm);

        var queryBuilder = new QueryBuilder { { "deep", deep.ToString() } };

        if (parameters != null)
        {
            if (parameters.Name is not null)
            {
                queryBuilder.Add("name", parameters.Name);
            }

            if (parameters.ExactName.HasValue)
            {
                queryBuilder.Add("exactName", parameters.ExactName?.ToString()!);
            }

            if (parameters.Uri is not null)
            {
                queryBuilder.Add("uri", parameters.Uri);
            }

            if (parameters.Owner is not null)
            {
                queryBuilder.Add("owner", parameters.Owner);
            }

            if (parameters.ResourceType is not null)
            {
                queryBuilder.Add("type", parameters.ResourceType);
            }

            if (parameters.Scope is not null)
            {
                queryBuilder.Add("scope", parameters.Scope);
            }
        }

        var url = path + queryBuilder.ToQueryString();

        var responseMessage = await this.httpClient.GetAsync(url, cancellationToken);

        return responseMessage!;
    }
    #endregion

    #region IKeycloakPolicyClient

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> CreatePolicyWithResponseAsync(
        string realm,
        string resourceId,
        Policy policy,
        CancellationToken cancellationToken
    )
    {
        var path = ApiUrls.CreatePolicy.WithRealm(realm).Replace("{resourceId}", resourceId);

        var responseMessage = await this.httpClient.PostAsJsonAsync(
            path,
            policy,
            cancellationToken
        );

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> DeletePolicyWithResponseAsync(
        string realm,
        string policyId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.DeletePolicy.WithRealm(realm).Replace("{id}", policyId);

        var responseMessage = await this.httpClient.DeleteAsync(path, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> GetPoliciesWithResponseAsync(
        string realm,
        GetPoliciesRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetPolicies.WithRealm(realm);

        var queryBuilder = new QueryBuilder();

        parameters ??= new();
        if (parameters.First.HasValue)
        {
            queryBuilder.Add("first", parameters.First?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Max.HasValue)
        {
            queryBuilder.Add("max", parameters.Max?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (!string.IsNullOrEmpty(parameters.PermissionName))
        {
            queryBuilder.Add("name", parameters.PermissionName);
        }

        if (!string.IsNullOrEmpty(parameters.ResourceId))
        {
            queryBuilder.Add("resource", parameters.ResourceId);
        }

        if (!string.IsNullOrEmpty(parameters.Scope))
        {
            queryBuilder.Add("scope", parameters.Scope);
        }

        var url = path + queryBuilder.ToQueryString();

        var responseMessage = await this.httpClient.GetAsync(url, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> GetPolicyWithResponseAsync(
        string realm,
        string policyId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetPolicy.WithRealm(realm).Replace("{id}", policyId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> UpdatePolicyWithResponseAsync(
        string realm,
        string policyId,
        Policy policy,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.UpdatePolicy.WithRealm(realm).Replace("{id}", policyId);

        var responseMessage = await this.httpClient.PutAsJsonAsync(path, policy, cancellationToken);

        return responseMessage!;
    }

    #endregion
}
