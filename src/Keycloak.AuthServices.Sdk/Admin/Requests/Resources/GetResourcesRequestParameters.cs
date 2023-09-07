namespace Keycloak.AuthServices.Sdk.Admin.Requests.Resources;

using Refit;

/// <summary>
/// Optional parameters for the <see cref="IKeycloakProtectedResourceClient.GetResources"/> endpoint.
/// </summary>
public class GetResourcesRequestParameters
{
    /// <summary>
    /// Query resources given a name or pattern.
    /// </summary>
    [AliasAs("name")] 
    public string? Name { get; set; }

    /// <summary>
    /// By default, the <see cref="Name"/> filter will match any resource with the given pattern.
    /// Set this to true to restrict the query to only return resources with an exact match.
    /// </summary>
    /// <remarks>
    ///     https://github.com/keycloak/keycloak-documentation/blob/main/authorization_services/topics/service-protection-resources-api-papi.adoc#querying-resources
    ///     https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_resources_api
    /// </remarks>
    [AliasAs("exactName")]
    public bool? ExactName { get; set; }

    /// <summary>
    /// Query resources that match a URI pattern.
    /// </summary>
    [AliasAs("uri")]
    public string? Uri { get; set; }
    
    /// <summary>
    /// Query resources given an owner.
    /// </summary>
    [AliasAs("owner")]
    public string? Owner { get; set; }

    /// <summary>
    /// Query resources of a specific type.
    /// </summary>
    [AliasAs("type")]
    public string? ResourceType { get; set; }

    /// <summary>
    /// Query resources with a specific scope.
    /// </summary>
    [AliasAs("scope")]
    public string? Scope { get; set; }

    /// <summary>
    /// When set to true, the response will include all available data for the resource.
    /// </summary>
    [AliasAs("deep")]
    public bool? Deep { get; set; }
}
