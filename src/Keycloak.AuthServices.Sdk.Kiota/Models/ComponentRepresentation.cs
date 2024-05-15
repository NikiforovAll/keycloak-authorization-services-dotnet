// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models {
    #pragma warning disable CS1591
    public class ComponentRepresentation : IAdditionalDataHolder, IParsable 
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The config property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public MultivaluedHashMapStringString? Config { get; set; }
#nullable restore
#else
        public MultivaluedHashMapStringString Config { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The parentId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ParentId { get; set; }
#nullable restore
#else
        public string ParentId { get; set; }
#endif
        /// <summary>The providerId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ProviderId { get; set; }
#nullable restore
#else
        public string ProviderId { get; set; }
#endif
        /// <summary>The providerType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ProviderType { get; set; }
#nullable restore
#else
        public string ProviderType { get; set; }
#endif
        /// <summary>The subType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? SubType { get; set; }
#nullable restore
#else
        public string SubType { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="ComponentRepresentation"/> and sets the default values.
        /// </summary>
        public ComponentRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ComponentRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ComponentRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ComponentRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"config", n => { Config = n.GetObjectValue<MultivaluedHashMapStringString>(MultivaluedHashMapStringString.CreateFromDiscriminatorValue); } },
                {"id", n => { Id = n.GetStringValue(); } },
                {"name", n => { Name = n.GetStringValue(); } },
                {"parentId", n => { ParentId = n.GetStringValue(); } },
                {"providerId", n => { ProviderId = n.GetStringValue(); } },
                {"providerType", n => { ProviderType = n.GetStringValue(); } },
                {"subType", n => { SubType = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<MultivaluedHashMapStringString>("config", Config);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("parentId", ParentId);
            writer.WriteStringValue("providerId", ProviderId);
            writer.WriteStringValue("providerType", ProviderType);
            writer.WriteStringValue("subType", SubType);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
