// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Users.Item.ConfiguredUserStorageCredentialTypes {
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\users\{user-id}\configured-user-storage-credential-types
    /// </summary>
    public class ConfiguredUserStorageCredentialTypesRequestBuilder : BaseRequestBuilder {
        /// <summary>
        /// Instantiates a new <see cref="ConfiguredUserStorageCredentialTypesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ConfiguredUserStorageCredentialTypesRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users/{user%2Did}/configured-user-storage-credential-types", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new <see cref="ConfiguredUserStorageCredentialTypesRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ConfiguredUserStorageCredentialTypesRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users/{user%2Did}/configured-user-storage-credential-types", rawUrl) {
        }
        /// <summary>
        /// Returned values can contain for example &quot;password&quot;, &quot;otp&quot; etc. This will always return empty list for &quot;local&quot; users, which are not backed by any user storage
        /// </summary>
        /// <returns>A List&lt;string&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<string>?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task<List<string>> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendPrimitiveCollectionAsync<string>(requestInfo, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.ToList();
        }
        /// <summary>
        /// Returned values can contain for example &quot;password&quot;, &quot;otp&quot; etc. This will always return empty list for &quot;local&quot; users, which are not backed by any user storage
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default) {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default) {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="ConfiguredUserStorageCredentialTypesRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public ConfiguredUserStorageCredentialTypesRequestBuilder WithUrl(string rawUrl) {
            return new ConfiguredUserStorageCredentialTypesRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
