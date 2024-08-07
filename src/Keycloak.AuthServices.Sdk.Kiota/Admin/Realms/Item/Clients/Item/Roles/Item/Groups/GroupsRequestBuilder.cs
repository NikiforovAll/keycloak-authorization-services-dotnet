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
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\clients\{client-uuid}\roles\{role-name}\groups
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
    public partial class GroupsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public GroupsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/roles/{role%2Dname}/groups{?briefRepresentation*,first*,max*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public GroupsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/clients/{client%2Duuid}/roles/{role%2Dname}/groups{?briefRepresentation*,first*,max*}", rawUrl)
        {
        }
        /// <summary>
        /// Returns a stream of groups that have the specified role name
        /// </summary>
        /// <returns>A List&lt;global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation>?> GetAsync(Action<RequestConfiguration<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<List<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation>> GetAsync(Action<RequestConfiguration<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation>(requestInfo, global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.GroupRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.AsList();
        }
        /// <summary>
        /// Returns a stream of groups that have the specified role name
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>> requestConfiguration = default)
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
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Clients.Item.Roles.Item.Groups.GroupsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Returns a stream of groups that have the specified role name
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.16.0")]
        public partial class GroupsRequestBuilderGetQueryParameters 
        {
            /// <summary>if false, return a full representation of the {@code GroupRepresentation} objects.</summary>
            [QueryParameter("briefRepresentation")]
            public bool? BriefRepresentation { get; set; }
            /// <summary>first result to return. Ignored if negative or {@code null}.</summary>
            [QueryParameter("first")]
            public int? First { get; set; }
            /// <summary>maximum number of results to return. Ignored if negative or {@code null}.</summary>
            [QueryParameter("max")]
            public int? Max { get; set; }
        }
    }
}
