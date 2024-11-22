// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class AccessToken : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The acr property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Acr { get; set; }
#nullable restore
#else
        public string Acr { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The address property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AddressClaimSet? Address { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AddressClaimSet Address { get; set; }
#endif
        /// <summary>The allowedOrigins property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? AllowedOrigins { get; set; }
#nullable restore
#else
        public List<string> AllowedOrigins { get; set; }
#endif
        /// <summary>The at_hash property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AtHash { get; set; }
#nullable restore
#else
        public string AtHash { get; set; }
#endif
        /// <summary>The authorization property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Authorization? Authorization { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Authorization Authorization { get; set; }
#endif
        /// <summary>The auth_time property</summary>
        public long? AuthTime { get; set; }
        /// <summary>The azp property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Azp { get; set; }
#nullable restore
#else
        public string Azp { get; set; }
#endif
        /// <summary>The birthdate property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Birthdate { get; set; }
#nullable restore
#else
        public string Birthdate { get; set; }
#endif
        /// <summary>The c_hash property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? CHash { get; set; }
#nullable restore
#else
        public string CHash { get; set; }
#endif
        /// <summary>The claims_locales property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ClaimsLocales { get; set; }
#nullable restore
#else
        public string ClaimsLocales { get; set; }
#endif
        /// <summary>The cnf property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Confirmation? Cnf { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Confirmation Cnf { get; set; }
#endif
        /// <summary>The email property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Email { get; set; }
#nullable restore
#else
        public string Email { get; set; }
#endif
        /// <summary>The email_verified property</summary>
        public bool? EmailVerified { get; set; }
        /// <summary>The exp property</summary>
        public long? Exp { get; set; }
        /// <summary>The family_name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FamilyName { get; set; }
#nullable restore
#else
        public string FamilyName { get; set; }
#endif
        /// <summary>The gender property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Gender { get; set; }
#nullable restore
#else
        public string Gender { get; set; }
#endif
        /// <summary>The given_name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? GivenName { get; set; }
#nullable restore
#else
        public string GivenName { get; set; }
#endif
        /// <summary>The iat property</summary>
        public long? Iat { get; set; }
        /// <summary>The iss property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Iss { get; set; }
#nullable restore
#else
        public string Iss { get; set; }
#endif
        /// <summary>The jti property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Jti { get; set; }
#nullable restore
#else
        public string Jti { get; set; }
#endif
        /// <summary>The locale property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Locale { get; set; }
#nullable restore
#else
        public string Locale { get; set; }
#endif
        /// <summary>The middle_name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? MiddleName { get; set; }
#nullable restore
#else
        public string MiddleName { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The nbf property</summary>
        public long? Nbf { get; set; }
        /// <summary>The nickname property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Nickname { get; set; }
#nullable restore
#else
        public string Nickname { get; set; }
#endif
        /// <summary>The nonce property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Nonce { get; set; }
#nullable restore
#else
        public string Nonce { get; set; }
#endif
        /// <summary>The otherClaims property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_otherClaims? OtherClaims { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_otherClaims OtherClaims { get; set; }
#endif
        /// <summary>The phone_number property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PhoneNumber { get; set; }
#nullable restore
#else
        public string PhoneNumber { get; set; }
#endif
        /// <summary>The phone_number_verified property</summary>
        public bool? PhoneNumberVerified { get; set; }
        /// <summary>The picture property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Picture { get; set; }
#nullable restore
#else
        public string Picture { get; set; }
#endif
        /// <summary>The preferred_username property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PreferredUsername { get; set; }
#nullable restore
#else
        public string PreferredUsername { get; set; }
#endif
        /// <summary>The profile property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Profile { get; set; }
#nullable restore
#else
        public string Profile { get; set; }
#endif
        /// <summary>The realm_access property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Access? RealmAccess { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Access RealmAccess { get; set; }
#endif
        /// <summary>The resource_access property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_resource_access? ResourceAccess { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_resource_access ResourceAccess { get; set; }
#endif
        /// <summary>The scope property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Scope { get; set; }
#nullable restore
#else
        public string Scope { get; set; }
#endif
        /// <summary>The s_hash property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SHash { get; set; }
#nullable restore
#else
        public string SHash { get; set; }
#endif
        /// <summary>The sid property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Sid { get; set; }
#nullable restore
#else
        public string Sid { get; set; }
#endif
        /// <summary>The sub property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Sub { get; set; }
#nullable restore
#else
        public string Sub { get; set; }
#endif
        /// <summary>The trustedCerts property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? TrustedCerts { get; set; }
#nullable restore
#else
        public List<string> TrustedCerts { get; set; }
#endif
        /// <summary>The typ property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Typ { get; set; }
#nullable restore
#else
        public string Typ { get; set; }
#endif
        /// <summary>The updated_at property</summary>
        public long? UpdatedAt { get; set; }
        /// <summary>The website property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Website { get; set; }
#nullable restore
#else
        public string Website { get; set; }
#endif
        /// <summary>The zoneinfo property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Zoneinfo { get; set; }
#nullable restore
#else
        public string Zoneinfo { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken"/> and sets the default values.
        /// </summary>
        public AccessToken()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "acr", n => { Acr = n.GetStringValue(); } },
                { "address", n => { Address = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AddressClaimSet>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AddressClaimSet.CreateFromDiscriminatorValue); } },
                { "allowed-origins", n => { AllowedOrigins = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "at_hash", n => { AtHash = n.GetStringValue(); } },
                { "auth_time", n => { AuthTime = n.GetLongValue(); } },
                { "authorization", n => { Authorization = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Authorization>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Authorization.CreateFromDiscriminatorValue); } },
                { "azp", n => { Azp = n.GetStringValue(); } },
                { "birthdate", n => { Birthdate = n.GetStringValue(); } },
                { "c_hash", n => { CHash = n.GetStringValue(); } },
                { "claims_locales", n => { ClaimsLocales = n.GetStringValue(); } },
                { "cnf", n => { Cnf = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Confirmation>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Confirmation.CreateFromDiscriminatorValue); } },
                { "email", n => { Email = n.GetStringValue(); } },
                { "email_verified", n => { EmailVerified = n.GetBoolValue(); } },
                { "exp", n => { Exp = n.GetLongValue(); } },
                { "family_name", n => { FamilyName = n.GetStringValue(); } },
                { "gender", n => { Gender = n.GetStringValue(); } },
                { "given_name", n => { GivenName = n.GetStringValue(); } },
                { "iat", n => { Iat = n.GetLongValue(); } },
                { "iss", n => { Iss = n.GetStringValue(); } },
                { "jti", n => { Jti = n.GetStringValue(); } },
                { "locale", n => { Locale = n.GetStringValue(); } },
                { "middle_name", n => { MiddleName = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nbf", n => { Nbf = n.GetLongValue(); } },
                { "nickname", n => { Nickname = n.GetStringValue(); } },
                { "nonce", n => { Nonce = n.GetStringValue(); } },
                { "otherClaims", n => { OtherClaims = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_otherClaims>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_otherClaims.CreateFromDiscriminatorValue); } },
                { "phone_number", n => { PhoneNumber = n.GetStringValue(); } },
                { "phone_number_verified", n => { PhoneNumberVerified = n.GetBoolValue(); } },
                { "picture", n => { Picture = n.GetStringValue(); } },
                { "preferred_username", n => { PreferredUsername = n.GetStringValue(); } },
                { "profile", n => { Profile = n.GetStringValue(); } },
                { "realm_access", n => { RealmAccess = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Access>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Access.CreateFromDiscriminatorValue); } },
                { "resource_access", n => { ResourceAccess = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_resource_access>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_resource_access.CreateFromDiscriminatorValue); } },
                { "s_hash", n => { SHash = n.GetStringValue(); } },
                { "scope", n => { Scope = n.GetStringValue(); } },
                { "sid", n => { Sid = n.GetStringValue(); } },
                { "sub", n => { Sub = n.GetStringValue(); } },
                { "trusted-certs", n => { TrustedCerts = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "typ", n => { Typ = n.GetStringValue(); } },
                { "updated_at", n => { UpdatedAt = n.GetLongValue(); } },
                { "website", n => { Website = n.GetStringValue(); } },
                { "zoneinfo", n => { Zoneinfo = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("acr", Acr);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AddressClaimSet>("address", Address);
            writer.WriteCollectionOfPrimitiveValues<string>("allowed-origins", AllowedOrigins);
            writer.WriteStringValue("at_hash", AtHash);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Authorization>("authorization", Authorization);
            writer.WriteLongValue("auth_time", AuthTime);
            writer.WriteStringValue("azp", Azp);
            writer.WriteStringValue("birthdate", Birthdate);
            writer.WriteStringValue("c_hash", CHash);
            writer.WriteStringValue("claims_locales", ClaimsLocales);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Confirmation>("cnf", Cnf);
            writer.WriteStringValue("email", Email);
            writer.WriteBoolValue("email_verified", EmailVerified);
            writer.WriteLongValue("exp", Exp);
            writer.WriteStringValue("family_name", FamilyName);
            writer.WriteStringValue("gender", Gender);
            writer.WriteStringValue("given_name", GivenName);
            writer.WriteLongValue("iat", Iat);
            writer.WriteStringValue("iss", Iss);
            writer.WriteStringValue("jti", Jti);
            writer.WriteStringValue("locale", Locale);
            writer.WriteStringValue("middle_name", MiddleName);
            writer.WriteStringValue("name", Name);
            writer.WriteLongValue("nbf", Nbf);
            writer.WriteStringValue("nickname", Nickname);
            writer.WriteStringValue("nonce", Nonce);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_otherClaims>("otherClaims", OtherClaims);
            writer.WriteStringValue("phone_number", PhoneNumber);
            writer.WriteBoolValue("phone_number_verified", PhoneNumberVerified);
            writer.WriteStringValue("picture", Picture);
            writer.WriteStringValue("preferred_username", PreferredUsername);
            writer.WriteStringValue("profile", Profile);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.Access>("realm_access", RealmAccess);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AccessToken_resource_access>("resource_access", ResourceAccess);
            writer.WriteStringValue("scope", Scope);
            writer.WriteStringValue("s_hash", SHash);
            writer.WriteStringValue("sid", Sid);
            writer.WriteStringValue("sub", Sub);
            writer.WriteCollectionOfPrimitiveValues<string>("trusted-certs", TrustedCerts);
            writer.WriteStringValue("typ", Typ);
            writer.WriteLongValue("updated_at", UpdatedAt);
            writer.WriteStringValue("website", Website);
            writer.WriteStringValue("zoneinfo", Zoneinfo);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
