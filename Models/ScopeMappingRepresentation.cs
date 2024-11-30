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
    public partial class ScopeMappingRepresentation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The client property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Client { get; set; }
#nullable restore
#else
        public string Client { get; set; }
#endif
        /// <summary>The clientScope property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ClientScope { get; set; }
#nullable restore
#else
        public string ClientScope { get; set; }
#endif
        /// <summary>The clientTemplate property</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ClientTemplate { get; set; }
#nullable restore
#else
        public string ClientTemplate { get; set; }
#endif
        /// <summary>The roles property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Roles { get; set; }
#nullable restore
#else
        public List<string> Roles { get; set; }
#endif
        /// <summary>The self property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Self { get; set; }
#nullable restore
#else
        public string Self { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeMappingRepresentation"/> and sets the default values.
        /// </summary>
        public ScopeMappingRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeMappingRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeMappingRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeMappingRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "client", n => { Client = n.GetStringValue(); } },
                { "clientScope", n => { ClientScope = n.GetStringValue(); } },
                { "clientTemplate", n => { ClientTemplate = n.GetStringValue(); } },
                { "roles", n => { Roles = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "self", n => { Self = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("client", Client);
            writer.WriteStringValue("clientScope", ClientScope);
            writer.WriteStringValue("clientTemplate", ClientTemplate);
            writer.WriteCollectionOfPrimitiveValues<string>("roles", Roles);
            writer.WriteStringValue("self", Self);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618