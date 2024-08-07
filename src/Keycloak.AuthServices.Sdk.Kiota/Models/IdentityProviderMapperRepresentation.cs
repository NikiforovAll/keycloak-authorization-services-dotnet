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
    public partial class IdentityProviderMapperRepresentation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The config property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation_config? Config { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation_config Config { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The identityProviderAlias property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityProviderAlias { get; set; }
#nullable restore
#else
        public string IdentityProviderAlias { get; set; }
#endif
        /// <summary>The identityProviderMapper property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdentityProviderMapper { get; set; }
#nullable restore
#else
        public string IdentityProviderMapper { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation"/> and sets the default values.
        /// </summary>
        public IdentityProviderMapperRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "config", n => { Config = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation_config>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation_config.CreateFromDiscriminatorValue); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "identityProviderAlias", n => { IdentityProviderAlias = n.GetStringValue(); } },
                { "identityProviderMapper", n => { IdentityProviderMapper = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderMapperRepresentation_config>("config", Config);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("identityProviderAlias", IdentityProviderAlias);
            writer.WriteStringValue("identityProviderMapper", IdentityProviderMapper);
            writer.WriteStringValue("name", Name);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
