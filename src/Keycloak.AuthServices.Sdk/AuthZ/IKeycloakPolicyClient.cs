namespace Keycloak.AuthServices.Sdk.AuthZ;

using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Constants;
using Admin.Models.Policies;
using Admin.Requests.Policy;
using Refit;

/// <summary>
/// Must be used by the owner of the resource for whom the policy is being created.
/// </summary>
public interface IKeycloakPolicyClient
{
    // Note that this is a representation of endpoints that are in a different documentation zone (i.e.: not the REST API docs directly):
    // https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_authorization_uma_policy_api

    /// <summary>
    /// Get a stream of policies
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="parameters">Optional query parameters</param>
    /// <returns>A stream of policies, filtered according to the query parameters.</returns>
    [Get(KeycloakClientApiConstants.GetPolicies)]
    [Headers("Accept: application/json")]
    Task<IEnumerable<Policy>> GetPolicies(string realm, [Query] GetPoliciesRequestParameters? parameters = default);

    /// <summary>
    /// Get representation of a Policy
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID</param>
    /// <returns>The policy representation <see cref="Policy"/></returns>
    [Get(KeycloakClientApiConstants.GetPolicy)]
    [Headers("Accept: application/json")]
    Task<Policy> GetPolicy(string realm, [AliasAs("id")] string policyId);

    /// <summary>
    /// Create a policy
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="resourceId">The resource ID to create the policy for.</param>
    /// <param name="policy">Policy representation</param>
    /// <returns></returns>
    [Post(KeycloakClientApiConstants.CreatePolicy)]
    [Headers("Content-Type: application/json")]
    Task<HttpResponseMessage> CreatePolicy(string realm, [AliasAs("id")] string resourceId, [Body] Policy policy);

    /// <summary>
    /// Update a policy
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID.</param>
    /// <param name="policy">Policy representation</param>
    /// <returns></returns>
    [Put(KeycloakClientApiConstants.UpdatePolicy)]
    [Headers("Content-Type: application/json")]
    Task UpdatePolicy(string realm, [AliasAs("id")] string policyId, [Body] Policy policy);
    
    /// <summary>
    /// Delete a policy.
    /// </summary>
    /// <param name="realm">Realm name (not ID).</param>
    /// <param name="policyId">Policy ID.</param> 
    /// <returns></returns>
    [Delete(KeycloakClientApiConstants.DeletePolicy)]
    Task DeletePolicy(string realm, [AliasAs("id")] string policyId);
}
