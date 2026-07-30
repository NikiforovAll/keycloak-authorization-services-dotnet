namespace Keycloak.AuthServices.Sdk.Admin.Requests.Organizations;

/// <summary>
/// Query parameters for <see cref="IKeycloakOrganizationClient.GetOrganizationMembersAsync"/>.
/// </summary>
public class GetOrganizationMembersRequestParameters
{
    /// <summary>Whether the <see cref="Search"/> value must match exactly.</summary>
    public bool? Exact { get; set; }

    /// <summary>Pagination offset.</summary>
    public int? First { get; set; }

    /// <summary>Maximum number of results to return.</summary>
    public int? Max { get; set; }

    /// <summary>Filters by membership type (e.g. <c>MANAGED</c>, <c>UNMANAGED</c>).</summary>
    public string? MembershipType { get; set; }

    /// <summary>String matching member username, email, first, or last name.</summary>
    public string? Search { get; set; }
}