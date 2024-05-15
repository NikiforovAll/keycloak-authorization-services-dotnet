// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models {
    #pragma warning disable CS1591
    public class RealmEventsConfigRepresentation : IAdditionalDataHolder, IParsable 
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The adminEventsDetailsEnabled property</summary>
        public bool? AdminEventsDetailsEnabled { get; set; }
        /// <summary>The adminEventsEnabled property</summary>
        public bool? AdminEventsEnabled { get; set; }
        /// <summary>The enabledEventTypes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? EnabledEventTypes { get; set; }
#nullable restore
#else
        public List<string> EnabledEventTypes { get; set; }
#endif
        /// <summary>The eventsEnabled property</summary>
        public bool? EventsEnabled { get; set; }
        /// <summary>The eventsExpiration property</summary>
        public long? EventsExpiration { get; set; }
        /// <summary>The eventsListeners property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? EventsListeners { get; set; }
#nullable restore
#else
        public List<string> EventsListeners { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="RealmEventsConfigRepresentation"/> and sets the default values.
        /// </summary>
        public RealmEventsConfigRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="RealmEventsConfigRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static RealmEventsConfigRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new RealmEventsConfigRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"adminEventsDetailsEnabled", n => { AdminEventsDetailsEnabled = n.GetBoolValue(); } },
                {"adminEventsEnabled", n => { AdminEventsEnabled = n.GetBoolValue(); } },
                {"enabledEventTypes", n => { EnabledEventTypes = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                {"eventsEnabled", n => { EventsEnabled = n.GetBoolValue(); } },
                {"eventsExpiration", n => { EventsExpiration = n.GetLongValue(); } },
                {"eventsListeners", n => { EventsListeners = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("adminEventsDetailsEnabled", AdminEventsDetailsEnabled);
            writer.WriteBoolValue("adminEventsEnabled", AdminEventsEnabled);
            writer.WriteCollectionOfPrimitiveValues<string>("enabledEventTypes", EnabledEventTypes);
            writer.WriteBoolValue("eventsEnabled", EventsEnabled);
            writer.WriteLongValue("eventsExpiration", EventsExpiration);
            writer.WriteCollectionOfPrimitiveValues<string>("eventsListeners", EventsListeners);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
