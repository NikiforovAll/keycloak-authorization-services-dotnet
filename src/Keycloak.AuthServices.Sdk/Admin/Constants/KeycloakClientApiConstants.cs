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

    internal const string DeleteResource = $"{GetResource}";

    internal const string GetResourceByExactName = $"{GetResources}?&exactName=true";

    #endregion

    #region User API

    internal const string GetUsers = $"{GetRealm}/users";

    internal const string GetUser = $"{GetUsers}/{{id}}";

    internal const string CreateUser = $"{GetUsers}";

    internal const string UpdateUser = $"{GetUser}";

    internal const string SendVerifyEmail = $"{GetUser}/send-verify-email";

    internal const string ExecuteActionsEmail = $"{GetUser}/execute-actions-email";

    internal const string GetUserGroups = $"{GetUser}/groups";

    internal const string UserGroupUpdate = $"{GetUserGroups}/{{group_id}}";

    #endregion

    #region Group API

    internal const string GetGroups = $"{GetRealm}/groups";

    internal const string GetGroup = $"{GetGroups}/{{id}}";

    internal const string CreateGroup = $"{GetGroups}";

    internal const string UpdateGroup = $"{GetGroup}";

    internal const string DeleteGroup = $"{GetGroup}";

    internal const string CreateChildGroup = $"{GetGroup}/children";

    #endregion

    #region Policy API

    internal const string GetPolicies = "/realms/{realm}/authz/protection/uma-policy";

    internal const string GetPolicy = $"{GetPolicies}/{{id}}";

    internal const string CreatePolicy = $"{GetPolicy}";

    internal const string UpdatePolicy = $"{GetPolicy}";

    internal const string DeletePolicy = $"{GetPolicy}";

    #endregion
}
