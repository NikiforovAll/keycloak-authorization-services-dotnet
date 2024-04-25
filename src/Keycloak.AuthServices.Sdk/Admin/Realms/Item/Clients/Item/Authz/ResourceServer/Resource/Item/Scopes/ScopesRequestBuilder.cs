// <auto-generated/>
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Clients.Item.Authz.ResourceServer.Resource.Item.Scopes {
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\clients\{client-uuid}\authz\resource-server\resource\{resource-id}\scopes
    /// </summary>
    public class ScopesRequestBuilder : BaseRequestBuilder {
        /// <summary>
        /// Instantiates a new <see cref="ScopesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ScopesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/authz/resource-server/resource/{resource%2Did}/scopes{?_id*,deep*,exactName*,first*,matchingUri*,max*,name*,owner*,scope*,type*,uri*}", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new <see cref="ScopesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ScopesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/authz/resource-server/resource/{resource%2Did}/scopes{?_id*,deep*,exactName*,first*,matchingUri*,max*,name*,owner*,scope*,type*,uri*}", rawUrl) {
        }
        /// <returns>A List&lt;ScopeRepresentation&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<ScopeRepresentation>?> GetAsync(Action<RequestConfiguration<ScopesRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task<List<ScopeRepresentation>> GetAsync(Action<RequestConfiguration<ScopesRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<ScopeRepresentation>(requestInfo, ScopeRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.ToList();
        }
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<ScopesRequestBuilderGetQueryParameters>>? requestConfiguration = default) {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<ScopesRequestBuilderGetQueryParameters>> requestConfiguration = default) {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="ScopesRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public ScopesRequestBuilder WithUrl(string rawUrl) {
            return new ScopesRequestBuilder(rawUrl, RequestAdapter);
        }
        public class ScopesRequestBuilderGetQueryParameters {
            [QueryParameter("deep")]
            public bool? Deep { get; set; }
            [QueryParameter("exactName")]
            public bool? ExactName { get; set; }
            [QueryParameter("first")]
            public int? First { get; set; }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("_id")]
            public string? Id { get; set; }
#nullable restore
#else
            [QueryParameter("_id")]
            public string Id { get; set; }
#endif
            [QueryParameter("matchingUri")]
            public bool? MatchingUri { get; set; }
            [QueryParameter("max")]
            public int? Max { get; set; }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("name")]
            public string? Name { get; set; }
#nullable restore
#else
            [QueryParameter("name")]
            public string Name { get; set; }
#endif
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("owner")]
            public string? Owner { get; set; }
#nullable restore
#else
            [QueryParameter("owner")]
            public string Owner { get; set; }
#endif
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("scope")]
            public string? Scope { get; set; }
#nullable restore
#else
            [QueryParameter("scope")]
            public string Scope { get; set; }
#endif
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("type")]
            public string? Type { get; set; }
#nullable restore
#else
            [QueryParameter("type")]
            public string Type { get; set; }
#endif
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("uri")]
            public string? Uri { get; set; }
#nullable restore
#else
            [QueryParameter("uri")]
            public string Uri { get; set; }
#endif
        }
    }
}
