// <auto-generated/>
#pragma warning disable CS0618
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\users\{user-id}\credentials
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class CredentialsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the Keycloak.AuthServices.Sdk.Kiota.Admin.admin.realms.item.users.item.credentials.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.WithCredentialItemRequestBuilder"/></returns>
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.WithCredentialItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("credentialId", position);
                return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.Item.WithCredentialItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.CredentialsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CredentialsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users/{user%2Did}/credentials", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.CredentialsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public CredentialsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/users/{user%2Did}/credentials", rawUrl)
        {
        }
        /// <returns>A List&lt;global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.CredentialRepresentation&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.CredentialRepresentation>?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<List<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.CredentialRepresentation>> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.CredentialRepresentation>(requestInfo, global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.CredentialRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.AsList();
        }
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
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.CredentialsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.CredentialsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Users.Item.Credentials.CredentialsRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
#pragma warning restore CS0618
