// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Models {
    public class KeyStoreConfig : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The format property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Format { get; set; }
#nullable restore
#else
        public string Format { get; set; }
#endif
        /// <summary>The keyAlias property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? KeyAlias { get; set; }
#nullable restore
#else
        public string KeyAlias { get; set; }
#endif
        /// <summary>The keyPassword property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? KeyPassword { get; set; }
#nullable restore
#else
        public string KeyPassword { get; set; }
#endif
        /// <summary>The realmAlias property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RealmAlias { get; set; }
#nullable restore
#else
        public string RealmAlias { get; set; }
#endif
        /// <summary>The realmCertificate property</summary>
        public bool? RealmCertificate { get; set; }
        /// <summary>The storePassword property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? StorePassword { get; set; }
#nullable restore
#else
        public string StorePassword { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="KeyStoreConfig"/> and sets the default values.
        /// </summary>
        public KeyStoreConfig() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="KeyStoreConfig"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static KeyStoreConfig CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new KeyStoreConfig();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"format", n => { Format = n.GetStringValue(); } },
                {"keyAlias", n => { KeyAlias = n.GetStringValue(); } },
                {"keyPassword", n => { KeyPassword = n.GetStringValue(); } },
                {"realmAlias", n => { RealmAlias = n.GetStringValue(); } },
                {"realmCertificate", n => { RealmCertificate = n.GetBoolValue(); } },
                {"storePassword", n => { StorePassword = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("format", Format);
            writer.WriteStringValue("keyAlias", KeyAlias);
            writer.WriteStringValue("keyPassword", KeyPassword);
            writer.WriteStringValue("realmAlias", RealmAlias);
            writer.WriteBoolValue("realmCertificate", RealmCertificate);
            writer.WriteStringValue("storePassword", StorePassword);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
