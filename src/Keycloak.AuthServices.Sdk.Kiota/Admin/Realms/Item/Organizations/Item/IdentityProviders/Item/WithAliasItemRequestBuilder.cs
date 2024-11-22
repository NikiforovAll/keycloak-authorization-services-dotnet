// <auto-generated/>
using Keycloak.AuthServices.Sdk.Kiota.Admin.Models;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.IdentityProviders.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \admin\realms\{realm}\organizations\{id}\identity-providers\{alias}
    /// </summary>
    public class WithAliasItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.IdentityProviders.Item.WithAliasItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithAliasItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/organizations/{id}/identity-providers/{alias}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.IdentityProviders.Item.WithAliasItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithAliasItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/admin/realms/{realm}/organizations/{id}/identity-providers/{alias}", rawUrl)
        {
        }
        /// <summary>
        /// Breaks the association between the identity provider and the organization. The provider itself is not deleted. If no provider is found, or if it is not currently associated with the org, an error response is returned
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
        /// Searches for an identity provider with the given alias. If one is found and is associated with the organization, it is returned. Otherwise, an error response with status NOT_FOUND is returned
        /// </summary>
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderRepresentation"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderRepresentation?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderRepresentation> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderRepresentation>(requestInfo, Keycloak.AuthServices.Sdk.Kiota.Admin.Models.IdentityProviderRepresentation.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Breaks the association between the identity provider and the organization. The provider itself is not deleted. If no provider is found, or if it is not currently associated with the org, an error response is returned
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
        /// Searches for an identity provider with the given alias. If one is found and is associated with the organization, it is returned. Otherwise, an error response with status NOT_FOUND is returned
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
        /// <returns>A <see cref="Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.IdentityProviders.Item.WithAliasItemRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.IdentityProviders.Item.WithAliasItemRequestBuilder WithUrl(string rawUrl)
        {
            return new Keycloak.AuthServices.Sdk.Kiota.Admin.Admin.Realms.Item.Organizations.Item.IdentityProviders.Item.WithAliasItemRequestBuilder(rawUrl, RequestAdapter);
        }
    }
}
