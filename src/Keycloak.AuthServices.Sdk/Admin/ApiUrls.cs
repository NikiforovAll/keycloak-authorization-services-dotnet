namespace Keycloak.AuthServices.Sdk.Admin;

/// <summary>
/// Keycloak API endpoints
/// </summary>
internal static class ApiUrls
{
    private const string AdminApiBase = "/admin";

    private const string RealmParam = "{realm}";

    private const string Realms = "realms";

    internal const string GetRealm = $"{AdminApiBase}/{Realms}/{RealmParam}";

    #region User API

    internal const string GetUsers = $"{GetRealm}/users";

    internal const string GetUser = $"{GetRealm}/users/{{id}}";

    internal const string CreateUser = $"{GetRealm}/users";

    internal const string UpdateUser = $"{GetRealm}/users/{{id}}";

    internal const string DeleteUser = $"{GetRealm}/users/{{id}}";

    internal const string SendVerifyEmail = $"{GetRealm}/users/{{id}}/send-verify-email";

    internal const string ExecuteActionsEmail = $"{GetRealm}/users/{{id}}/execute-actions-email";
    internal const string GetUserGroups = $"{GetRealm}/users/{{id}}/groups";

    internal const string JoinGroup = $"{GetRealm}/users/{{id}}/groups/{{groupId}}";

    internal const string LeaveGroup = $"{GetRealm}/users/{{id}}/groups/{{groupId}}";
    #endregion

    #region Group API

    internal const string GetGroups = $"{GetRealm}/groups";

    internal const string CreateGroup = $"{GetRealm}/groups";

    internal const string GetGroup = $"{GetRealm}/groups/{{id}}";

    internal const string UpdateGroup = $"{GetRealm}/groups/{{id}}";

    internal const string DeleteGroup = $"{GetRealm}/groups";

    internal const string CreateChildGroup = $"{GetRealm}/groups/{{id}}/children";

    #endregion

    public static string WithRealm(this string path, string realm) =>
        path.Replace(RealmParam, realm);
}
