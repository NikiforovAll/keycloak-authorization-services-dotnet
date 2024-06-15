// <auto-generated/>
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.Item;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\default-groups
    /// </summary>
    public class DefaultGroupsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the Keycloak.AuthServices.Sdk.Kiota.Admin.admin.realms.item.defaultGroups.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.Item.WithGroupItemRequestBuilder"/></returns>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.Item.WithGroupItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("groupId", position);
                return new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.Item.WithGroupItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.DefaultGroupsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public DefaultGroupsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/default-groups", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.DefaultGroupsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public DefaultGroupsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/default-groups", rawUrl)
        {
        }
        /// <summary>
        /// Get group hierarchy.  Only name and ids are returned.
        /// </summary>
        /// <returns>A List&lt;Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation>?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<List<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation>> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation>(requestInfo, Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.ToList();
        }
        /// <summary>
        /// Get group hierarchy.  Only name and ids are returned.
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
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.DefaultGroupsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.DefaultGroupsRequestBuilder WithUrl(string rawUrl)
        {
            return new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.DefaultGroups.DefaultGroupsRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
