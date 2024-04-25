// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Models {
    public class AuthenticationExecutionExportRepresentation : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The authenticator property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Authenticator { get; set; }
#nullable restore
#else
        public string Authenticator { get; set; }
#endif
        /// <summary>The authenticatorConfig property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AuthenticatorConfig { get; set; }
#nullable restore
#else
        public string AuthenticatorConfig { get; set; }
#endif
        /// <summary>The authenticatorFlow property</summary>
        public bool? AuthenticatorFlow { get; set; }
        /// <summary>The autheticatorFlow property</summary>
        [Obsolete("")]
        public bool? AutheticatorFlow { get; set; }
        /// <summary>The flowAlias property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FlowAlias { get; set; }
#nullable restore
#else
        public string FlowAlias { get; set; }
#endif
        /// <summary>The priority property</summary>
        public int? Priority { get; set; }
        /// <summary>The requirement property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Requirement { get; set; }
#nullable restore
#else
        public string Requirement { get; set; }
#endif
        /// <summary>The userSetupAllowed property</summary>
        public bool? UserSetupAllowed { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="AuthenticationExecutionExportRepresentation"/> and sets the default values.
        /// </summary>
        public AuthenticationExecutionExportRepresentation() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="AuthenticationExecutionExportRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static AuthenticationExecutionExportRepresentation CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new AuthenticationExecutionExportRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"authenticator", n => { Authenticator = n.GetStringValue(); } },
                {"authenticatorConfig", n => { AuthenticatorConfig = n.GetStringValue(); } },
                {"authenticatorFlow", n => { AuthenticatorFlow = n.GetBoolValue(); } },
                {"autheticatorFlow", n => { AutheticatorFlow = n.GetBoolValue(); } },
                {"flowAlias", n => { FlowAlias = n.GetStringValue(); } },
                {"priority", n => { Priority = n.GetIntValue(); } },
                {"requirement", n => { Requirement = n.GetStringValue(); } },
                {"userSetupAllowed", n => { UserSetupAllowed = n.GetBoolValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("authenticator", Authenticator);
            writer.WriteStringValue("authenticatorConfig", AuthenticatorConfig);
            writer.WriteBoolValue("authenticatorFlow", AuthenticatorFlow);
            writer.WriteBoolValue("autheticatorFlow", AutheticatorFlow);
            writer.WriteStringValue("flowAlias", FlowAlias);
            writer.WriteIntValue("priority", Priority);
            writer.WriteStringValue("requirement", Requirement);
            writer.WriteBoolValue("userSetupAllowed", UserSetupAllowed);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
