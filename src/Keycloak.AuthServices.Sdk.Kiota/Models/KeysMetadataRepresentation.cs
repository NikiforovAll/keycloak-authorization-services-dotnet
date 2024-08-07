// <auto-generated/>
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    #pragma warning disable CS1591
    public partial class KeysMetadataRepresentation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The active property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation_active? Active { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation_active Active { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The keys property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeyMetadataRepresentation>? Keys { get; set; }
#nullable restore
#else
        public List<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeyMetadataRepresentation> Keys { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation"/> and sets the default values.
        /// </summary>
        public KeysMetadataRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "active", n => { Active = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation_active>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation_active.CreateFromDiscriminatorValue); } },
                { "keys", n => { Keys = n.GetCollectionOfObjectValues<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeyMetadataRepresentation>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeyMetadataRepresentation.CreateFromDiscriminatorValue)?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeysMetadataRepresentation_active>("active", Active);
            writer.WriteCollectionOfObjectValues<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.KeyMetadataRepresentation>("keys", Keys);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
