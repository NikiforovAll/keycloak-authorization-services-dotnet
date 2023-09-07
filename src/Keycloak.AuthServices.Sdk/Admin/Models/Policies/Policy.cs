namespace Keycloak.AuthServices.Sdk.Admin.Models.Policies;

/// <summary>
/// See https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_api for details
/// </summary>
public class Policy
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Type { get; init; } = "uma";
    public string? Logic { get; init; } // POSITIVE
    public string? DecisionStrategy { get; init; } // UNANIMOUS 
    public string? Owner { get; init; } // The SID of the owner
    public string? Condition { get; init; } // Typically not used or recommended
    public string[]? Scopes { get; init; }
    public string[]? Groups { get; set; }
    public string[]? Roles { get; init; }
    public string[]? Clients { get; init; }
}
