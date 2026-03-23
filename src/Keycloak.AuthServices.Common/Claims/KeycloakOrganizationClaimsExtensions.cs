namespace Keycloak.AuthServices.Common.Claims;

using System.Security.Claims;
using System.Text.Json;
using static Keycloak.AuthServices.Common.KeycloakConstants;

/// <summary>
/// Extension methods for extracting Keycloak organization membership from claims.
/// </summary>
public static class KeycloakOrganizationClaimsExtensions
{
    /// <summary>
    /// Gets the list of organizations the user is a member of.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>A list of organization memberships. Empty if no organization claim is present.</returns>
    public static IReadOnlyList<OrganizationMembership> GetOrganizations(
        this ClaimsPrincipal principal
    )
    {
        ArgumentNullException.ThrowIfNull(principal);
        return principal.Claims.GetOrganizations();
    }

    /// <summary>
    /// Determines whether the user is a member of the specified organization by alias.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <param name="organizationAlias">The organization alias.</param>
    /// <returns><c>true</c> if the user is a member of the organization; otherwise, <c>false</c>.</returns>
    public static bool IsMemberOf(this ClaimsPrincipal principal, string organizationAlias)
    {
        ArgumentNullException.ThrowIfNull(principal);
        ArgumentNullException.ThrowIfNull(organizationAlias);

        return principal
            .Claims.GetOrganizations()
            .Any(o => o.Alias.Equals(organizationAlias, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Determines whether the user is a member of the specified organization by identifier.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <param name="organizationId">The organization identifier.</param>
    /// <returns><c>true</c> if the user is a member of the organization; otherwise, <c>false</c>.</returns>
    public static bool IsMemberOfById(this ClaimsPrincipal principal, string organizationId)
    {
        ArgumentNullException.ThrowIfNull(principal);
        ArgumentNullException.ThrowIfNull(organizationId);

        return principal
            .Claims.GetOrganizations()
            .Any(o =>
                o.Id != null && o.Id.Equals(organizationId, StringComparison.OrdinalIgnoreCase)
            );
    }

    /// <summary>
    /// Gets the list of organizations from the claims collection.
    /// </summary>
    /// <remarks>
    /// Supports two claim formats:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <b>Vanilla (KC 26+)</b>: multiple string-valued <c>organization</c> claims, one per alias.
    /// This is the default format produced by the built-in Organization Membership mapper.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <b>Rich JSON</b>: a single JSON-valued <c>organization</c> claim containing a map of
    /// <c>alias → {id?, attributes?}</c>. This format can be produced by custom protocol mappers.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <param name="claims">The claims collection.</param>
    /// <returns>A list of organization memberships. Empty if no organization claim is present.</returns>
    public static IReadOnlyList<OrganizationMembership> GetOrganizations(
        this IEnumerable<Claim> claims
    )
    {
        var orgClaims = claims
            .Where(c => c.Type.Equals(OrganizationClaimType, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (orgClaims.Count == 0)
        {
            return [];
        }

        // Rich JSON format: single claim with JSON value type
        var jsonClaim = orgClaims.FirstOrDefault(c =>
            c.ValueType.Equals(
                KeycloakClaimsExtensions.JsonClaimValueType,
                StringComparison.OrdinalIgnoreCase
            )
        );

        if (jsonClaim is not null)
        {
            return ParseJsonOrganizationClaim(jsonClaim.Value);
        }

        // Vanilla format: multiple string claims (one per org alias)
        return orgClaims
            .Where(c => !string.IsNullOrEmpty(c.Value))
            .Select(c => new OrganizationMembership { Alias = c.Value })
            .ToList();
    }

    private static IReadOnlyList<OrganizationMembership> ParseJsonOrganizationClaim(
        string claimValue
    )
    {
        if (string.IsNullOrEmpty(claimValue))
        {
            return [];
        }

        using var document = JsonDocument.Parse(claimValue);
        var root = document.RootElement;

        if (root.ValueKind != JsonValueKind.Object)
        {
            return [];
        }

        var organizations = new List<OrganizationMembership>();

        foreach (var property in root.EnumerateObject())
        {
            var alias = property.Name;
            string? id = null;
            Dictionary<string, string[]>? attributes = null;

            if (property.Value.ValueKind == JsonValueKind.Object)
            {
                foreach (var field in property.Value.EnumerateObject())
                {
                    if (field.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                    {
                        id = field.Value.GetString();
                    }
                    else if (field.Value.ValueKind == JsonValueKind.Array)
                    {
                        attributes ??= new Dictionary<string, string[]>(
                            StringComparer.OrdinalIgnoreCase
                        );
                        attributes[field.Name] = field
                            .Value.EnumerateArray()
                            .Where(e => e.ValueKind == JsonValueKind.String)
                            .Select(e => e.GetString()!)
                            .ToArray();
                    }
                }
            }

            organizations.Add(
                new OrganizationMembership
                {
                    Alias = alias,
                    Id = id,
                    Attributes = attributes,
                }
            );
        }

        return organizations;
    }
}
