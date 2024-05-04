namespace Keycloak.AuthServices.Sdk.Protection.Requests;

/// <summary>
/// Optional parameters for the <see cref="IKeycloakProtectedResourceClient.GetResourcesIdsAsync"/> endpoint.
/// </summary>
public class GetResourcesRequestParameters
{
    /// <summary>
    /// Query resources given a name or pattern.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// By default, the <see cref="Name"/> filter will match any resource with the given pattern.
    /// Set this to true to restrict the query to only return resources with an exact match.
    /// </summary>
    /// <remarks>
    ///     https://github.com/keycloak/keycloak-documentation/blob/main/authorization_services/topics/service-protection-resources-api-papi.adoc#querying-resources
    ///     https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_resources_api
    /// </remarks>
    public bool? ExactName { get; set; }

    /// <summary>
    /// Query resources that match a URI pattern.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Query resources given an owner.
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// Query resources of a specific type.
    /// </summary>
    public string? ResourceType { get; set; }

    /// <summary>
    /// Query resources with a specific scope.
    /// </summary>
    public string? Scope { get; set; }
}
