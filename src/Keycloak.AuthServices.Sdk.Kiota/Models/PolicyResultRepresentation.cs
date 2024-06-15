// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models
{
    #pragma warning disable CS1591
    public class PolicyResultRepresentation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The associatedPolicies property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation>? AssociatedPolicies { get; set; }
#nullable restore
#else
        public List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation> AssociatedPolicies { get; set; }
#endif
        /// <summary>The policy property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyRepresentation? Policy { get; set; }
#nullable restore
#else
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyRepresentation Policy { get; set; }
#endif
        /// <summary>The scopes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Scopes { get; set; }
#nullable restore
#else
        public List<string> Scopes { get; set; }
#endif
        /// <summary>The status property</summary>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Models.DecisionEffect? Status { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation"/> and sets the default values.
        /// </summary>
        public PolicyResultRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "associatedPolicies", n => { AssociatedPolicies = n.GetCollectionOfObjectValues<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation>(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation.CreateFromDiscriminatorValue)?.ToList(); } },
                { "policy", n => { Policy = n.GetObjectValue<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyRepresentation>(Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyRepresentation.CreateFromDiscriminatorValue); } },
                { "scopes", n => { Scopes = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                { "status", n => { Status = n.GetEnumValue<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.DecisionEffect>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyResultRepresentation>("associatedPolicies", AssociatedPolicies);
            writer.WriteObjectValue<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.PolicyRepresentation>("policy", Policy);
            writer.WriteCollectionOfPrimitiveValues<string>("scopes", Scopes);
            writer.WriteEnumValue<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.DecisionEffect>("status", Status);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
