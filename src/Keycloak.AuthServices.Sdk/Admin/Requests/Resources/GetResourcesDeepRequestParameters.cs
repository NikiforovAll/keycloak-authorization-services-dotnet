namespace Keycloak.AuthServices.Sdk.Admin.Requests.Resources;

/// <inheritdoc />
public class GetResourcesDeepRequestParameters : GetResourcesRequestParameters
{
    /// <inheritdoc />
    public GetResourcesDeepRequestParameters()
    {
        // TODO: This whole mechanism should be done differently but the Keycloak API 
        // TODO: returns different data structures from the same API endpoint depending
        // TODO: on the request parameters.

        Deep = true;
    }
}