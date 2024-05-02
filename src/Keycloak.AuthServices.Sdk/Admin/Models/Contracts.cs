#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 612 // Disable "CS0612 '...' is obsolete"
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"
#pragma warning disable 8604 // Disable "CS8604 Possible null reference argument for parameter"

namespace Keycloak.AuthServices.Sdk.Admin.Models;

using System = global::System;

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AbstractPolicyRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("policies")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Policies { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resources")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Resources { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Scopes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("logic")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public Logic? Logic { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("decisionStrategy")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public DecisionStrategy? DecisionStrategy { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("owner")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Owner { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resourcesData")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ResourceRepresentation>? ResourcesData { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopesData")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ScopeRepresentation>? ScopesData { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class Access
{
    [System.Text.Json.Serialization.JsonPropertyName("roles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Roles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("verify_caller")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Verify_caller { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AccessToken
{
    [System.Text.Json.Serialization.JsonPropertyName("jti")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Jti { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("exp")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Exp { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("nbf")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Nbf { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("iat")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Iat { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("iss")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Iss { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("sub")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Sub { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("typ")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Typ { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("azp")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Azp { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otherClaims")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? OtherClaims { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("nonce")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Nonce { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("auth_time")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Auth_time { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("session_state")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Session_state { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("at_hash")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? At_hash { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("c_hash")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? C_hash { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("given_name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Given_name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("family_name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Family_name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("middle_name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Middle_name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("nickname")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Nickname { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("preferred_username")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Preferred_username { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("profile")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Profile { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("picture")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Picture { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("website")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Website { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("email")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Email { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("email_verified")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Email_verified { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("gender")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Gender { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("birthdate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Birthdate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("zoneinfo")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Zoneinfo { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("locale")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Locale { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("phone_number")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Phone_number { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("phone_number_verified")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Phone_number_verified { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("address")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public AddressClaimSet? Address { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("updated_at")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Updated_at { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("claims_locales")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Claims_locales { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("acr")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Acr { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("s_hash")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? S_hash { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authTime")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public int? AuthTime { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("sid")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Sid { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("trusted-certs")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? TrustedCerts { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("allowed-origins")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? AllowedOrigins { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realm_access")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public Access? Realm_access { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resource_access")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, Access>? Resource_access { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("authorization")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public Authorization? Authorization { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("cnf")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public Confirmation? Cnf { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scope")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Scope { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AddressClaimSet
{
    [System.Text.Json.Serialization.JsonPropertyName("formatted")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Formatted { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("street_address")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Street_address { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("locality")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Locality { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("region")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Region { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("postal_code")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Postal_code { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("country")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Country { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AdminEventRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("time")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Time { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realmId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RealmId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authDetails")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public AuthDetailsRepresentation? AuthDetails { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("operationType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? OperationType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resourceType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ResourceType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resourcePath")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ResourcePath { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("representation")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Representation { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("error")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Error { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
[System.Obsolete]
public partial class ApplicationRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("rootUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RootUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("adminUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AdminUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("baseUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? BaseUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("surrogateAuthRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? SurrogateAuthRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("alwaysDisplayInConsole")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AlwaysDisplayInConsole { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientAuthenticatorType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientAuthenticatorType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("secret")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Secret { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registrationAccessToken")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RegistrationAccessToken { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<string>? DefaultRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("redirectUris")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? RedirectUris { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webOrigins")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebOrigins { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("notBefore")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? NotBefore { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("bearerOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? BearerOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("consentRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ConsentRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("standardFlowEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? StandardFlowEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("implicitFlowEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ImplicitFlowEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("directAccessGrantsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? DirectAccessGrantsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("serviceAccountsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ServiceAccountsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authorizationServicesEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AuthorizationServicesEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("directGrantsOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? DirectGrantsOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("publicClient")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? PublicClient { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("frontchannelLogout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? FrontchannelLogout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocol")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Protocol { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Attributes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticationFlowBindingOverrides")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        string
    >? AuthenticationFlowBindingOverrides { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("fullScopeAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? FullScopeAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("nodeReRegistrationTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? NodeReRegistrationTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registeredNodes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, int>? RegisteredNodes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocolMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ProtocolMapperRepresentation>? ProtocolMappers { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientTemplate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? ClientTemplate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateConfig")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateConfig { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateScope")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateScope { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateMappers { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? DefaultClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("optionalClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? OptionalClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("authorizationSettings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public ResourceServerRepresentation? AuthorizationSettings { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("access")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, bool>? Access { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("origin")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Origin { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("claims")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public Claims? Claims { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AuthDetailsRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("realmId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RealmId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? UserId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ipAddress")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? IpAddress { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AuthenticationExecutionExportRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("authenticatorConfig")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AuthenticatorConfig { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticator")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Authenticator { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticatorFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AuthenticatorFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("requirement")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Requirement { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("priority")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Priority { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("autheticatorFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? AutheticatorFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("flowAlias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FlowAlias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userSetupAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? UserSetupAllowed { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AuthenticationExecutionInfoRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("requirement")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Requirement { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("alias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Alias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("requirementChoices")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? RequirementChoices { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("configurable")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Configurable { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticationFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AuthenticationFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticationConfig")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AuthenticationConfig { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("flowId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FlowId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("level")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Level { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("index")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Index { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AuthenticationExecutionRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("authenticatorConfig")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AuthenticatorConfig { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticator")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Authenticator { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticatorFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AuthenticatorFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("requirement")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Requirement { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("priority")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Priority { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("autheticatorFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? AutheticatorFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("flowId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FlowId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("parentFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ParentFlow { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AuthenticationFlowRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("alias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Alias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("topLevel")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? TopLevel { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("builtIn")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? BuiltIn { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticationExecutions")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<AuthenticationExecutionExportRepresentation>? AuthenticationExecutions { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AuthenticatorConfigInfoRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("helpText")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? HelpText { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("properties")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ConfigPropertyRepresentation>? Properties { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class AuthenticatorConfigRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("alias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Alias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class Authorization
{
    [System.Text.Json.Serialization.JsonPropertyName("permissions")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<Permission>? Permissions { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class CertificateRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("privateKey")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? PrivateKey { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("publicKey")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? PublicKey { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("certificate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Certificate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("kid")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Kid { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClaimRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Username { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("profile")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Profile { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("picture")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Picture { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("website")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Website { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("email")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Email { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("gender")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Gender { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("locale")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Locale { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("address")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Address { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("phone")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Phone { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientInitialAccessCreatePresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("expiration")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Expiration { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("count")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Count { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientInitialAccessPresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("token")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Token { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("timestamp")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Timestamp { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("expiration")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Expiration { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("count")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Count { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("remainingCount")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? RemainingCount { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientMappingsRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("client")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Client { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("mappings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<RoleRepresentation>? Mappings { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientPoliciesRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("policies")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ClientPolicyRepresentation>? Policies { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientPolicyConditionRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("condition")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Condition { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("configuration")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<object>? Configuration { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientPolicyExecutorRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("executor")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Executor { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("configuration")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<object>? Configuration { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientPolicyRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("conditions")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ClientPolicyConditionRepresentation>? Conditions { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("profiles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Profiles { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientProfileRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("executors")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ClientPolicyExecutorRepresentation>? Executors { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientProfilesRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("profiles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ClientProfileRepresentation>? Profiles { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("globalProfiles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ClientProfileRepresentation>? GlobalProfiles { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("rootUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RootUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("adminUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AdminUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("baseUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? BaseUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("surrogateAuthRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? SurrogateAuthRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("alwaysDisplayInConsole")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AlwaysDisplayInConsole { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientAuthenticatorType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientAuthenticatorType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("secret")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Secret { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registrationAccessToken")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RegistrationAccessToken { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<string>? DefaultRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("redirectUris")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? RedirectUris { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webOrigins")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebOrigins { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("notBefore")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? NotBefore { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("bearerOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? BearerOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("consentRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ConsentRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("standardFlowEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? StandardFlowEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("implicitFlowEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ImplicitFlowEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("directAccessGrantsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? DirectAccessGrantsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("serviceAccountsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ServiceAccountsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authorizationServicesEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AuthorizationServicesEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("directGrantsOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? DirectGrantsOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("publicClient")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? PublicClient { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("frontchannelLogout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? FrontchannelLogout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocol")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Protocol { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Attributes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticationFlowBindingOverrides")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        string
    >? AuthenticationFlowBindingOverrides { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("fullScopeAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? FullScopeAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("nodeReRegistrationTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? NodeReRegistrationTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registeredNodes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, int>? RegisteredNodes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocolMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ProtocolMapperRepresentation>? ProtocolMappers { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientTemplate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? ClientTemplate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateConfig")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateConfig { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateScope")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateScope { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateMappers { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? DefaultClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("optionalClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? OptionalClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("authorizationSettings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public ResourceServerRepresentation? AuthorizationSettings { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("access")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, bool>? Access { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("origin")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Origin { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ClientScopeRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocol")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Protocol { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Attributes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocolMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ProtocolMapperRepresentation>? ProtocolMappers { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
[System.Obsolete]
public partial class ClientTemplateRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocol")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Protocol { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("fullScopeAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? FullScopeAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("bearerOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? BearerOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("consentRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ConsentRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("standardFlowEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? StandardFlowEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("implicitFlowEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ImplicitFlowEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("directAccessGrantsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? DirectAccessGrantsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("serviceAccountsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ServiceAccountsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("publicClient")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? PublicClient { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("frontchannelLogout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? FrontchannelLogout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Attributes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocolMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ProtocolMapperRepresentation>? ProtocolMappers { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ComponentExportRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("subType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SubType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("subComponents")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public MultivaluedHashMapStringComponentExportRepresentation? SubComponents { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public MultivaluedHashMapStringString? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ComponentRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("parentId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ParentId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("subType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SubType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public MultivaluedHashMapStringString? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ComponentTypeRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("helpText")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? HelpText { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("properties")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ConfigPropertyRepresentation>? Properties { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("metadata")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? Metadata { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class Composites
{
    [System.Text.Json.Serialization.JsonPropertyName("realm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Realm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("client")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? Client { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("application")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? Application { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ConfigPropertyRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("label")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Label { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("helpText")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? HelpText { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultValue")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public object? DefaultValue { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("options")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Options { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("secret")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Secret { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("required")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Required { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("readOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ReadOnly { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class Confirmation
{
    [System.Text.Json.Serialization.JsonPropertyName("x5t#S256")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? X5t_S256 { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("jkt")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Jkt { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class CredentialRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userLabel")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? UserLabel { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("createdDate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? CreatedDate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("secretData")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SecretData { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("credentialData")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? CredentialData { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("priority")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Priority { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("value")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Value { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("temporary")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Temporary { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("device")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? Device { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("hashedSaltedValue")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? HashedSaltedValue { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("salt")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? Salt { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("hashIterations")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public int? HashIterations { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("counter")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public int? Counter { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("algorithm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? Algorithm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("digits")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public int? Digits { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("period")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public int? Period { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public Config? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public enum DecisionEffect
{
    [System.Runtime.Serialization.EnumMember(Value = @"PERMIT")]
    PERMIT = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"DENY")]
    DENY = 1,
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public enum DecisionStrategy
{
    [System.Runtime.Serialization.EnumMember(Value = @"AFFIRMATIVE")]
    AFFIRMATIVE = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"UNANIMOUS")]
    UNANIMOUS = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"CONSENSUS")]
    CONSENSUS = 2,
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public enum EnforcementMode
{
    [System.Runtime.Serialization.EnumMember(Value = @"PERMISSIVE")]
    PERMISSIVE = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"ENFORCING")]
    ENFORCING = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"DISABLED")]
    DISABLED = 2,
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class EvaluationResultRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("resource")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public ResourceRepresentation? Resource { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ScopeRepresentation>? Scopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("policies")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<PolicyResultRepresentation>? Policies { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("status")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public DecisionEffect? Status { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("allowedScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ScopeRepresentation>? AllowedScopes { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class EventRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("time")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Time { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realmId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RealmId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? UserId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("sessionId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SessionId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ipAddress")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? IpAddress { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("error")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Error { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("details")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Details { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class FederatedIdentityRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("identityProvider")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? IdentityProvider { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? UserId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? UserName { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class GlobalRequestResult
{
    [System.Text.Json.Serialization.JsonPropertyName("successRequests")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? SuccessRequests { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("failedRequests")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? FailedRequests { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class GroupRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("path")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Path { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("parentId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ParentId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("subGroupCount")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? SubGroupCount { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("subGroups")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<GroupRepresentation>? SubGroups { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? Attributes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realmRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? RealmRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? ClientRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("access")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, bool>? Access { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class IDToken
{
    [System.Text.Json.Serialization.JsonPropertyName("jti")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Jti { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("exp")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Exp { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("nbf")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Nbf { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("iat")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Iat { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("iss")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Iss { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("sub")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Sub { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("typ")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Typ { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("azp")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Azp { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otherClaims")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? OtherClaims { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("nonce")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Nonce { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("auth_time")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Auth_time { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("session_state")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Session_state { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("at_hash")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? At_hash { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("c_hash")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? C_hash { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("given_name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Given_name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("family_name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Family_name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("middle_name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Middle_name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("nickname")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Nickname { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("preferred_username")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Preferred_username { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("profile")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Profile { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("picture")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Picture { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("website")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Website { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("email")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Email { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("email_verified")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Email_verified { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("gender")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Gender { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("birthdate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Birthdate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("zoneinfo")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Zoneinfo { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("locale")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Locale { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("phone_number")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Phone_number { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("phone_number_verified")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Phone_number_verified { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("address")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public AddressClaimSet? Address { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("updated_at")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Updated_at { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("claims_locales")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Claims_locales { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("acr")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Acr { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("s_hash")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? S_hash { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authTime")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public int? AuthTime { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("sid")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Sid { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class IdentityProviderMapperRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("identityProviderAlias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? IdentityProviderAlias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("identityProviderMapper")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? IdentityProviderMapper { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class IdentityProviderMapperTypeRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("category")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Category { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("helpText")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? HelpText { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("properties")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ConfigPropertyRepresentation>? Properties { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class IdentityProviderRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("alias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Alias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("internalId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? InternalId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("updateProfileFirstLoginMode")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? UpdateProfileFirstLoginMode { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("trustEmail")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? TrustEmail { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("storeToken")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? StoreToken { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("addReadTokenRoleOnCreate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AddReadTokenRoleOnCreate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticateByDefault")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AuthenticateByDefault { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("linkOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? LinkOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("firstBrokerLoginFlowAlias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FirstBrokerLoginFlowAlias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("postBrokerLoginFlowAlias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? PostBrokerLoginFlowAlias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Config { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("updateProfileFirstLogin")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UpdateProfileFirstLogin { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class InstallationAdapterConfig
{
    [System.Text.Json.Serialization.JsonPropertyName("realm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Realm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realm-public-key")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RealmPublicKey { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("auth-server-url")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AuthServerUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ssl-required")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SslRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("bearer-only")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? BearerOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resource")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Resource { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("public-client")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? PublicClient { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("verify-token-audience")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? VerifyTokenAudience { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("credentials")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? Credentials { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("use-resource-role-mappings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? UseResourceRoleMappings { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("confidential-port")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? ConfidentialPort { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("policy-enforcer")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public PolicyEnforcerConfig? PolicyEnforcer { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class KeyMetadataRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("providerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerPriority")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? ProviderPriority { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("kid")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Kid { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("status")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Status { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("algorithm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Algorithm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("publicKey")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? PublicKey { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("certificate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Certificate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("use")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public KeyUse? Use { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("validTo")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? ValidTo { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class KeyStoreConfig
{
    [System.Text.Json.Serialization.JsonPropertyName("realmCertificate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? RealmCertificate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("storePassword")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? StorePassword { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("keyPassword")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? KeyPassword { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("keyAlias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? KeyAlias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realmAlias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RealmAlias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("format")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Format { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public enum KeyUse
{
    [System.Runtime.Serialization.EnumMember(Value = @"SIG")]
    SIG = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"ENC")]
    ENC = 1,
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class KeysMetadataRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("active")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Active { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("keys")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<KeyMetadataRepresentation>? Keys { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public enum Logic
{
    [System.Runtime.Serialization.EnumMember(Value = @"POSITIVE")]
    POSITIVE = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"NEGATIVE")]
    NEGATIVE = 1,
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ManagementPermissionReference
{
    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resource")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Resource { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopePermissions")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? ScopePermissions { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class MappingsRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("realmMappings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<RoleRepresentation>? RealmMappings { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientMappings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        ClientMappingsRepresentation
    >? ClientMappings { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class MethodConfig
{
    [System.Text.Json.Serialization.JsonPropertyName("method")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Method { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Scopes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes-enforcement-mode")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public ScopeEnforcementMode? ScopesEnforcementMode { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class MultivaluedHashMapStringComponentExportRepresentation
    : System.Collections.Generic.Dictionary<
        string,
        System.Collections.ObjectModel.Collection<ComponentExportRepresentation>
    > { }

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class MultivaluedHashMapStringString
    : System.Collections.Generic.Dictionary<
        string,
        System.Collections.ObjectModel.Collection<string>
    > { }

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class MultivaluedMapStringString
    : System.Collections.Generic.Dictionary<
        string,
        System.Collections.ObjectModel.Collection<string>
    > { }

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
[System.Obsolete]
public partial class OAuthClientRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("rootUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RootUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("adminUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AdminUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("baseUrl")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? BaseUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("surrogateAuthRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? SurrogateAuthRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("alwaysDisplayInConsole")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AlwaysDisplayInConsole { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientAuthenticatorType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientAuthenticatorType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("secret")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Secret { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registrationAccessToken")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RegistrationAccessToken { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<string>? DefaultRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("redirectUris")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? RedirectUris { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webOrigins")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebOrigins { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("notBefore")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? NotBefore { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("bearerOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? BearerOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("consentRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ConsentRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("standardFlowEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? StandardFlowEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("implicitFlowEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ImplicitFlowEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("directAccessGrantsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? DirectAccessGrantsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("serviceAccountsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ServiceAccountsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authorizationServicesEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AuthorizationServicesEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("directGrantsOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? DirectGrantsOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("publicClient")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? PublicClient { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("frontchannelLogout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? FrontchannelLogout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocol")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Protocol { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Attributes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticationFlowBindingOverrides")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        string
    >? AuthenticationFlowBindingOverrides { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("fullScopeAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? FullScopeAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("nodeReRegistrationTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? NodeReRegistrationTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registeredNodes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, int>? RegisteredNodes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocolMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ProtocolMapperRepresentation>? ProtocolMappers { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientTemplate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? ClientTemplate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateConfig")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateConfig { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateScope")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateScope { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("useTemplateMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UseTemplateMappers { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? DefaultClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("optionalClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? OptionalClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("authorizationSettings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public ResourceServerRepresentation? AuthorizationSettings { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("access")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, bool>? Access { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("origin")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Origin { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("claims")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public Claims2? Claims { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PathCacheConfig
{
    [System.Text.Json.Serialization.JsonPropertyName("max-entries")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? MaxEntries { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("lifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Lifespan { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PathConfig
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("path")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Path { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("methods")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<MethodConfig>? Methods { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Scopes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enforcement-mode")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public EnforcementMode? EnforcementMode { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("claim-information-point")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.IDictionary<string, object>
    >? ClaimInformationPoint { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("invalidated")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Invalidated { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("staticPath")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? StaticPath { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("static")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Static { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PathSegment
{
    [System.Text.Json.Serialization.JsonPropertyName("path")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Path { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("matrixParameters")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public MultivaluedMapStringString? MatrixParameters { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class Permission
{
    [System.Text.Json.Serialization.JsonPropertyName("rsid")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Rsid { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("rsname")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Rsname { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Scopes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("claims")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? Claims { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public enum PolicyEnforcementMode
{
    [System.Runtime.Serialization.EnumMember(Value = @"ENFORCING")]
    ENFORCING = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"PERMISSIVE")]
    PERMISSIVE = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"DISABLED")]
    DISABLED = 2,
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PolicyEnforcerConfig
{
    [System.Text.Json.Serialization.JsonPropertyName("enforcement-mode")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public EnforcementMode? EnforcementMode { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("paths")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<PathConfig>? Paths { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("path-cache")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public PathCacheConfig? PathCache { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("lazy-load-paths")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? LazyLoadPaths { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("on-deny-redirect-to")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? OnDenyRedirectTo { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("user-managed-access")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public UserManagedAccessConfig? UserManagedAccess { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("claim-information-point")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.IDictionary<string, object>
    >? ClaimInformationPoint { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("http-method-as-scope")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? HttpMethodAsScope { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Realm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("auth-server-url")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AuthServerUrl { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("credentials")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? Credentials { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("resource")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Resource { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PolicyEvaluationRequest
{
    [System.Text.Json.Serialization.JsonPropertyName("context")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.IDictionary<string, string>
    >? Context { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resources")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ResourceRepresentation>? Resources { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? UserId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("roleIds")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? RoleIds { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("entitlements")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Entitlements { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PolicyEvaluationResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("results")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<EvaluationResultRepresentation>? Results { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("entitlements")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Entitlements { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("status")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public DecisionEffect? Status { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("rpt")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public AccessToken? Rpt { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PolicyProviderRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("group")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Group { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PolicyRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("policies")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Policies { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resources")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Resources { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Scopes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("logic")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public Logic? Logic { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("decisionStrategy")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public DecisionStrategy? DecisionStrategy { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("owner")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Owner { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resourcesData")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ResourceRepresentation>? ResourcesData { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopesData")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ScopeRepresentation>? ScopesData { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PolicyResultRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("policy")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public PolicyRepresentation? Policy { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("status")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public DecisionEffect? Status { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("associatedPolicies")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<PolicyResultRepresentation>? AssociatedPolicies { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Scopes { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ProtocolMapperEvaluationRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("mapperId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? MapperId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("mapperName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? MapperName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("containerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ContainerId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("containerName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ContainerName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("containerType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ContainerType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocolMapper")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProtocolMapper { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ProtocolMapperRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocol")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Protocol { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocolMapper")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProtocolMapper { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("consentRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? ConsentRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("consentText")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? ConsentText { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class PublishedRealmRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("realm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Realm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("public_key")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Public_key { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("token-service")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? TokenService { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("account-service")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AccountService { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("tokens-not-before")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? TokensNotBefore { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class RealmEventsConfigRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("eventsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? EventsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("eventsExpiration")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? EventsExpiration { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("eventsListeners")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? EventsListeners { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabledEventTypes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? EnabledEventTypes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("adminEventsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AdminEventsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("adminEventsDetailsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AdminEventsDetailsEnabled { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class RealmRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Realm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayNameHtml")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayNameHtml { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("notBefore")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? NotBefore { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultSignatureAlgorithm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DefaultSignatureAlgorithm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("revokeRefreshToken")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? RevokeRefreshToken { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("refreshTokenMaxReuse")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? RefreshTokenMaxReuse { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("accessTokenLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? AccessTokenLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("accessTokenLifespanForImplicitFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? AccessTokenLifespanForImplicitFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ssoSessionIdleTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? SsoSessionIdleTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ssoSessionMaxLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? SsoSessionMaxLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ssoSessionIdleTimeoutRememberMe")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? SsoSessionIdleTimeoutRememberMe { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ssoSessionMaxLifespanRememberMe")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? SsoSessionMaxLifespanRememberMe { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("offlineSessionIdleTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? OfflineSessionIdleTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("offlineSessionMaxLifespanEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? OfflineSessionMaxLifespanEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("offlineSessionMaxLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? OfflineSessionMaxLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientSessionIdleTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? ClientSessionIdleTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientSessionMaxLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? ClientSessionMaxLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientOfflineSessionIdleTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? ClientOfflineSessionIdleTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientOfflineSessionMaxLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? ClientOfflineSessionMaxLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("accessCodeLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? AccessCodeLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("accessCodeLifespanUserAction")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? AccessCodeLifespanUserAction { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("accessCodeLifespanLogin")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? AccessCodeLifespanLogin { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("actionTokenGeneratedByAdminLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? ActionTokenGeneratedByAdminLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("actionTokenGeneratedByUserLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? ActionTokenGeneratedByUserLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("oauth2DeviceCodeLifespan")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Oauth2DeviceCodeLifespan { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("sslRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SslRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("passwordCredentialGrantAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? PasswordCredentialGrantAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registrationAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? RegistrationAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registrationEmailAsUsername")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? RegistrationEmailAsUsername { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("rememberMe")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? RememberMe { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("verifyEmail")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? VerifyEmail { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("loginWithEmailAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? LoginWithEmailAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("duplicateEmailsAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? DuplicateEmailsAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resetPasswordAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ResetPasswordAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("editUsernameAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? EditUsernameAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userCacheEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UserCacheEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("realmCacheEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? RealmCacheEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("bruteForceProtected")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? BruteForceProtected { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("permanentLockout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? PermanentLockout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("maxTemporaryLockouts")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? MaxTemporaryLockouts { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("maxFailureWaitSeconds")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? MaxFailureWaitSeconds { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("minimumQuickLoginWaitSeconds")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? MinimumQuickLoginWaitSeconds { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("waitIncrementSeconds")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? WaitIncrementSeconds { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("quickLoginCheckMilliSeconds")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? QuickLoginCheckMilliSeconds { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("maxDeltaTimeSeconds")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? MaxDeltaTimeSeconds { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("failureFactor")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? FailureFactor { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("privateKey")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? PrivateKey { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("publicKey")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? PublicKey { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("certificate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? Certificate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("codeSecret")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? CodeSecret { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("roles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public RolesRepresentation? Roles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("groups")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<GroupRepresentation>? Groups { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<string>? DefaultRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultRole")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public RoleRepresentation? DefaultRole { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultGroups")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? DefaultGroups { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("requiredCredentials")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<string>? RequiredCredentials { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("passwordPolicy")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? PasswordPolicy { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otpPolicyType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? OtpPolicyType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otpPolicyAlgorithm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? OtpPolicyAlgorithm { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otpPolicyInitialCounter")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? OtpPolicyInitialCounter { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otpPolicyDigits")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? OtpPolicyDigits { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otpPolicyLookAheadWindow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? OtpPolicyLookAheadWindow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otpPolicyPeriod")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? OtpPolicyPeriod { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otpPolicyCodeReusable")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? OtpPolicyCodeReusable { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("otpSupportedApplications")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? OtpSupportedApplications { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("localizationTexts")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.IDictionary<string, string>
    >? LocalizationTexts { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyRpEntityName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyRpEntityName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicySignatureAlgorithms")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebAuthnPolicySignatureAlgorithms { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyRpId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyRpId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName(
        "webAuthnPolicyAttestationConveyancePreference"
    )]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyAttestationConveyancePreference { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyAuthenticatorAttachment")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyAuthenticatorAttachment { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyRequireResidentKey")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyRequireResidentKey { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyUserVerificationRequirement")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyUserVerificationRequirement { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyCreateTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? WebAuthnPolicyCreateTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName(
        "webAuthnPolicyAvoidSameAuthenticatorRegister"
    )]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? WebAuthnPolicyAvoidSameAuthenticatorRegister { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyAcceptableAaguids")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebAuthnPolicyAcceptableAaguids { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyExtraOrigins")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebAuthnPolicyExtraOrigins { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyPasswordlessRpEntityName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyPasswordlessRpEntityName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName(
        "webAuthnPolicyPasswordlessSignatureAlgorithms"
    )]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebAuthnPolicyPasswordlessSignatureAlgorithms { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyPasswordlessRpId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyPasswordlessRpId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName(
        "webAuthnPolicyPasswordlessAttestationConveyancePreference"
    )]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyPasswordlessAttestationConveyancePreference { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName(
        "webAuthnPolicyPasswordlessAuthenticatorAttachment"
    )]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyPasswordlessAuthenticatorAttachment { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName(
        "webAuthnPolicyPasswordlessRequireResidentKey"
    )]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyPasswordlessRequireResidentKey { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName(
        "webAuthnPolicyPasswordlessUserVerificationRequirement"
    )]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? WebAuthnPolicyPasswordlessUserVerificationRequirement { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyPasswordlessCreateTimeout")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? WebAuthnPolicyPasswordlessCreateTimeout { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName(
        "webAuthnPolicyPasswordlessAvoidSameAuthenticatorRegister"
    )]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? WebAuthnPolicyPasswordlessAvoidSameAuthenticatorRegister { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyPasswordlessAcceptableAaguids")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebAuthnPolicyPasswordlessAcceptableAaguids { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("webAuthnPolicyPasswordlessExtraOrigins")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? WebAuthnPolicyPasswordlessExtraOrigins { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientProfiles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public ClientProfilesRepresentation? ClientProfiles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientPolicies")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public ClientPoliciesRepresentation? ClientPolicies { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("users")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UserRepresentation>? Users { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("federatedUsers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UserRepresentation>? FederatedUsers { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopeMappings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ScopeMappingRepresentation>? ScopeMappings { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientScopeMappings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<ScopeMappingRepresentation>
    >? ClientScopeMappings { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clients")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ClientRepresentation>? Clients { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ClientScopeRepresentation>? ClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultDefaultClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? DefaultDefaultClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultOptionalClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? DefaultOptionalClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("browserSecurityHeaders")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        string
    >? BrowserSecurityHeaders { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("smtpServer")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? SmtpServer { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("userFederationProviders")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UserFederationProviderRepresentation>? UserFederationProviders { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("userFederationMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UserFederationMapperRepresentation>? UserFederationMappers { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("loginTheme")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? LoginTheme { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("accountTheme")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AccountTheme { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("adminTheme")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? AdminTheme { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("emailTheme")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? EmailTheme { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("eventsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? EventsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("eventsExpiration")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? EventsExpiration { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("eventsListeners")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? EventsListeners { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabledEventTypes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? EnabledEventTypes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("adminEventsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AdminEventsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("adminEventsDetailsEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AdminEventsDetailsEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("identityProviders")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<IdentityProviderRepresentation>? IdentityProviders { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("identityProviderMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<IdentityProviderMapperRepresentation>? IdentityProviderMappers { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("protocolMappers")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ProtocolMapperRepresentation>? ProtocolMappers { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("components")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public MultivaluedHashMapStringComponentExportRepresentation? Components { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("internationalizationEnabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? InternationalizationEnabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("supportedLocales")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? SupportedLocales { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultLocale")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DefaultLocale { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticationFlows")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<AuthenticationFlowRepresentation>? AuthenticationFlows { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("authenticatorConfig")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<AuthenticatorConfigRepresentation>? AuthenticatorConfig { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("requiredActions")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<RequiredActionProviderRepresentation>? RequiredActions { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("browserFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? BrowserFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("registrationFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? RegistrationFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("directGrantFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DirectGrantFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resetCredentialsFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ResetCredentialsFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientAuthenticationFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientAuthenticationFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("dockerAuthenticationFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DockerAuthenticationFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("firstBrokerLoginFlow")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FirstBrokerLoginFlow { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Attributes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("keycloakVersion")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? KeycloakVersion { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userManagedAccessAllowed")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? UserManagedAccessAllowed { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("social")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? Social { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("updateProfileOnInitialSocialLogin")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? UpdateProfileOnInitialSocialLogin { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("socialProviders")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.IDictionary<string, string>? SocialProviders { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("applicationScopeMappings")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<ScopeMappingRepresentation>
    >? ApplicationScopeMappings { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("applications")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<ApplicationRepresentation>? Applications { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("oauthClients")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<OAuthClientRepresentation>? OauthClients { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientTemplates")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<ClientTemplateRepresentation>? ClientTemplates { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("oAuth2DevicePollingInterval")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? OAuth2DevicePollingInterval { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class RequiredActionProviderRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("alias")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Alias { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("defaultAction")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? DefaultAction { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("priority")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Priority { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ResourceOwnerRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ResourceRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("_id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? _id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("uris")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Uris { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("type")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Type { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ScopeRepresentation>? Scopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("icon_uri")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Icon_uri { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("owner")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public Owner? Owner { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ownerManagedAccess")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? OwnerManagedAccess { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? Attributes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("uri")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? Uri { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopesUma")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ScopeRepresentation>? ScopesUma { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ResourceServerRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("allowRemoteResourceManagement")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? AllowRemoteResourceManagement { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("policyEnforcementMode")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public PolicyEnforcementMode? PolicyEnforcementMode { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("resources")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ResourceRepresentation>? Resources { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("policies")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<PolicyRepresentation>? Policies { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ScopeRepresentation>? Scopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("decisionStrategy")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public DecisionStrategy? DecisionStrategy { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class RoleRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("description")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Description { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopeParamRequired")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public bool? ScopeParamRequired { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("composite")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Composite { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("composites")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public Composites? Composites { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientRole")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ClientRole { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("containerId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ContainerId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? Attributes { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class RolesRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("realm")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<RoleRepresentation>? Realm { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("client")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<RoleRepresentation>
    >? Client { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("application")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<RoleRepresentation>
    >? Application { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public enum ScopeEnforcementMode
{
    [System.Runtime.Serialization.EnumMember(Value = @"ALL")]
    ALL = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"ANY")]
    ANY = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"DISABLED")]
    DISABLED = 2,
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ScopeMappingRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("self")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Self { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("client")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Client { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientTemplate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public string? ClientTemplate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientScope")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientScope { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("roles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Roles { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class ScopeRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("iconUri")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? IconUri { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("policies")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<PolicyRepresentation>? Policies { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("resources")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<ResourceRepresentation>? Resources { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayName { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class SocialLinkRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("socialProvider")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SocialProvider { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("socialUserId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SocialUserId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("socialUsername")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? SocialUsername { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UPAttribute
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("validations")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.IDictionary<string, object>
    >? Validations { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("annotations")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? Annotations { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("required")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public UPAttributeRequired? Required { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("permissions")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public UPAttributePermissions? Permissions { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("selector")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public UPAttributeSelector? Selector { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("group")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Group { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("multivalued")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Multivalued { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UPAttributePermissions
{
    [System.Text.Json.Serialization.JsonPropertyName("view")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? View { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("edit")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Edit { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UPAttributeRequired
{
    [System.Text.Json.Serialization.JsonPropertyName("roles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Roles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Scopes { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UPAttributeSelector
{
    [System.Text.Json.Serialization.JsonPropertyName("scopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Scopes { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UPConfig
{
    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UPAttribute>? Attributes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("groups")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UPGroup>? Groups { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("unmanagedAttributePolicy")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Text.Json.Serialization.JsonConverter(
        typeof(System.Text.Json.Serialization.JsonStringEnumConverter)
    )]
    public UnmanagedAttributePolicy? UnmanagedAttributePolicy { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UPGroup
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayHeader")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayHeader { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayDescription")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayDescription { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("annotations")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? Annotations { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public enum UnmanagedAttributePolicy
{
    [System.Runtime.Serialization.EnumMember(Value = @"ENABLED")]
    ENABLED = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"ADMIN_VIEW")]
    ADMIN_VIEW = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"ADMIN_EDIT")]
    ADMIN_EDIT = 2,
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserConsentRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("clientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("grantedClientScopes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? GrantedClientScopes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("createdDate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? CreatedDate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("lastUpdatedDate")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? LastUpdatedDate { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("grantedRealmRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<string>? GrantedRealmRoles { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserFederationMapperRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("federationProviderDisplayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FederationProviderDisplayName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("federationMapperType")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FederationMapperType { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Config { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserFederationProviderRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("providerName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ProviderName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("config")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Config { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("priority")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? Priority { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("fullSyncPeriod")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? FullSyncPeriod { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("changedSyncPeriod")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? ChangedSyncPeriod { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("lastSync")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? LastSync { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserManagedAccessConfig
{
    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserProfileAttributeGroupMetadata
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayHeader")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayHeader { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayDescription")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayDescription { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("annotations")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? Annotations { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserProfileAttributeMetadata
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Name { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("displayName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? DisplayName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("required")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Required { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("readOnly")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? ReadOnly { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("annotations")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, object>? Annotations { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("validators")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.IDictionary<string, object>
    >? Validators { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("group")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Group { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("multivalued")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Multivalued { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserProfileMetadata
{
    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UserProfileAttributeMetadata>? Attributes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("groups")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UserProfileAttributeGroupMetadata>? Groups { get; set; } =
        default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Username { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("firstName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FirstName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("lastName")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? LastName { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("email")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Email { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("emailVerified")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? EmailVerified { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("attributes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? Attributes { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userProfileMetadata")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public UserProfileMetadata? UserProfileMetadata { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("self")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Self { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("origin")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Origin { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("createdTimestamp")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? CreatedTimestamp { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("enabled")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Enabled { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("totp")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? Totp { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("federationLink")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? FederationLink { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("serviceAccountClientId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? ServiceAccountClientId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("credentials")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<CredentialRepresentation>? Credentials { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("disableableCredentialTypes")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? DisableableCredentialTypes { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("requiredActions")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? RequiredActions { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("federatedIdentities")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<FederatedIdentityRepresentation>? FederatedIdentities { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("realmRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? RealmRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? ClientRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clientConsents")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<UserConsentRepresentation>? ClientConsents { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("notBefore")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public int? NotBefore { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("applicationRoles")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.IDictionary<
        string,
        System.Collections.Generic.ICollection<string>
    >? ApplicationRoles { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("socialLinks")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    [System.Obsolete]
    public System.Collections.Generic.ICollection<SocialLinkRepresentation>? SocialLinks { get; set; } =
        default!;

    [System.Text.Json.Serialization.JsonPropertyName("groups")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.ICollection<string>? Groups { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("access")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, bool>? Access { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class UserSessionRepresentation
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Id { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("username")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? Username { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("userId")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? UserId { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("ipAddress")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public string? IpAddress { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("start")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? Start { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("lastAccess")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public long? LastAccess { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("rememberMe")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public bool? RememberMe { get; set; } = default!;

    [System.Text.Json.Serialization.JsonPropertyName("clients")]
    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    )]
    public System.Collections.Generic.IDictionary<string, string>? Clients { get; set; } = default!;

    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
[System.Obsolete]
public partial class Claims : ClaimRepresentation { }

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
[System.Obsolete]
public partial class Config
    : System.Collections.Generic.Dictionary<
        string,
        System.Collections.ObjectModel.Collection<string>
    >
{
    private System.Collections.Generic.IDictionary<string, object>? _additionalProperties;

    [System.Text.Json.Serialization.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get
        {
            return _additionalProperties
                ?? (
                    _additionalProperties = new System.Collections.Generic.Dictionary<
                        string,
                        object
                    >()
                );
        }
        set { _additionalProperties = value; }
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
[System.Obsolete]
public partial class Claims2 : ClaimRepresentation { }

[System.CodeDom.Compiler.GeneratedCode(
    "NJsonSchema",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class Owner : ResourceOwnerRepresentation { }

[System.CodeDom.Compiler.GeneratedCode(
    "NSwag",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class FileParameter
{
    public FileParameter(System.IO.Stream data)
        : this(data, null, null) { }

    public FileParameter(System.IO.Stream data, string? fileName)
        : this(data, fileName, null) { }

    public FileParameter(System.IO.Stream data, string? fileName, string? contentType)
    {
        Data = data;
        FileName = fileName;
        ContentType = contentType;
    }

    public System.IO.Stream Data { get; private set; }

    public string? FileName { get; private set; }

    public string? ContentType { get; private set; }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NSwag",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class FileResponse : System.IDisposable
{
    private System.IDisposable? _client;
    private System.IDisposable? _response;

    public int StatusCode { get; private set; }

    public System.Collections.Generic.IReadOnlyDictionary<
        string,
        System.Collections.Generic.IEnumerable<string>
    > Headers { get; private set; }

    public System.IO.Stream Stream { get; private set; }

    public bool IsPartial
    {
        get { return StatusCode == 206; }
    }

    public FileResponse(
        int statusCode,
        System.Collections.Generic.IReadOnlyDictionary<
            string,
            System.Collections.Generic.IEnumerable<string>
        > headers,
        System.IO.Stream stream,
        System.IDisposable? client,
        System.IDisposable? response
    )
    {
        StatusCode = statusCode;
        Headers = headers;
        Stream = stream;
        _client = client;
        _response = response;
    }

    public void Dispose()
    {
        Stream.Dispose();
        if (_response != null)
            _response.Dispose();
        if (_client != null)
            _client.Dispose();
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NSwag",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class KeycloakException : System.Exception
{
    public int StatusCode { get; private set; }

    public string? Response { get; private set; }

    public System.Collections.Generic.IReadOnlyDictionary<
        string,
        System.Collections.Generic.IEnumerable<string>
    > Headers { get; private set; }

    public KeycloakException(
        string message,
        int statusCode,
        string? response,
        System.Collections.Generic.IReadOnlyDictionary<
            string,
            System.Collections.Generic.IEnumerable<string>
        > headers,
        System.Exception? innerException
    )
        : base(
            message
                + "\n\nStatus: "
                + statusCode
                + "\nResponse: \n"
                + (
                    (response == null)
                        ? "(null)"
                        : response.Substring(0, response.Length >= 512 ? 512 : response.Length)
                ),
            innerException
        )
    {
        StatusCode = statusCode;
        Response = response;
        Headers = headers;
    }

    public override string ToString()
    {
        return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
    }
}

[System.CodeDom.Compiler.GeneratedCode(
    "NSwag",
    "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))"
)]
public partial class KeycloakException<TResult> : KeycloakException
{
    public TResult Result { get; private set; }

    public KeycloakException(
        string message,
        int statusCode,
        string? response,
        System.Collections.Generic.IReadOnlyDictionary<
            string,
            System.Collections.Generic.IEnumerable<string>
        > headers,
        TResult result,
        System.Exception? innerException
    )
        : base(message, statusCode, response, headers, innerException)
    {
        Result = result;
    }
}

#pragma warning restore  108
#pragma warning restore  114
#pragma warning restore  472
#pragma warning restore  612
#pragma warning restore 1573
#pragma warning restore 1591
#pragma warning restore 8073
#pragma warning restore 3016
#pragma warning restore 8603
#pragma warning restore 8604
