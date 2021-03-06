namespace Keycloak.AuthServices.Sdk.Admin.Constants;

/// <summary>
/// Keycloak API endpoints
/// </summary>
internal static class KeycloakClientApiConstants
{
    private const string AdminApiBase = "/admin";

    private const string Realms = "realms";

    internal const string GetRealm = $"{AdminApiBase}/{Realms}/{{realm}}";


    #region Resource API

    internal const string GetResources = "/realms/{realm}/authz/protection/resource_set";

    internal const string GetResource = $"{GetResources}/{{id}}";

    internal const string CreateResource = $"{GetResources}";

    internal const string PutResource = $"{GetResource}";

    internal const string GetResourceByExactName = "/realms/{realm}/authz/protection/resource_set?&exactName=true";

    #endregion
}
