// <auto-generated/>
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.Clients;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.Realm;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\client-templates\{client-scope-id}\scope-mappings
    /// </summary>
    public class ScopeMappingsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The clients property</summary>
        [Obsolete("")]
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.Clients.ClientsRequestBuilder Clients
        {
            get => new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.Clients.ClientsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The realm property</summary>
        [Obsolete("")]
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.Realm.RealmRequestBuilder Realm
        {
            get => new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.Realm.RealmRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.ScopeMappingsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ScopeMappingsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/client-templates/{client%2Dscope%2Did}/scope-mappings", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.ScopeMappingsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ScopeMappingsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/client-templates/{client%2Dscope%2Did}/scope-mappings", rawUrl)
        {
        }
        /// <summary>
        /// Get all scope mappings for the client
        /// </summary>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MappingsRepresentation"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MappingsRepresentation?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MappingsRepresentation> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MappingsRepresentation>(requestInfo, Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MappingsRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get all scope mappings for the client
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
        [Obsolete("")]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.ScopeMappingsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        [Obsolete("")]
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.ScopeMappingsRequestBuilder WithUrl(string rawUrl)
        {
            return new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.ClientTemplates.Item.ScopeMappings.ScopeMappingsRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
