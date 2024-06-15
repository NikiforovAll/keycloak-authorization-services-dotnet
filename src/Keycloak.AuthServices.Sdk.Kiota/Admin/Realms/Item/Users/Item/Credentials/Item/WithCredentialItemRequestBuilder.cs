// <auto-generated/>
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.MoveAfter;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.MoveToFirst;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.UserLabel;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\users\{user-id}\credentials\{credentialId}
    /// </summary>
    public class WithCredentialItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The moveAfter property</summary>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.MoveAfter.MoveAfterRequestBuilder MoveAfter
        {
            get => new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.MoveAfter.MoveAfterRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The moveToFirst property</summary>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.MoveToFirst.MoveToFirstRequestBuilder MoveToFirst
        {
            get => new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.MoveToFirst.MoveToFirstRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The userLabel property</summary>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.UserLabel.UserLabelRequestBuilder UserLabel
        {
            get => new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.UserLabel.UserLabelRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.WithCredentialItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithCredentialItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users/{user%2Did}/credentials/{credentialId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.WithCredentialItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithCredentialItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users/{user%2Did}/credentials/{credentialId}", rawUrl)
        {
        }
        /// <summary>
        /// Remove a credential for a user
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            await RequestAdapter.SendNoContentAsync(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Remove a credential for a user
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.DELETE, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.WithCredentialItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.WithCredentialItemRequestBuilder WithUrl(string rawUrl)
        {
            return new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.WithCredentialItemRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
