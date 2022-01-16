namespace Keycloak.AuthServices.Authorization;

using Requirements;

public static class ProtectedResourcePolicy
{
    public static string From(string resource, string id, string scope) =>
        new DecisionRequirement(resource, id, scope).ToString();

    public static string From(string resource, string scope) =>
        new DecisionRequirement(resource, scope).ToString();

    public static bool Match(string policy) => policy.Contains('#');
}
