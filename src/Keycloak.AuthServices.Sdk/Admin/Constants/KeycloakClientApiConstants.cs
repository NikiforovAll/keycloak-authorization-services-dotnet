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

    internal const string GetResourceByExactName = "/realms/{realm}/authz/protection/resource_set?&exactName=true";

    #endregion

    #region User API

    internal const string GetUsers = $"{GetRealm}/users";

    internal const string GetUser = $"{GetRealm}/users/{{id}}";

    internal const string CreateUser = $"{GetRealm}/users";

    internal const string UpdateUser = $"{GetRealm}/users/{{id}}";

    internal const string SendVerifyEmail = $"{GetRealm}/users/{{id}}/send-verify-email";

    internal const string ExecuteActionsEmail = $"{GetRealm}/users/{{id}}/execute-actions-email";

    internal const string UserGroupUpdate = $"{UpdateUser}/groups/{{group_id}}";

    #endregion

    #region Group API

    internal const string GetGroups = $"{GetRealm}/groups";

    internal const string GetGroup = $"{GetRealm}/groups/{{id}}";

    internal const string CreateGroup = $"{GetGroups}";

    internal const string UpdateGroup = $"{GetGroup}";

    internal const string DeleteGroup = $"{GetGroup}";

    internal const string CreateChildGroup = $"{GetGroup}/children";

    #endregion

    #region Permission Ticket API

    internal const string GetPermissionTickets = "/realms/{realm}/authz/protection/permission/ticket";

    internal const string CreatePermissionTicket = $"{GetPermissionTickets}";

    internal const string UpdatePermissionTicket = $"{GetPermissionTickets}";

    internal const string DeletePermissionTicket = $"{GetPermissionTickets}/{{id}}";

    #endregion

    #region Policy API

    internal const string GetPolicies = "/realms/{realm}/authz/protection/uma-policy";

    internal const string GetPolicy = $"{GetPolicies}/{{id}}";

    internal const string CreatePolicy = $"{GetPolicy}";

    internal const string UpdatePolicy = $"{GetPolicy}";

    internal const string DeletePolicy = $"{GetPolicy}";

    #endregion
}
