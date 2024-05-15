// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models {
    #pragma warning disable CS1591
    public class IDToken : IAdditionalDataHolder, IParsable 
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
        public AddressClaimSet? Address { get; set; }
#nullable restore
#else
        public AddressClaimSet Address { get; set; }
#endif
        /// <summary>The at_hash property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AtHash { get; set; }
#nullable restore
#else
        public string AtHash { get; set; }
#endif
        /// <summary>The auth_time property</summary>
        public long? Auth_time { get; set; }
        /// <summary>The authTime property</summary>
        [Obsolete("")]
        public int? AuthTime { get; set; }
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
        public IDToken_otherClaims? OtherClaims { get; set; }
#nullable restore
#else
        public IDToken_otherClaims OtherClaims { get; set; }
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
        /// <summary>The session_state property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SessionState { get; set; }
#nullable restore
#else
        public string SessionState { get; set; }
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
        /// Instantiates a new <see cref="IDToken"/> and sets the default values.
        /// </summary>
        public IDToken()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="IDToken"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static IDToken CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new IDToken();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"acr", n => { Acr = n.GetStringValue(); } },
                {"address", n => { Address = n.GetObjectValue<AddressClaimSet>(AddressClaimSet.CreateFromDiscriminatorValue); } },
                {"at_hash", n => { AtHash = n.GetStringValue(); } },
                {"authTime", n => { AuthTime = n.GetIntValue(); } },
                {"azp", n => { Azp = n.GetStringValue(); } },
                {"birthdate", n => { Birthdate = n.GetStringValue(); } },
                {"c_hash", n => { CHash = n.GetStringValue(); } },
                {"claims_locales", n => { ClaimsLocales = n.GetStringValue(); } },
                {"email", n => { Email = n.GetStringValue(); } },
                {"email_verified", n => { EmailVerified = n.GetBoolValue(); } },
                {"exp", n => { Exp = n.GetLongValue(); } },
                {"family_name", n => { FamilyName = n.GetStringValue(); } },
                {"gender", n => { Gender = n.GetStringValue(); } },
                {"given_name", n => { GivenName = n.GetStringValue(); } },
                {"iat", n => { Iat = n.GetLongValue(); } },
                {"iss", n => { Iss = n.GetStringValue(); } },
                {"jti", n => { Jti = n.GetStringValue(); } },
                {"locale", n => { Locale = n.GetStringValue(); } },
                {"middle_name", n => { MiddleName = n.GetStringValue(); } },
                {"name", n => { Name = n.GetStringValue(); } },
                {"nbf", n => { Nbf = n.GetLongValue(); } },
                {"nickname", n => { Nickname = n.GetStringValue(); } },
                {"nonce", n => { Nonce = n.GetStringValue(); } },
                {"otherClaims", n => { OtherClaims = n.GetObjectValue<IDToken_otherClaims>(IDToken_otherClaims.CreateFromDiscriminatorValue); } },
                {"phone_number", n => { PhoneNumber = n.GetStringValue(); } },
                {"phone_number_verified", n => { PhoneNumberVerified = n.GetBoolValue(); } },
                {"picture", n => { Picture = n.GetStringValue(); } },
                {"preferred_username", n => { PreferredUsername = n.GetStringValue(); } },
                {"profile", n => { Profile = n.GetStringValue(); } },
                {"s_hash", n => { SHash = n.GetStringValue(); } },
                {"session_state", n => { SessionState = n.GetStringValue(); } },
                {"sid", n => { Sid = n.GetStringValue(); } },
                {"sub", n => { Sub = n.GetStringValue(); } },
                {"typ", n => { Typ = n.GetStringValue(); } },
                {"updated_at", n => { UpdatedAt = n.GetLongValue(); } },
                {"website", n => { Website = n.GetStringValue(); } },
                {"zoneinfo", n => { Zoneinfo = n.GetStringValue(); } },
                {"auth_time", n => { Auth_time = n.GetLongValue(); } },
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
            writer.WriteObjectValue<AddressClaimSet>("address", Address);
            writer.WriteStringValue("at_hash", AtHash);
            writer.WriteLongValue("auth_time", Auth_time);
            writer.WriteIntValue("authTime", AuthTime);
            writer.WriteStringValue("azp", Azp);
            writer.WriteStringValue("birthdate", Birthdate);
            writer.WriteStringValue("c_hash", CHash);
            writer.WriteStringValue("claims_locales", ClaimsLocales);
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
            writer.WriteObjectValue<IDToken_otherClaims>("otherClaims", OtherClaims);
            writer.WriteStringValue("phone_number", PhoneNumber);
            writer.WriteBoolValue("phone_number_verified", PhoneNumberVerified);
            writer.WriteStringValue("picture", Picture);
            writer.WriteStringValue("preferred_username", PreferredUsername);
            writer.WriteStringValue("profile", Profile);
            writer.WriteStringValue("session_state", SessionState);
            writer.WriteStringValue("s_hash", SHash);
            writer.WriteStringValue("sid", Sid);
            writer.WriteStringValue("sub", Sub);
            writer.WriteStringValue("typ", Typ);
            writer.WriteLongValue("updated_at", UpdatedAt);
            writer.WriteStringValue("website", Website);
            writer.WriteStringValue("zoneinfo", Zoneinfo);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
