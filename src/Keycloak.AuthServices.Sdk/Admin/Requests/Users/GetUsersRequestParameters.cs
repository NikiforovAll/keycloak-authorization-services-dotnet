namespace Keycloak.AuthServices.Sdk.Admin.Requests.Users;

using Models.Users;
using Refit;

/// <summary>
/// Optional request parameters for the <see cref="IKeycloakUserClient.GetUsers"/> endpoint.
/// It can be called in three different ways.
/// <list type="number">
///     <item>
///         <description>
///             Don’t specify any criteria. A stream of all users within that realm will be returned (limited by pagination).
///         </description>
///     </item>
///     <item>
///         <description>
///             If <see cref="Search"/> is specified, other criteria such as <see cref="LastName"/>
///             will be ignored even though you may set them. The <see cref="Search"/> string will be matched against
///             the <see cref="User.FirstName"/>, <see cref="User.LastName"/>, <see cref="User.Username"/>
///             and the <see cref="User.Email"/> of a <see cref="User"/>.
///         </description>
///     </item>
///     <item>
///         <description>
///             If <see cref="Search"/> is unspecified but any of <see cref="LastName"/>, <see cref="FirstName"/>,
///             <see cref="Email"/> or <see cref="Username"/> are specified, then those criteria are matched against
///             their respective fields on a <see cref="User"/> entity. Combined with a logical <c>AND</c>.
///         </description>
///     </item>
/// </list>
/// </summary>
public class GetUsersRequestParameters
{
    /// <summary>
    /// Defines whether brief representations are returned. Default is false.
    /// </summary>
    [AliasAs("briefRepresentation")]
    public bool? BriefRepresentation { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="User.Email"/>,
    /// or the complete <see cref="User.Email"/> if <see cref="Exact"/> is true.
    /// </summary>
    [AliasAs("email")]
    public string? Email { get; init; }

    /// <summary>
    /// Search for whether the email has been verified.
    /// </summary>
    [AliasAs("emailVerified")]
    public bool? EmailVerified { get; init; }

    /// <summary>
    /// Search for whether the <see cref="User"/> is enabled or not.
    /// </summary>
    [AliasAs("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>
    /// Defines whether the params <see cref="LastName"/>, <see cref="FirstName"/>,
    /// <see cref="Email"/> and <see cref="Username"/> must match exactly
    /// </summary>
    [AliasAs("exact")]
    public bool? Exact { get; init; }

    /// <summary>
    /// Pagination offset.
    /// </summary>
    [AliasAs("first")]
    public int? First { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="User.FirstName"/>,
    /// or the complete <see cref="User.FirstName"/> if <see cref="Exact"/> is true.
    /// </summary>
    [AliasAs("firstName")]
    public string? FirstName { get; init; }

    /// <summary>
    /// Search for the alias of an Identity Provider linked to the <see cref="User"/>.
    /// </summary>
    [AliasAs("idpAlias")]
    public string? IdpAlias { get; init; }

    /// <summary>
    /// Search for a <see cref="FederatedIdentity.UserId"/> at an Identity Provider linked to the <see cref="User"/>.
    /// </summary>
    [AliasAs("idpUserId")]
    public string? IdpUserId { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="User.LastName"/>,
    /// or the complete <see cref="User.LastName"/> if <see cref="Exact"/> is true.
    /// </summary>
    [AliasAs("lastName")]
    public string? LastName { get; init; }

    /// <summary>
    /// Maximum results size. Default is 100.
    /// </summary>
    [AliasAs("max")]
    public int? Max { get; init; }

    /// <summary>
    /// A query to search for custom attributes, in the format "<c>key1:value2 key2:value2</c>".
    /// </summary>
    [AliasAs("q")]
    public string? Query { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="Username"/>, <see cref="FirstName"/>,
    /// <see cref="LastName"/> or <see cref="Email"/>.
    /// </summary>
    [AliasAs("search")]
    public string? Search { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="User.Username"/>,
    /// or the complete <see cref="User.Username"/> if <see cref="Exact"/> is true.
    /// </summary>
    [AliasAs("username")]
    public string? Username { get; init; }
}
