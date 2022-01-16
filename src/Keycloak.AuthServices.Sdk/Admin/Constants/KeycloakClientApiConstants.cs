namespace Keycloak.AuthServices.Sdk.Admin.Constants;

public static class KeycloakClientApiConstants
{
    private const string AdminApiBase = "/admin";

    private const string Realms = "realms";

    public const string GetRealm = $"{AdminApiBase}/{Realms}/{{realm}}";


    #region Resource API

    public const string GetResources = "/realms/{realm}/authz/protection/resource_set";

    public const string GetResource = $"{GetResources}/{{id}}";

    public const string CreateResource = $"{GetResources}";

    #endregion

}
