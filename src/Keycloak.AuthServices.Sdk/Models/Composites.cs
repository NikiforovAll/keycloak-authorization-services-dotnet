// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Models {
    public class Composites : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The application property</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Composites_application? Application { get; set; }
#nullable restore
#else
        public Composites_application Application { get; set; }
#endif
        /// <summary>The client property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Composites_client? Client { get; set; }
#nullable restore
#else
        public Composites_client Client { get; set; }
#endif
        /// <summary>The realm property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Realm { get; set; }
#nullable restore
#else
        public List<string> Realm { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="Composites"/> and sets the default values.
        /// </summary>
        public Composites() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="Composites"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Composites CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Composites();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"application", n => { Application = n.GetObjectValue<Composites_application>(Composites_application.CreateFromDiscriminatorValue); } },
                {"client", n => { Client = n.GetObjectValue<Composites_client>(Composites_client.CreateFromDiscriminatorValue); } },
                {"realm", n => { Realm = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<Composites_application>("application", Application);
            writer.WriteObjectValue<Composites_client>("client", Client);
            writer.WriteCollectionOfPrimitiveValues<string>("realm", Realm);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
