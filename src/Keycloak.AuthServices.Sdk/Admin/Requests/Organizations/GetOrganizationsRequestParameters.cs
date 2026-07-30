namespace Keycloak.AuthServices.Sdk.Admin.Requests.Organizations;

/// <summary>
/// Query parameters for <see cref="IKeycloakOrganizationClient.GetOrganizationsAsync"/>.
/// </summary>
public class GetOrganizationsRequestParameters
{
    /// <summary>If <c>false</c>, return the full representation. Otherwise only basic fields.</summary>
    public bool? BriefRepresentation { get; set; }

    /// <summary>Whether the <see cref="Search"/> value must match exactly.</summary>
    public bool? Exact { get; set; }

    /// <summary>Pagination offset.</summary>
    public int? First { get; set; }

    /// <summary>Maximum number of results to return.</summary>
    public int? Max { get; set; }

    /// <summary>Attribute search query in the format <c>key1:value1 key2:value2</c>.</summary>
    public string? Query { get; set; }

    /// <summary>String matching either an organization name or a registered domain.</summary>
    public string? Search { get; set; }
}