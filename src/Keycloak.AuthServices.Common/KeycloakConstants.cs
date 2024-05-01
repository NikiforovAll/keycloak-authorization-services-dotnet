﻿namespace Keycloak.AuthServices.Common;

/// <summary>
/// Well-known constants
/// </summary>
public static class KeycloakConstants
{
    /// <summary>
    /// Token endpoint
    /// </summary>
    public const string TokenEndpointPath = "protocol/openid-connect/token";

    /// <summary>
    /// Token Claim - Resource Access
    /// </summary>
    public const string ResourceAccessClaimType = "resource_access";

    /// <summary>
    /// Token Claim - Realm Access
    /// </summary>
    public const string RealmAccessClaimType = "realm_access";

    /// <summary>
    /// Role Claim
    /// </summary>
    public const string RoleClaimType = "role";

    /// <summary>
    /// Name Claim
    /// </summary>
    public const string NameClaimType = "preferred_username";
}
