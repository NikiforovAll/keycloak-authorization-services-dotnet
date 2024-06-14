// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models
{
    #pragma warning disable CS1591
    public class AuthenticationExecutionInfoRepresentation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The alias property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Alias { get; set; }
#nullable restore
#else
        public string Alias { get; set; }
#endif
        /// <summary>The authenticationConfig property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AuthenticationConfig { get; set; }
#nullable restore
#else
        public string AuthenticationConfig { get; set; }
#endif
        /// <summary>The authenticationFlow property</summary>
        public bool? AuthenticationFlow { get; set; }
        /// <summary>The configurable property</summary>
        public bool? Configurable { get; set; }
        /// <summary>The description property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The displayName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DisplayName { get; set; }
#nullable restore
#else
        public string DisplayName { get; set; }
#endif
        /// <summary>The flowId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FlowId { get; set; }
#nullable restore
#else
        public string FlowId { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The index property</summary>
        public int? Index { get; set; }
        /// <summary>The level property</summary>
        public int? Level { get; set; }
        /// <summary>The priority property</summary>
        public int? Priority { get; set; }
        /// <summary>The providerId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ProviderId { get; set; }
#nullable restore
#else
        public string ProviderId { get; set; }
#endif
        /// <summary>The requirement property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Requirement { get; set; }
#nullable restore
#else
        public string Requirement { get; set; }
#endif
        /// <summary>The requirementChoices property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RequirementChoices { get; set; }
#nullable restore
#else
        public List<string> RequirementChoices { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AuthenticationExecutionInfoRepresentation"/> and sets the default values.
        /// </summary>
        public AuthenticationExecutionInfoRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AuthenticationExecutionInfoRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AuthenticationExecutionInfoRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Keycloak.AuthServices.Sdk.Kiota.Admin.Models.AuthenticationExecutionInfoRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "alias", n => { Alias = n.GetStringValue(); } },
                { "authenticationConfig", n => { AuthenticationConfig = n.GetStringValue(); } },
                { "authenticationFlow", n => { AuthenticationFlow = n.GetBoolValue(); } },
                { "configurable", n => { Configurable = n.GetBoolValue(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "displayName", n => { DisplayName = n.GetStringValue(); } },
                { "flowId", n => { FlowId = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "index", n => { Index = n.GetIntValue(); } },
                { "level", n => { Level = n.GetIntValue(); } },
                { "priority", n => { Priority = n.GetIntValue(); } },
                { "providerId", n => { ProviderId = n.GetStringValue(); } },
                { "requirement", n => { Requirement = n.GetStringValue(); } },
                { "requirementChoices", n => { RequirementChoices = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("alias", Alias);
            writer.WriteStringValue("authenticationConfig", AuthenticationConfig);
            writer.WriteBoolValue("authenticationFlow", AuthenticationFlow);
            writer.WriteBoolValue("configurable", Configurable);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteStringValue("flowId", FlowId);
            writer.WriteStringValue("id", Id);
            writer.WriteIntValue("index", Index);
            writer.WriteIntValue("level", Level);
            writer.WriteIntValue("priority", Priority);
            writer.WriteStringValue("providerId", ProviderId);
            writer.WriteStringValue("requirement", Requirement);
            writer.WriteCollectionOfPrimitiveValues<string>("requirementChoices", RequirementChoices);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
