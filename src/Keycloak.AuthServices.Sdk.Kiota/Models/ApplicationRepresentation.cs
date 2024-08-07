// <auto-generated/>
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Models
{
    [Obsolete("")]
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    #pragma warning disable CS1591
    public partial class ApplicationRepresentation : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The access property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_access? Access { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_access Access { get; set; }
#endif
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The adminUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? AdminUrl { get; set; }
#nullable restore
#else
        public string AdminUrl { get; set; }
#endif
        /// <summary>The alwaysDisplayInConsole property</summary>
        public bool? AlwaysDisplayInConsole { get; set; }
        /// <summary>The attributes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_attributes? Attributes { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_attributes Attributes { get; set; }
#endif
        /// <summary>The authenticationFlowBindingOverrides property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_authenticationFlowBindingOverrides? AuthenticationFlowBindingOverrides { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_authenticationFlowBindingOverrides AuthenticationFlowBindingOverrides { get; set; }
#endif
        /// <summary>The authorizationServicesEnabled property</summary>
        public bool? AuthorizationServicesEnabled { get; set; }
        /// <summary>The authorizationSettings property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceServerRepresentation? AuthorizationSettings { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceServerRepresentation AuthorizationSettings { get; set; }
#endif
        /// <summary>The baseUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BaseUrl { get; set; }
#nullable restore
#else
        public string BaseUrl { get; set; }
#endif
        /// <summary>The bearerOnly property</summary>
        public bool? BearerOnly { get; set; }
        /// <summary>The claims property</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_claims? Claims { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_claims Claims { get; set; }
#endif
        /// <summary>The clientAuthenticatorType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ClientAuthenticatorType { get; set; }
#nullable restore
#else
        public string ClientAuthenticatorType { get; set; }
#endif
        /// <summary>The clientId property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ClientId { get; set; }
#nullable restore
#else
        public string ClientId { get; set; }
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
        /// <summary>The consentRequired property</summary>
        public bool? ConsentRequired { get; set; }
        /// <summary>The defaultClientScopes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? DefaultClientScopes { get; set; }
#nullable restore
#else
        public List<string> DefaultClientScopes { get; set; }
#endif
        /// <summary>The defaultRoles property</summary>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? DefaultRoles { get; set; }
#nullable restore
#else
        public List<string> DefaultRoles { get; set; }
#endif
        /// <summary>The description property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The directAccessGrantsEnabled property</summary>
        public bool? DirectAccessGrantsEnabled { get; set; }
        /// <summary>The directGrantsOnly property</summary>
        [Obsolete("")]
        public bool? DirectGrantsOnly { get; set; }
        /// <summary>The enabled property</summary>
        public bool? Enabled { get; set; }
        /// <summary>The frontchannelLogout property</summary>
        public bool? FrontchannelLogout { get; set; }
        /// <summary>The fullScopeAllowed property</summary>
        public bool? FullScopeAllowed { get; set; }
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The implicitFlowEnabled property</summary>
        public bool? ImplicitFlowEnabled { get; set; }
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The nodeReRegistrationTimeout property</summary>
        public int? NodeReRegistrationTimeout { get; set; }
        /// <summary>The notBefore property</summary>
        public int? NotBefore { get; set; }
        /// <summary>The optionalClientScopes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? OptionalClientScopes { get; set; }
#nullable restore
#else
        public List<string> OptionalClientScopes { get; set; }
#endif
        /// <summary>The origin property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Origin { get; set; }
#nullable restore
#else
        public string Origin { get; set; }
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
        /// <summary>The publicClient property</summary>
        public bool? PublicClient { get; set; }
        /// <summary>The redirectUris property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RedirectUris { get; set; }
#nullable restore
#else
        public List<string> RedirectUris { get; set; }
#endif
        /// <summary>The registeredNodes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_registeredNodes? RegisteredNodes { get; set; }
#nullable restore
#else
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_registeredNodes RegisteredNodes { get; set; }
#endif
        /// <summary>The registrationAccessToken property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RegistrationAccessToken { get; set; }
#nullable restore
#else
        public string RegistrationAccessToken { get; set; }
#endif
        /// <summary>The rootUrl property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RootUrl { get; set; }
#nullable restore
#else
        public string RootUrl { get; set; }
#endif
        /// <summary>The secret property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Secret { get; set; }
#nullable restore
#else
        public string Secret { get; set; }
#endif
        /// <summary>The serviceAccountsEnabled property</summary>
        public bool? ServiceAccountsEnabled { get; set; }
        /// <summary>The standardFlowEnabled property</summary>
        public bool? StandardFlowEnabled { get; set; }
        /// <summary>The surrogateAuthRequired property</summary>
        public bool? SurrogateAuthRequired { get; set; }
        /// <summary>The type property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Type { get; set; }
#nullable restore
#else
        public string Type { get; set; }
#endif
        /// <summary>The useTemplateConfig property</summary>
        [Obsolete("")]
        public bool? UseTemplateConfig { get; set; }
        /// <summary>The useTemplateMappers property</summary>
        [Obsolete("")]
        public bool? UseTemplateMappers { get; set; }
        /// <summary>The useTemplateScope property</summary>
        [Obsolete("")]
        public bool? UseTemplateScope { get; set; }
        /// <summary>The webOrigins property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? WebOrigins { get; set; }
#nullable restore
#else
        public List<string> WebOrigins { get; set; }
#endif
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation"/> and sets the default values.
        /// </summary>
        public ApplicationRepresentation()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "access", n => { Access = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_access>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_access.CreateFromDiscriminatorValue); } },
                { "adminUrl", n => { AdminUrl = n.GetStringValue(); } },
                { "alwaysDisplayInConsole", n => { AlwaysDisplayInConsole = n.GetBoolValue(); } },
                { "attributes", n => { Attributes = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_attributes>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_attributes.CreateFromDiscriminatorValue); } },
                { "authenticationFlowBindingOverrides", n => { AuthenticationFlowBindingOverrides = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_authenticationFlowBindingOverrides>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_authenticationFlowBindingOverrides.CreateFromDiscriminatorValue); } },
                { "authorizationServicesEnabled", n => { AuthorizationServicesEnabled = n.GetBoolValue(); } },
                { "authorizationSettings", n => { AuthorizationSettings = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceServerRepresentation>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceServerRepresentation.CreateFromDiscriminatorValue); } },
                { "baseUrl", n => { BaseUrl = n.GetStringValue(); } },
                { "bearerOnly", n => { BearerOnly = n.GetBoolValue(); } },
                { "claims", n => { Claims = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_claims>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_claims.CreateFromDiscriminatorValue); } },
                { "clientAuthenticatorType", n => { ClientAuthenticatorType = n.GetStringValue(); } },
                { "clientId", n => { ClientId = n.GetStringValue(); } },
                { "clientTemplate", n => { ClientTemplate = n.GetStringValue(); } },
                { "consentRequired", n => { ConsentRequired = n.GetBoolValue(); } },
                { "defaultClientScopes", n => { DefaultClientScopes = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "defaultRoles", n => { DefaultRoles = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "description", n => { Description = n.GetStringValue(); } },
                { "directAccessGrantsEnabled", n => { DirectAccessGrantsEnabled = n.GetBoolValue(); } },
                { "directGrantsOnly", n => { DirectGrantsOnly = n.GetBoolValue(); } },
                { "enabled", n => { Enabled = n.GetBoolValue(); } },
                { "frontchannelLogout", n => { FrontchannelLogout = n.GetBoolValue(); } },
                { "fullScopeAllowed", n => { FullScopeAllowed = n.GetBoolValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "implicitFlowEnabled", n => { ImplicitFlowEnabled = n.GetBoolValue(); } },
                { "name", n => { Name = n.GetStringValue(); } },
                { "nodeReRegistrationTimeout", n => { NodeReRegistrationTimeout = n.GetIntValue(); } },
                { "notBefore", n => { NotBefore = n.GetIntValue(); } },
                { "optionalClientScopes", n => { OptionalClientScopes = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "origin", n => { Origin = n.GetStringValue(); } },
                { "protocol", n => { Protocol = n.GetStringValue(); } },
                { "protocolMappers", n => { ProtocolMappers = n.GetCollectionOfObjectValues<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation.CreateFromDiscriminatorValue)?.AsList(); } },
                { "publicClient", n => { PublicClient = n.GetBoolValue(); } },
                { "redirectUris", n => { RedirectUris = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
                { "registeredNodes", n => { RegisteredNodes = n.GetObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_registeredNodes>(global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_registeredNodes.CreateFromDiscriminatorValue); } },
                { "registrationAccessToken", n => { RegistrationAccessToken = n.GetStringValue(); } },
                { "rootUrl", n => { RootUrl = n.GetStringValue(); } },
                { "secret", n => { Secret = n.GetStringValue(); } },
                { "serviceAccountsEnabled", n => { ServiceAccountsEnabled = n.GetBoolValue(); } },
                { "standardFlowEnabled", n => { StandardFlowEnabled = n.GetBoolValue(); } },
                { "surrogateAuthRequired", n => { SurrogateAuthRequired = n.GetBoolValue(); } },
                { "type", n => { Type = n.GetStringValue(); } },
                { "useTemplateConfig", n => { UseTemplateConfig = n.GetBoolValue(); } },
                { "useTemplateMappers", n => { UseTemplateMappers = n.GetBoolValue(); } },
                { "useTemplateScope", n => { UseTemplateScope = n.GetBoolValue(); } },
                { "webOrigins", n => { WebOrigins = n.GetCollectionOfPrimitiveValues<string>()?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_access>("access", Access);
            writer.WriteStringValue("adminUrl", AdminUrl);
            writer.WriteBoolValue("alwaysDisplayInConsole", AlwaysDisplayInConsole);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_attributes>("attributes", Attributes);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_authenticationFlowBindingOverrides>("authenticationFlowBindingOverrides", AuthenticationFlowBindingOverrides);
            writer.WriteBoolValue("authorizationServicesEnabled", AuthorizationServicesEnabled);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ResourceServerRepresentation>("authorizationSettings", AuthorizationSettings);
            writer.WriteStringValue("baseUrl", BaseUrl);
            writer.WriteBoolValue("bearerOnly", BearerOnly);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_claims>("claims", Claims);
            writer.WriteStringValue("clientAuthenticatorType", ClientAuthenticatorType);
            writer.WriteStringValue("clientId", ClientId);
            writer.WriteStringValue("clientTemplate", ClientTemplate);
            writer.WriteBoolValue("consentRequired", ConsentRequired);
            writer.WriteCollectionOfPrimitiveValues<string>("defaultClientScopes", DefaultClientScopes);
            writer.WriteCollectionOfPrimitiveValues<string>("defaultRoles", DefaultRoles);
            writer.WriteStringValue("description", Description);
            writer.WriteBoolValue("directAccessGrantsEnabled", DirectAccessGrantsEnabled);
            writer.WriteBoolValue("directGrantsOnly", DirectGrantsOnly);
            writer.WriteBoolValue("enabled", Enabled);
            writer.WriteBoolValue("frontchannelLogout", FrontchannelLogout);
            writer.WriteBoolValue("fullScopeAllowed", FullScopeAllowed);
            writer.WriteStringValue("id", Id);
            writer.WriteBoolValue("implicitFlowEnabled", ImplicitFlowEnabled);
            writer.WriteStringValue("name", Name);
            writer.WriteIntValue("nodeReRegistrationTimeout", NodeReRegistrationTimeout);
            writer.WriteIntValue("notBefore", NotBefore);
            writer.WriteCollectionOfPrimitiveValues<string>("optionalClientScopes", OptionalClientScopes);
            writer.WriteStringValue("origin", Origin);
            writer.WriteStringValue("protocol", Protocol);
            writer.WriteCollectionOfObjectValues<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ProtocolMapperRepresentation>("protocolMappers", ProtocolMappers);
            writer.WriteBoolValue("publicClient", PublicClient);
            writer.WriteCollectionOfPrimitiveValues<string>("redirectUris", RedirectUris);
            writer.WriteObjectValue<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.ApplicationRepresentation_registeredNodes>("registeredNodes", RegisteredNodes);
            writer.WriteStringValue("registrationAccessToken", RegistrationAccessToken);
            writer.WriteStringValue("rootUrl", RootUrl);
            writer.WriteStringValue("secret", Secret);
            writer.WriteBoolValue("serviceAccountsEnabled", ServiceAccountsEnabled);
            writer.WriteBoolValue("standardFlowEnabled", StandardFlowEnabled);
            writer.WriteBoolValue("surrogateAuthRequired", SurrogateAuthRequired);
            writer.WriteStringValue("type", Type);
            writer.WriteBoolValue("useTemplateConfig", UseTemplateConfig);
            writer.WriteBoolValue("useTemplateMappers", UseTemplateMappers);
            writer.WriteBoolValue("useTemplateScope", UseTemplateScope);
            writer.WriteCollectionOfPrimitiveValues<string>("webOrigins", WebOrigins);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
