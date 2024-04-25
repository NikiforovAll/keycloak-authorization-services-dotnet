// <auto-generated/>
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Clients.Item.Certificates.Item.Download;
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Clients.Item.Certificates.Item.Generate;
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Clients.Item.Certificates.Item.GenerateAndDownload;
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Clients.Item.Certificates.Item.Upload;
using Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Clients.Item.Certificates.Item.UploadCertificate;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Admin.Admin.Realms.Item.Clients.Item.Certificates.Item {
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\clients\{client-uuid}\certificates\{attr}
    /// </summary>
    public class WithAttrItemRequestBuilder : BaseRequestBuilder {
        /// <summary>The download property</summary>
        public DownloadRequestBuilder Download { get =>
            new DownloadRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The generate property</summary>
        public GenerateRequestBuilder Generate { get =>
            new GenerateRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The generateAndDownload property</summary>
        public GenerateAndDownloadRequestBuilder GenerateAndDownload { get =>
            new GenerateAndDownloadRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The upload property</summary>
        public UploadRequestBuilder Upload { get =>
            new UploadRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The uploadCertificate property</summary>
        public UploadCertificateRequestBuilder UploadCertificate { get =>
            new UploadCertificateRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="WithAttrItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithAttrItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/certificates/{attr}", pathParameters) {
        }
        /// <summary>
        /// Instantiates a new <see cref="WithAttrItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithAttrItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/certificates/{attr}", rawUrl) {
        }
        /// <summary>
        /// Get key info
        /// </summary>
        /// <returns>A <see cref="CertificateRepresentation"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<CertificateRepresentation?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default) {
#nullable restore
#else
        public async Task<CertificateRepresentation> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default) {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<CertificateRepresentation>(requestInfo, CertificateRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get key info
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
        /// <returns>A <see cref="WithAttrItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public WithAttrItemRequestBuilder WithUrl(string rawUrl) {
            return new WithAttrItemRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
