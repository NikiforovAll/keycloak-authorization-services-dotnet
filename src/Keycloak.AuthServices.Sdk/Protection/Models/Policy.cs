namespace Keycloak.AuthServices.Sdk.Protection.Models;

using System.Runtime.Serialization;

/// <summary>
/// Represents a policy used for service protection.
/// </summary>
/// <remarks>
/// See <see href="https://www.keycloak.org/docs/latest/authorization_services/index.html#_service_protection_api"/> for details.
/// </remarks>
public class Policy
{
    /// <summary>
    /// Gets or sets the ID of the policy.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the policy.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the description of the policy.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the type of the policy.
    /// </summary>
    public string? Type { get; init; } = "uma";

    /// <summary>
    /// Gets or sets the logic of the policy.
    /// </summary>
    public string? Logic { get; init; } // POSITIVE

    /// <summary>
    /// Gets or sets the decision strategy of the policy.
    /// </summary>
    public DecisionStrategy DecisionStrategy { get; init; } = DecisionStrategy.UNANIMOUS;

    /// <summary>
    /// Gets or sets the owner of the policy.
    /// </summary>
    public string? Owner { get; init; } // The SID of the owner

    /// <summary>
    /// Gets or sets the condition of the policy.
    /// </summary>
    public string? Condition { get; init; } // Typically not used or recommended

    /// <summary>
    /// Gets or sets the scopes associated with the policy.
    /// </summary>
    public string[]? Scopes { get; init; }

    /// <summary>
    /// Gets or sets the groups associated with the policy.
    /// </summary>
    public string[]? Groups { get; set; }

    /// <summary>
    /// Gets or sets the roles associated with the policy.
    /// </summary>
    public string[]? Roles { get; init; }

    /// <summary>
    /// Gets or sets the clients associated with the policy.
    /// </summary>
    public string[]? Clients { get; init; }
}

/// <summary>
/// </summary>
public enum DecisionStrategy
{
    /// <summary>
    /// </summary>
    [EnumMember(Value = "AFFIRMATIVE")]
    AFFIRMATIVE,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "UNANIMOUS")]
    UNANIMOUS,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "CONSENSUS")]
    CONSENSUS,
}
