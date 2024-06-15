// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models
{
    #pragma warning disable CS1591
    public class ResourceRepresentation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The attributes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_attributes? Attributes { get; set; }
#nullable restore
#else
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_attributes Attributes { get; set; }
#endif
        /// <summary>The displayName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>The icon_uri property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IconUri { get; set; }
#nullable restore
#else
        public string IconUri { get; set; }
#endif
        /// <summary>The _id property</summary>
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
        /// <summary>The owner property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_owner? Owner { get; private set; }
#nullable restore
#else
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_owner Owner { get; private set; }
#endif
        /// <summary>The ownerManagedAccess property</summary>
        public bool? OwnerManagedAccess { get; set; }
        /// <summary>The scopes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation>? Scopes { get; set; }
#nullable restore
#else
        public List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation> Scopes { get; set; }
#endif
        /// <summary>The scopesUma property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation>? ScopesUma { get; set; }
#nullable restore
#else
        public List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation> ScopesUma { get; set; }
#endif
        /// <summary>The type property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Type { get; set; }
#nullable restore
#else
        public string Type { get; set; }
#endif
        /// <summary>The uri property</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Uri { get; set; }
#nullable restore
#else
        public string Uri { get; set; }
#endif
        /// <summary>The uris property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Uris { get; set; }
#nullable restore
#else
        public List<string> Uris { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation"/> and sets the default values.
        /// </summary>
        public ResourceRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "attributes", n => { Attributes = n.GetObjectValue<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_attributes>(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_attributes.CreateFromDiscriminatorValue); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "icon_uri", n => { IconUri = n.GetStringValue(); } },
                { "_id", n => { Id = n.GetStringValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "owner", n => { Owner = n.GetObjectValue<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_owner>(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_owner.CreateFromDiscriminatorValue); } },
                { "ownerManagedAccess", n => { OwnerManagedAccess = n.GetBoolValue(); } },
                { "scopes", n => { Scopes = n.GetCollectionOfObjectValues<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation>(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation.CreateFromDiscriminatorValue)?.ToList(); } },
                { "scopesUma", n => { ScopesUma = n.GetCollectionOfObjectValues<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation>(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation.CreateFromDiscriminatorValue)?.ToList(); } },
                { "type", n => { Type = n.GetStringValue(); } },
                { "uri", n => { Uri = n.GetStringValue(); } },
                { "uris", n => { Uris = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceRepresentation_attributes>("attributes", Attributes);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteStringValue("icon_uri", IconUri);
            writer.WriteStringValue("_id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteBoolValue("ownerManagedAccess", OwnerManagedAccess);
            writer.WriteCollectionOfObjectValues<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation>("scopes", Scopes);
            writer.WriteCollectionOfObjectValues<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ScopeRepresentation>("scopesUma", ScopesUma);
            writer.WriteStringValue("type", Type);
            writer.WriteStringValue("uri", Uri);
            writer.WriteCollectionOfPrimitiveValues<string>("uris", Uris);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
