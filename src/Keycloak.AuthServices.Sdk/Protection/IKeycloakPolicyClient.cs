namespace Keycloak.AuthServices.Sdk.Protection;

using Keycloak.AuthServices.Sdk.Protection.Models;
using Keycloak.AuthServices.Sdk.Protection.Requests;

/// <summary>
/// Must be used by the owner of the resource for whom the policy is being created.
/// </summary>
/// <remarks>
/// https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_authorization_uma_policy_api
/// </remarks>
public interface IKeycloakPolicyClient
{
    /// <summary>
    /// Gets all Policies
    /// </summary>
    /// <remarks>
    /// https://github.com/keycloak/keycloak/blob/main/docs/documentation/authorization_services/topics/service-protection-policy-api.adoc#querying-permission
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request</returns>
    Task<HttpResponseMessage> GetPoliciesWithResponseAsync(
        string realm,
        GetPoliciesRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets all Policies
    /// </summary>
    /// <remarks>
    /// https://github.com/keycloak/keycloak/blob/main/docs/documentation/authorization_services/topics/service-protection-policy-api.adoc#querying-permission
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of policy representations <see cref="Policy"/></returns>
    async Task<IEnumerable<Policy>> GetPoliciesAsync(
        string realm,
        GetPoliciesRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetPoliciesWithResponseAsync(
            realm,
            parameters,
            cancellationToken
        );

        return await response.GetResponseAsync<IEnumerable<Policy>>(cancellationToken)
            ?? Enumerable.Empty<Policy>();
    }

    /// <summary>
    /// Gets a Policy
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request</returns>
    Task<HttpResponseMessage> GetPolicyWithResponseAsync(
        string realm,
        string policyId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get representation of a Policy
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The policy representation <see cref="Policy"/></returns>
    async Task<Policy> GetPolicyAsync(
        string realm,
        string policyId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.GetPolicyWithResponseAsync(realm, policyId, cancellationToken);

        return await response.GetResponseAsync<Policy>(cancellationToken) ?? new();
    }

    /// <summary>
    /// Creates a policy
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">The resource ID to create the policy for.</param>
    /// <param name="policy">Policy representation</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The HttpResponseMessage of the request</returns>
    Task<HttpResponseMessage> CreatePolicyWithResponseAsync(
        string realm,
        string resourceId,
        Policy policy,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Creates a policy
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">The resource ID to create the policy for.</param>
    /// <param name="policy">Policy representation</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The created policy representation <see cref="Policy"/></returns>
    async Task CreatePolicyAsync(
        string realm,
        string resourceId,
        Policy policy,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.CreatePolicyWithResponseAsync(
            realm,
            resourceId,
            policy,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Updates a policy
    /// </summary>
    /// <remarks>
    ///  https://github.com/keycloak/keycloak/blob/main/docs/documentation/authorization_services/topics/service-protection-policy-api.adoc#managing-resource-permissions-using-the-policy-api
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID.</param>
    /// <param name="policy">Policy object.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> UpdatePolicyWithResponseAsync(
        string realm,
        string policyId,
        Policy policy,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates a policy
    /// </summary>
    /// <remarks>
    ///  https://github.com/keycloak/keycloak/blob/main/docs/documentation/authorization_services/topics/service-protection-policy-api.adoc#managing-resource-permissions-using-the-policy-api
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID.</param>
    /// <param name="policy">Policy object.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task UpdatePolicyAsync(
        string realm,
        string policyId,
        Policy policy,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.UpdatePolicyWithResponseAsync(
            realm,
            policyId,
            policy,
            cancellationToken
        );

        await response.EnsureResponseAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a policy
    /// </summary>
    /// <remarks>
    ///  https://github.com/keycloak/keycloak/blob/main/docs/documentation/authorization_services/topics/service-protection-policy-api.adoc#removing-a-permission
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> DeletePolicyWithResponseAsync(
        string realm,
        string policyId,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a policy
    /// </summary>
    /// <remarks>
    ///  https://github.com/keycloak/keycloak/blob/main/docs/documentation/authorization_services/topics/service-protection-policy-api.adoc#removing-a-permission
    /// </remarks>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async Task DeletePolicyAsync(
        string realm,
        string policyId,
        CancellationToken cancellationToken = default
    )
    {
        var response = await this.DeletePolicyWithResponseAsync(realm, policyId, cancellationToken);

        await response.EnsureResponseAsync(cancellationToken);
    }
}
