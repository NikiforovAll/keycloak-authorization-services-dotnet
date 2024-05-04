namespace Keycloak.AuthServices.Sdk.Admin.Requests.Users;

/// <summary>
/// Optional request parameters for the GetUserGroups endpoint.
/// It can be called in three different ways.
/// <list type="number">
///     <item>
///         <description>
///             Don’t specify any criteria. A stream of all groups within that realm will be returned (limited by pagination).
///         </description>
///     </item>
///     <item>
///         <description>
///             If <see cref="Search"/> is specified, other criteria will be ignored even though you may set them.
///         </description>
///     </item>
/// </list>
/// </summary>
public class GetUserGroupsRequestParameters
{
    /// <summary>
    /// Defines whether brief representations are returned. Default is false.
    /// </summary>
    public bool? BriefRepresentation { get; init; }

    /// <summary>
    /// Pagination offset.
    /// </summary>
    public int? First { get; init; }

    /// <summary>
    /// Maximum results size. Default is 100.
    /// </summary>
    public int? Max { get; init; }

    /// <summary>
    /// Search for a string contained in Username, FirstName,
    /// LastName or Email.
    /// </summary>
    public string? Search { get; init; }
}
