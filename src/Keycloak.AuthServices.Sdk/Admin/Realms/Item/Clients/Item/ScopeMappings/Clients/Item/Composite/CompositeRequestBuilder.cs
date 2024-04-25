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
namespace Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Clients.Item.ScopeMappings.Clients.Item.Composite {
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\clients\{client-uuid}\scope-mappings\clients\{client}\composite
    /// </summary>
    public class CompositeRequestBuilder : BaseRequestBuilder {
        /// <summary>
        /// Instantiates a new <see cref="CompositeRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CompositeRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/scope-mappings/clients/{client}/composite{?briefRepresentation*}", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new <see cref="CompositeRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CompositeRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/scope-mappings/clients/{client}/composite{?briefRepresentation*}", rawUrl) {
        }
        /// <summary>
        /// Get effective client roles Returns the roles for the client that are associated with the client&apos;s scope.
        /// </summary>
        /// <returns>A List&lt;RoleRepresentation&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<RoleRepresentation>?> GetAsync(Action<RequestConfiguration<CompositeRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task<List<RoleRepresentation>> GetAsync(Action<RequestConfiguration<CompositeRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<RoleRepresentation>(requestInfo, RoleRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.ToList();
        }
        /// <summary>
        /// Get effective client roles Returns the roles for the client that are associated with the client&apos;s scope.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<CompositeRequestBuilderGetQueryParameters>>? requestConfiguration = default) {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<CompositeRequestBuilderGetQueryParameters>> requestConfiguration = default) {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="CompositeRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public CompositeRequestBuilder WithUrl(string rawUrl) {
            return new CompositeRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Get effective client roles Returns the roles for the client that are associated with the client&apos;s scope.
        /// </summary>
        public class CompositeRequestBuilderGetQueryParameters {
            /// <summary>if false, return roles with their attributes</summary>
            [QueryParameter("briefRepresentation")]
            public bool? BriefRepresentation { get; set; }
        }
    }
}
