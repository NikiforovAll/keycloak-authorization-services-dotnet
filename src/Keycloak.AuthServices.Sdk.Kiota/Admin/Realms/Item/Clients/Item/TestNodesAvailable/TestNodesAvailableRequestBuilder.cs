// <auto-generated/>
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.TestNodesAvailable
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\clients\{client-uuid}\test-nodes-available
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class TestNodesAvailableRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.TestNodesAvailable.TestNodesAvailableRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TestNodesAvailableRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/test-nodes-available", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.TestNodesAvailable.TestNodesAvailableRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public TestNodesAvailableRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/test-nodes-available", rawUrl)
        {
        }
        /// <summary>
        /// Test if registered cluster nodes are available Tests availability by sending &apos;ping&apos; request to all cluster nodes.
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GlobalRequestResult"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GlobalRequestResult?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GlobalRequestResult> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GlobalRequestResult>(requestInfo, global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GlobalRequestResult.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Test if registered cluster nodes are available Tests availability by sending &apos;ping&apos; request to all cluster nodes.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
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
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.TestNodesAvailable.TestNodesAvailableRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.TestNodesAvailable.TestNodesAvailableRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.TestNodesAvailable.TestNodesAvailableRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
