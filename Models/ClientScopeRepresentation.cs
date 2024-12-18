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
    public partial class ClientScopeRepresentation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The attributes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation_attributes? Attributes { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation_attributes Attributes { get; set; }
#endif
        /// <summary>The description property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
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
        /// <summary>The protocol property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Protocol { get; set; }
#nullable restore
#else
        public string Protocol { get; set; }
#endif
        /// <summary>The protocolMappers property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation>? ProtocolMappers { get; set; }
#nullable restore
#else
        public List<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation> ProtocolMappers { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation"/> and sets the default values.
        /// </summary>
        public ClientScopeRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "attributes", n => { Attributes = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation_attributes>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation_attributes.CreateFromDiscriminatorValue); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "protocol", n => { Protocol = n.GetStringValue(); } },
                { "protocolMappers", n => { ProtocolMappers = n.GetCollectionOfObjectValues<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation.CreateFromDiscriminatorValue)?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ClientScopeRepresentation_attributes>("attributes", Attributes);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteStringValue("protocol", Protocol);
            writer.WriteCollectionOfObjectValues<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation>("protocolMappers", ProtocolMappers);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
