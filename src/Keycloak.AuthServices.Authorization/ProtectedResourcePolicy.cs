namespace Keycloak.AuthServices.Authorization;

public static class ProtectedResourcePolicy
{
    public static string From(string resource, string id, string scope) =>
        $"{resource}/{id}#{scope}";

    public static string From(string resource, string scope) =>
        $"{resource}#{scope}";

    public static bool Match(string policy) => policy.Contains('#');
}
