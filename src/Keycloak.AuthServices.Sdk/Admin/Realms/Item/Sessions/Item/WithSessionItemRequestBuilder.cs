// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Sessions.Item {
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\sessions\{session}
    /// </summary>
    public class WithSessionItemRequestBuilder : BaseRequestBuilder {
        /// <summary>
        /// Instantiates a new <see cref="WithSessionItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithSessionItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/sessions/{session}{?isOffline*}", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new <see cref="WithSessionItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithSessionItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/sessions/{session}{?isOffline*}", rawUrl) {
        }
        /// <summary>
        /// Any client that has an admin url will also be told to invalidate this particular session.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task DeleteAsync(Action<RequestConfiguration<WithSessionItemRequestBuilderDeleteQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task DeleteAsync(Action<RequestConfiguration<WithSessionItemRequestBuilderDeleteQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            await RequestAdapter.SendNoContentAsync(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Any client that has an admin url will also be told to invalidate this particular session.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<WithSessionItemRequestBuilderDeleteQueryParameters>>? requestConfiguration = default) {
#nullable restore
#else
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<WithSessionItemRequestBuilderDeleteQueryParameters>> requestConfiguration = default) {
#endif
            var requestInfo = new RequestInformation(Method.DELETE, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="WithSessionItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public WithSessionItemRequestBuilder WithUrl(string rawUrl) {
            return new WithSessionItemRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Any client that has an admin url will also be told to invalidate this particular session.
        /// </summary>
        public class WithSessionItemRequestBuilderDeleteQueryParameters {
            [QueryParameter("isOffline")]
            public bool? IsOffline { get; set; }
        }
    }
}
