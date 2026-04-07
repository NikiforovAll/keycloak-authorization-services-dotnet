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
    /// <param name="claimType">Optional claim type override. Defaults to <see cref="KeycloakConstants.OrganizationClaimType"/>.</param>
    /// <returns>A list of organization memberships. Empty if no organization claim is present.</returns>
    public static IReadOnlyList<OrganizationMembership> GetOrganizations(
        this ClaimsPrincipal principal,
        string? claimType = null
    )
    {
        ArgumentNullException.ThrowIfNull(principal);
        return GetOrganizations(principal.Claims, claimType);
    }

    /// <summary>
    /// Determines whether the user is a member of the specified organization by alias.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <param name="organizationAlias">The organization alias.</param>
    /// <param name="claimType">Optional claim type override. Defaults to <see cref="KeycloakConstants.OrganizationClaimType"/>.</param>
    /// <returns><c>true</c> if the user is a member of the organization; otherwise, <c>false</c>.</returns>
    public static bool IsMemberOf(
        this ClaimsPrincipal principal,
        string organizationAlias,
        string? claimType = null
    )
    {
        ArgumentNullException.ThrowIfNull(principal);
        ArgumentNullException.ThrowIfNull(organizationAlias);

        return GetOrganizations(principal.Claims, claimType)
            .Any(o => o.Alias.Equals(organizationAlias, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Determines whether the user is a member of the specified organization by identifier.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <param name="organizationId">The organization identifier.</param>
    /// <param name="claimType">Optional claim type override. Defaults to <see cref="KeycloakConstants.OrganizationClaimType"/>.</param>
    /// <returns><c>true</c> if the user is a member of the organization; otherwise, <c>false</c>.</returns>
    public static bool IsMemberOfById(
        this ClaimsPrincipal principal,
        string organizationId,
        string? claimType = null
    )
    {
        ArgumentNullException.ThrowIfNull(principal);
        ArgumentNullException.ThrowIfNull(organizationId);

        return GetOrganizations(principal.Claims, claimType)
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
    /// <param name="claimType">Optional claim type override. Defaults to <see cref="KeycloakConstants.OrganizationClaimType"/>.</param>
    /// <returns>A list of organization memberships. Empty if no organization claim is present.</returns>
    public static IReadOnlyList<OrganizationMembership> GetOrganizations(
        this IEnumerable<Claim> claims,
        string? claimType = null
    )
    {
        var effectiveClaimType = claimType ?? OrganizationClaimType;

        // Single pass: look for JSON claim or collect vanilla string aliases.
        // JSON claim takes priority — as soon as one is found we can stop scanning.
        Claim? jsonClaim = null;
        List<string>? stringAliases = null;

        foreach (var c in claims)
        {
            if (!c.Type.Equals(effectiveClaimType, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (
                c.ValueType.Equals(
                    KeycloakClaimsExtensions.JsonClaimValueType,
                    StringComparison.OrdinalIgnoreCase
                )
            )
            {
                jsonClaim = c;
                break;
            }

            if (!string.IsNullOrEmpty(c.Value))
            {
                stringAliases ??= [];
                stringAliases.Add(c.Value);
            }
        }

        if (jsonClaim is not null)
        {
            return ParseJsonOrganizationClaim(jsonClaim.Value);
        }

        if (stringAliases is null)
        {
            return [];
        }

        // Vanilla format: multiple string claims (one per org alias)
        return stringAliases.Select(alias => new OrganizationMembership { Alias = alias }).ToList();
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
