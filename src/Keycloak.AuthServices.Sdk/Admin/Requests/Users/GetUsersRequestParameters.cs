namespace Keycloak.AuthServices.Sdk.Admin.Requests.Users;

using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;

/// <summary>
/// Optional request parameters for the <see cref="IKeycloakUserClient.GetUsersAsync"/> endpoint.
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
///             the <see cref="UserRepresentation.FirstName"/>, <see cref="UserRepresentation.LastName"/>, <see cref="UserRepresentation.Username"/>
///             and the <see cref="UserRepresentation.Email"/> of a <see cref="UserRepresentation"/>.
///         </description>
///     </item>
///     <item>
///         <description>
///             If <see cref="Search"/> is unspecified but any of <see cref="LastName"/>, <see cref="FirstName"/>,
///             <see cref="Email"/> or <see cref="Username"/> are specified, then those criteria are matched against
///             their respective fields on a <see cref="UserRepresentation"/> entity. Combined with a logical <c>AND</c>.
///         </description>
///     </item>
/// </list>
/// </summary>
public class GetUsersRequestParameters
{
    /// <summary>
    /// Defines whether brief representations are returned. Default is false.
    /// </summary>
    public bool? BriefRepresentation { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="UserRepresentation.Email"/>,
    /// or the complete <see cref="UserRepresentation.Email"/> if <see cref="Exact"/> is true.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Search for whether the email has been verified.
    /// </summary>
    public bool? EmailVerified { get; init; }

    /// <summary>
    /// Search for whether the <see cref="UserRepresentation"/> is enabled or not.
    /// </summary>
    public bool? Enabled { get; init; }

    /// <summary>
    /// Defines whether the params <see cref="LastName"/>, <see cref="FirstName"/>,
    /// <see cref="Email"/> and <see cref="Username"/> must match exactly
    /// </summary>
    public bool? Exact { get; init; }

    /// <summary>
    /// Pagination offset.
    /// </summary>
    public int? First { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="UserRepresentation.FirstName"/>,
    /// or the complete <see cref="UserRepresentation.FirstName"/> if <see cref="Exact"/> is true.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Search for the alias of an Identity Provider linked to the <see cref="UserRepresentation"/>.
    /// </summary>
    public string? IdpAlias { get; init; }

    /// <summary>
    /// Search for a <see cref="FederatedIdentityRepresentation.UserId"/> at an Identity Provider linked to the <see cref="UserRepresentation"/>.
    /// </summary>
    public string? IdpUserId { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="UserRepresentation.LastName"/>,
    /// or the complete <see cref="UserRepresentation.LastName"/> if <see cref="Exact"/> is true.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Maximum results size. Default is 100.
    /// </summary>
    public int? Max { get; init; }

    /// <summary>
    /// A query to search for custom attributes, in the format "<c>key1:value2 key2:value2</c>".
    /// </summary>
    public string? Query { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="Username"/>, <see cref="FirstName"/>,
    /// <see cref="LastName"/> or <see cref="Email"/>.
    /// </summary>
    public string? Search { get; init; }

    /// <summary>
    /// Search for a string contained in <see cref="UserRepresentation.Username"/>,
    /// or the complete <see cref="UserRepresentation.Username"/> if <see cref="Exact"/> is true.
    /// </summary>
    public string? Username { get; init; }
}
