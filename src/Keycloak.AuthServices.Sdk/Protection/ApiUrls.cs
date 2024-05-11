namespace Keycloak.AuthServices.Sdk.Protection;

/// <summary>
/// Keycloak Protection API
/// </summary>
internal static class ApiUrls
{
    #region ProtectedResource
    internal const string GetResources = "/realms/{realm}/authz/protection/resource_set";

    internal const string GetResource = $"{GetResources}/{{id}}";

    internal const string CreateResource = GetResources;

    internal const string UpdateResource = $"{GetResources}/{{id}}";

    internal const string DeleteResource = $"{GetResources}/{{id}}";
    #endregion

    #region Policy
    internal const string GetPolicies = "/realms/{realm}/authz/protection/uma-policy";

    internal const string GetPolicy = $"{GetPolicies}/{{id}}";

    internal const string CreatePolicy = $"{GetPolicies}/{{resourceId}}";

    internal const string UpdatePolicy = $"{GetPolicies}/{{id}}";

    internal const string DeletePolicy = $"{GetPolicies}/{{id}}";
    #endregion
    public static string WithRealm(this string path, string realm) =>
        path.Replace("{realm}", realm);
}
