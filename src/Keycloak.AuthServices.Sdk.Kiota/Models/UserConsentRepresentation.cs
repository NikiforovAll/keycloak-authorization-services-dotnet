// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models {
    public class UserConsentRepresentation : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The clientId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ClientId { get; set; }
#nullable restore
#else
        public string ClientId { get; set; }
#endif
        /// <summary>The createdDate property</summary>
        public long? CreatedDate { get; set; }
        /// <summary>The grantedClientScopes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? GrantedClientScopes { get; set; }
#nullable restore
#else
        public List<string> GrantedClientScopes { get; set; }
#endif
        /// <summary>The grantedRealmRoles property</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? GrantedRealmRoles { get; set; }
#nullable restore
#else
        public List<string> GrantedRealmRoles { get; set; }
#endif
        /// <summary>The lastUpdatedDate property</summary>
        public long? LastUpdatedDate { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="UserConsentRepresentation"/> and sets the default values.
        /// </summary>
        public UserConsentRepresentation() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="UserConsentRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static UserConsentRepresentation CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new UserConsentRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"clientId", n => { ClientId = n.GetStringValue(); } },
                {"createdDate", n => { CreatedDate = n.GetLongValue(); } },
                {"grantedClientScopes", n => { GrantedClientScopes = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                {"grantedRealmRoles", n => { GrantedRealmRoles = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                {"lastUpdatedDate", n => { LastUpdatedDate = n.GetLongValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("clientId", ClientId);
            writer.WriteLongValue("createdDate", CreatedDate);
            writer.WriteCollectionOfPrimitiveValues<string>("grantedClientScopes", GrantedClientScopes);
            writer.WriteCollectionOfPrimitiveValues<string>("grantedRealmRoles", GrantedRealmRoles);
            writer.WriteLongValue("lastUpdatedDate", LastUpdatedDate);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}