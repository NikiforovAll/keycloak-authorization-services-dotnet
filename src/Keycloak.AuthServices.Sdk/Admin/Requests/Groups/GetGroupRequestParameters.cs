namespace Keycloak.AuthServices.Sdk.Admin.Requests.Groups;

using Keycloak.AuthServices.Sdk.Admin.Models;
using Refit;

/// <summary>
/// Optional request parameters for the <see cref="IKeycloakGroupClient.GetGroups"/> endpoint.
/// It can be called in three different ways.
/// <list type="number">
///     <item>
///         <description>
///             Don’t specify any criteria. A stream of all groups within that realm will be returned (limited by pagination).
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
public class GetGroupRequestParameters
{
    /// <summary>
    /// Defines whether brief representations are returned. Default is false.
    /// </summary>
    [AliasAs("briefRepresentation")]
    public bool? BriefRepresentation { get; init; }

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
}
