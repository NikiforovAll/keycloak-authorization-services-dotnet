namespace Keycloak.AuthServices.Sdk.Admin.Requests.Users;

/// <summary>
/// Optional request parameters for the <see cref="IKeycloakClient.GetUserCountAsync"/> endpoint.
/// If these are not specified, the total amount of users in the provided realm will be returned, otherwise, the amount
/// of users in the realm that match the criteria defined in these parameters will be returned.
/// </summary>
public class GetUserCountRequestParameters
{
    /// <summary>
    /// An optional filter for a user's email.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Whether a user's email has to be verified to be included.
    /// </summary>
    public bool? EmailVerified { get; init; }

    /// <summary>
    /// Whether a user has to be enabled to be included.
    /// </summary>
    public bool? Enabled { get; init; }

    /// <summary>
    /// An optional filter for a user's first name.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// An optional filter for a user's last name.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// An optional query to search for custom attributes, in the format "<c>key1:value2 key2:value2</c>".
    /// </summary>
    public string? Query { get; init; }

    /// <summary>
    /// An optional search string for all the fields of a user.
    /// Default search behavior is prefix-based (e.g., foo or foo*). Use foo for infix search and "foo" for exact search.
    /// </summary>
    public string? Search { get; init; }

    /// <summary>
    /// An optional filter for a user's username.
    /// </summary>
    public string? Username { get; init; }
}
