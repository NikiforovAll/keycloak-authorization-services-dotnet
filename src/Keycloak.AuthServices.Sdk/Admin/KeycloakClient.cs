namespace Keycloak.AuthServices.Sdk.Admin;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Keycloak.AuthServices.Sdk.Admin.Requests.Organizations;
using Keycloak.AuthServices.Sdk.Admin.Requests.Users;
using Keycloak.AuthServices.Sdk.Utils;

/// <summary>
/// Represents a client for interacting with the Keycloak Admin API.
/// </summary>
public class KeycloakClient : IKeycloakClient
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakClient"/> class.
    /// </summary>
    /// <param name="httpClient"></param>
    public KeycloakClient(HttpClient httpClient) => this.httpClient = httpClient;

    #region RealmRegion
    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetRealmWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetRealm.WithRealm(realm);

        var responseMessage = await this.httpClient.GetAsync(
            path,
            cancellationToken: cancellationToken
        );

        return responseMessage!;
    }
    #endregion

    #region UserRegion
    /// <inheritdoc/>
    public async Task<HttpResponseMessage> CreateUserWithResponseAsync(
        string realm,
        UserRepresentation user,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.CreateUser.WithRealm(realm);

        var responseMessage = await this.httpClient.PostAsJsonAsync(path, user, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetUsersWithResponseAsync(
        string realm,
        GetUsersRequestParameters? parameters = null,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetUsers.WithRealm(realm);

        var query = string.Empty;

        if (parameters is not null)
        {
#pragma warning disable CA1305 // Specify IFormatProvider
            var queryParameters = new List<KeyValuePair<string, string?>>()
            {
                new("briefRepresentation", parameters.BriefRepresentation?.ToString()),
                new("email", parameters.Email),
                new("emailVerified", parameters.EmailVerified?.ToString()),
                new("enabled", parameters.Enabled?.ToString()),
                new("exact", parameters.Exact?.ToString()),
                new("first", parameters.First?.ToString()),
                new("firstName", parameters.FirstName),
                new("idpAlias", parameters.IdpAlias),
                new("idpUserId", parameters.IdpUserId),
                new("lastName", parameters.LastName),
                new("max", parameters.Max?.ToString()),
                new("q", parameters.Query),
                new("search", parameters.Search),
                new("username", parameters.Username),
            };
#pragma warning restore CA1305 // Specify IFormatProvider

            query = new QueryBuilder(queryParameters.Where(q => q.Value is not null)!)
                .ToQueryString()
                .ToString();
        }

        var responseMessage = await this.httpClient.GetAsync(path + query, cancellationToken);

        return responseMessage;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetUserCountWithResponseAsync(
        string realm,
        GetUserCountRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetUserCount.WithRealm(realm);

        var query = string.Empty;

        if (parameters is not null)
        {
            var queryParameters = new List<KeyValuePair<string, string?>>()
            {
                new("email", parameters.Email),
                new("emailVerified", parameters.EmailVerified?.ToString()),
                new("enabled", parameters.Enabled?.ToString()),
                new("firstName", parameters.FirstName),
                new("lastName", parameters.LastName),
                new("q", parameters.Query),
                new("search", parameters.Search),
                new("username", parameters.Username),
            };

            query = new QueryBuilder(queryParameters.Where(q => q.Value is not null)!)
                .ToQueryString()
                .ToString();
        }

        var responseMessage = await this.httpClient.GetAsync(path + query, cancellationToken);

        return responseMessage;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetUserWithResponseAsync(
        string realm,
        string userId,
        bool includeUserProfileMetadata = false,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetUser.WithRealm(realm).Replace("{id}", userId);

        var query = includeUserProfileMetadata
            ? new QueryBuilder()
                .Add("includeUserProfileMetadata", includeUserProfileMetadata.ToString())
                .ToQueryString()
                .ToString()
            : string.Empty;

        var responseMessage = await this.httpClient.GetAsync(path + query, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> UpdateUserWithResponseAsync(
        string realm,
        string userId,
        UserRepresentation user,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.UpdateUser.WithRealm(realm).Replace("{id}", userId);

        var responseMessage = await this.httpClient.PutAsJsonAsync(path, user, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> DeleteUserWithResponseAsync(
        string realm,
        string userId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.DeleteUser.WithRealm(realm).Replace("{id}", userId);

        var responseMessage = await this.httpClient.DeleteAsync(path, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> SendVerifyEmailWithResponseAsync(
        string realm,
        string userId,
        string? clientId = null,
        string? redirectUri = null,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.SendVerifyEmail.WithRealm(realm).Replace("{id}", userId);

        var queryBuilder = new QueryBuilder();

        if (clientId is not null)
        {
            queryBuilder.Add("client_id", clientId);
        }

        if (redirectUri is not null)
        {
            queryBuilder.Add("redirect_uri", redirectUri);
        }

        var url = path + queryBuilder.ToQueryString();

        using var content = new StringContent(string.Empty);
        var responseMessage = await this.httpClient.PutAsync(url, content, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> ExecuteActionsEmailWithResponseAsync(
        string realm,
        string userId,
        ExecuteActionsEmailRequest request,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var path = ApiUrls.ExecuteActionsEmail.WithRealm(realm).Replace("{id}", userId);

        var queryBuilder = new QueryBuilder();

        if (request.ClientId is not null)
        {
            queryBuilder.Add("client_id", request.ClientId);
        }

        if (request.RedirectUri is not null)
        {
            queryBuilder.Add("redirect_uri", request.RedirectUri);
        }

        if (request.Lifespan.HasValue)
        {
            queryBuilder.Add("lifespan", request.Lifespan?.ToString(CultureInfo.InvariantCulture)!);
        }

        var url = path + queryBuilder.ToQueryString();

        var responseMessage = await this.httpClient.PutAsJsonAsync(
            url,
            request.Actions,
            cancellationToken
        );

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> GetUserGroupsWithResponseAsync(
        string realm,
        string userId,
        GetUserGroupsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetUserGroups.Replace("{realm}", realm).Replace("{id}", userId);

        var queryBuilder = new QueryBuilder();

        parameters ??= new();
        if (parameters.BriefRepresentation.HasValue)
        {
            queryBuilder.Add("briefRepresentation", parameters.BriefRepresentation?.ToString()!);
        }

        if (parameters.First.HasValue)
        {
            queryBuilder.Add("first", parameters.First?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Max.HasValue)
        {
            queryBuilder.Add("max", parameters.Max?.ToString(CultureInfo.InvariantCulture)!);
        }

        var url = path + queryBuilder.ToQueryString();

        var responseMessage = await this.httpClient.GetAsync(url, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> JoinGroupWithResponseAsync(
        string realm,
        string userId,
        string groupId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls
            .JoinGroup.WithRealm(realm)
            .Replace("{id}", userId)
            .Replace("{groupId}", groupId);

        using var content = new StringContent(string.Empty);
        var responseMessage = await this.httpClient.PutAsync(path, content, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> LeaveGroupWithResponseAsync(
        string realm,
        string userId,
        string groupId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls
            .LeaveGroup.WithRealm(realm)
            .Replace("{id}", userId)
            .Replace("{groupId}", groupId);

        var responseMessage = await this.httpClient.DeleteAsync(path, cancellationToken);

        return responseMessage!;
    }
    #endregion

    #region GroupRegion

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> GetGroupsWithResponseAsync(
        string realm,
        GetGroupsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetGroups.WithRealm(realm);

        var queryBuilder = new QueryBuilder();

        parameters ??= new();
        if (parameters.BriefRepresentation.HasValue)
        {
            queryBuilder.Add(
                "briefRepresentation",
                parameters.BriefRepresentation?.ToString(CultureInfo.InvariantCulture)!
            );
        }

        if (parameters.First.HasValue)
        {
            queryBuilder.Add("first", parameters.First?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Exact.HasValue)
        {
            queryBuilder.Add("exact", parameters.Exact?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Max.HasValue)
        {
            queryBuilder.Add("max", parameters.Max?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Search is not null)
        {
            queryBuilder.Add("search", parameters.Search.ToString()!);
        }

        var url = path + queryBuilder.ToQueryString();

        var responseMessage = await this.httpClient.GetAsync(url, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> GetSubGroupsWithResponseAsync(
        string realm,
        string parentGroupId,
        GetGroupsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetSubGroups.WithRealm(realm).Replace("{id}", parentGroupId);

        var queryBuilder = new QueryBuilder();

        parameters ??= new();
        if (parameters.BriefRepresentation.HasValue)
        {
            queryBuilder.Add(
                "briefRepresentation",
                parameters.BriefRepresentation?.ToString(CultureInfo.InvariantCulture)!
            );
        }

        if (parameters.First.HasValue)
        {
            queryBuilder.Add("first", parameters.First?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Exact.HasValue)
        {
            queryBuilder.Add("exact", parameters.Exact?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Max.HasValue)
        {
            queryBuilder.Add("max", parameters.Max?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Search is not null)
        {
            queryBuilder.Add("search", parameters.Search.ToString()!);
        }

        var url = path + queryBuilder.ToQueryString();

        var responseMessage = await this.httpClient.GetAsync(url, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> GetGroupWithResponseAsync(
        string realm,
        string groupId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetGroup.WithRealm(realm).Replace("{id}", groupId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> CreateGroupWithResponseAsync(
        string realm,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.CreateGroup.WithRealm(realm);

        var responseMessage = await this.httpClient.PostAsJsonAsync(path, group, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> UpdateGroupWithResponseAsync(
        string realm,
        string groupId,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.UpdateGroup.WithRealm(realm).Replace("{id}", groupId);

        var responseMessage = await this.httpClient.PutAsJsonAsync(path, group, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> CreateChildGroupWithResponseAsync(
        string realm,
        string groupId,
        GroupRepresentation group,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.CreateChildGroup.WithRealm(realm).Replace("{id}", groupId);

        var responseMessage = await this.httpClient.PostAsJsonAsync(path, group, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> DeleteGroupWithResponseAsync(
        string realm,
        string groupId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.DeleteGroup.WithRealm(realm).Replace("{id}", groupId);

        var responseMessage = await this.httpClient.DeleteAsync(path, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> ResetPasswordWithResponseAsync(
        string realm,
        string userId,
        CredentialRepresentation credential,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.ResetPassword.WithRealm(realm).Replace("{id}", userId);

        var responseMessage = await this.httpClient.PutAsJsonAsync(
            path,
            credential,
            cancellationToken
        );

        return responseMessage;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> DeleteCredentialWithResponseAsync(
        string realm,
        string userId,
        string credentialId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls
            .DeleteCredential.WithRealm(realm)
            .Replace("{id}", userId)
            .Replace("{credentialId}", credentialId);

        var responseMessage = await this.httpClient.DeleteAsync(path, cancellationToken);

        return responseMessage!;
    }

    ///<inheritdoc/>
    public async Task<HttpResponseMessage> GetCredentialsWithResponseAsync(
        string realm,
        string userId,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetCredentials.WithRealm(realm).Replace("{id}", userId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    #endregion

    #region OrganizationRegion

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetOrganizationsWithResponseAsync(
        string realm,
        GetOrganizationsRequestParameters? parameters = default,
        CancellationToken cancellationToken = default
    )
    {
        var path = ApiUrls.GetOrganizations.WithRealm(realm);

        var queryBuilder = new QueryBuilder();

        parameters ??= new();
        if (parameters.BriefRepresentation.HasValue)
        {
            queryBuilder.Add("briefRepresentation", parameters.BriefRepresentation?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Exact.HasValue)
        {
            queryBuilder.Add("exact", parameters.Exact?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.First.HasValue)
        {
            queryBuilder.Add("first", parameters.First?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Max.HasValue)
        {
            queryBuilder.Add("max", parameters.Max?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Query is not null)
        {
            queryBuilder.Add("q", parameters.Query.ToString()!);
        }

        if (parameters.Search is not null)
        {
            queryBuilder.Add("search", parameters.Search.ToString()!);
        }

        var url = path + queryBuilder.ToQueryString();

        var responseMessage = await this.httpClient.GetAsync(url, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetOrganizationCountWithResponseAsync(
        string realm,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.GetOrganizationCount.WithRealm(realm);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetOrganizationWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.GetOrganization.WithRealm(realm).Replace("{id}", orgId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> CreateOrganizationWithResponseAsync(
        string realm,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.CreateOrganization.WithRealm(realm);

        var responseMessage = await this.httpClient.PostAsJsonAsync(path, organization, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> UpdateOrganizationWithResponseAsync(
        string realm,
        string orgId,
        OrganizationRepresentation organization,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.UpdateOrganization.WithRealm(realm).Replace("{id}", orgId);

        var responseMessage = await this.httpClient.PutAsJsonAsync(path, organization, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> DeleteOrganizationWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.DeleteOrganization.WithRealm(realm).Replace("{id}", orgId);

        var responseMessage = await this.httpClient.DeleteAsync(path, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetOrganizationMembersWithResponseAsync(
        string realm,
        string orgId,
        GetOrganizationMembersRequestParameters? parameters = default,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.GetOrganizationMembers.WithRealm(realm).Replace("{id}", orgId);

        var queryBuilder = new QueryBuilder();

        parameters ??= new();
        if (parameters.Exact.HasValue)
        {
            queryBuilder.Add("exact", parameters.Exact?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.First.HasValue)
        {
            queryBuilder.Add("first", parameters.First?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.Max.HasValue)
        {
            queryBuilder.Add("max", parameters.Max?.ToString(CultureInfo.InvariantCulture)!);
        }

        if (parameters.MembershipType is not null)
        {
            queryBuilder.Add("membershipType", parameters.MembershipType.ToString()!);
        }

        if (parameters.Search is not null)
        {
            queryBuilder.Add("search", parameters.Search.ToString()!);
        }

        var url = path + queryBuilder.ToQueryString();

        var responseMessage = await this.httpClient.GetAsync(url, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetOrganizationMemberCountWithResponseAsync(
        string realm,
        string orgId,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.GetOrganizationMemberCount.WithRealm(realm).Replace("{id}", orgId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.GetOrganizationMember.WithRealm(realm).Replace("{id}", orgId).Replace("{memberId}", memberId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> AddOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.AddOrganizationMember.WithRealm(realm).Replace("{id}", orgId);

        var responseMessage = await this.httpClient.PostAsJsonAsync(path, userId, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> RemoveOrganizationMemberWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.RemoveOrganizationMember.WithRealm(realm).Replace("{id}", orgId).Replace("{memberId}", memberId);

        var responseMessage = await this.httpClient.DeleteAsync(path, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetOrganizationMemberGroupsWithResponseAsync(
        string realm,
        string orgId,
        string memberId,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.GetOrganizationMemberGroups.WithRealm(realm).Replace("{id}", orgId).Replace("{memberId}", memberId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    /// <inheritdoc/>
    public async Task<HttpResponseMessage> GetUserOrganizationsWithResponseAsync(
        string realm,
        string memberId,
        CancellationToken cancellationToken = default)
    {
        var path = ApiUrls.GetUserOrganizations.WithRealm(realm).Replace("{memberId}", memberId);

        var responseMessage = await this.httpClient.GetAsync(path, cancellationToken);

        return responseMessage!;
    }

    #endregion
