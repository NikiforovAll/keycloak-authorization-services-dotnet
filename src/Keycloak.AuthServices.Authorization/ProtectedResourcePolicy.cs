namespace Keycloak.AuthServices.Authorization;

/// <summary>
/// </summary>
public static class ProtectedResourcePolicy
{
    /// <summary>
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="id"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public static string From(string resource, string id, string scope) =>
        $"{resource}/{id}#{scope}";

    /// <summary>
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public static string From(string resource, string scope) =>
        $"{resource}#{scope}";

    /// <summary>
    /// </summary>
    /// <param name="policy"></param>
    /// <returns></returns>
    public static bool Match(string policy) => policy.Contains('#');
}
