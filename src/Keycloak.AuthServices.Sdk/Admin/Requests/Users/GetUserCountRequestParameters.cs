namespace Keycloak.AuthServices.Sdk.Admin.Requests.Users;

/// <summary>
/// Optional request parameters for the <see cref="IKeycloakClient.GetUserCountAsync"/> endpoint.
/// It can be called in three different ways.
/// <list type="number">
///     <item>
///         <description>
///             Don’t specify any criteria. The number of all users within that realm will be returned, not limited by
///             pagination.
///         </description>
///     </item>
///     <item>
///         <description>
///             If <see cref="Search"/> is specified, other criteria such as <see cref="LastName"/> will be ignored
///             even though you set them. The <see cref="Search"/> string will be matched against the first and last
///             name, the username and the email of a user.
///         </description>
///     </item>
///     <item>
///         <description>
///             If <see cref="Search"/> is unspecified but any of <see cref="LastName"/>, <see cref="FirstName"/>,
///             <see cref="Email"/> or <see cref="Username"/>, those criteria are matched against their respective
///             fields on a user entity. Combined with a logical and.
///         </description>
///     </item>
/// </list>
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
