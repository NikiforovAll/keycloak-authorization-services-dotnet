namespace Keycloak.AuthServices.Sdk.Protection;

/// <summary>
/// Keycloak Protection API
/// </summary>
internal static class ApiUrls
{
    internal const string GetResources = "/realms/{realm}/authz/protection/resource_set";

    internal const string GetResource = $"{GetResources}/{{id}}";

    internal const string CreateResource = $"{GetResources}";

    internal const string UpdateResource = $"{GetResource}";

    internal const string DeleteResource = $"{GetResource}";

    public static string WithRealm(this string path, string realm) =>
        path.Replace("{realm}", realm);
}
