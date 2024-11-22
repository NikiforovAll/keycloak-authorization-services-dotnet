// <auto-generated/>
#pragma warning disable CS0618
using Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item.Organizations;
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\organizations\{id}\members\{id}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class MembersItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The organizations property</summary>
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item.Organizations.OrganizationsRequestBuilder Organizations
        {
            get => new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item.Organizations.OrganizationsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item.MembersItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MembersItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/organizations/{id}/members/{id}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item.MembersItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public MembersItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/organizations/{id}/members/{id}", rawUrl)
        {
        }
        /// <summary>
        /// Breaks the association between the user and organization. The user itself is deleted in case the membership is managed, otherwise the user is not deleted. If no user is found, or if they are not a member of the organization, an error response is returned
        /// </summary>
        /// <returns>A <see cref="Stream"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<Stream?> DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<Stream> DeleteAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            return await RequestAdapter.SendPrimitiveAsync<Stream>(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Searches for auser with the given id. If one is found, and is currently a member of the organization, returns it. Otherwise,an error response with status NOT_FOUND is returned
        /// </summary>
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MemberRepresentation"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MemberRepresentation?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MemberRepresentation> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MemberRepresentation>(requestInfo, global::Keycloak.AuthServices.Sdk.Kiota.Admin.Models.MemberRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Breaks the association between the user and organization. The user itself is deleted in case the membership is managed, otherwise the user is not deleted. If no user is found, or if they are not a member of the organization, an error response is returned
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
        /// Searches for auser with the given id. If one is found, and is currently a member of the organization, returns it. Otherwise,an error response with status NOT_FOUND is returned
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
        /// <returns>A <see cref="global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item.MembersItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item.MembersItemRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.Members.Item.MembersItemRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
#pragma warning restore CS0618
